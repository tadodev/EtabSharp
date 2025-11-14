using EtabSharp.Exceptions;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// PropMaterial partial class - Steel Materials
/// </summary>
public partial class PropMaterial
{
    #region Steel Materials

    /// <inheritdoc/>
    public SteelMaterial AddSteelMaterial(SteelMaterial material)
    {
        try
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));
            if (!material.IsValid())
                throw new ArgumentException("Invalid steel material properties", nameof(material));

            _logger.LogInformation("Creating steel material '{MaterialName}' with Fy={Fy}, Fu={Fu}",
                material.Name, material.Fy, material.Fu);

            // Step 1: Set material type
            int ret = SetMaterial(material.Name, eMatType.Steel, material.Color,
                material.Notes, material.GUID);
            if (ret != 0)
                throw new EtabsException(ret, "SetMaterial",
                    $"Failed to set material type for '{material.Name}'");

            // Step 2: Set isotropic properties
            ret = SetMPIsotropic(material.Name, material.IsotropicProps);
            if (ret != 0)
                throw new EtabsException(ret, "SetMPIsotropic",
                    $"Failed to set isotropic properties for '{material.Name}'");

            // Step 3: Set steel-specific properties
            ret = SetOSteel_1(
                material.Name,
                material.Fy,
                material.Fu,
                material.EFy,
                material.EFu,
                material.SSType,
                material.SSHysType,
                material.StrainAtHardening,
                material.StrainAtMaxStress,
                material.StrainAtRupture,
                material.FinalSlope,
                material.Temp
            );

            if (ret != 0)
                throw new EtabsException(ret, "SetOSteel_1",
                    $"Failed to set steel properties for '{material.Name}'");

            // Optional: Set damping if provided
            if (material.Damping != null)
            {
                SetDamping(material.Name, material.Damping);
            }

            // Optional: Set weight/mass if provided
            if (material.WeightMass != null)
            {
                SetWeightAndMass(material.Name, 1, material.WeightMass.W, material.WeightMass.Temp);
            }

            _logger.LogInformation("Successfully created steel material '{MaterialName}'", material.Name);
            return material;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating steel material '{Name}'", material?.Name);
            throw new EtabsException($"Unexpected error creating steel material '{material?.Name}'", ex);
        }
    }

    /// <inheritdoc/>
    public SteelMaterial AddSteelMaterial(string name, double fy, double fu)
    {
        var material = SteelMaterial.Create(name, fy, fu);
        return AddSteelMaterial(material);
    }

    /// <inheritdoc/>
    public (double Fy, double Fu, double EFy, double EFu, int SSType, int SSHysType,
        double StrainAtHardening, double StrainAtMaxStress, double StrainAtRupture)
        GetOSteel(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double fy = 0, fu = 0, eFy = 0, eFu = 0;
            double strainHardening = 0, strainMaxStress = 0, strainRupture = 0;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOSteel(name, ref fy, ref fu, ref eFy, ref eFu,
                ref ssType, ref ssHysType, ref strainHardening, ref strainMaxStress,
                ref strainRupture, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get steel properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOSteel",
                    $"Failed to get steel properties for '{name}'");
            }

            return (fy, fu, eFy, eFu, ssType, ssHysType, strainHardening,
                strainMaxStress, strainRupture);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting steel properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting steel properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public SteelMaterial GetSteelMaterial(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            // Get basic material info
            var (matType, color, notes, guid) = GetMaterial(name);
            if (matType != eMatType.Steel)
                throw new InvalidOperationException($"Material '{name}' is not a steel material");

            // Get isotropic properties
            var isotropic = GetMPIsotropic(name, temp);

            // Get steel-specific properties
            double fy = 0, fu = 0, eFy = 0, eFu = 0, finalSlope = 0;
            double strainHardening = 0, strainMaxStress = 0, strainRupture = 0;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOSteel_1(name, ref fy, ref fu, ref eFy, ref eFu,
                ref ssType, ref ssHysType, ref strainHardening, ref strainMaxStress,
                ref strainRupture, ref finalSlope, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get steel properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOSteel_1",
                    $"Failed to get steel properties for '{name}'");
            }

            return new SteelMaterial
            {
                Name = name,
                Color = color,
                Notes = notes,
                GUID = guid,
                IsotropicProps = isotropic,
                Fy = fy,
                Fu = fu,
                EFy = eFy,
                EFu = eFu,
                SSType = ssType,
                SSHysType = ssHysType,
                StrainAtHardening = strainHardening,
                StrainAtMaxStress = strainMaxStress,
                StrainAtRupture = strainRupture,
                FinalSlope = finalSlope,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting steel material '{Name}'", name);
            throw new EtabsException($"Unexpected error getting steel material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOSteel(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainAtMaxStress, double strainAtRupture, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fy <= 0)
                throw new ArgumentOutOfRangeException(nameof(fy), "Yield stress must be positive");
            if (fu <= fy)
                throw new ArgumentOutOfRangeException(nameof(fu), "Ultimate stress must be greater than yield stress");

            _logger.LogInformation("Setting steel properties for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetOSteel(name, fy, fu, eFy, eFu, ssType, ssHysType,
                strainAtHardening, strainAtMaxStress, strainAtRupture, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set steel properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOSteel",
                    $"Failed to set steel properties for '{name}'");
            }

            _logger.LogInformation("Successfully set steel properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting steel properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting steel properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOSteel_1(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainAtMaxStress, double strainAtRupture, double finalSlope,
        double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fy <= 0)
                throw new ArgumentOutOfRangeException(nameof(fy), "Yield stress must be positive");
            if (fu <= fy)
                throw new ArgumentOutOfRangeException(nameof(fu), "Ultimate stress must be greater than yield stress");

            _logger.LogInformation("Setting steel properties (extended) for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetOSteel_1(name, fy, fu, eFy, eFu, ssType, ssHysType,
                strainAtHardening, strainAtMaxStress, strainAtRupture, finalSlope, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set steel properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOSteel_1",
                    $"Failed to set steel properties for '{name}'");
            }

            _logger.LogInformation("Successfully set steel properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting steel properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting steel properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public List<SteelMaterial> GetAllSteelMaterials()
    {
        try
        {
            var names = GetNameList(eMatType.Steel);
            var materials = new List<SteelMaterial>();

            foreach (var name in names)
            {
                try
                {
                    materials.Add(GetSteelMaterial(name));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get steel material '{Name}', skipping", name);
                }
            }

            return materials;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all steel materials");
            throw new EtabsException("Unexpected error getting all steel materials", ex);
        }
    }

    #endregion
}
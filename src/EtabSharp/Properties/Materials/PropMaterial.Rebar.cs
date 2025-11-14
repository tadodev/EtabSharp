using EtabSharp.Exceptions;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// PropMaterial partial class - Rebar Materials
/// </summary>
public partial class PropMaterial
{
    #region Rebar Materials

    /// <inheritdoc/>
    public RebarMaterial AddRebarMaterial(RebarMaterial material)
    {
        try
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));
            if (!material.IsValid())
                throw new ArgumentException("Invalid rebar material properties", nameof(material));

            _logger.LogInformation("Creating rebar material '{MaterialName}' with Fy={Fy}, Fu={Fu}",
                material.Name, material.Fy, material.Fu);

            // Step 1: Set material type
            int ret = SetMaterial(material.Name, eMatType.Rebar, material.Color,
                material.Notes, material.GUID);
            if (ret != 0)
                throw new EtabsException(ret, "SetMaterial",
                    $"Failed to set material type for '{material.Name}'");

            // Step 2: Set isotropic properties
            ret = SetMPIsotropic(material.Name, material.IsotropicProps);
            if (ret != 0)
                throw new EtabsException(ret, "SetMPIsotropic",
                    $"Failed to set isotropic properties for '{material.Name}'");

            // Step 3: Set rebar-specific properties
            ret = SetORebar_1(
                material.Name,
                material.Fy,
                material.Fu,
                material.EFy,
                material.EFu,
                material.SSType,
                material.SSHysType,
                material.StrainAtHardening,
                material.StrainUltimate,
                material.FinalSlope,
                material.UseCaltransSSDefaults,
                material.Temp
            );

            if (ret != 0)
                throw new EtabsException(ret, "SetORebar_1",
                    $"Failed to set rebar properties for '{material.Name}'");

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

            _logger.LogInformation("Successfully created rebar material '{MaterialName}'", material.Name);
            return material;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating rebar material '{Name}'", material?.Name);
            throw new EtabsException($"Unexpected error creating rebar material '{material?.Name}'", ex);
        }
    }

    /// <inheritdoc/>
    public RebarMaterial AddRebarMaterial(string name, double fy, double fu)
    {
        var material = RebarMaterial.Create(name, fy, fu);
        return AddRebarMaterial(material);
    }

    /// <inheritdoc/>
    public (double Fy, double Fu, double EFy, double EFu, int SSType, int SSHysType,
        double StrainAtHardening, double StrainUltimate, bool UseCaltransSSDefaults)
        GetORebar(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double fy = 0, fu = 0, eFy = 0, eFu = 0;
            double strainHardening = 0, strainUltimate = 0;
            bool useCaltrans = false;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetORebar(name, ref fy, ref fu, ref eFy, ref eFu,
                ref ssType, ref ssHysType, ref strainHardening, ref strainUltimate,
                ref useCaltrans, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get rebar properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetORebar",
                    $"Failed to get rebar properties for '{name}'");
            }

            return (fy, fu, eFy, eFu, ssType, ssHysType, strainHardening,
                strainUltimate, useCaltrans);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting rebar properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting rebar properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public RebarMaterial GetRebarMaterial(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            // Get basic material info
            var (matType, color, notes, guid) = GetMaterial(name);
            if (matType != eMatType.Rebar)
                throw new InvalidOperationException($"Material '{name}' is not a rebar material");

            // Get isotropic properties
            var isotropic = GetMPIsotropic(name, temp);

            // Get rebar-specific properties
            double fy = 0, fu = 0, eFy = 0, eFu = 0, finalSlope = 0;
            double strainHardening = 0, strainUltimate = 0;
            bool useCaltrans = false;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetORebar_1(name, ref fy, ref fu, ref eFy, ref eFu,
                ref ssType, ref ssHysType, ref strainHardening, ref strainUltimate,
                ref finalSlope, ref useCaltrans, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get rebar properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetORebar_1",
                    $"Failed to get rebar properties for '{name}'");
            }

            return new RebarMaterial
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
                StrainUltimate = strainUltimate,
                FinalSlope = finalSlope,
                UseCaltransSSDefaults = useCaltrans,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting rebar material '{Name}'", name);
            throw new EtabsException($"Unexpected error getting rebar material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetORebar(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainUltimate, bool useCaltransSSDefaults, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fy <= 0)
                throw new ArgumentOutOfRangeException(nameof(fy), "Yield stress must be positive");
            if (fu <= fy)
                throw new ArgumentOutOfRangeException(nameof(fu), "Ultimate stress must be greater than yield stress");

            _logger.LogInformation("Setting rebar properties for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetORebar(name, fy, fu, eFy, eFu, ssType, ssHysType,
                strainAtHardening, strainUltimate, useCaltransSSDefaults, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set rebar properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetORebar",
                    $"Failed to set rebar properties for '{name}'");
            }

            _logger.LogInformation("Successfully set rebar properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting rebar properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting rebar properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetORebar_1(string name, double fy, double fu, double eFy, double eFu,
        int ssType, int ssHysType, double strainAtHardening,
        double strainUltimate, double finalSlope, bool useCaltransSSDefaults,
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

            _logger.LogInformation("Setting rebar properties (extended) for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetORebar_1(name, fy, fu, eFy, eFu, ssType, ssHysType,
                strainAtHardening, strainUltimate, finalSlope, useCaltransSSDefaults, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set rebar properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetORebar_1",
                    $"Failed to set rebar properties for '{name}'");
            }

            _logger.LogInformation("Successfully set rebar properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting rebar properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting rebar properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public List<RebarMaterial> GetAllRebarMaterials()
    {
        try
        {
            var names = GetNameList(eMatType.Rebar);
            var materials = new List<RebarMaterial>();

            foreach (var name in names)
            {
                try
                {
                    materials.Add(GetRebarMaterial(name));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get rebar material '{Name}', skipping", name);
                }
            }

            return materials;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all rebar materials");
            throw new EtabsException("Unexpected error getting all rebar materials", ex);
        }
    }

    #endregion
}
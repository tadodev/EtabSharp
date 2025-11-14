using EtabSharp.Exceptions;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// PropMaterial partial class - Concrete Materials
/// </summary>
public partial class PropMaterial
{
    #region Concrete Materials

    /// <inheritdoc/>
    public ConcreteMaterial AddConcreteMaterial(ConcreteMaterial material)
    {
        try
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));
            if (!material.IsValid())
                throw new ArgumentException("Invalid concrete material properties", nameof(material));

            _logger.LogInformation("Creating concrete material '{MaterialName}' with f'c={Fc}, Ec={Ec}",
                material.Name, material.Fc, material.IsotropicProps.E);

            // Step 1: Set material type
            int ret = SetMaterial(material.Name, eMatType.Concrete, material.Color,
                material.Notes, material.GUID);
            if (ret != 0)
                throw new EtabsException(ret, "SetMaterial",
                    $"Failed to set material type for '{material.Name}'");

            // Step 2: Set isotropic properties
            ret = SetMPIsotropic(material.Name, material.IsotropicProps);
            if (ret != 0)
                throw new EtabsException(ret, "SetMPIsotropic",
                    $"Failed to set isotropic properties for '{material.Name}'");

            // Step 3: Set concrete-specific properties
            ret = SetOConcrete_1(
                material.Name,
                material.Fc,
                material.IsLightweight,
                material.FcsFactor,
                material.SSType,
                material.SSHysType,
                material.StrainAtFc,
                material.StrainUltimate,
                material.FinalSlope,
                material.FrictionAngle,
                material.DilatationalAngle,
                material.Temp
            );

            if (ret != 0)
                throw new EtabsException(ret, "SetOConcrete_1",
                    $"Failed to set concrete properties for '{material.Name}'");

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

            _logger.LogInformation("Successfully created concrete material '{MaterialName}'", material.Name);
            return material;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating concrete material '{Name}'", material?.Name);
            throw new EtabsException($"Unexpected error creating concrete material '{material?.Name}'", ex);
        }
    }

    /// <inheritdoc/>
    public ConcreteMaterial AddConcreteMaterial(string name, double fc, double ec)
    {
        var material = ConcreteMaterial.Create(name, fc, ec);
        return AddConcreteMaterial(material);
    }

    /// <inheritdoc/>
    public (double Fc, bool IsLightweight, double FcsFactor, int SSType, int SSHysType,
        double StrainAtFc, double StrainUltimate, double FrictionAngle, double DilatationalAngle)
        GetOConcrete(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double fc = 0, fcsFactor = 0, strainAtFc = 0, strainUltimate = 0;
            double frictionAngle = 0, dilatationalAngle = 0;
            bool isLightweight = false;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOConcrete(name, ref fc, ref isLightweight,
                ref fcsFactor, ref ssType, ref ssHysType, ref strainAtFc, ref strainUltimate,
                ref frictionAngle, ref dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get concrete properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOConcrete",
                    $"Failed to get concrete properties for '{name}'");
            }

            return (fc, isLightweight, fcsFactor, ssType, ssHysType, strainAtFc,
                strainUltimate, frictionAngle, dilatationalAngle);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting concrete properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting concrete properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public ConcreteMaterial GetConcreteMaterial(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            // Get basic material info
            var (matType, color, notes, guid) = GetMaterial(name);
            if (matType != eMatType.Concrete)
                throw new InvalidOperationException($"Material '{name}' is not a concrete material");

            // Get isotropic properties
            var isotropic = GetMPIsotropic(name, temp);

            // Get concrete-specific properties
            double fc = 0, fcsFactor = 0, strainAtFc = 0, strainUltimate = 0;
            double finalSlope = 0, frictionAngle = 0, dilatationalAngle = 0;
            bool isLightweight = false;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOConcrete_1(name, ref fc, ref isLightweight,
                ref fcsFactor, ref ssType, ref ssHysType, ref strainAtFc, ref strainUltimate,
                ref finalSlope, ref frictionAngle, ref dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get concrete properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOConcrete_1",
                    $"Failed to get concrete properties for '{name}'");
            }

            return new ConcreteMaterial
            {
                Name = name,
                Color = color,
                Notes = notes,
                GUID = guid,
                IsotropicProps = isotropic,
                Fc = fc,
                IsLightweight = isLightweight,
                FcsFactor = fcsFactor,
                SSType = ssType,
                SSHysType = ssHysType,
                StrainAtFc = strainAtFc,
                StrainUltimate = strainUltimate,
                FinalSlope = finalSlope,
                FrictionAngle = frictionAngle,
                DilatationalAngle = dilatationalAngle,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting concrete material '{Name}'", name);
            throw new EtabsException($"Unexpected error getting concrete material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOConcrete(string name, double fc, bool isLightweight, double fcsFactor,
        int ssType, int ssHysType, double strainAtFc, double strainUltimate,
        double frictionAngle = 0.0, double dilatationalAngle = 0.0, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fc <= 0)
                throw new ArgumentOutOfRangeException(nameof(fc), "Compressive strength must be positive");

            _logger.LogInformation("Setting concrete properties for '{MaterialName}': f'c={Fc}", name, fc);

            int ret = _sapModel.PropMaterial.SetOConcrete(name, fc, isLightweight, fcsFactor,
                ssType, ssHysType, strainAtFc, strainUltimate, frictionAngle, dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set concrete properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOConcrete",
                    $"Failed to set concrete properties for '{name}'");
            }

            _logger.LogInformation("Successfully set concrete properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting concrete properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting concrete properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOConcrete_1(string name, double fc, bool isLightweight, double fcsFactor,
        int ssType, int ssHysType, double strainAtFc, double strainUltimate,
        double finalSlope, double frictionAngle = 0.0, double dilatationalAngle = 0.0,
        double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fc <= 0)
                throw new ArgumentOutOfRangeException(nameof(fc), "Compressive strength must be positive");

            _logger.LogInformation("Setting concrete properties (extended) for '{MaterialName}': f'c={Fc}",
                name, fc);

            int ret = _sapModel.PropMaterial.SetOConcrete_1(name, fc, isLightweight, fcsFactor,
                ssType, ssHysType, strainAtFc, strainUltimate, finalSlope,
                frictionAngle, dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set concrete properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOConcrete_1",
                    $"Failed to set concrete properties for '{name}'");
            }

            _logger.LogInformation("Successfully set concrete properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting concrete properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting concrete properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public List<ConcreteMaterial> GetAllConcreteMaterials()
    {
        try
        {
            var names = GetNameList(eMatType.Concrete);
            var materials = new List<ConcreteMaterial>();

            foreach (var name in names)
            {
                try
                {
                    materials.Add(GetConcreteMaterial(name));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get concrete material '{Name}', skipping", name);
                }
            }

            return materials;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all concrete materials");
            throw new EtabsException("Unexpected error getting all concrete materials", ex);
        }
    }

    #endregion
}
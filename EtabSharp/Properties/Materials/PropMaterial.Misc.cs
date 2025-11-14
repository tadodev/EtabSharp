using EtabSharp.Exceptions;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// PropMaterial partial class - Tendon, NoDesign, Advanced Properties, and Stress-Strain Curves
/// </summary>
public partial class PropMaterial
{
    #region Tendon Materials

    /// <inheritdoc/>
    public TendonMaterial AddTendonMaterial(TendonMaterial material)
    {
        try
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));
            if (!material.IsValid())
                throw new ArgumentException("Invalid tendon material properties", nameof(material));

            _logger.LogInformation("Creating tendon material '{MaterialName}' with Fy={Fy}, Fu={Fu}",
                material.Name, material.Fy, material.Fu);

            // Step 1: Set material type
            int ret = SetMaterial(material.Name, eMatType.Tendon, material.Color,
                material.Notes, material.GUID);
            if (ret != 0)
                throw new EtabsException(ret, "SetMaterial",
                    $"Failed to set material type for '{material.Name}'");

            // Step 2: Set isotropic properties
            ret = SetMPIsotropic(material.Name, material.IsotropicProps);
            if (ret != 0)
                throw new EtabsException(ret, "SetMPIsotropic",
                    $"Failed to set isotropic properties for '{material.Name}'");

            // Step 3: Set tendon-specific properties
            ret = SetOTendon_1(
                material.Name,
                material.Fy,
                material.Fu,
                material.SSType,
                material.SSHysType,
                material.FinalSlope,
                material.Temp
            );

            if (ret != 0)
                throw new EtabsException(ret, "SetOTendon_1",
                    $"Failed to set tendon properties for '{material.Name}'");

            if (material.Damping != null)
            {
                SetDamping(material.Name, material.Damping);
            }

            if (material.WeightMass != null)
            {
                SetWeightAndMass(material.Name, 1, material.WeightMass.W, material.WeightMass.Temp);
            }

            _logger.LogInformation("Successfully created tendon material '{MaterialName}'", material.Name);
            return material;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating tendon material '{Name}'", material?.Name);
            throw new EtabsException($"Unexpected error creating tendon material '{material?.Name}'", ex);
        }
    }

    /// <inheritdoc/>
    public TendonMaterial AddTendonMaterial(string name, double fy, double fu)
    {
        var material = TendonMaterial.Create(name, fy, fu);
        return AddTendonMaterial(material);
    }

    /// <inheritdoc/>
    public (double Fy, double Fu, int SSType, int SSHysType)
        GetOTendon(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double fy = 0, fu = 0;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOTendon(name, ref fy, ref fu,
                ref ssType, ref ssHysType, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get tendon properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOTendon",
                    $"Failed to get tendon properties for '{name}'");
            }

            return (fy, fu, ssType, ssHysType);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting tendon properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting tendon properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public TendonMaterial GetTendonMaterial(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            var (matType, color, notes, guid) = GetMaterial(name);
            if (matType != eMatType.Tendon)
                throw new InvalidOperationException($"Material '{name}' is not a tendon material");

            var isotropic = GetMPIsotropic(name, temp);

            double fy = 0, fu = 0, finalSlope = 0;
            int ssType = 0, ssHysType = 0;

            int ret = _sapModel.PropMaterial.GetOTendon_1(name, ref fy, ref fu,
                ref ssType, ref ssHysType, ref finalSlope, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get tendon properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetOTendon_1",
                    $"Failed to get tendon properties for '{name}'");
            }

            return new TendonMaterial
            {
                Name = name,
                Color = color,
                Notes = notes,
                GUID = guid,
                IsotropicProps = isotropic,
                Fy = fy,
                Fu = fu,
                SSType = ssType,
                SSHysType = ssHysType,
                FinalSlope = finalSlope,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting tendon material '{Name}'", name);
            throw new EtabsException($"Unexpected error getting tendon material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOTendon(string name, double fy, double fu, int ssType, int ssHysType,
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

            _logger.LogInformation("Setting tendon properties for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetOTendon(name, fy, fu, ssType, ssHysType, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set tendon properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOTendon",
                    $"Failed to set tendon properties for '{name}'");
            }

            _logger.LogInformation("Successfully set tendon properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting tendon properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting tendon properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetOTendon_1(string name, double fy, double fu, int ssType, int ssHysType,
        double finalSlope, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (fy <= 0)
                throw new ArgumentOutOfRangeException(nameof(fy), "Yield stress must be positive");
            if (fu <= fy)
                throw new ArgumentOutOfRangeException(nameof(fu), "Ultimate stress must be greater than yield stress");

            _logger.LogInformation("Setting tendon properties (extended) for '{MaterialName}': Fy={Fy}, Fu={Fu}",
                name, fy, fu);

            int ret = _sapModel.PropMaterial.SetOTendon_1(name, fy, fu, ssType, ssHysType, finalSlope, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set tendon properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetOTendon_1",
                    $"Failed to set tendon properties for '{name}'");
            }

            _logger.LogInformation("Successfully set tendon properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting tendon properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting tendon properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public List<TendonMaterial> GetAllTendonMaterials()
    {
        try
        {
            var names = GetNameList(eMatType.Tendon);
            var materials = new List<TendonMaterial>();

            foreach (var name in names)
            {
                try
                {
                    materials.Add(GetTendonMaterial(name));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to get tendon material '{Name}', skipping", name);
                }
            }

            return materials;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all tendon materials");
            throw new EtabsException("Unexpected error getting all tendon materials", ex);
        }
    }

    #endregion

    #region No Design Materials

    /// <inheritdoc/>
    public (double FrictionAngle, double DilatationalAngle)
        GetONoDesign(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double frictionAngle = 0, dilatationalAngle = 0;

            int ret = _sapModel.PropMaterial.GetONoDesign(name, ref frictionAngle,
                ref dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get no-design properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetONoDesign",
                    $"Failed to get no-design properties for '{name}'");
            }

            return (frictionAngle, dilatationalAngle);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting no-design properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting no-design properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetONoDesign(string name, double frictionAngle = 0.0,
        double dilatationalAngle = 0.0, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            _logger.LogInformation("Setting no-design properties for '{MaterialName}'", name);

            int ret = _sapModel.PropMaterial.SetONoDesign(name, frictionAngle, dilatationalAngle, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set no-design properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetONoDesign",
                    $"Failed to set no-design properties for '{name}'");
            }

            _logger.LogInformation("Successfully set no-design properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting no-design properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting no-design properties for '{name}'", ex);
        }
    }

    #endregion

    #region Stress-Strain Curves

    /// <inheritdoc/>
    public List<StressStrainPoint> GetSSCurve(string name, string sectName = "",
        double rebarArea = 0.0, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            int numberPoints = 0;
            int[] pointID = null;
            double[] strain = null;
            double[] stress = null;

            int ret = _sapModel.PropMaterial.GetSSCurve(name, ref numberPoints, ref pointID,
                ref strain, ref stress, sectName, rebarArea, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get stress-strain curve for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetSSCurve",
                    $"Failed to get stress-strain curve for '{name}'");
            }

            var points = new List<StressStrainPoint>();
            for (int i = 0; i < numberPoints; i++)
            {
                points.Add(new StressStrainPoint
                {
                    PointID = pointID[i],
                    Strain = strain[i],
                    Stress = stress[i]
                });
            }

            return points;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting stress-strain curve for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting stress-strain curve for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetSSCurve(string name, List<StressStrainPoint> points, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (points == null || points.Count == 0)
                throw new ArgumentException("Stress-strain points cannot be empty", nameof(points));

            _logger.LogInformation("Setting stress-strain curve for '{MaterialName}' with {Count} points",
                name, points.Count);

            int numberPoints = points.Count;
            int[] pointID = points.Select(p => p.PointID).ToArray();
            double[] strain = points.Select(p => p.Strain).ToArray();
            double[] stress = points.Select(p => p.Stress).ToArray();

            int ret = _sapModel.PropMaterial.SetSSCurve(name, numberPoints, ref pointID,
                ref strain, ref stress, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set stress-strain curve for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetSSCurve",
                    $"Failed to set stress-strain curve for '{name}'");
            }

            _logger.LogInformation("Successfully set stress-strain curve for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting stress-strain curve for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting stress-strain curve for '{name}'", ex);
        }
    }

    #endregion

    #region Temperature-Dependent Properties

    /// <inheritdoc/>
    public double[] GetTemp(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            int numberItems = 0;
            double[] temps = null;

            int ret = _sapModel.PropMaterial.GetTemp(name, ref numberItems, ref temps);

            if (ret != 0)
            {
                _logger.LogError("Failed to get temperatures for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetTemp",
                    $"Failed to get temperatures for '{name}'");
            }

            return temps ?? Array.Empty<double>();
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting temperatures for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting temperatures for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetTemp(string name, double[] temps)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (temps == null || temps.Length == 0)
                throw new ArgumentException("Temperature array cannot be empty", nameof(temps));

            _logger.LogInformation("Setting {Count} temperatures for '{MaterialName}'", temps.Length, name);

            int numberItems = temps.Length;
            int ret = _sapModel.PropMaterial.SetTemp(name, numberItems, ref temps);

            if (ret != 0)
            {
                _logger.LogError("Failed to set temperatures for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetTemp",
                    $"Failed to set temperatures for '{name}'");
            }

            _logger.LogInformation("Successfully set temperatures for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting temperatures for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting temperatures for '{name}'", ex);
        }
    }

    #endregion

    #region Anisotropic and Orthotropic Properties

    /// <inheritdoc/>
    public (double[] E, double[] U, double[] A, double[] G)
        GetMPAnisotropic(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double[] e = new double[3];
            double[] u = new double[3];
            double[] a = new double[3];
            double[] g = new double[3];

            int ret = _sapModel.PropMaterial.GetMPAnisotropic(name, ref e, ref u, ref a, ref g, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get anisotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetMPAnisotropic",
                    $"Failed to get anisotropic properties for '{name}'");
            }

            return (e, u, a, g);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting anisotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting anisotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMPAnisotropic(string name, double[] e, double[] u, double[] a,
        double[] g, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (e == null || e.Length != 3)
                throw new ArgumentException("E array must have 3 elements", nameof(e));
            if (u == null || u.Length != 3)
                throw new ArgumentException("U array must have 3 elements", nameof(u));
            if (a == null || a.Length != 3)
                throw new ArgumentException("A array must have 3 elements", nameof(a));
            if (g == null || g.Length != 3)
                throw new ArgumentException("G array must have 3 elements", nameof(g));

            _logger.LogInformation("Setting anisotropic properties for '{MaterialName}'", name);

            int ret = _sapModel.PropMaterial.SetMPAnisotropic(name, ref e, ref u, ref a, ref g, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set anisotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetMPAnisotropic",
                    $"Failed to set anisotropic properties for '{name}'");
            }

            _logger.LogInformation("Successfully set anisotropic properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting anisotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting anisotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public (double[] E, double[] U, double[] A, double[] G)
        GetMPOrthotropic(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double[] e = new double[3];
            double[] u = new double[3];
            double[] a = new double[3];
            double[] g = new double[3];

            int ret = _sapModel.PropMaterial.GetMPOrthotropic(name, ref e, ref u, ref a, ref g, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get orthotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetMPOrthotropic",
                    $"Failed to get orthotropic properties for '{name}'");
            }

            return (e, u, a, g);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting orthotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting orthotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMPOrthotropic(string name, double[] e, double[] u, double[] a,
        double[] g, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (e == null || e.Length != 3)
                throw new ArgumentException("E array must have 3 elements", nameof(e));
            if (u == null || u.Length != 3)
                throw new ArgumentException("U array must have 3 elements", nameof(u));
            if (a == null || a.Length != 3)
                throw new ArgumentException("A array must have 3 elements", nameof(a));
            if (g == null || g.Length != 3)
                throw new ArgumentException("G array must have 3 elements", nameof(g));

            _logger.LogInformation("Setting orthotropic properties for '{MaterialName}'", name);

            int ret = _sapModel.PropMaterial.SetMPOrthotropic(name, ref e, ref u, ref a, ref g, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set orthotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetMPOrthotropic",
                    $"Failed to set orthotropic properties for '{name}'");
            }

            _logger.LogInformation("Successfully set orthotropic properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting orthotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting orthotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public (double E, double A) GetMPUniaxial(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double e = 0, a = 0;

            int ret = _sapModel.PropMaterial.GetMPUniaxial(name, ref e, ref a, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get uniaxial properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetMPUniaxial",
                    $"Failed to get uniaxial properties for '{name}'");
            }

            return (e, a);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting uniaxial properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting uniaxial properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMPUniaxial(string name, double e, double a, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (e <= 0)
                throw new ArgumentOutOfRangeException(nameof(e), "Modulus of elasticity must be positive");

            _logger.LogInformation("Setting uniaxial properties for '{MaterialName}': E={E}, A={A}",
                name, e, a);

            int ret = _sapModel.PropMaterial.SetMPUniaxial(name, e, a, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set uniaxial properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetMPUniaxial",
                    $"Failed to set uniaxial properties for '{name}'");
            }

            _logger.LogInformation("Successfully set uniaxial properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting uniaxial properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting uniaxial properties for '{name}'", ex);
        }
    }

    #endregion

    #region Advanced Convenience Methods

    /// <inheritdoc/>
    public MaterialProperty GetCompleteMaterial(string name)
    {
        try
        {
            var (matType, _, _, _) = GetMaterial(name);

            return matType switch
            {
                eMatType.Concrete => GetConcreteMaterial(name),
                eMatType.Steel => GetSteelMaterial(name),
                eMatType.Rebar => GetRebarMaterial(name),
                eMatType.Tendon => GetTendonMaterial(name),
                _ => throw new NotSupportedException($"Material type {matType} not fully supported yet")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting complete material '{Name}'", name);
            throw new EtabsException($"Unexpected error getting complete material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int DeleteMaterialsByType(eMatType matType)
    {
        try
        {
            var names = GetNameList(matType);
            int deletedCount = 0;

            foreach (var name in names)
            {
                try
                {
                    if (Delete(name) == 0)
                        deletedCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete material '{Name}'", name);
                }
            }

            _logger.LogInformation("Deleted {Count} materials of type {MatType}", deletedCount, matType);
            return deletedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error deleting materials by type {MatType}", matType);
            throw new EtabsException($"Unexpected error deleting materials by type {matType}", ex);
        }
    }

    #endregion
}
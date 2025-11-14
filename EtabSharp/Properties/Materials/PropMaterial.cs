using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Properties.Materials.Constants;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// Comprehensive implementation for managing material properties in ETABS.
/// Part 1: Core management, isotropic properties, and material information.
/// </summary>
public partial class PropMaterial : IPropMaterial
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal PropMaterial(cSapModel sapModel, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
    }

    #region Material Management

    /// <inheritdoc/>
    public string AddMaterialFromLibrary(ref string name, eMatType matType, string region,
        string standard, string grade, string userName = "")
    {
        try
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region cannot be empty.", nameof(region));
            if (string.IsNullOrWhiteSpace(standard))
                throw new ArgumentException("Standard cannot be empty.", nameof(standard));
            if (string.IsNullOrWhiteSpace(grade))
                throw new ArgumentException("Grade cannot be empty.", nameof(grade));

            _logger.LogInformation(
                "Adding material from library: Type={MatType}, Region={Region}, Standard={Standard}, Grade={Grade}",
                matType, region, standard, grade);

            int ret = _sapModel.PropMaterial.AddMaterial(ref name, matType, region, standard, grade, userName);

            if (ret != 0)
            {
                _logger.LogError("Failed to add material from library. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "AddMaterial",
                    $"Failed to add material from library: {region}/{standard}/{grade}");
            }

            _logger.LogInformation("Successfully added material '{MaterialName}' from library", name);
            return name;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error adding material from library");
            throw new EtabsException("Unexpected error adding material from library", ex);
        }
    }

    /// <inheritdoc/>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(currentName))
                throw new ArgumentException("Current name cannot be empty.", nameof(currentName));
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("New name cannot be empty.", nameof(newName));

            _logger.LogInformation("Changing material name from '{OldName}' to '{NewName}'",
                currentName, newName);

            int ret = _sapModel.PropMaterial.ChangeName(currentName, newName);

            if (ret != 0)
            {
                _logger.LogError("Failed to change material name. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "ChangeName",
                    $"Failed to change material name from '{currentName}' to '{newName}'");
            }

            _logger.LogInformation("Successfully changed material name to '{NewName}'", newName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error changing material name");
            throw new EtabsException("Unexpected error changing material name", ex);
        }
    }

    /// <inheritdoc/>
    public int Count(eMatType matType = 0)
    {
        try
        {
            int count = _sapModel.PropMaterial.Count(matType);
            _logger.LogDebug("Material count: {Count} (type: {MatType})", count, matType);
            return count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting material count");
            throw new EtabsException("Unexpected error getting material count", ex);
        }
    }

    /// <inheritdoc/>
    public int Delete(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            _logger.LogInformation("Deleting material '{MaterialName}'", name);

            int ret = _sapModel.PropMaterial.Delete(name);

            if (ret != 0)
            {
                _logger.LogWarning("Failed to delete material '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
            }
            else
            {
                _logger.LogInformation("Successfully deleted material '{MaterialName}'", name);
            }

            return ret;
        }
        catch (Exception ex) when (ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error deleting material '{Name}'", name);
            throw new EtabsException($"Unexpected error deleting material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public string[] GetNameList(eMatType matType = 0)
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.PropMaterial.GetNameList(ref numberNames, ref names, matType);

            if (ret != 0)
            {
                _logger.LogError("Failed to get material name list. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetNameList", "Failed to retrieve material names");
            }

            _logger.LogDebug("Retrieved {Count} material names (type: {MatType})", numberNames, matType);
            return names ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting material name list");
            throw new EtabsException("Unexpected error retrieving material names", ex);
        }
    }

    /// <inheritdoc/>
    public (eMatType MatType, int Color, string Notes, string GUID) GetMaterial(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            eMatType matType = eMatType.Steel;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropMaterial.GetMaterial(name, ref matType, ref color, ref notes, ref guid);

            if (ret != 0)
            {
                _logger.LogError("Failed to get material info for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetMaterial", $"Failed to get material info for '{name}'");
            }

            return (matType, color, notes, guid);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting material info for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting material info for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMaterial(string name, eMatType matType, int color = -1,
        string notes = "", string guid = "")
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            _logger.LogInformation("Setting material '{MaterialName}' as type {MatType}", name, matType);

            int ret = _sapModel.PropMaterial.SetMaterial(name, matType, color, notes, guid);

            if (ret != 0)
            {
                _logger.LogError("Failed to set material '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetMaterial", $"Failed to set material '{name}'");
            }

            _logger.LogInformation("Successfully set material '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting material '{Name}'", name);
            throw new EtabsException($"Unexpected error setting material '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public (eMatType MatType, int SymType) GetTypeOAPI(string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            eMatType matType = eMatType.Steel;
            int symType = 0;

            int ret = _sapModel.PropMaterial.GetTypeOAPI(name, ref matType, ref symType);

            if (ret != 0)
            {
                _logger.LogError("Failed to get material type for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get material type for '{name}'");
            }

            return (matType, symType);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting material type for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting material type for '{name}'", ex);
        }
    }

    #endregion

    #region Isotropic Properties

    /// <inheritdoc/>
    public IsotropicProperties GetMPIsotropic(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double e = 0, u = 0, a = 0, g = 0;

            int ret = _sapModel.PropMaterial.GetMPIsotropic(name, ref e, ref u, ref a, ref g, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get isotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetMPIsotropic",
                    $"Failed to get isotropic properties for '{name}'");
            }

            return new IsotropicProperties
            {
                E = e,
                U = u,
                A = a,
                G = g,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting isotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting isotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMPIsotropic(string name, double e, double u, double a, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (e <= 0)
                throw new ArgumentOutOfRangeException(nameof(e), "Modulus of elasticity must be positive");

            _logger.LogInformation("Setting isotropic properties for '{MaterialName}': E={E}, U={U}, A={A}",
                name, e, u, a);

            int ret = _sapModel.PropMaterial.SetMPIsotropic(name, e, u, a, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set isotropic properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetMPIsotropic",
                    $"Failed to set isotropic properties for '{name}'");
            }

            _logger.LogInformation("Successfully set isotropic properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting isotropic properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting isotropic properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetMPIsotropic(string name, IsotropicProperties props)
    {
        return SetMPIsotropic(name, props.E, props.U, props.A, props.Temp);
    }

    #endregion

    #region Damping Properties

    /// <inheritdoc/>
    public DampingProperties GetDamping(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double modalRatio = 0, viscousMass = 0, viscousStiff = 0;
            double hystereticMass = 0, hystereticStiff = 0;

            int ret = _sapModel.PropMaterial.GetDamping(name, ref modalRatio, ref viscousMass,
                ref viscousStiff, ref hystereticMass, ref hystereticStiff, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get damping properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetDamping",
                    $"Failed to get damping properties for '{name}'");
            }

            return new DampingProperties
            {
                ModalRatio = modalRatio,
                ViscousMassCoeff = viscousMass,
                ViscousStiffCoeff = viscousStiff,
                HystereticMassCoeff = hystereticMass,
                HystereticStiffCoeff = hystereticStiff,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting damping properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting damping properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetDamping(string name, double modalRatio, double viscousMassCoeff,
        double viscousStiffCoeff, double hystereticMassCoeff,
        double hystereticStiffCoeff, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            _logger.LogInformation("Setting damping properties for '{MaterialName}'", name);

            int ret = _sapModel.PropMaterial.SetDamping(name, modalRatio, viscousMassCoeff,
                viscousStiffCoeff, hystereticMassCoeff, hystereticStiffCoeff, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set damping properties for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetDamping",
                    $"Failed to set damping properties for '{name}'");
            }

            _logger.LogInformation("Successfully set damping properties for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting damping properties for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting damping properties for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetDamping(string name, DampingProperties damping)
    {
        return SetDamping(name, damping.ModalRatio, damping.ViscousMassCoeff,
            damping.ViscousStiffCoeff, damping.HystereticMassCoeff,
            damping.HystereticStiffCoeff, damping.Temp);
    }

    #endregion

    #region Weight and Mass

    /// <inheritdoc/>
    public WeightMassProperties GetWeightAndMass(string name, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));

            double w = 0, m = 0;

            int ret = _sapModel.PropMaterial.GetWeightAndMass(name, ref w, ref m, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to get weight/mass for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "GetWeightAndMass",
                    $"Failed to get weight/mass for '{name}'");
            }

            return new WeightMassProperties
            {
                W = w,
                M = m,
                Temp = temp
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting weight/mass for '{Name}'", name);
            throw new EtabsException($"Unexpected error getting weight/mass for '{name}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SetWeightAndMass(string name, int myOption, double value, double temp = 0.0)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Material name cannot be empty.", nameof(name));
            if (myOption < 1 || myOption > 2)
                throw new ArgumentOutOfRangeException(nameof(myOption), "Option must be 1 (weight) or 2 (mass)");

            _logger.LogInformation("Setting {Type} for '{MaterialName}': {Value}",
                myOption == 1 ? "weight" : "mass", name, value);

            int ret = _sapModel.PropMaterial.SetWeightAndMass(name, myOption, value, temp);

            if (ret != 0)
            {
                _logger.LogError("Failed to set weight/mass for '{MaterialName}'. Return code: {ReturnCode}",
                    name, ret);
                throw new EtabsException(ret, "SetWeightAndMass",
                    $"Failed to set weight/mass for '{name}'");
            }

            _logger.LogInformation("Successfully set weight/mass for '{MaterialName}'", name);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting weight/mass for '{Name}'", name);
            throw new EtabsException($"Unexpected error setting weight/mass for '{name}'", ex);
        }
    }

    #endregion

    #region Convenience Methods

    /// <inheritdoc/>
    public bool MaterialExists(string name)
    {
        try
        {
            var allNames = GetNameList();
            return allNames.Contains(name, StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
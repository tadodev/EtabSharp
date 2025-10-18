using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Properties.Materials.Constants;
using EtabSharp.Properties.Materials.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Materials;

/// <summary>
/// Handles ETABS material creation (Concrete, Steel, Rebar, etc.)
/// </summary>
public sealed class PropMaterial : IPropMaterial
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal PropMaterial(cSapModel sapModel, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
    }

    ///<inheritdoc/>
    public PropConcrete AddConcreteMaterial(string name, double fpc, double Ec)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Material name cannot be empty.", nameof(name));

        if (fpc <= 0)
            throw new ArgumentOutOfRangeException(nameof(fpc), "Compressive strength (f'c) must be positive.");

        if (Ec <= 0)
            throw new ArgumentOutOfRangeException(nameof(Ec), "Elastic modulus (Ec) must be positive.");

        try
        {
            _logger.LogInformation("Creating concrete material '{Name}' with f'c={Fpc}, Ec={Ec}", name, fpc, Ec);

            // Step 1: Define material as Concrete
            int ret = _sapModel.PropMaterial.SetMaterial(name, eMatType.Concrete);
            if (ret != 0)
            {
                _logger.LogError("Error setting material {MaterialName} to Concrete. Return code: {ReturnCode}", name, ret);
                throw new EtabsMaterialException(ret, "SetMaterial", name, "Failed to initialize concrete material.");
            }

            // Step 2: Set isotropic mechanical properties
            ret = _sapModel.PropMaterial.SetMPIsotropic(name, Ec, PropConcreteDefault.U, PropConcreteDefault.Alpha);
            if (ret != 0)
            {
                _logger.LogError("Error setting isotropic properties for {MaterialName}. Return code: {ReturnCode}", name, ret);
                throw new EtabsMaterialException(ret, "SetMPIsotropic", name, "Failed to set isotropic properties.");
            }

            // Step 3: Concrete-specific properties
            ret = _sapModel.PropMaterial.SetOConcrete_1(
                name,
                fpc,
                PropConcreteDefault.IsLightweight,
                PropConcreteDefault.FcsFactor,
                PropConcreteDefault.SSType,
                PropConcreteDefault.SSHysType,
                PropConcreteDefault.StrainAtFc,
                PropConcreteDefault.StrainUltimate,
                PropConcreteDefault.FinalSlope,
                PropConcreteDefault.FrictionAngle,
                PropConcreteDefault.DilatationalAngle
            );

            if (ret != 0)
            {
                _logger.LogError("Error setting concrete-specific properties for {MaterialName}. Return code: {ReturnCode}", name, ret);
                throw new EtabsMaterialException(ret, "SetOConcrete_1", name, "Failed to set concrete-specific properties.");
            }

            _logger.LogInformation("Successfully created concrete material '{Name}'", name);

            return new PropConcrete
            {
                Name = name,
                fpc = fpc,
                Ec = Ec,
                nu = PropConcreteDefault.U,
                alpha = PropConcreteDefault.Alpha,
                IsLightWeight = PropConcreteDefault.IsLightweight,
                StrainAtFc = PropConcreteDefault.StrainAtFc,
                StrainAtU = PropConcreteDefault.StrainUltimate
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating concrete material '{Name}'", name);
            throw new EtabsMaterialException(name, $"Unexpected error creating concrete material: {ex.Message}");
        }
    }

    ///<inheritdoc/>
    public string AddDefaultMaterial(eMatType matType, string region, string standard, string grade)
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public PropRebar AddRebarMaterial(string name, double fy, double fu)
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public PropSteel AddSteelMaterial(string name, double fy, double fu)
    {
        throw new NotImplementedException();
    }

    ///<inheritdoc/>
    public string[] GetNameList(eMatType matType = 0)
    {
        try
        {
            int numberOfNames = 0;
            string[] names = null;

            int ret = _sapModel.PropMaterial.GetNameList(ref numberOfNames, ref names, matType);
            if (ret != 0)
            {
                _logger.LogError("Failed to get material name list. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetNameList", "Failed to retrieve material names from ETABS model.");
            }

            _logger.LogDebug("Retrieved {Count} materials of type {MatType}", numberOfNames, matType);
            return names ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting material name list");
            throw new EtabsException("Unexpected error retrieving material names", ex);
        }
    }
}

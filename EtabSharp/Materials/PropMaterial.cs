using EtabSharp.Constants;
using EtabSharp.Interfaces;
using EtabSharp.Models.Materials;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Materials;

/// <summary>
/// Adding Concrete material to Etabs Model with predefined f'c value
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

    public async Task<PropConcrete> AddConcreteMaterial(string name, double fpc, double Ec, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Material name cannot be empty");

        await Task.Run(() =>
        {
            int ret = _sapModel.PropMaterial.SetMaterial(name, eMatType.Concrete);
            if (ret != 0)
            {
                _logger.LogError("Error setting material {MaterialName} to Concrete. Return code: {ReturnCode}", name, ret);
                throw new InvalidOperationException($"Error setting material {name} to Concrete. Return code: {ret}");
            }
            ret = _sapModel.PropMaterial.SetMPIsotropic(name, Ec, PropConcreteDefault.U, PropConcreteDefault.Alpha);
            if (ret != 0)
            {
                _logger.LogError("Error setting concrete properties for material {MaterialName}. Return code: {ReturnCode}", name, ret);
                throw new InvalidOperationException($"Error setting concrete properties for material {name}. Return code: {ret}");
            }
            var isLightweight = PropConcreteDefault.IsLightweight;
            var fcsFactor = PropConcreteDefault.FcsFactor;
            var ssType = PropConcreteDefault.SSType;
            var ssHysType = PropConcreteDefault.SSHysType;
            var strainAtFc = PropConcreteDefault.StrainAtFc;
            var strainAtU = PropConcreteDefault.StrainUltimate;
            var finalSlope = PropConcreteDefault.FinalSlope;
            var frictionAngle = PropConcreteDefault.FrictionAngle;
            var dilatationalAngle = PropConcreteDefault.DilatationalAngle;

            ret = _sapModel.PropMaterial.SetOConcrete_1(name, fpc, isLightweight, fcsFactor, ssType, ssHysType, strainAtFc, strainAtU, finalSlope, frictionAngle, dilatationalAngle);
            if (ret != 0)
            {
                _logger.LogError("Error setting concrete properties for material {MaterialName}. Return code: {ReturnCode}", name, ret);
                throw new InvalidOperationException($"Error setting concrete properties for material {name}. Return code: {ret}");
            }
        }, cancellationToken);

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

    public Task<string> AddDefaultMaterial(eMatType matType, string Region, string standard, string grade, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PropRebar> AddRebarMaterial(string name, double fyp, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PropSteel> AddSteelMaterial(string name, double fy, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string[]> GetNameList(eMatType MatType = 0, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

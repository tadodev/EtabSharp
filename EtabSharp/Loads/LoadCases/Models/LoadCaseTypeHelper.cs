using ETABSv1;

namespace EtabSharp.Loads.LoadCases.Models;

/// <summary>
/// Helper class for load case type information.
/// </summary>
public static class LoadCaseTypeHelper
{
    /// <summary>
    /// Gets a friendly description for a load case type.
    /// </summary>
    public static string GetDescription(eLoadCaseType caseType)
    {
        return caseType switch
        {
            eLoadCaseType.LinearStatic => "Linear Static Analysis - First-order elastic analysis",
            eLoadCaseType.NonlinearStatic => "Nonlinear Static Analysis - P-Delta, material nonlinearity, or staged construction",
            eLoadCaseType.Modal => "Modal Analysis - Natural frequencies and mode shapes",
            eLoadCaseType.ResponseSpectrum => "Response Spectrum Analysis - Seismic analysis using response spectrum",
            eLoadCaseType.LinearHistory => "Linear Time History - Direct integration time history (linear)",
            eLoadCaseType.NonlinearHistory => "Nonlinear Time History - Direct integration time history (nonlinear)",
            eLoadCaseType.LinearDynamic => "Linear Dynamic Analysis - Dynamic analysis with linear behavior",
            eLoadCaseType.NonlinearDynamic => "Nonlinear Dynamic Analysis - Dynamic analysis with nonlinear behavior",
            eLoadCaseType.MovingLoad => "Moving Load Analysis - Moving or traveling loads (bridges)",
            eLoadCaseType.Buckling => "Buckling Analysis - Elastic stability analysis",
            eLoadCaseType.SteadyState => "Steady State Analysis - Frequency domain steady state",
            eLoadCaseType.PowerSpectralDensity => "Power Spectral Density - Random vibration analysis",
            eLoadCaseType.LinearStaticMultiStep => "Linear Static Multi-Step - Sequential linear static stages",
            eLoadCaseType.HyperStatic => "Hyperstatic Analysis - Secondary forces from prestressing",
            _ => "Unknown Load Case Type"
        };
    }

    /// <summary>
    /// Checks if a case type is static.
    /// </summary>
    public static bool IsStatic(eLoadCaseType caseType)
    {
        return caseType switch
        {
            eLoadCaseType.LinearStatic => true,
            eLoadCaseType.NonlinearStatic => true,
            eLoadCaseType.LinearStaticMultiStep => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks if a case type is dynamic.
    /// </summary>
    public static bool IsDynamic(eLoadCaseType caseType)
    {
        return caseType switch
        {
            eLoadCaseType.Modal => true,
            eLoadCaseType.ResponseSpectrum => true,
            eLoadCaseType.LinearHistory => true,
            eLoadCaseType.NonlinearHistory => true,
            eLoadCaseType.LinearDynamic => true,
            eLoadCaseType.NonlinearDynamic => true,
            eLoadCaseType.SteadyState => true,
            eLoadCaseType.PowerSpectralDensity => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks if a case type is nonlinear.
    /// </summary>
    public static bool IsNonlinear(eLoadCaseType caseType)
    {
        return caseType switch
        {
            eLoadCaseType.NonlinearStatic => true,
            eLoadCaseType.NonlinearHistory => true,
            eLoadCaseType.NonlinearDynamic => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks if a case type is time history.
    /// </summary>
    public static bool IsTimeHistory(eLoadCaseType caseType)
    {
        return caseType switch
        {
            eLoadCaseType.LinearHistory => true,
            eLoadCaseType.NonlinearHistory => true,
            _ => false
        };
    }

    /// <summary>
    /// Gets all available load case types.
    /// </summary>
    public static eLoadCaseType[] GetAllTypes()
    {
        return new[]
        {
            eLoadCaseType.LinearStatic,
            eLoadCaseType.NonlinearStatic,
            eLoadCaseType.Modal,
            eLoadCaseType.ResponseSpectrum,
            eLoadCaseType.LinearHistory,
            eLoadCaseType.NonlinearHistory,
            eLoadCaseType.LinearDynamic,
            eLoadCaseType.NonlinearDynamic,
            eLoadCaseType.MovingLoad,
            eLoadCaseType.Buckling,
            eLoadCaseType.SteadyState,
            eLoadCaseType.PowerSpectralDensity,
            eLoadCaseType.LinearStaticMultiStep,
            eLoadCaseType.HyperStatic
        };
    }

    /// <summary>
    /// Gets common load case types for typical building projects.
    /// </summary>
    public static eLoadCaseType[] GetCommonBuildingTypes()
    {
        return new[]
        {
            eLoadCaseType.LinearStatic,
            eLoadCaseType.Modal,
            eLoadCaseType.ResponseSpectrum,
            eLoadCaseType.NonlinearStatic
        };
    }
}
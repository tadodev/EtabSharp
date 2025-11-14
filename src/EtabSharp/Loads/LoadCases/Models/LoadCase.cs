using ETABSv1;

namespace EtabSharp.Loads.LoadCases.Models;

/// <summary>
/// Represents a load case in the ETABS model.
/// Load cases define how load patterns are combined and analyzed.
/// </summary>
public class LoadCase
{
    /// <summary>
    /// Gets or sets the unique name of the load case.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of load case.
    /// </summary>
    public eLoadCaseType CaseType { get; set; } = eLoadCaseType.LinearStatic;

    /// <summary>
    /// Gets or sets the subtype of the load case (specific to case type).
    /// </summary>
    public int SubType { get; set; } = 0;

    /// <summary>
    /// Gets or sets the design type for the load case.
    /// </summary>
    public eLoadPatternType DesignType { get; set; } = eLoadPatternType.Dead;

    /// <summary>
    /// Gets or sets the design type option.
    /// 1 = Strength
    /// 2 = Serviceability
    /// 3 = Other
    /// </summary>
    public int DesignTypeOption { get; set; } = 1;

    /// <summary>
    /// Gets or sets whether this is an automatically generated case.
    /// </summary>
    public bool IsAuto { get; set; } = false;

    /// <summary>
    /// Gets the case type as a string.
    /// </summary>
    public string CaseTypeString => CaseType switch
    {
        eLoadCaseType.LinearStatic => "Linear Static",
        eLoadCaseType.NonlinearStatic => "Nonlinear Static",
        eLoadCaseType.Modal => "Modal",
        eLoadCaseType.ResponseSpectrum => "Response Spectrum",
        eLoadCaseType.LinearHistory => "Linear History",
        eLoadCaseType.NonlinearHistory => "Nonlinear History",
        eLoadCaseType.LinearDynamic => "Linear Dynamic",
        eLoadCaseType.NonlinearDynamic => "Nonlinear Dynamic",
        eLoadCaseType.MovingLoad => "Moving Load",
        eLoadCaseType.Buckling => "Buckling",
        eLoadCaseType.SteadyState => "Steady State",
        eLoadCaseType.PowerSpectralDensity => "Power Spectral Density",
        eLoadCaseType.LinearStaticMultiStep => "Linear Static Multi-Step",
        eLoadCaseType.HyperStatic => "Hyperstatic",
        _ => "Unknown"
    };

    /// <summary>
    /// Gets the design type option as a string.
    /// </summary>
    public string DesignTypeOptionString => DesignTypeOption switch
    {
        1 => "Strength",
        2 => "Serviceability",
        3 => "Other",
        _ => "Unknown"
    };

    /// <summary>
    /// Creates a linear static load case.
    /// </summary>
    public static LoadCase CreateLinearStatic(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.LinearStatic,
            SubType = 0,
            DesignType = eLoadPatternType.Dead,
            DesignTypeOption = 1
        };
    }

    /// <summary>
    /// Creates a nonlinear static load case.
    /// </summary>
    public static LoadCase CreateNonlinearStatic(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.NonlinearStatic,
            SubType = 0,
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 1
        };
    }

    /// <summary>
    /// Creates a modal eigen load case.
    /// </summary>
    public static LoadCase CreateModalEigen(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.Modal,
            SubType = 0, // Eigen
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 3
        };
    }

    /// <summary>
    /// Creates a modal ritz load case.
    /// </summary>
    public static LoadCase CreateModalRitz(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.Modal,
            SubType = 1, // Ritz
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 3
        };
    }

    /// <summary>
    /// Creates a response spectrum load case.
    /// </summary>
    public static LoadCase CreateResponseSpectrum(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.ResponseSpectrum,
            SubType = 0,
            DesignType = eLoadPatternType.Quake,
            DesignTypeOption = 1
        };
    }

    /// <summary>
    /// Creates a linear history load case.
    /// </summary>
    public static LoadCase CreateLinearHistory(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.LinearHistory,
            SubType = 0,
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 1
        };
    }

    /// <summary>
    /// Creates a nonlinear history load case.
    /// </summary>
    public static LoadCase CreateNonlinearHistory(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.NonlinearHistory,
            SubType = 0,
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 1
        };
    }

    /// <summary>
    /// Creates a buckling load case.
    /// </summary>
    public static LoadCase CreateBuckling(string name)
    {
        return new LoadCase
        {
            Name = name,
            CaseType = eLoadCaseType.Buckling,
            SubType = 0,
            DesignType = eLoadPatternType.Other,
            DesignTypeOption = 1
        };
    }

    public override string ToString()
    {
        string autoInfo = IsAuto ? " [Auto]" : "";
        return $"LoadCase: {Name} [{CaseTypeString}] - {DesignTypeOptionString}{autoInfo}";
    }
}
namespace EtabSharp.Design.Concrete.Codes.ACI318_14.Models;

/// <summary>
/// Enumeration of concrete design preference items for ACI 318-14.
/// Item numbers correspond to the ETABS API (1-18).
/// </summary>
public enum ConcretePreferenceItem
{
    /// <summary>
    /// Number of interaction curves.
    /// Value >= 4 and divisible by 4
    /// </summary>
    NumberOfInteractionCurves = 1,

    /// <summary>
    /// Number of interaction points.
    /// Value >= 5 and odd
    /// </summary>
    NumberOfInteractionPoints = 2,

    /// <summary>
    /// Consider minimum eccentricity.
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    ConsiderMinimumEccentricity = 3,

    /// <summary>
    /// Design for B/C Capacity Ratio?
    /// Value: 1=No, 2=Yes
    /// </summary>
    DesignForBCCapacityRatio = 4,

    /// <summary>
    /// Seismic Design Category.
    /// Value: 1=A, 2=B, 3=C, 4=D, 5=E, 6=F
    /// </summary>
    SeismicDesignCategory = 5,

    /// <summary>
    /// Design System Omega0.
    /// Value > 0
    /// </summary>
    DesignSystemOmega0 = 6,

    /// <summary>
    /// Design System Rho.
    /// Value > 0
    /// </summary>
    DesignSystemRho = 7,

    /// <summary>
    /// Design System Sds.
    /// Value >= 0
    /// </summary>
    DesignSystemSds = 8,

    /// <summary>
    /// Consider ICC_ESR 2017.
    /// Value: 1=No, 2=Yes
    /// </summary>
    ConsiderICCESR2017 = 9,

    /// <summary>
    /// Phi tension controlled.
    /// Value > 0
    /// </summary>
    PhiTensionControlled = 10,

    /// <summary>
    /// Phi compression controlled tied.
    /// Value > 0
    /// </summary>
    PhiCompressionControlledTied = 11,

    /// <summary>
    /// Phi compression controlled spiral.
    /// Value > 0
    /// </summary>
    PhiCompressionControlledSpiral = 12,

    /// <summary>
    /// Phi shear and/or torsion.
    /// Value > 0
    /// </summary>
    PhiShearAndTorsion = 13,

    /// <summary>
    /// Phi shear seismic.
    /// Value > 0
    /// </summary>
    PhiShearSeismic = 14,

    /// <summary>
    /// Phi joint shear.
    /// Value > 0
    /// </summary>
    PhiJointShear = 15,

    /// <summary>
    /// Pattern live load factor.
    /// Value >= 0
    /// </summary>
    PatternLiveLoadFactor = 16,

    /// <summary>
    /// Utilization factor limit.
    /// Value > 0
    /// </summary>
    UtilizationFactorLimit = 17,

    /// <summary>
    /// Multi-response case design.
    /// Value: 1=Envelopes, 2=Step-by-step, 3=Last step, 4=Envelopes-All, 5=Step-by-step-All
    /// </summary>
    MultiResponseCaseDesign = 18
}
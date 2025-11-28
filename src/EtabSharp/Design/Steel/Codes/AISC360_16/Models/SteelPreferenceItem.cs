namespace EtabSharp.Design.Steel.Codes.AISC360_16.Models;

/// <summary>
/// Enumeration of steel design preference items for AISC 360-16.
/// Item numbers correspond to the ETABS API (2-54).
/// </summary>
public enum SteelPreferenceItem
{
    /// <summary>
    /// Multi-Response Case Design.
    /// Value: 1=Envelopes, 2=Step-by-step, 3=Last step, 4=Envelopes--All, 5=Step-by-step--All
    /// </summary>
    MultiResponseCaseDesign = 2,

    /// <summary>
    /// Framing Type.
    /// Value: 1=SMF, 2=IMF, 3=OMF, 4=SCBF, 5=OCBF, 6=OCBFI, 7=EBF, 8=BRBF
    /// </summary>
    FramingType = 3,

    /// <summary>
    /// Seismic Design Category.
    /// Value: 1=A, 2=B, 3=C, 4=D, 5=E, 6=F
    /// </summary>
    SeismicDesignCategory = 4,

    /// <summary>
    /// Importance Factor I.
    /// Value > 0
    /// </summary>
    ImportanceFactorI = 5,

    /// <summary>
    /// Design System Rho.
    /// Value > 0
    /// </summary>
    DesignSystemRho = 6,

    /// <summary>
    /// Design System Sds.
    /// Value > 0
    /// </summary>
    DesignSystemSds = 7,

    /// <summary>
    /// Design System R.
    /// Value > 0
    /// </summary>
    DesignSystemR = 8,

    /// <summary>
    /// Design System Omega0.
    /// Value > 0
    /// </summary>
    DesignSystemOmega0 = 9,

    /// <summary>
    /// Design System Cd.
    /// Value > 0
    /// </summary>
    DesignSystemCd = 10,

    /// <summary>
    /// Design provision.
    /// Value: 1=LRFD, 2=ASD
    /// </summary>
    DesignProvision = 11,

    /// <summary>
    /// Analysis Method.
    /// Value: 1=Direct Analysis, 2=Effective Length, 3=Limited 1st Order
    /// </summary>
    AnalysisMethod = 12,

    /// <summary>
    /// Second Order Method.
    /// Value: 1=General 2nd Order, 2=Amplified 1st Order
    /// </summary>
    SecondOrderMethod = 13,

    /// <summary>
    /// Stiffness Reduction Method.
    /// Value: 1=Tau-b variable, 2=Tau-b Fixed, 3=No Modification
    /// </summary>
    StiffnessReductionMethod = 14,

    /// <summary>
    /// Add notional load cases into seismic combos?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    AddNotionalLoadCases = 15,

    /// <summary>
    /// BRB Beta Factor.
    /// Value > 0
    /// </summary>
    BRBBetaFactor = 16,

    /// <summary>
    /// BRB BetaOmega Factor.
    /// Value > 0
    /// </summary>
    BRBBetaOmegaFactor = 17,

    /// <summary>
    /// Phi (Bending).
    /// Value > 0
    /// </summary>
    PhiBending = 18,

    /// <summary>
    /// Phi (Compression).
    /// Value > 0
    /// </summary>
    PhiCompression = 19,

    /// <summary>
    /// Phi (Tension-Yielding).
    /// Value > 0
    /// </summary>
    PhiTensionYielding = 20,

    /// <summary>
    /// Phi (Tension-Fracture).
    /// Value > 0
    /// </summary>
    PhiTensionFracture = 21,

    /// <summary>
    /// Phi (Shear).
    /// Value > 0
    /// </summary>
    PhiShear = 22,

    /// <summary>
    /// Phi (Shear-Short Webbed Rolled I).
    /// Value > 0
    /// </summary>
    PhiShearShortWebbedRolledI = 23,

    /// <summary>
    /// Phi (Torsion).
    /// Value > 0
    /// </summary>
    PhiTorsion = 24,

    /// <summary>
    /// Ignore Seismic Code?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    IgnoreSeismicCode = 25,

    /// <summary>
    /// Ignore Special Seismic Load?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    IgnoreSpecialSeismicLoad = 26,

    /// <summary>
    /// Is Doubler Plate Plug-Welded?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    IsDoublerPlatePlugWelded = 27,

    /// <summary>
    /// HSS Welding Type.
    /// Value: 1=ERW, 2=SAW
    /// </summary>
    HSSWeldingType = 28,

    /// <summary>
    /// Reduce HSS Thickness?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    ReduceHSSThickness = 29,

    /// <summary>
    /// Consider Deflection?
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    ConsiderDeflection = 30,

    /// <summary>
    /// DL deflection limit, L/Value.
    /// Value > 0
    /// </summary>
    DLDeflectionLimit = 31,

    /// <summary>
    /// SDL + LL deflection limit, L/Value.
    /// Value > 0
    /// </summary>
    SDLPlusLLDeflectionLimit = 32,

    /// <summary>
    /// LL deflection limit, L/Value.
    /// Value > 0
    /// </summary>
    LLDeflectionLimit = 33,

    /// <summary>
    /// Total load deflection limit, L/Value.
    /// Value > 0
    /// </summary>
    TotalDeflectionLimit = 34,

    /// <summary>
    /// Total load deflection minus camber limit, L/Value.
    /// Value > 0
    /// </summary>
    TotalDeflectionMinusCamberLimit = 35,

    /// <summary>
    /// Pattern Live Load Factor.
    /// Value > 0
    /// </summary>
    PatternLiveLoadFactor = 36,

    /// <summary>
    /// Demand/Capacity Ratio Limit.
    /// Value > 0
    /// </summary>
    DemandCapacityRatioLimit = 37,

    /// <summary>
    /// Max Number of Auto Iterations.
    /// Value > 0
    /// </summary>
    MaxAutoIterations = 38,

    /// <summary>
    /// Omega (Bending).
    /// Value > 0
    /// </summary>
    OmegaBending = 39,

    /// <summary>
    /// Omega (Compression).
    /// Value > 0
    /// </summary>
    OmegaCompression = 40,

    /// <summary>
    /// Omega (Tension-Yielding).
    /// Value > 0
    /// </summary>
    OmegaTensionYielding = 41,

    /// <summary>
    /// Omega (Tension-Fracture).
    /// Value > 0
    /// </summary>
    OmegaTensionFracture = 42,

    /// <summary>
    /// Omega (Shear).
    /// Value > 0
    /// </summary>
    OmegaShear = 43,

    /// <summary>
    /// Omega (Shear-Short Webbed Rolled I).
    /// Value > 0
    /// </summary>
    OmegaShearShortWebbedRolledI = 44,

    /// <summary>
    /// Omega (Torsion).
    /// Value > 0
    /// </summary>
    OmegaTorsion = 45
}
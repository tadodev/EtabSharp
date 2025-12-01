namespace EtabSharp.Design.Steel.Codes.AISC360_16.Models;

/// <summary>
/// Enumeration of steel design overwrite items for AISC 360-16.
/// Item numbers correspond to the ETABS API (2-51).
/// </summary>
public enum SteelOverwriteItem
{
    /// <summary>
    /// Framing type (SMF, IMF, OMF, SCBF, OCBF, OCBFI, EBF, BRBF).
    /// Value: 0=Program Default, 1=SMF, 2=IMF, 3=OMF, 4=SCBF, 5=OCBF, 6=OCBFI, 7=EBF, 8=BRBF
    /// </summary>
    FramingType = 2,

    /// <summary>
    /// Omega0 - System overstrength factor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    Omega0 = 3,

    /// <summary>
    /// Connection type for moment frames.
    /// Value: 0=Program Default, 1=RBS, 2=BUEEP-4E, 3=BUEEP-8E, 4=BUEEP-4ES, 5=BUEEP-8ES, 6=BFP, 7=WUF-W, 8=DTMC, 9=Others
    /// </summary>
    ConnectionType = 4,

    /// <summary>
    /// Relative hinge distance left, Sh/L.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    RelativeHingeDistanceLeft = 5,

    /// <summary>
    /// Relative hinge distance right, Sh/L.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    RelativeHingeDistanceRight = 6,

    /// <summary>
    /// Relative Yc parameter, Yc/h.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    RelativeYcParameter = 7,

    /// <summary>
    /// BRB Beta Factor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    BRBBetaFactor = 8,

    /// <summary>
    /// BRB BetaOmega Factor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    BRBBetaOmegaFactor = 9,

    /// <summary>
    /// Perform RBS capacity design.
    /// Value: 0=Program Default, 1=No, 2=Yes
    /// </summary>
    PerformRBSCapacityDesign = 10,

    /// <summary>
    /// Consider deflection.
    /// Value: 0=Program Default, 1=No, 2=Yes
    /// </summary>
    ConsiderDeflection = 11,

    /// <summary>
    /// Deflection check type.
    /// Value: 0=Program Default, 1=Ratio, 2=Absolute, 3=Both
    /// </summary>
    DeflectionCheckType = 12,

    /// <summary>
    /// DL deflection limit, L/Value.
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    DLDeflectionLimitRatio = 13,

    /// <summary>
    /// SDL + LL deflection limit, L/Value.
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    SDLPlusLLDeflectionLimitRatio = 14,

    /// <summary>
    /// LL deflection limit, L/Value.
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    LLDeflectionLimitRatio = 15,

    /// <summary>
    /// Total load deflection limit, L/Value.
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    TotalDeflectionLimitRatio = 16,

    /// <summary>
    /// Total load deflection minus camber limit, L/Value.
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    TotalDeflectionMinusCamberLimitRatio = 17,

    /// <summary>
    /// DL deflection limit, absolute [L].
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    DLDeflectionLimitAbsolute = 18,

    /// <summary>
    /// SDL + LL deflection limit, absolute [L].
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    SDLPlusLLDeflectionLimitAbsolute = 19,

    /// <summary>
    /// LL deflection limit, absolute [L].
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    LLDeflectionLimitAbsolute = 20,

    /// <summary>
    /// Total load deflection limit, absolute [L].
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    TotalDeflectionLimitAbsolute = 21,

    /// <summary>
    /// Total load deflection minus camber limit, absolute [L].
    /// Value >= 0; 0 means no check for this item.
    /// </summary>
    TotalDeflectionMinusCamberLimitAbsolute = 22,

    /// <summary>
    /// Specified camber [L].
    /// Value >= 0.
    /// </summary>
    SpecifiedCamber = 23,

    /// <summary>
    /// Net area to total area ratio.
    /// Value >= 0; 0 means use program default value.
    /// </summary>
    NetAreaToTotalAreaRatio = 24,

    /// <summary>
    /// Live load reduction factor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    LiveLoadReductionFactor = 25,

    /// <summary>
    /// Unbraced length ratio, Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    UnbracedLengthRatioMajor = 26,

    /// <summary>
    /// Unbraced length ratio, Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    UnbracedLengthRatioMinor = 27,

    /// <summary>
    /// Unbraced length ratio, Lateral Torsional Buckling.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    UnbracedLengthRatioLTB = 28,

    /// <summary>
    /// Effective length factor, K1 Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorK1Major = 29,

    /// <summary>
    /// Effective length factor, K1 Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorK1Minor = 30,

    /// <summary>
    /// Effective length factor, K2 Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorK2Major = 31,

    /// <summary>
    /// Effective length factor, K2 Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorK2Minor = 32,

    /// <summary>
    /// Effective length factor, K Lateral Torsional Buckling.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorKLTB = 33,

    /// <summary>
    /// Moment coefficient, Cm Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MomentCoefficientCmMajor = 34,

    /// <summary>
    /// Moment coefficient, Cm Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MomentCoefficientCmMinor = 35,

    /// <summary>
    /// Bending coefficient, Cb.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    BendingCoefficientCb = 36,

    /// <summary>
    /// Nonsway moment factor, B1 Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    NonswayMomentFactorB1Major = 37,

    /// <summary>
    /// Nonsway moment factor, B1 Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    NonswayMomentFactorB1Minor = 38,

    /// <summary>
    /// Sway moment factor, B2 Major.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    SwayMomentFactorB2Major = 39,

    /// <summary>
    /// Sway moment factor, B2 Minor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    SwayMomentFactorB2Minor = 40,

    /// <summary>
    /// Reduce HSS thickness.
    /// Value: 0=Program Default, 1=No, 2=Yes
    /// </summary>
    ReduceHSSThickness = 41,

    /// <summary>
    /// HSS welding type.
    /// Value: 0=Program Default, 1=ERW, 2=SAW
    /// </summary>
    HSSWeldingType = 42,

    /// <summary>
    /// Yield stress, Fy [F/L^2].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    YieldStressFy = 43,

    /// <summary>
    /// Expected to specified Fy ratio, Ry.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    ExpectedFyRatioRy = 44,

    /// <summary>
    /// Compressive capacity, Pnc [F].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    CompressiveCapacityPnc = 45,

    /// <summary>
    /// Tensile capacity, Pnt [F].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    TensileCapacityPnt = 46,

    /// <summary>
    /// Major bending capacity, Mn3 [FL].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MajorBendingCapacityMn3 = 47,

    /// <summary>
    /// Minor bending capacity, Mn2 [FL].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MinorBendingCapacityMn2 = 48,

    /// <summary>
    /// Major shear capacity, Vn2 [F].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MajorShearCapacityVn2 = 49,

    /// <summary>
    /// Minor shear capacity, Vn3 [F].
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MinorShearCapacityVn3 = 50,

    /// <summary>
    /// Demand/capacity ratio limit.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    DemandCapacityRatioLimit = 51
}
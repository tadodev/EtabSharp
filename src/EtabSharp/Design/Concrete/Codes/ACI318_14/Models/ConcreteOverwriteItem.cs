namespace EtabSharp.Design.Concrete.Codes.ACI318_14.Models;

/// <summary>
/// Enumeration of concrete design overwrite items for ACI 318-14.
/// Item numbers correspond to the ETABS API (1-13).
/// </summary>
public enum ConcreteOverwriteItem
{
    /// <summary>
    /// Framing type.
    /// Value: 0=Program Default, 1=Sway Special, 2=Sway Intermediate, 3=Sway Ordinary, 4=Non-sway
    /// </summary>
    FramingType = 1,

    /// <summary>
    /// Live load reduction factor.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    LiveLoadReductionFactor = 2,

    /// <summary>
    /// Unbraced length ratio, Major axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    UnbracedLengthRatioMajor = 3,

    /// <summary>
    /// Unbraced length ratio, Minor axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    UnbracedLengthRatioMinor = 4,

    /// <summary>
    /// Effective length factor, K Major axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorKMajor = 5,

    /// <summary>
    /// Effective length factor, K Minor axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    EffectiveLengthFactorKMinor = 6,

    /// <summary>
    /// Moment coefficient, Cm Major axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MomentCoefficientCmMajor = 7,

    /// <summary>
    /// Moment coefficient, Cm Minor axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    MomentCoefficientCmMinor = 8,

    /// <summary>
    /// Nonsway moment factor, Db Major axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    NonswayMomentFactorDbMajor = 9,

    /// <summary>
    /// Nonsway moment factor, Db Minor axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    NonswayMomentFactorDbMinor = 10,

    /// <summary>
    /// Sway moment factor, Ds Major axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    SwayMomentFactorDsMajor = 11,

    /// <summary>
    /// Sway moment factor, Ds Minor axis.
    /// Value >= 0; 0 means use program determined value.
    /// </summary>
    SwayMomentFactorDsMinor = 12,

    /// <summary>
    /// Consider Minimum Eccentricity.
    /// Value: 0=No, Any other value=Yes
    /// </summary>
    ConsiderMinimumEccentricity = 13
}
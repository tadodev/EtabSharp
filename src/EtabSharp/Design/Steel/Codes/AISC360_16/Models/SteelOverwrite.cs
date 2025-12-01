namespace EtabSharp.Design.Steel.Codes.AISC360_16.Models;

/// <summary>
/// Represents a steel design overwrite for a frame element.
/// </summary>
public class SteelOverwrite
{
    /// <summary>
    /// Name of the frame object.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// The overwrite item being set/retrieved.
    /// </summary>
    public SteelOverwriteItem Item { get; set; }

    /// <summary>
    /// The value of the overwrite item.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Indicates if the value is program determined (true) or user-specified (false).
    /// </summary>
    public bool IsProgramDetermined { get; set; }

    /// <summary>
    /// Description of the overwrite item for display purposes.
    /// </summary>
    public string ItemDescription => GetItemDescription(Item);

    /// <inheritdoc />
    public override string ToString()
    {
        var source = IsProgramDetermined ? "Program" : "User";
        return $"{FrameName} - {ItemDescription}: {Value:F4} ({source})";
    }

    private static string GetItemDescription(SteelOverwriteItem item)
    {
        return item switch
        {
            SteelOverwriteItem.FramingType => "Framing Type",
            SteelOverwriteItem.Omega0 => "Omega0",
            SteelOverwriteItem.ConnectionType => "Connection Type",
            SteelOverwriteItem.RelativeHingeDistanceLeft => "Hinge Distance Left (Sh/L)",
            SteelOverwriteItem.RelativeHingeDistanceRight => "Hinge Distance Right (Sh/L)",
            SteelOverwriteItem.RelativeYcParameter => "Yc Parameter (Yc/h)",
            SteelOverwriteItem.BRBBetaFactor => "BRB Beta Factor",
            SteelOverwriteItem.BRBBetaOmegaFactor => "BRB BetaOmega Factor",
            SteelOverwriteItem.PerformRBSCapacityDesign => "Perform RBS Capacity Design",
            SteelOverwriteItem.ConsiderDeflection => "Consider Deflection",
            SteelOverwriteItem.DeflectionCheckType => "Deflection Check Type",
            SteelOverwriteItem.DLDeflectionLimitRatio => "DL Deflection Limit (L/Value)",
            SteelOverwriteItem.SDLPlusLLDeflectionLimitRatio => "SDL+LL Deflection Limit (L/Value)",
            SteelOverwriteItem.LLDeflectionLimitRatio => "LL Deflection Limit (L/Value)",
            SteelOverwriteItem.TotalDeflectionLimitRatio => "Total Deflection Limit (L/Value)",
            SteelOverwriteItem.TotalDeflectionMinusCamberLimitRatio => "Total-Camber Limit (L/Value)",
            SteelOverwriteItem.DLDeflectionLimitAbsolute => "DL Deflection Limit (Absolute)",
            SteelOverwriteItem.SDLPlusLLDeflectionLimitAbsolute => "SDL+LL Deflection Limit (Absolute)",
            SteelOverwriteItem.LLDeflectionLimitAbsolute => "LL Deflection Limit (Absolute)",
            SteelOverwriteItem.TotalDeflectionLimitAbsolute => "Total Deflection Limit (Absolute)",
            SteelOverwriteItem.TotalDeflectionMinusCamberLimitAbsolute => "Total-Camber Limit (Absolute)",
            SteelOverwriteItem.SpecifiedCamber => "Specified Camber",
            SteelOverwriteItem.NetAreaToTotalAreaRatio => "Net/Total Area Ratio",
            SteelOverwriteItem.LiveLoadReductionFactor => "Live Load Reduction Factor",
            SteelOverwriteItem.UnbracedLengthRatioMajor => "Unbraced Length Ratio (Major)",
            SteelOverwriteItem.UnbracedLengthRatioMinor => "Unbraced Length Ratio (Minor)",
            SteelOverwriteItem.UnbracedLengthRatioLTB => "Unbraced Length Ratio (LTB)",
            SteelOverwriteItem.EffectiveLengthFactorK1Major => "K1 Major",
            SteelOverwriteItem.EffectiveLengthFactorK1Minor => "K1 Minor",
            SteelOverwriteItem.EffectiveLengthFactorK2Major => "K2 Major",
            SteelOverwriteItem.EffectiveLengthFactorK2Minor => "K2 Minor",
            SteelOverwriteItem.EffectiveLengthFactorKLTB => "K LTB",
            SteelOverwriteItem.MomentCoefficientCmMajor => "Cm Major",
            SteelOverwriteItem.MomentCoefficientCmMinor => "Cm Minor",
            SteelOverwriteItem.BendingCoefficientCb => "Cb",
            SteelOverwriteItem.NonswayMomentFactorB1Major => "B1 Major",
            SteelOverwriteItem.NonswayMomentFactorB1Minor => "B1 Minor",
            SteelOverwriteItem.SwayMomentFactorB2Major => "B2 Major",
            SteelOverwriteItem.SwayMomentFactorB2Minor => "B2 Minor",
            SteelOverwriteItem.ReduceHSSThickness => "Reduce HSS Thickness",
            SteelOverwriteItem.HSSWeldingType => "HSS Welding Type",
            SteelOverwriteItem.YieldStressFy => "Yield Stress Fy",
            SteelOverwriteItem.ExpectedFyRatioRy => "Ry (Expected/Specified Fy)",
            SteelOverwriteItem.CompressiveCapacityPnc => "Compressive Capacity Pnc",
            SteelOverwriteItem.TensileCapacityPnt => "Tensile Capacity Pnt",
            SteelOverwriteItem.MajorBendingCapacityMn3 => "Major Bending Capacity Mn3",
            SteelOverwriteItem.MinorBendingCapacityMn2 => "Minor Bending Capacity Mn2",
            SteelOverwriteItem.MajorShearCapacityVn2 => "Major Shear Capacity Vn2",
            SteelOverwriteItem.MinorShearCapacityVn3 => "Minor Shear Capacity Vn3",
            SteelOverwriteItem.DemandCapacityRatioLimit => "Demand/Capacity Ratio Limit",
            _ => item.ToString()
        };
    }
}
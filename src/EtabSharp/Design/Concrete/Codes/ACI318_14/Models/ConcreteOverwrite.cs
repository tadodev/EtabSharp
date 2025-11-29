namespace EtabSharp.Design.Concrete.Codes.ACI318_14.Models;


/// <summary>
/// Represents a concrete design overwrite for a frame element.
/// </summary>
public class ConcreteOverwrite
{
    /// <summary>
    /// Name of the frame object.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// The overwrite item being set/retrieved.
    /// </summary>
    public ConcreteOverwriteItem Item { get; set; }

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

    private static string GetItemDescription(ConcreteOverwriteItem item)
    {
        return item switch
        {
            ConcreteOverwriteItem.FramingType => "Framing Type",
            ConcreteOverwriteItem.LiveLoadReductionFactor => "Live Load Reduction Factor",
            ConcreteOverwriteItem.UnbracedLengthRatioMajor => "Unbraced Length Ratio (Major)",
            ConcreteOverwriteItem.UnbracedLengthRatioMinor => "Unbraced Length Ratio (Minor)",
            ConcreteOverwriteItem.EffectiveLengthFactorKMajor => "K Major",
            ConcreteOverwriteItem.EffectiveLengthFactorKMinor => "K Minor",
            ConcreteOverwriteItem.MomentCoefficientCmMajor => "Cm Major",
            ConcreteOverwriteItem.MomentCoefficientCmMinor => "Cm Minor",
            ConcreteOverwriteItem.NonswayMomentFactorDbMajor => "Db Major",
            ConcreteOverwriteItem.NonswayMomentFactorDbMinor => "Db Minor",
            ConcreteOverwriteItem.SwayMomentFactorDsMajor => "Ds Major",
            ConcreteOverwriteItem.SwayMomentFactorDsMinor => "Ds Minor",
            ConcreteOverwriteItem.ConsiderMinimumEccentricity => "Consider Minimum Eccentricity",
            _ => item.ToString()
        };
    }
}
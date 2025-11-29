namespace EtabSharp.Design.Concrete.Codes.ACI318_14.Models;

/// <summary>
/// Represents a concrete design preference setting.
/// </summary>
public class ConcretePreference
{
    /// <summary>
    /// The preference item.
    /// </summary>
    public ConcretePreferenceItem Item { get; set; }

    /// <summary>
    /// The value of the preference.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Description of the preference item for display purposes.
    /// </summary>
    public string ItemDescription => GetItemDescription(Item);

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{ItemDescription}: {Value:F4}";
    }

    private static string GetItemDescription(ConcretePreferenceItem item)
    {
        return item switch
        {
            ConcretePreferenceItem.NumberOfInteractionCurves => "Number of Interaction Curves",
            ConcretePreferenceItem.NumberOfInteractionPoints => "Number of Interaction Points",
            ConcretePreferenceItem.ConsiderMinimumEccentricity => "Consider Minimum Eccentricity",
            ConcretePreferenceItem.DesignForBCCapacityRatio => "Design for B/C Capacity Ratio",
            ConcretePreferenceItem.SeismicDesignCategory => "Seismic Design Category",
            ConcretePreferenceItem.DesignSystemOmega0 => "Design System Omega0",
            ConcretePreferenceItem.DesignSystemRho => "Design System Rho",
            ConcretePreferenceItem.DesignSystemSds => "Design System Sds",
            ConcretePreferenceItem.ConsiderICCESR2017 => "Consider ICC_ESR 2017",
            ConcretePreferenceItem.PhiTensionControlled => "Phi (Tension Controlled)",
            ConcretePreferenceItem.PhiCompressionControlledTied => "Phi (Compression Controlled Tied)",
            ConcretePreferenceItem.PhiCompressionControlledSpiral => "Phi (Compression Controlled Spiral)",
            ConcretePreferenceItem.PhiShearAndTorsion => "Phi (Shear and/or Torsion)",
            ConcretePreferenceItem.PhiShearSeismic => "Phi (Shear Seismic)",
            ConcretePreferenceItem.PhiJointShear => "Phi (Joint Shear)",
            ConcretePreferenceItem.PatternLiveLoadFactor => "Pattern Live Load Factor",
            ConcretePreferenceItem.UtilizationFactorLimit => "Utilization Factor Limit",
            ConcretePreferenceItem.MultiResponseCaseDesign => "Multi-Response Case Design",
            _ => item.ToString()
        };
    }
}
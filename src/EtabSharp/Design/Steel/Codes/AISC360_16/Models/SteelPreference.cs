namespace EtabSharp.Design.Steel.Codes.AISC360_16.Models;

/// <summary>
/// Represents a steel design preference setting.
/// </summary>
public class SteelPreference
{
    /// <summary>
    /// The preference item.
    /// </summary>
    public SteelPreferenceItem Item { get; set; }

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

    private static string GetItemDescription(SteelPreferenceItem item)
    {
        return item switch
        {
            SteelPreferenceItem.MultiResponseCaseDesign => "Multi-Response Case Design",
            SteelPreferenceItem.FramingType => "Framing Type",
            SteelPreferenceItem.SeismicDesignCategory => "Seismic Design Category",
            SteelPreferenceItem.ImportanceFactorI => "Importance Factor I",
            SteelPreferenceItem.DesignSystemRho => "Design System Rho",
            SteelPreferenceItem.DesignSystemSds => "Design System Sds",
            SteelPreferenceItem.DesignSystemR => "Design System R",
            SteelPreferenceItem.DesignSystemOmega0 => "Design System Omega0",
            SteelPreferenceItem.DesignSystemCd => "Design System Cd",
            SteelPreferenceItem.DesignProvision => "Design Provision",
            SteelPreferenceItem.AnalysisMethod => "Analysis Method",
            SteelPreferenceItem.SecondOrderMethod => "Second Order Method",
            SteelPreferenceItem.StiffnessReductionMethod => "Stiffness Reduction Method",
            SteelPreferenceItem.AddNotionalLoadCases => "Add Notional Load Cases",
            SteelPreferenceItem.BRBBetaFactor => "BRB Beta Factor",
            SteelPreferenceItem.BRBBetaOmegaFactor => "BRB BetaOmega Factor",
            SteelPreferenceItem.PhiBending => "Phi (Bending)",
            SteelPreferenceItem.PhiCompression => "Phi (Compression)",
            SteelPreferenceItem.PhiTensionYielding => "Phi (Tension-Yielding)",
            SteelPreferenceItem.PhiTensionFracture => "Phi (Tension-Fracture)",
            SteelPreferenceItem.PhiShear => "Phi (Shear)",
            SteelPreferenceItem.PhiShearShortWebbedRolledI => "Phi (Shear-Short Webbed Rolled I)",
            SteelPreferenceItem.PhiTorsion => "Phi (Torsion)",
            SteelPreferenceItem.IgnoreSeismicCode => "Ignore Seismic Code",
            SteelPreferenceItem.IgnoreSpecialSeismicLoad => "Ignore Special Seismic Load",
            SteelPreferenceItem.IsDoublerPlatePlugWelded => "Doubler Plate Plug-Welded",
            SteelPreferenceItem.HSSWeldingType => "HSS Welding Type",
            SteelPreferenceItem.ReduceHSSThickness => "Reduce HSS Thickness",
            SteelPreferenceItem.ConsiderDeflection => "Consider Deflection",
            SteelPreferenceItem.DLDeflectionLimit => "DL Deflection Limit (L/Value)",
            SteelPreferenceItem.SDLPlusLLDeflectionLimit => "SDL+LL Deflection Limit (L/Value)",
            SteelPreferenceItem.LLDeflectionLimit => "LL Deflection Limit (L/Value)",
            SteelPreferenceItem.TotalDeflectionLimit => "Total Deflection Limit (L/Value)",
            SteelPreferenceItem.TotalDeflectionMinusCamberLimit => "Total-Camber Limit (L/Value)",
            SteelPreferenceItem.PatternLiveLoadFactor => "Pattern Live Load Factor",
            SteelPreferenceItem.DemandCapacityRatioLimit => "Demand/Capacity Ratio Limit",
            SteelPreferenceItem.MaxAutoIterations => "Max Auto Iterations",
            SteelPreferenceItem.OmegaBending => "Omega (Bending)",
            SteelPreferenceItem.OmegaCompression => "Omega (Compression)",
            SteelPreferenceItem.OmegaTensionYielding => "Omega (Tension-Yielding)",
            SteelPreferenceItem.OmegaTensionFracture => "Omega (Tension-Fracture)",
            SteelPreferenceItem.OmegaShear => "Omega (Shear)",
            SteelPreferenceItem.OmegaShearShortWebbedRolledI => "Omega (Shear-Short Webbed Rolled I)",
            SteelPreferenceItem.OmegaTorsion => "Omega (Torsion)",
            _ => item.ToString()
        };
    }
}
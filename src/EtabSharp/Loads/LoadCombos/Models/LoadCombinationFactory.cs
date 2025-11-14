namespace EtabSharp.Loads.LoadCombos.Models;

/// <summary>
/// Helper class for creating standard load combinations
/// </summary>
public static class LoadCombinationFactory
{
    /// <summary>
    /// Creates a basic linear combination
    /// </summary>
    public static LoadCombination CreateLinear(string name, params (string caseName, double factor)[] cases)
    {
        var combo = new LoadCombination
        {
            Name = name,
            ComboType = (int)LoadComboType.LinearAdd
        };

        foreach (var (caseName, factor) in cases)
        {
            combo.AddCase(LoadCombinationCase.FromLoadCase(caseName, factor));
        }

        return combo;
    }

    /// <summary>
    /// Creates an envelope combination
    /// </summary>
    public static LoadCombination CreateEnvelope(string name, params string[] caseNames)
    {
        var combo = new LoadCombination
        {
            Name = name,
            ComboType = (int)LoadComboType.Envelope
        };

        foreach (var caseName in caseNames)
        {
            combo.AddCase(LoadCombinationCase.FromLoadCase(caseName, 1.0));
        }

        return combo;
    }

    /// <summary>
    /// Creates a typical dead + live combination
    /// </summary>
    public static LoadCombination CreateDeadPlusLive(string name, double deadFactor = 1.0, double liveFactor = 1.0)
    {
        return CreateLinear(name, ("DEAD", deadFactor), ("LIVE", liveFactor));
    }

    /// <summary>
    /// Creates ACI 318 ultimate strength combinations
    /// </summary>
    public static List<LoadCombination> CreateACI318Ultimate()
    {
        return new List<LoadCombination>
        {
            CreateLinear("1.4D", ("DEAD", 1.4)),
            CreateLinear("1.2D+1.6L", ("DEAD", 1.2), ("LIVE", 1.6)),
            CreateLinear("1.2D+1.0L+1.0E", ("DEAD", 1.2), ("LIVE", 1.0), ("EARTHQUAKE", 1.0)),
            CreateLinear("0.9D+1.0E", ("DEAD", 0.9), ("EARTHQUAKE", 1.0))
        };
    }

    /// <summary>
    /// Creates ASCE 7 load combinations
    /// </summary>
    public static List<LoadCombination> CreateASCE7()
    {
        return new List<LoadCombination>
        {
            CreateLinear("1.4D", ("DEAD", 1.4)),
            CreateLinear("1.2D+1.6L+0.5Lr", ("DEAD", 1.2), ("LIVE", 1.6), ("LROOF", 0.5)),
            CreateLinear("1.2D+1.6Lr+L", ("DEAD", 1.2), ("LROOF", 1.6), ("LIVE", 1.0)),
            CreateLinear("1.2D+1.0W+L+0.5Lr", ("DEAD", 1.2), ("WIND", 1.0), ("LIVE", 1.0), ("LROOF", 0.5)),
            CreateLinear("1.2D+1.0E+L", ("DEAD", 1.2), ("EARTHQUAKE", 1.0), ("LIVE", 1.0)),
            CreateLinear("0.9D+1.0W", ("DEAD", 0.9), ("WIND", 1.0)),
            CreateLinear("0.9D+1.0E", ("DEAD", 0.9), ("EARTHQUAKE", 1.0))
        };
    }
}
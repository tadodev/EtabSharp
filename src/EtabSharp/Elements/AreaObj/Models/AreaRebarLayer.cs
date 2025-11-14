namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents a single rebar layer in an area element.
/// </summary>
public class AreaRebarLayer
{
    /// <summary>
    /// Layer identifier
    /// </summary>
    public string LayerID { get; set; } = "";

    /// <summary>
    /// Layer type (boundary, distributed, etc.)
    /// </summary>
    public int LayerType { get; set; }

    /// <summary>
    /// Clear cover to reinforcement
    /// </summary>
    public double ClearCover { get; set; }

    /// <summary>
    /// Bar size name (e.g., "#8", "25M", "H16")
    /// </summary>
    public string BarSize { get; set; } = "";

    /// <summary>
    /// Bar area
    /// </summary>
    public double BarArea { get; set; }

    /// <summary>
    /// Bar spacing
    /// </summary>
    public double BarSpacing { get; set; }

    /// <summary>
    /// Number of bars
    /// </summary>
    public int NumberOfBars { get; set; }

    /// <summary>
    /// Whether bars are confined
    /// </summary>
    public bool IsConfined { get; set; }

    public override string ToString()
    {
        return $"Layer {LayerID}: {NumberOfBars} × {BarSize} @ {BarSpacing:F2}";
    }
}
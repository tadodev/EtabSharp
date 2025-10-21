namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents lateral bracing data for a frame object.
/// </summary>
public class LateralBracingData
{
    /// <summary>
    /// Gets or sets the list of bracing entries.
    /// </summary>
    public List<LateralBracingEntry> Entries { get; set; } = new List<LateralBracingEntry>();

    /// <summary>
    /// Gets the number of bracing entries.
    /// </summary>
    public int Count => Entries.Count;

    /// <summary>
    /// Adds a bracing entry.
    /// </summary>
    public void AddEntry(LateralBracingEntry entry)
    {
        if (entry != null)
        {
            Entries.Add(entry);
        }
    }
}

/// <summary>
/// Represents a single lateral bracing entry.
/// </summary>
public class LateralBracingEntry
{
    /// <summary>
    /// Gets or sets the frame name associated with this bracing.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the bracing type.
    /// </summary>
    public BracingType BracingType { get; set; } = BracingType.Point;

    /// <summary>
    /// Gets or sets the bracing location.
    /// </summary>
    public BracingLocation Location { get; set; } = BracingLocation.Top;

    /// <summary>
    /// Gets or sets the relative distance 1 (start point for uniform or point location).
    /// </summary>
    public double RelativeDistance1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the relative distance 2 (end point for uniform bracing only).
    /// </summary>
    public double RelativeDistance2 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the actual distance 1 (start point for uniform or point location). [L]
    /// </summary>
    public double ActualDistance1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the actual distance 2 (end point for uniform bracing only). [L]
    /// </summary>
    public double ActualDistance2 { get; set; } = 0.0;

    /// <summary>
    /// Returns a string representation of the bracing entry.
    /// </summary>
    public override string ToString()
    {
        return $"Frame: {FrameName} | Type: {BracingType} | Location: {Location} | RD1: {RelativeDistance1:F3}";
    }
}

/// <summary>
/// Bracing type enumeration.
/// </summary>
public enum BracingType
{
    /// <summary>Point bracing</summary>
    Point = 1,
    /// <summary>Uniform bracing</summary>
    Uniform = 2
}

/// <summary>
/// Bracing location enumeration.
/// </summary>
public enum BracingLocation
{
    /// <summary>Top flange</summary>
    Top = 1,
    /// <summary>Bottom flange</summary>
    Bottom = 2,
    /// <summary>Both top and bottom</summary>
    All = 3
}

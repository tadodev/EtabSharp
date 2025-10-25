namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Represents a rectangular frame section (beam or column).
/// </summary>
public record PropFrameRectangle
{
    /// <summary>
    /// Section name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Material name
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Section depth (local 3-axis direction)
    /// </summary>
    public double Depth { get; set; }

    /// <summary>
    /// Section width (local 2-axis direction)
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Section color (-1 for auto)
    /// </summary>
    public int Color { get; set; } = -1;
}
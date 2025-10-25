namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Represents a circular frame section (typically columns).
/// </summary>
public record PropFrameCircle
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
    /// Outside diameter
    /// </summary>
    public double Diameter { get; set; }

    /// <summary>
    /// Section color (-1 for auto)
    /// </summary>
    public int Color { get; set; } = -1;
}
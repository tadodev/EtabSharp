namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents lateral bracing assignment for a frame.
/// </summary>
public class FrameLateralBracing
{
    /// <summary>
    /// Bracing type
    /// </summary>
    public int BracingType { get; set; }

    /// <summary>
    /// Location type
    /// </summary>
    public int Location { get; set; }

    /// <summary>
    /// Start distance (relative or absolute)
    /// </summary>
    public double Distance1 { get; set; }

    /// <summary>
    /// End distance (relative or absolute)
    /// </summary>
    public double Distance2 { get; set; }

    /// <summary>
    /// Whether distances are relative (0-1)
    /// </summary>
    public bool IsRelativeDistance { get; set; } = true;

    public override string ToString()
    {
        return $"Bracing Type {BracingType} from {Distance1:F2} to {Distance2:F2}";
    }
}
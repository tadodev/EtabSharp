namespace EtabSharp.AnalysisResultsSetup.Models;

/// <summary>
/// Represents base reaction location settings.
/// </summary>
public class BaseReactionLocation
{
    /// <summary>
    /// Gets or sets the global X coordinate for base reactions.
    /// </summary>
    public double GlobalX { get; set; }

    /// <summary>
    /// Gets or sets the global Y coordinate for base reactions.
    /// </summary>
    public double GlobalY { get; set; }

    /// <summary>
    /// Gets or sets the global Z coordinate for base reactions.
    /// </summary>
    public double GlobalZ { get; set; }

    public BaseReactionLocation()
    {
    }

    public BaseReactionLocation(double gx, double gy, double gz)
    {
        GlobalX = gx;
        GlobalY = gy;
        GlobalZ = gz;
    }

    /// <summary>
    /// Creates a base reaction location at the origin.
    /// </summary>
    public static BaseReactionLocation AtOrigin() => new BaseReactionLocation(0, 0, 0);

    public override string ToString()
    {
        return $"Base Reaction Location: ({GlobalX:F3}, {GlobalY:F3}, {GlobalZ:F3})";
    }
}
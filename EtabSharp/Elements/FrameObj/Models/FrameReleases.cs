namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents end release conditions for a frame (hinges/pins).
/// </summary>
public class FrameReleases
{
    /// <summary>
    /// Gets or sets the I-end (start) releases.
    /// </summary>
    public FrameEndReleases IEndReleases { get; set; } = new FrameEndReleases();

    /// <summary>
    /// Gets or sets the J-end releases.
    /// </summary>
    public FrameEndReleases JEndReleases { get; set; } = new FrameEndReleases();

    /// <summary>
    /// Gets or sets the I-end partial fixity values.
    /// Only applies to released degrees of freedom.
    /// </summary>
    public FrameEndPartialFixity IEndPartialFixity { get; set; } = new FrameEndPartialFixity();

    /// <summary>
    /// Gets or sets the J-end partial fixity values.
    /// Only applies to released degrees of freedom.
    /// </summary>
    public FrameEndPartialFixity JEndPartialFixity { get; set; } = new FrameEndPartialFixity();

    /// <summary>
    /// Validates the releases to ensure they don't cause instability.
    /// </summary>
    /// <returns>True if valid, false if would cause instability</returns>
    public bool IsValid()
    {
        // Check for unstable release combinations
        // Both ends released for any translation or R1 is unstable
        if (IEndReleases.U1 && JEndReleases.U1) return false;
        if (IEndReleases.U2 && JEndReleases.U2) return false;
        if (IEndReleases.U3 && JEndReleases.U3) return false;
        if (IEndReleases.R1 && JEndReleases.R1) return false;

        // R2 released at both ends with U3 at either end
        if (IEndReleases.R2 && JEndReleases.R2 && (IEndReleases.U3 || JEndReleases.U3)) return false;

        // R3 released at both ends with U2 at either end
        if (IEndReleases.R3 && JEndReleases.R3 && (IEndReleases.U2 || JEndReleases.U2)) return false;

        return true;
    }
}
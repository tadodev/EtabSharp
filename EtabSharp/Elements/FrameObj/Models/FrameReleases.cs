namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents end release conditions for a frame (hinges/pins).
/// </summary>
public class FrameReleases
{
    /// <summary>
    /// I-end (start) releases [P, V2, V3, T, M2, M3]
    /// </summary>
    public FrameEndRelease IEnd { get; set; } = new();

    /// <summary>
    /// J-end (end) releases [P, V2, V3, T, M2, M3]
    /// </summary>
    public FrameEndRelease JEnd { get; set; } = new();

    /// <summary>
    /// Creates fixed-fixed frame (no releases)
    /// </summary>
    public static FrameReleases FixedFixed() => new();

    /// <summary>
    /// Creates pinned-pinned frame (M2, M3 released at both ends)
    /// </summary>
    public static FrameReleases PinnedPinned() => new()
    {
        IEnd = FrameEndRelease.Pinned(),
        JEnd = FrameEndRelease.Pinned()
    };

    /// <summary>
    /// Creates fixed-pinned frame
    /// </summary>
    public static FrameReleases FixedPinned() => new()
    {
        IEnd = FrameEndRelease.Fixed(),
        JEnd = FrameEndRelease.Pinned()
    };

    public override string ToString()
    {
        return $"I-end: {IEnd}, J-end: {JEnd}";
    }
}
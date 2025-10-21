namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents tension/compression force limits for a frame.
/// Used for tension-only cables or compression-only struts.
/// </summary>
public class FrameTCLimits
{
    /// <summary>
    /// Whether compression limit exists
    /// </summary>
    public bool CompressionLimitExists { get; set; }

    /// <summary>
    /// Compression force limit (negative value)
    /// </summary>
    public double CompressionLimit { get; set; }

    /// <summary>
    /// Whether tension limit exists
    /// </summary>
    public bool TensionLimitExists { get; set; }

    /// <summary>
    /// Tension force limit (positive value)
    /// </summary>
    public double TensionLimit { get; set; }

    /// <summary>
    /// Creates tension-only member (cable)
    /// </summary>
    public static FrameTCLimits TensionOnly() => new()
    {
        CompressionLimitExists = true,
        CompressionLimit = 0,
        TensionLimitExists = false
    };

    /// <summary>
    /// Creates compression-only member (strut)
    /// </summary>
    public static FrameTCLimits CompressionOnly() => new()
    {
        CompressionLimitExists = false,
        TensionLimitExists = true,
        TensionLimit = 0
    };

    /// <summary>
    /// No limits (normal frame)
    /// </summary>
    public static FrameTCLimits None() => new();

    public override string ToString()
    {
        if (CompressionLimitExists && CompressionLimit == 0)
            return "Tension-Only";
        if (TensionLimitExists && TensionLimit == 0)
            return "Compression-Only";
        return "No Limits";
    }
}
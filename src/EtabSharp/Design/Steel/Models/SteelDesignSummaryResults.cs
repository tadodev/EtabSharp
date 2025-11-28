using ETABSv1;

namespace EtabSharp.Design.Steel.Models;

/// <summary>
/// Collection of steel design summary results.
/// </summary>
public class SteelDesignSummaryResults
{
    /// <summary>
    /// Number of results returned.
    /// </summary>
    public int NumberResults { get; set; }

    /// <summary>
    /// List of individual design result records.
    /// </summary>
    public List<SteelDesignSummaryResult> Results { get; set; } = new();

    /// <summary>
    /// Indicates if the operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Error code (0 = success).
    /// </summary>
    public int ReturnCode { get; set; }

    /// <summary>
    /// Error message if operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets results for a specific frame.
    /// </summary>
    public List<SteelDesignSummaryResult> GetForFrame(string frameName)
    {
        return Results.Where(r => r.FrameName.Equals(frameName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets results for a specific frame type.
    /// </summary>
    public List<SteelDesignSummaryResult> GetForFrameType(eFrameDesignOrientation frameType)
    {
        return Results.Where(r => r.FrameType == frameType).ToList();
    }

    /// <summary>
    /// Gets only passing results.
    /// </summary>
    public List<SteelDesignSummaryResult> GetPassing()
    {
        return Results.Where(r => r.Passes).ToList();
    }

    /// <summary>
    /// Gets only failing results.
    /// </summary>
    public List<SteelDesignSummaryResult> GetFailing()
    {
        return Results.Where(r => !r.Passes).ToList();
    }

    /// <summary>
    /// Gets results with ratio exceeding specified threshold.
    /// </summary>
    public List<SteelDesignSummaryResult> GetExceedingRatio(double threshold = 1.0)
    {
        return Results.Where(r => r.ControllingRatio > threshold).ToList();
    }

    /// <summary>
    /// Gets the maximum ratio among all results.
    /// </summary>
    public double MaxRatio => Results.Any() ? Results.Max(r => r.ControllingRatio) : 0.0;

    /// <summary>
    /// Gets the frame with the maximum ratio.
    /// </summary>
    public SteelDesignSummaryResult? GetMaxRatioFrame()
    {
        return Results.OrderByDescending(r => r.ControllingRatio).FirstOrDefault();
    }

    /// <summary>
    /// Gets a summary of pass/fail counts.
    /// </summary>
    public (int Passing, int Failing, int Total) GetPassFailSummary()
    {
        var passing = Results.Count(r => r.Passes);
        var failing = Results.Count(r => !r.Passes);
        return (passing, failing, Results.Count);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var (passing, failing, total) = GetPassFailSummary();
        return $"Steel Design Results: {total} frames | Pass: {passing} | Fail: {failing} | Max Ratio: {MaxRatio:F3}";
    }
}
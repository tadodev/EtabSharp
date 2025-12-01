namespace EtabSharp.Design.Concrete.Models;

/// <summary>
/// Collection of concrete joint design results.
/// </summary>
public class ConcreteJointDesignResults
{
    /// <summary>
    /// Number of results returned.
    /// </summary>
    public int NumberResults { get; set; }

    /// <summary>
    /// List of individual joint design result records.
    /// </summary>
    public List<ConcreteJointDesignResult> Results { get; set; } = new();

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
    public List<ConcreteJointDesignResult> GetForFrame(string frameName)
    {
        return Results.Where(r => r.FrameName.Equals(frameName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets only passing results.
    /// </summary>
    public List<ConcreteJointDesignResult> GetPassing()
    {
        return Results.Where(r => r.Passes).ToList();
    }

    /// <summary>
    /// Gets only failing results.
    /// </summary>
    public List<ConcreteJointDesignResult> GetFailing()
    {
        return Results.Where(r => !r.Passes).ToList();
    }

    /// <summary>
    /// Gets results with ratio exceeding specified threshold.
    /// </summary>
    public List<ConcreteJointDesignResult> GetExceedingRatio(double threshold = 1.0)
    {
        return Results.Where(r => r.ControllingRatio > threshold).ToList();
    }

    /// <summary>
    /// Gets the maximum ratio among all results.
    /// </summary>
    public double MaxRatio => Results.Any() ? Results.Max(r => r.ControllingRatio) : 0.0;

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
        return $"Concrete Joint Results: {total} frames | Pass: {passing} | Fail: {failing} | Max Ratio: {MaxRatio:F3}";
    }
}
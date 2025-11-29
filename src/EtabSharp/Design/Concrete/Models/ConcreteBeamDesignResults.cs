namespace EtabSharp.Design.Concrete.Models;

/// <summary>
/// Collection of concrete beam design results.
/// </summary>
public class ConcreteBeamDesignResults
{
    /// <summary>
    /// Number of results returned.
    /// </summary>
    public int NumberResults { get; set; }

    /// <summary>
    /// List of individual beam design result records.
    /// </summary>
    public List<ConcreteBeamDesignResult> Results { get; set; } = new();

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
    public List<ConcreteBeamDesignResult> GetForFrame(string frameName)
    {
        return Results.Where(r => r.FrameName.Equals(frameName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets only passing results (no errors).
    /// </summary>
    public List<ConcreteBeamDesignResult> GetPassing()
    {
        return Results.Where(r => r.Passes).ToList();
    }

    /// <summary>
    /// Gets only failing results (with errors).
    /// </summary>
    public List<ConcreteBeamDesignResult> GetFailing()
    {
        return Results.Where(r => !r.Passes).ToList();
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
        return $"Concrete Beam Results: {total} locations | Pass: {passing} | Fail: {failing}";
    }
}
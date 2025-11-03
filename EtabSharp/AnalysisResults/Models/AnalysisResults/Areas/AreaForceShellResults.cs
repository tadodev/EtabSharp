namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

/// <summary>
/// Collection of area force shell results.
/// </summary>
public class AreaForceShellResults
{
    /// <summary>
    /// Number of results returned.
    /// </summary>
    public int NumberResults { get; set; }

    /// <summary>
    /// List of individual result records.
    /// </summary>
    public List<AreaForceShellResult> Results { get; set; } = new();

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

    public override string ToString()
    {
        return $"Area Force Shell Results: {NumberResults} records | Success: {IsSuccess}";
    }
}
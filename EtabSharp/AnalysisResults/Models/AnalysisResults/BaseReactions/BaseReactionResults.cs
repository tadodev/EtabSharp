namespace EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;

public class BaseReactionResults
{
    public int NumberResults { get; set; }
    public List<BaseReactionResult> Results { get; set; } = new();

    /// <summary>
    /// Global X coordinate of base reaction location.
    /// </summary>
    public double GlobalX { get; set; }

    /// <summary>
    /// Global Y coordinate of base reaction location.
    /// </summary>
    public double GlobalY { get; set; }

    /// <summary>
    /// Global Z coordinate of base reaction location.
    /// </summary>
    public double GlobalZ { get; set; }

    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
namespace EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;

public class BaseReactionWithCentroidResults
{
    public int NumberResults { get; set; }
    public List<BaseReactionWithCentroidResult> Results { get; set; } = new();
    public double GlobalX { get; set; }
    public double GlobalY { get; set; }
    public double GlobalZ { get; set; }
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
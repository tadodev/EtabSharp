namespace EtabSharp.AnalysisResults.Models.AnalysisResults.AssembledMass;

public class AssembledJointMassResults
{
    public int NumberResults { get; set; }
    public List<AssembledJointMassResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
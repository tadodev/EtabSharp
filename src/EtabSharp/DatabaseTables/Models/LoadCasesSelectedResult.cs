namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from load cases selected for display.
/// </summary>
public class LoadCasesSelectedResult
{
    public int NumberSelected { get; set; }
    public List<string> LoadCaseNames { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from load patterns selected for display.
/// </summary>
public class LoadPatternsSelectedResult
{
    public int NumberSelected { get; set; }
    public List<string> LoadPatternNames { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
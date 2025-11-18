namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from GetOutputOptionsForDisplay.
/// </summary>
public class OutputOptionsForDisplayResult
{
    public OutputOptionsForDisplay Options { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from load combinations selected for display.
/// </summary>
public class LoadCombinationsSelectedResult
{
    public int NumberSelected { get; set; }
    public List<string> LoadComboNames { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
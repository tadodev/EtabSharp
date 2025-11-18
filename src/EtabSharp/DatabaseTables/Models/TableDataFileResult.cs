namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from table data operations with files.
/// </summary>
public class TableDataFileResult
{
    public string TableKey { get; set; } = string.Empty;
    public int TableVersion { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
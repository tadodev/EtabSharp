namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from table data operations returning CSV.
/// </summary>
public class TableDataCsvResult
{
    public string TableKey { get; set; } = string.Empty;
    public int TableVersion { get; set; }
    public List<string> FieldKeysIncluded { get; set; } = new();
    public string CsvData { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}

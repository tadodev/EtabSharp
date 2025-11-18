namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from GetAllTables.
/// </summary>
public class AllTablesResult
{
    public int NumberOfTables { get; set; }
    public List<DatabaseTableInfo> Tables { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Represents metadata for a single database table.
/// </summary>
public class DatabaseTableInfo
{
    /// <summary>
    /// Unique key identifying the table.
    /// </summary>
    public string TableKey { get; set; } = string.Empty;

    /// <summary>
    /// Display name of the table.
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// Import type indicator.
    /// </summary>
    public TableImportType ImportType { get; set; }

    /// <summary>
    /// Indicates if the table is empty (only available in GetAllTables).
    /// </summary>
    public bool IsEmpty { get; set; }

    public override string ToString()
    {
        return $"{TableName} ({TableKey}) - {ImportType}";
    }
}
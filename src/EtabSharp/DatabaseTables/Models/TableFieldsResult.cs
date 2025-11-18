namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from GetAllFieldsInTable.
/// </summary>
public class TableFieldsResult
{
    public string TableKey { get; set; } = string.Empty;
    public int TableVersion { get; set; }
    public int NumberOfFields { get; set; }
    public List<TableFieldInfo> Fields { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets only the importable (editable) fields.
    /// </summary>
    public List<TableFieldInfo> ImportableFields => Fields.Where(f => f.IsImportable).ToList();
}
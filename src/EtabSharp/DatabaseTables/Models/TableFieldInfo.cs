namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Represents metadata for a single field (column) in a table.
/// </summary>
public class TableFieldInfo
{
    /// <summary>
    /// Unique key identifying the field.
    /// </summary>
    public string FieldKey { get; set; } = string.Empty;

    /// <summary>
    /// Display name of the field.
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Description of the field.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Units string for the field (empty if dimensionless).
    /// </summary>
    public string UnitsString { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the field can be imported/edited.
    /// </summary>
    public bool IsImportable { get; set; }

    public override string ToString()
    {
        return $"{FieldName} ({FieldKey}) - {UnitsString} - Importable: {IsImportable}";
    }
}
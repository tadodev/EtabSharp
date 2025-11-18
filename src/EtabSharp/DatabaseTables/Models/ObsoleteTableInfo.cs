namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Represents an obsolete table with migration information.
/// </summary>
public class ObsoleteTableInfo
{
    public string TableKey { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{TableKey}: {Notes}";
    }
}

/// <summary>
/// Result from GetObsoleteTableKeyList.
/// </summary>
public class ObsoleteTablesResult
{
    public int NumberOfTables { get; set; }
    public List<ObsoleteTableInfo> ObsoleteTables { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
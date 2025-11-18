namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from table data operations returning XML.
/// </summary>
public class TableDataXmlResult
{
    public string TableKey { get; set; } = string.Empty;
    public int TableVersion { get; set; }
    public List<string> FieldKeysIncluded { get; set; } = new();
    public string XmlData { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}
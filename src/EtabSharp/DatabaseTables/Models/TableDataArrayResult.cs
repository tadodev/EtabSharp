namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from table data operations returning arrays.
/// </summary>
public class TableDataArrayResult
{
    public string TableKey { get; set; } = string.Empty;
    public int TableVersion { get; set; }
    public List<string> FieldKeysIncluded { get; set; } = new();
    public int NumberOfRecords { get; set; }

    /// <summary>
    /// Table data as flat string array.
    /// Data is organized row-by-row with columns concatenated.
    /// </summary>
    public List<string> TableData { get; set; } = new();

    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets table data as structured rows.
    /// </summary>
    public List<Dictionary<string, string>> GetStructuredData()
    {
        var rows = new List<Dictionary<string, string>>();
        int numFields = FieldKeysIncluded.Count;

        if (numFields == 0 || TableData.Count == 0)
            return rows;

        for (int i = 0; i < NumberOfRecords; i++)
        {
            var row = new Dictionary<string, string>();
            for (int j = 0; j < numFields; j++)
            {
                int index = i * numFields + j;
                if (index < TableData.Count)
                {
                    row[FieldKeysIncluded[j]] = TableData[index];
                }
            }
            rows.Add(row);
        }

        return rows;
    }

    /// <summary>
    /// Sets table data from structured rows.
    /// </summary>
    public void SetStructuredData(List<Dictionary<string, string>> rows)
    {
        TableData.Clear();
        NumberOfRecords = rows.Count;

        foreach (var row in rows)
        {
            foreach (var fieldKey in FieldKeysIncluded)
            {
                TableData.Add(row.ContainsKey(fieldKey) ? row[fieldKey] : string.Empty);
            }
        }
    }
}
namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Result from applying edited tables to the model.
/// </summary>
public class ApplyEditedTablesResult
{
    public int NumFatalErrors { get; set; }
    public int NumErrorMsgs { get; set; }
    public int NumWarnMsgs { get; set; }
    public int NumInfoMsgs { get; set; }
    public string ImportLog { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Indicates if there were any errors during import.
    /// </summary>
    public bool HasErrors => NumFatalErrors > 0 || NumErrorMsgs > 0;

    /// <summary>
    /// Indicates if there were any warnings during import.
    /// </summary>
    public bool HasWarnings => NumWarnMsgs > 0;

    public override string ToString()
    {
        return $"Apply Result: Errors={NumFatalErrors + NumErrorMsgs}, Warnings={NumWarnMsgs}, Info={NumInfoMsgs}";
    }
}
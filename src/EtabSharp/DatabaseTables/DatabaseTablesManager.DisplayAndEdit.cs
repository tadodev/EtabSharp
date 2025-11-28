using EtabSharp.DatabaseTables.Models;
using EtabSharp.Exceptions;
using Microsoft.Extensions.Logging;

namespace EtabSharp.DatabaseTables;

/// <summary>
/// Partial class for display options and table get/set operations.
/// </summary>
public partial class DatabaseTablesManager
{
    #region Set Display Options

    /// <summary>
    /// Gets table data for display as XML string.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayXMLString.
    /// </summary>
    public TableDataXmlResult GetTableForDisplayXml(string tableKey, string[]? fieldKeys = null, string groupName = "",
        bool includeSchema = false)
    {
        var result = new TableDataXmlResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            string[] fieldKeyList = fieldKeys ?? new[] { "" };
            int tableVersion = 0;
            string xmlString = string.Empty;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayXMLString(
                tableKey,
                ref fieldKeyList,
                groupName ?? "",
                includeSchema,
                ref tableVersion,
                ref xmlString);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get table '{tableKey}' as XML. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForDisplayXMLString", result.ErrorMessage);
            }

            result.FieldKeysIncluded = fieldKeyList.Where(k => !string.IsNullOrEmpty(k)).ToList();
            result.XmlData = xmlString;
            result.IsSuccess = true;

            _logger.LogDebug("Retrieved table '{TableKey}' as XML for display", tableKey);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting table as XML: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    #endregion

    #region Get Table for Editing

    /// <summary>
    /// Gets table data for editing as array.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingArray.
    /// </summary>
    public TableDataArrayResult GetTableForEditingArray(string tableKey, string groupName = "")
    {
        var result = new TableDataArrayResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            int tableVersion = 0;
            string[] fieldsKeysIncluded = Array.Empty<string>();
            int numberRecords = 0;
            string[] tableData = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetTableForEditingArray(
                tableKey,
                groupName ?? "",
                ref tableVersion,
                ref fieldsKeysIncluded,
                ref numberRecords,
                ref tableData);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;
            result.NumberOfRecords = numberRecords;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get table '{tableKey}' for editing. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForEditingArray", result.ErrorMessage);
            }

            result.FieldKeysIncluded = fieldsKeysIncluded.ToList();
            result.TableData = tableData.ToList();
            result.IsSuccess = true;

            _logger.LogDebug("Retrieved table '{TableKey}' for editing: {Records} records, {Fields} fields",
                tableKey, numberRecords, fieldsKeysIncluded.Length);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting table for editing: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets table data for editing as CSV string.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingCSVString.
    /// </summary>
    public TableDataCsvResult GetTableForEditingCsv(string tableKey, string groupName = "", string separator = ",")
    {
        var result = new TableDataCsvResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            int tableVersion = 0;
            string csvString = string.Empty;

            int ret = _sapModel.DatabaseTables.GetTableForEditingCSVString(
                tableKey,
                groupName ?? "",
                ref tableVersion,
                ref csvString,
                separator);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get table '{tableKey}' as CSV for editing. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForEditingCSVString", result.ErrorMessage);
            }

            result.CsvData = csvString;
            result.IsSuccess = true;

            _logger.LogDebug("Retrieved table '{TableKey}' as CSV for editing", tableKey);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting table as CSV for editing: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets table data for editing and saves to CSV file.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingCSVFile.
    /// </summary>
    public TableDataFileResult GetTableForEditingCsvFile(string tableKey, string csvFilePath, string groupName = "",
        string separator = ",")
    {
        var result = new TableDataFileResult { TableKey = tableKey, FilePath = csvFilePath };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (string.IsNullOrEmpty(csvFilePath))
                throw new ArgumentException("CSV file path cannot be null or empty", nameof(csvFilePath));

            int tableVersion = 0;

            int ret = _sapModel.DatabaseTables.GetTableForEditingCSVFile(
                tableKey,
                groupName ?? "",
                ref tableVersion,
                csvFilePath,
                separator);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to save table '{tableKey}' to CSV file for editing. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForEditingCSVFile", result.ErrorMessage);
            }

            result.IsSuccess = true;
            _logger.LogDebug("Saved table '{TableKey}' to CSV file for editing: {FilePath}", tableKey, csvFilePath);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error saving table to CSV file for editing: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    #endregion

    #region Set Table for Editing

    /// <summary>
    /// Sets table data for editing from array.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingArray.
    /// </summary>
    public int SetTableForEditingArray(string tableKey, int tableVersion, string[] fieldKeys, string[] tableData)
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (fieldKeys == null || fieldKeys.Length == 0)
                throw new ArgumentException("Field keys array cannot be null or empty", nameof(fieldKeys));
            if (tableData == null)
                throw new ArgumentNullException(nameof(tableData));

            // Calculate number of records
            int numberRecords = fieldKeys.Length > 0 ? tableData.Length / fieldKeys.Length : 0;

            int version = tableVersion;
            int ret = _sapModel.DatabaseTables.SetTableForEditingArray(
                tableKey,
                ref version,
                ref fieldKeys,
                numberRecords,
                ref tableData);

            if (ret != 0)
                throw new EtabsException(ret, "SetTableForEditingArray",
                    $"Failed to set table '{tableKey}' for editing. Return code: {ret}");

            _logger.LogDebug("Set table '{TableKey}' for editing: {Records} records, {Fields} fields",
                tableKey, numberRecords, fieldKeys.Length);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting table for editing: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets table data for editing from CSV string.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingCSVString.
    /// </summary>
    public int SetTableForEditingCsv(string tableKey, int tableVersion, string csvString, string separator = ",")
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (csvString == null)
                throw new ArgumentNullException(nameof(csvString));

            int version = tableVersion;
            string csv = csvString;
            int ret = _sapModel.DatabaseTables.SetTableForEditingCSVString(
                tableKey,
                ref version,
                ref csv,
                separator);

            if (ret != 0)
                throw new EtabsException(ret, "SetTableForEditingCSVString",
                    $"Failed to set table '{tableKey}' for editing from CSV. Return code: {ret}");

            _logger.LogDebug("Set table '{TableKey}' for editing from CSV string", tableKey);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting table from CSV for editing: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets table data for editing from CSV file.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingCSVFile.
    /// </summary>
    public int SetTableForEditingCsvFile(string tableKey, int tableVersion, string csvFilePath, string separator = ",")
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (string.IsNullOrEmpty(csvFilePath))
                throw new ArgumentException("CSV file path cannot be null or empty", nameof(csvFilePath));

            int version = tableVersion;
            int ret = _sapModel.DatabaseTables.SetTableForEditingCSVFile(
                tableKey,
                ref version,
                csvFilePath,
                separator);

            if (ret != 0)
                throw new EtabsException(ret, "SetTableForEditingCSVFile",
                    $"Failed to set table '{tableKey}' for editing from CSV file. Return code: {ret}");

            _logger.LogDebug("Set table '{TableKey}' for editing from CSV file: {FilePath}", tableKey, csvFilePath);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting table from CSV file for editing: {ex.Message}", ex);
        }
    }

    #endregion

    #region Apply and Cancel Editing

    /// <summary>
    /// Applies all queued edited tables to the model.
    /// This is when changes actually take effect.
    /// IMPORTANT: Save your model before calling this function.
    /// Wraps cSapModel.DatabaseTables.ApplyEditedTables.
    /// </summary>
    public ApplyEditedTablesResult ApplyEditedTables(bool fillImportLog = true)
    {
        var result = new ApplyEditedTablesResult();

        try
        {
            int numFatalErrors = 0;
            int numErrorMsgs = 0;
            int numWarnMsgs = 0;
            int numInfoMsgs = 0;
            string importLog = string.Empty;

            _logger.LogInformation("Applying edited tables to model...");

            int ret = _sapModel.DatabaseTables.ApplyEditedTables(
                fillImportLog,
                ref numFatalErrors,
                ref numErrorMsgs,
                ref numWarnMsgs,
                ref numInfoMsgs,
                ref importLog);

            result.ReturnCode = ret;
            result.NumFatalErrors = numFatalErrors;
            result.NumErrorMsgs = numErrorMsgs;
            result.NumWarnMsgs = numWarnMsgs;
            result.NumInfoMsgs = numInfoMsgs;
            result.ImportLog = importLog;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to apply edited tables. Return code: {ret}. " +
                                      $"Fatal Errors: {numFatalErrors}, Errors: {numErrorMsgs}, Warnings: {numWarnMsgs}";

                _logger.LogError("Failed to apply edited tables: {ErrorMessage}", result.ErrorMessage);

                if (fillImportLog && !string.IsNullOrEmpty(importLog))
                {
                    _logger.LogError("Import Log:\n{ImportLog}", importLog);
                }

                throw new EtabsException(ret, "ApplyEditedTables", result.ErrorMessage);
            }

            result.IsSuccess = !result.HasErrors;

            if (result.HasErrors)
            {
                _logger.LogWarning(
                    "Applied edited tables with errors: Fatal={Fatal}, Errors={Errors}, Warnings={Warnings}, Info={Info}",
                    numFatalErrors, numErrorMsgs, numWarnMsgs, numInfoMsgs);
            }
            else if (result.HasWarnings)
            {
                _logger.LogInformation("Applied edited tables with warnings: Warnings={Warnings}, Info={Info}",
                    numWarnMsgs, numInfoMsgs);
            }
            else
            {
                _logger.LogInformation("Successfully applied edited tables: Info={Info}", numInfoMsgs);
            }

            if (fillImportLog && !string.IsNullOrEmpty(importLog))
            {
                _logger.LogDebug("Import Log:\n{ImportLog}", importLog);
            }

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error applying edited tables: {ex.Message}";
            _logger.LogError(ex, "Unexpected error applying edited tables");
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Cancels all pending table edits without applying them.
    /// Wraps cSapModel.DatabaseTables.CancelTableEditing.
    /// </summary>
    public int CancelTableEditing()
    {
        try
        {
            int ret = _sapModel.DatabaseTables.CancelTableEditing();

            if (ret != 0)
                throw new EtabsException(ret, "CancelTableEditing",
                    $"Failed to cancel table editing. Return code: {ret}");

            _logger.LogDebug("Cancelled all pending table edits");
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error cancelling table editing: {ex.Message}", ex);
        }
    }

    #endregion

    #region Excel Integration

    /// <summary>
    /// Shows specified tables in Excel for viewing/editing.
    /// Excel must be installed on the computer for this function to work.
    /// Wraps cSapModel.DatabaseTables.ShowTablesInExcel.
    /// </summary>
    public int ShowTablesInExcel(string[] tableKeys, int windowHandle = 0)
    {
        try
        {
            if (tableKeys == null || tableKeys.Length == 0)
                throw new ArgumentException("Table keys array cannot be null or empty", nameof(tableKeys));

            int ret = _sapModel.DatabaseTables.ShowTablesInExcel(ref tableKeys, windowHandle);

            if (ret != 0)
                throw new EtabsException(ret, "ShowTablesInExcel",
                    $"Failed to show tables in Excel. Return code: {ret}. Ensure Excel is installed.");

            _logger.LogDebug("Showing {Count} tables in Excel", tableKeys.Length);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error showing tables in Excel: {ex.Message}", ex);
        }
    }

    #endregion

    #region Set Load Case for display

    /// <summary>
    /// Sets load cases selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadCasesSelectedForDisplay.
    /// </summary>
    public int SetLoadCasesSelectedForDisplay(string[] loadCaseNames)
    {
        try
        {
            if (loadCaseNames == null || loadCaseNames.Length == 0)
                throw new ArgumentException("Load case names array cannot be null or empty", nameof(loadCaseNames));

            int ret = _sapModel.DatabaseTables.SetLoadCasesSelectedForDisplay(ref loadCaseNames);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadCasesSelectedForDisplay",
                    $"Failed to set selected load cases. Return code: {ret}");

            _logger.LogDebug("Set {Count} load cases for display", loadCaseNames.Length);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting load cases for display: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets load combinations selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadCombinationsSelectedForDisplay.
    /// </summary>
    public int SetLoadCombinationsSelectedForDisplay(string[] loadComboNames)
    {
        try
        {
            if (loadComboNames == null || loadComboNames.Length == 0)
                throw new ArgumentException("Load combination names array cannot be null or empty",
                    nameof(loadComboNames));

            int ret = _sapModel.DatabaseTables.SetLoadCombinationsSelectedForDisplay(ref loadComboNames);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadCombinationsSelectedForDisplay",
                    $"Failed to set selected load combinations. Return code: {ret}");

            _logger.LogDebug("Set {Count} load combinations for display", loadComboNames.Length);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting load combinations for display: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets load patterns selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadPatternsSelectedForDisplay.
    /// </summary>
    public int SetLoadPatternsSelectedForDisplay(string[] loadPatternNames)
    {
        try
        {
            if (loadPatternNames == null || loadPatternNames.Length == 0)
                throw new ArgumentException("Load pattern names array cannot be null or empty",
                    nameof(loadPatternNames));

            int ret = _sapModel.DatabaseTables.SetLoadPatternsSelectedForDisplay(ref loadPatternNames);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadPatternsSelectedForDisplay",
                    $"Failed to set selected load patterns. Return code: {ret}");

            _logger.LogDebug("Set {Count} load patterns for display", loadPatternNames.Length);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting load patterns for display: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets output options for display.
    /// Wraps cSapModel.DatabaseTables.SetOutputOptionsForDisplay.
    /// </summary>
    public int SetOutputOptionsForDisplay(OutputOptionsForDisplay options)
    {
        try
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            int ret = _sapModel.DatabaseTables.SetOutputOptionsForDisplay(
                options.IsUserBaseReactionLocation,
                options.UserBaseReactionX,
                options.UserBaseReactionY,
                options.UserBaseReactionZ,
                options.IsAllModes,
                options.StartMode,
                options.EndMode,
                options.IsAllBucklingModes,
                options.StartBucklingMode,
                options.EndBucklingMode,
                options.MultistepStatic,
                options.NonlinearStatic,
                options.ModalHistory,
                options.DirectHistory,
                options.Combo);

            if (ret != 0)
                throw new EtabsException(ret, "SetOutputOptionsForDisplay",
                    $"Failed to set output options for display. Return code: {ret}");

            _logger.LogDebug("Set output options for display");
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting output options for display: {ex.Message}", ex);
        }
    }

    #endregion

    #region Get Table for Display

    /// <summary>
    /// Gets table data for display as array.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayArray.
    /// </summary>
    public TableDataArrayResult GetTableForDisplayArray(string tableKey, string[]? fieldKeys = null,
        string groupName = "")
    {
        var result = new TableDataArrayResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            // If fieldKeys is null, request all fields with single blank string
            string[] fieldKeyList = fieldKeys ?? new[] { "" };
            int tableVersion = 0;
            string[] fieldsKeysIncluded = Array.Empty<string>();
            int numberRecords = 0;
            string[] tableData = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetTableForDisplayArray(
                tableKey,
                ref fieldKeyList,
                groupName ?? "",
                ref tableVersion,
                ref fieldsKeysIncluded,
                ref numberRecords,
                ref tableData);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;
            result.NumberOfRecords = numberRecords;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get table '{tableKey}' for display. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForDisplayArray", result.ErrorMessage);
            }

            result.FieldKeysIncluded = fieldsKeysIncluded.ToList();
            result.TableData = tableData.ToList();
            result.IsSuccess = true;

            _logger.LogDebug("Retrieved table '{TableKey}' for display: {Records} records, {Fields} fields",
                tableKey, numberRecords, fieldsKeysIncluded.Length);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting table for display: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets table data for display as CSV string.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayCSVString.
    /// </summary>
    public TableDataCsvResult GetTableForDisplayCsv(string tableKey, string[]? fieldKeys = null, string groupName = "",
        string separator = ",")
    {
        var result = new TableDataCsvResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            string[] fieldKeyList = fieldKeys ?? new[] { "" };
            int tableVersion = 0;
            string csvString = string.Empty;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayCSVString(
                tableKey,
                ref fieldKeyList,
                groupName ?? "",
                ref tableVersion,
                ref csvString,
                separator);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get table '{tableKey}' as CSV. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForDisplayCSVString", result.ErrorMessage);
            }

            result.FieldKeysIncluded = fieldKeyList.Where(k => !string.IsNullOrEmpty(k)).ToList();
            result.CsvData = csvString;
            result.IsSuccess = true;

            _logger.LogDebug("Retrieved table '{TableKey}' as CSV for display", tableKey);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting table as CSV: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets table data for display and saves to CSV file.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayCSVFile.
    /// </summary>
    public TableDataFileResult GetTableForDisplayCsvFile(string tableKey, string csvFilePath,
        string[]? fieldKeys = null, string groupName = "", string separator = ",")
    {
        var result = new TableDataFileResult { TableKey = tableKey, FilePath = csvFilePath };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (string.IsNullOrEmpty(csvFilePath))
                throw new ArgumentException("CSV file path cannot be null or empty", nameof(csvFilePath));

            string[] fieldKeyList = fieldKeys ?? new[] { "" };
            int tableVersion = 0;

            int ret = _sapModel.DatabaseTables.GetTableForDisplayCSVFile(
                tableKey,
                ref fieldKeyList,
                groupName ?? "",
                ref tableVersion,
                csvFilePath,
                separator);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to save table '{tableKey}' to CSV file. Return code: {ret}";
                throw new EtabsException(ret, "GetTableForDisplayCSVFile", result.ErrorMessage);
            }

            result.IsSuccess = true;
            _logger.LogDebug("Saved table '{TableKey}' to CSV file: {FilePath}", tableKey, csvFilePath);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error saving table to CSV file: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

#endregion
}


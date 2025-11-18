using EtabSharp.DatabaseTables.Models;
using Microsoft.Extensions.Logging;

namespace EtabSharp.DatabaseTables;

/// <summary>
/// Partial class for helper and convenience methods.
/// </summary>
public partial class DatabaseTablesManager
{
    #region Helper Methods

    /// <summary>
    /// Gets a table, modifies it using a callback, and applies the changes.
    /// This is a convenience method that handles the full edit workflow.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="modifyCallback">Callback function to modify the table data</param>
    /// <param name="groupName">Optional group name to filter results</param>
    /// <returns>Result of applying the edited table</returns>
    public ApplyEditedTablesResult EditTableWorkflow(
        string tableKey,
        Func<TableDataArrayResult, TableDataArrayResult> modifyCallback,
        string groupName = "")
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));
            if (modifyCallback == null)
                throw new ArgumentNullException(nameof(modifyCallback));

            _logger.LogInformation("Starting edit workflow for table '{TableKey}'", tableKey);

            // Step 1: Get table for editing
            var tableData = GetTableForEditingArray(tableKey, groupName);

            if (!tableData.IsSuccess)
            {
                _logger.LogError("Failed to get table for editing: {ErrorMessage}", tableData.ErrorMessage);
                return new ApplyEditedTablesResult
                {
                    IsSuccess = false,
                    ErrorMessage = $"Failed to get table: {tableData.ErrorMessage}",
                    ReturnCode = tableData.ReturnCode
                };
            }

            _logger.LogDebug("Retrieved table with {Records} records", tableData.NumberOfRecords);

            // Step 2: Modify table using callback
            var modifiedData = modifyCallback(tableData);

            if (modifiedData == null)
            {
                throw new InvalidOperationException("Modify callback returned null");
            }

            _logger.LogDebug("Modified table now has {Records} records", modifiedData.NumberOfRecords);

            // Step 3: Set table for editing
            int setResult = SetTableForEditingArray(
                modifiedData.TableKey,
                modifiedData.TableVersion,
                modifiedData.FieldKeysIncluded.ToArray(),
                modifiedData.TableData.ToArray());

            if (setResult != 0)
            {
                _logger.LogError("Failed to set modified table for editing");
                return new ApplyEditedTablesResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Failed to set modified table for editing",
                    ReturnCode = setResult
                };
            }

            // Step 4: Apply changes
            var applyResult = ApplyEditedTables(true);

            if (applyResult.IsSuccess)
            {
                _logger.LogInformation("Successfully completed edit workflow for table '{TableKey}'", tableKey);
            }
            else
            {
                _logger.LogError("Edit workflow completed with errors for table '{TableKey}': {ErrorMessage}",
                    tableKey, applyResult.ErrorMessage);
            }

            return applyResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in edit workflow for table '{TableKey}'", tableKey);
            return new ApplyEditedTablesResult
            {
                IsSuccess = false,
                ErrorMessage = $"Unexpected error in edit workflow: {ex.Message}",
                ReturnCode = -1
            };
        }
    }

    /// <summary>
    /// Checks if a table exists and is not empty.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>True if table exists and has data</returns>
    public bool IsTableAvailable(string tableKey)
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                return false;

            var allTables = GetAllTables();

            if (!allTables.IsSuccess)
                return false;

            var table = allTables.Tables.FirstOrDefault(t =>
                t.TableKey.Equals(tableKey, StringComparison.OrdinalIgnoreCase));

            return table != null && !table.IsEmpty;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error checking if table '{TableKey}' is available", tableKey);
            return false;
        }
    }

    /// <summary>
    /// Gets field keys that are importable (editable) for a table.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>List of importable field keys</returns>
    public List<string> GetImportableFields(string tableKey)
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                return new List<string>();

            var fieldsResult = GetAllFieldsInTable(tableKey);

            if (!fieldsResult.IsSuccess)
            {
                _logger.LogWarning("Failed to get fields for table '{TableKey}': {ErrorMessage}",
                    tableKey, fieldsResult.ErrorMessage);
                return new List<string>();
            }

            return fieldsResult.ImportableFields.Select(f => f.FieldKey).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error getting importable fields for table '{TableKey}'", tableKey);
            return new List<string>();
        }
    }

    /// <summary>
    /// Gets a summary of table information.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>Table summary including field info and record count</returns>
    public TableSummary? GetTableSummary(string tableKey)
    {
        try
        {
            if (string.IsNullOrEmpty(tableKey))
                return null;

            // Get table metadata
            var allTables = GetAllTables();
            if (!allTables.IsSuccess)
                return null;

            var tableInfo = allTables.Tables.FirstOrDefault(t =>
                t.TableKey.Equals(tableKey, StringComparison.OrdinalIgnoreCase));

            if (tableInfo == null)
                return null;

            // Get fields
            var fieldsResult = GetAllFieldsInTable(tableKey);
            if (!fieldsResult.IsSuccess)
                return null;

            // Get data to count records
            var dataResult = GetTableForDisplayArray(tableKey);

            return new TableSummary
            {
                TableKey = tableInfo.TableKey,
                TableName = tableInfo.TableName,
                ImportType = tableInfo.ImportType,
                IsEmpty = tableInfo.IsEmpty,
                TableVersion = fieldsResult.TableVersion,
                TotalFields = fieldsResult.NumberOfFields,
                ImportableFields = fieldsResult.ImportableFields.Count,
                TotalRecords = dataResult.IsSuccess ? dataResult.NumberOfRecords : 0
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error getting summary for table '{TableKey}'", tableKey);
            return null;
        }
    }

    /// <summary>
    /// Gets all available table summaries.
    /// </summary>
    /// <returns>List of table summaries</returns>
    public List<TableSummary> GetAllTableSummaries()
    {
        var summaries = new List<TableSummary>();

        try
        {
            var availableTables = GetAvailableTables();

            if (!availableTables.IsSuccess)
            {
                _logger.LogWarning("Failed to get available tables");
                return summaries;
            }

            foreach (var table in availableTables.Tables)
            {
                var summary = GetTableSummary(table.TableKey);
                if (summary != null)
                {
                    summaries.Add(summary);
                }
            }

            _logger.LogDebug("Retrieved summaries for {Count} tables", summaries.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all table summaries");
        }

        return summaries;
    }

    /// <summary>
    /// Validates that a table can be edited.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>Validation result</returns>
    public TableValidationResult ValidateTableForEditing(string tableKey)
    {
        var result = new TableValidationResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Table key cannot be null or empty");
                return result;
            }

            // Check if table exists
            var allTables = GetAllTables();
            if (!allTables.IsSuccess)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Failed to retrieve table list");
                return result;
            }

            var tableInfo = allTables.Tables.FirstOrDefault(t =>
                t.TableKey.Equals(tableKey, StringComparison.OrdinalIgnoreCase));

            if (tableInfo == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add($"Table '{tableKey}' not found");
                return result;
            }

            // Check if table is importable
            if (tableInfo.ImportType == TableImportType.NotImportable)
            {
                result.IsValid = false;
                result.ValidationErrors.Add($"Table '{tableKey}' is not importable (read-only)");
                return result;
            }

            // Check if table is empty
            if (tableInfo.IsEmpty)
            {
                result.ValidationWarnings.Add($"Table '{tableKey}' is currently empty");
            }

            // Get importable fields
            var importableFields = GetImportableFields(tableKey);
            if (importableFields.Count == 0)
            {
                result.ValidationWarnings.Add($"Table '{tableKey}' has no importable fields");
            }

            result.IsValid = result.ValidationErrors.Count == 0;
            result.ImportType = tableInfo.ImportType;
            result.ImportableFieldCount = importableFields.Count;

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating table '{TableKey}' for editing", tableKey);
            result.IsValid = false;
            result.ValidationErrors.Add($"Unexpected error: {ex.Message}");
            return result;
        }
    }

    #endregion
}

#region Helper Model Classes

/// <summary>
/// Summary information for a database table.
/// </summary>
public class TableSummary
{
    public string TableKey { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public TableImportType ImportType { get; set; }
    public bool IsEmpty { get; set; }
    public int TableVersion { get; set; }
    public int TotalFields { get; set; }
    public int ImportableFields { get; set; }
    public int TotalRecords { get; set; }

    public override string ToString()
    {
        return $"{TableName} ({TableKey}): {TotalRecords} records, {ImportableFields}/{TotalFields} importable fields";
    }
}

/// <summary>
/// Result of table validation for editing.
/// </summary>
public class TableValidationResult
{
    public string TableKey { get; set; } = string.Empty;
    public bool IsValid { get; set; }
    public TableImportType ImportType { get; set; }
    public int ImportableFieldCount { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public List<string> ValidationWarnings { get; set; } = new();

    public override string ToString()
    {
        if (IsValid)
            return $"Table '{TableKey}' is valid for editing";

        return $"Table '{TableKey}' validation failed: {string.Join(", ", ValidationErrors)}";
    }
}

#endregion
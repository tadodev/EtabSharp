using EtabSharp.DatabaseTables.Models;

namespace EtabSharp.Interfaces.DatabaseTable;

/// <summary>
/// Interface for interacting with ETABS database tables for editing and display.
/// Provides access to model data through interactive tables similar to the ETABS GUI.
/// </summary>
public interface IDatabaseTables
{
    #region Table Discovery

    /// <summary>
    /// Gets all available tables in the model.
    /// Wraps cSapModel.DatabaseTables.GetAvailableTables.
    /// </summary>
    /// <returns>List of available tables with metadata</returns>
    AvailableTablesResult GetAvailableTables();

    /// <summary>
    /// Gets all tables including empty ones.
    /// Wraps cSapModel.DatabaseTables.GetAllTables.
    /// </summary>
    /// <returns>List of all tables with metadata and empty status</returns>
    AllTablesResult GetAllTables();

    /// <summary>
    /// Gets all fields (columns) in a specific table.
    /// Wraps cSapModel.DatabaseTables.GetAllFieldsInTable.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>Table field information including importability</returns>
    TableFieldsResult GetAllFieldsInTable(string tableKey);

    /// <summary>
    /// Gets list of obsolete table keys with notes.
    /// Wraps cSapModel.DatabaseTables.GetObsoleteTableKeyList.
    /// </summary>
    /// <returns>List of obsolete tables with migration notes</returns>
    ObsoleteTablesResult GetObsoleteTableKeyList();

    #endregion

    #region Display Options

    /// <summary>
    /// Gets load cases selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadCasesSelectedForDisplay.
    /// </summary>
    /// <returns>List of selected load case names</returns>
    LoadCasesSelectedResult GetLoadCasesSelectedForDisplay();

    /// <summary>
    /// Gets load combinations selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadCombinationsSelectedForDisplay.
    /// </summary>
    /// <returns>List of selected load combination names</returns>
    LoadCombinationsSelectedResult GetLoadCombinationsSelectedForDisplay();

    /// <summary>
    /// Gets load patterns selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadPatternsSelectedForDisplay.
    /// </summary>
    /// <returns>List of selected load pattern names</returns>
    LoadPatternsSelectedResult GetLoadPatternsSelectedForDisplay();

    /// <summary>
    /// Gets output options for display including base reaction location and mode ranges.
    /// Wraps cSapModel.DatabaseTables.GetOutputOptionsForDisplay.
    /// </summary>
    /// <returns>Output display options</returns>
    OutputOptionsForDisplayResult GetOutputOptionsForDisplay();

    /// <summary>
    /// Sets load cases selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadCasesSelectedForDisplay.
    /// </summary>
    /// <param name="loadCaseNames">Array of load case names to select</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadCasesSelectedForDisplay(string[] loadCaseNames);

    /// <summary>
    /// Sets load combinations selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadCombinationsSelectedForDisplay.
    /// </summary>
    /// <param name="loadComboNames">Array of load combination names to select</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadCombinationsSelectedForDisplay(string[] loadComboNames);

    /// <summary>
    /// Sets load patterns selected for display.
    /// Wraps cSapModel.DatabaseTables.SetLoadPatternsSelectedForDisplay.
    /// </summary>
    /// <param name="loadPatternNames">Array of load pattern names to select</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadPatternsSelectedForDisplay(string[] loadPatternNames);

    /// <summary>
    /// Sets output options for display.
    /// Wraps cSapModel.DatabaseTables.SetOutputOptionsForDisplay.
    /// </summary>
    /// <param name="options">Output display options to set</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOutputOptionsForDisplay(OutputOptionsForDisplay options);

    #endregion

    #region Get Table for Display

    /// <summary>
    /// Gets table data for display as array.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayArray.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="fieldKeys">Optional list of field keys to retrieve (null for all)</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <returns>Table data as array</returns>
    TableDataArrayResult GetTableForDisplayArray(string tableKey, string[]? fieldKeys = null, string groupName = "");

    /// <summary>
    /// Gets table data for display as CSV string.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayCSVString.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="fieldKeys">Optional list of field keys to retrieve (null for all)</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>Table data as CSV string</returns>
    TableDataCsvResult GetTableForDisplayCsv(string tableKey, string[]? fieldKeys = null, string groupName = "", string separator = ",");

    /// <summary>
    /// Gets table data for display and saves to CSV file.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayCSVFile.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="csvFilePath">Path where CSV file will be saved</param>
    /// <param name="fieldKeys">Optional list of field keys to retrieve (null for all)</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>Result with table version</returns>
    TableDataFileResult GetTableForDisplayCsvFile(string tableKey, string csvFilePath, string[]? fieldKeys = null, string groupName = "", string separator = ",");

    /// <summary>
    /// Gets table data for display as XML string.
    /// Wraps cSapModel.DatabaseTables.GetTableForDisplayXMLString.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="fieldKeys">Optional list of field keys to retrieve (null for all)</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <param name="includeSchema">Include XML schema in output</param>
    /// <returns>Table data as XML string</returns>
    TableDataXmlResult GetTableForDisplayXml(string tableKey, string[]? fieldKeys = null, string groupName = "", bool includeSchema = false);

    #endregion

    #region Get Table for Editing

    /// <summary>
    /// Gets table data for editing as array.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingArray.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <returns>Editable table data as array</returns>
    TableDataArrayResult GetTableForEditingArray(string tableKey, string groupName = "");

    /// <summary>
    /// Gets table data for editing as CSV string.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingCSVString.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>Editable table data as CSV string</returns>
    TableDataCsvResult GetTableForEditingCsv(string tableKey, string groupName = "", string separator = ",");

    /// <summary>
    /// Gets table data for editing and saves to CSV file.
    /// Wraps cSapModel.DatabaseTables.GetTableForEditingCSVFile.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="csvFilePath">Path where CSV file will be saved</param>
    /// <param name="groupName">Optional group name to filter results (empty string for all)</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>Result with table version</returns>
    TableDataFileResult GetTableForEditingCsvFile(string tableKey, string csvFilePath, string groupName = "", string separator = ",");

    #endregion

    #region Set Table for Editing

    /// <summary>
    /// Sets table data for editing from array.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingArray.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="tableVersion">Table version from Get operation</param>
    /// <param name="fieldKeys">Field keys included in the data</param>
    /// <param name="tableData">Table data array</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTableForEditingArray(string tableKey, int tableVersion, string[] fieldKeys, string[] tableData);

    /// <summary>
    /// Sets table data for editing from CSV string.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingCSVString.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="tableVersion">Table version from Get operation</param>
    /// <param name="csvString">CSV string with table data</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTableForEditingCsv(string tableKey, int tableVersion, string csvString, string separator = ",");

    /// <summary>
    /// Sets table data for editing from CSV file.
    /// This queues the table for editing but does not apply changes until ApplyEditedTables is called.
    /// Wraps cSapModel.DatabaseTables.SetTableForEditingCSVFile.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="tableVersion">Table version from Get operation</param>
    /// <param name="csvFilePath">Path to CSV file with table data</param>
    /// <param name="separator">CSV separator character (default: ",")</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTableForEditingCsvFile(string tableKey, int tableVersion, string csvFilePath, string separator = ",");

    #endregion

    #region Apply and Cancel Editing

    /// <summary>
    /// Applies all queued edited tables to the model.
    /// This is when changes actually take effect.
    /// Wraps cSapModel.DatabaseTables.ApplyEditedTables.
    /// </summary>
    /// <param name="fillImportLog">Whether to generate detailed import log</param>
    /// <returns>Result with error counts and import log</returns>
    ApplyEditedTablesResult ApplyEditedTables(bool fillImportLog = true);

    /// <summary>
    /// Cancels all pending table edits without applying them.
    /// Wraps cSapModel.DatabaseTables.CancelTableEditing.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CancelTableEditing();

    #endregion

    #region Excel Integration

    /// <summary>
    /// Shows specified tables in Excel for viewing/editing.
    /// Wraps cSapModel.DatabaseTables.ShowTablesInExcel.
    /// </summary>
    /// <param name="tableKeys">Array of table keys to show</param>
    /// <param name="windowHandle">Parent window handle (use 0 for default)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ShowTablesInExcel(string[] tableKeys, int windowHandle = 0);

    #endregion

    #region Helper Methods

    /// <summary>
    /// Gets a table, modifies it using a callback, and applies the changes.
    /// This is a convenience method that handles the full edit workflow.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <param name="modifyCallback">Callback function to modify the table data</param>
    /// <param name="groupName">Optional group name to filter results</param>
    /// <returns>Result of applying the edited table</returns>
    ApplyEditedTablesResult EditTableWorkflow(string tableKey, Func<TableDataArrayResult, TableDataArrayResult> modifyCallback, string groupName = "");

    /// <summary>
    /// Checks if a table exists and is not empty.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>True if table exists and has data</returns>
    bool IsTableAvailable(string tableKey);

    /// <summary>
    /// Gets field keys that are importable (editable) for a table.
    /// </summary>
    /// <param name="tableKey">Unique key identifying the table</param>
    /// <returns>List of importable field keys</returns>
    List<string> GetImportableFields(string tableKey);

    #endregion
}
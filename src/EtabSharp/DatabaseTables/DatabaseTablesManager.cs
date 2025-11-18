using EtabSharp.DatabaseTables.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.DatabaseTable;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.DatabaseTables;

/// <summary>
/// Manages database table operations for the ETABS model.
/// Implements the IDatabaseTables interface by wrapping cSapModel.DatabaseTables operations.
/// </summary>
public partial class DatabaseTablesManager : IDatabaseTables
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the DatabaseTablesManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public DatabaseTablesManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogDebug("DatabaseTablesManager initialized");
    }

    #region Table Discovery

    /// <summary>
    /// Gets all available tables in the model.
    /// Wraps cSapModel.DatabaseTables.GetAvailableTables.
    /// </summary>
    public AvailableTablesResult GetAvailableTables()
    {
        var result = new AvailableTablesResult();

        try
        {
            int numberTables = 0;
            string[] tableKey = Array.Empty<string>();
            string[] tableName = Array.Empty<string>();
            int[] importType = Array.Empty<int>();

            int ret = _sapModel.DatabaseTables.GetAvailableTables(
                ref numberTables,
                ref tableKey,
                ref tableName,
                ref importType);

            result.ReturnCode = ret;
            result.NumberOfTables = numberTables;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get available tables. Return code: {ret}";
                throw new EtabsException(ret, "GetAvailableTables", result.ErrorMessage);
            }

            for (int i = 0; i < numberTables; i++)
            {
                result.Tables.Add(new DatabaseTableInfo
                {
                    TableKey = tableKey[i],
                    TableName = tableName[i],
                    ImportType = (TableImportType)importType[i],
                    IsEmpty = false // Not provided by GetAvailableTables
                });
            }

            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} available tables", numberTables);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting available tables: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets all tables including empty ones.
    /// Wraps cSapModel.DatabaseTables.GetAllTables.
    /// </summary>
    public AllTablesResult GetAllTables()
    {
        var result = new AllTablesResult();

        try
        {
            int numberTables = 0;
            string[] tableKey = Array.Empty<string>();
            string[] tableName = Array.Empty<string>();
            int[] importType = Array.Empty<int>();
            bool[] isEmpty = Array.Empty<bool>();

            int ret = _sapModel.DatabaseTables.GetAllTables(
                ref numberTables,
                ref tableKey,
                ref tableName,
                ref importType,
                ref isEmpty);

            result.ReturnCode = ret;
            result.NumberOfTables = numberTables;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get all tables. Return code: {ret}";
                throw new EtabsException(ret, "GetAllTables", result.ErrorMessage);
            }

            for (int i = 0; i < numberTables; i++)
            {
                result.Tables.Add(new DatabaseTableInfo
                {
                    TableKey = tableKey[i],
                    TableName = tableName[i],
                    ImportType = (TableImportType)importType[i],
                    IsEmpty = isEmpty[i]
                });
            }

            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} tables ({EmptyCount} empty)",
                numberTables, result.Tables.Count(t => t.IsEmpty));

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting all tables: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets all fields (columns) in a specific table.
    /// Wraps cSapModel.DatabaseTables.GetAllFieldsInTable.
    /// </summary>
    public TableFieldsResult GetAllFieldsInTable(string tableKey)
    {
        var result = new TableFieldsResult { TableKey = tableKey };

        try
        {
            if (string.IsNullOrEmpty(tableKey))
                throw new ArgumentException("Table key cannot be null or empty", nameof(tableKey));

            int tableVersion = 0;
            int numberFields = 0;
            string[] fieldKey = Array.Empty<string>();
            string[] fieldName = Array.Empty<string>();
            string[] description = Array.Empty<string>();
            string[] unitsString = Array.Empty<string>();
            bool[] isImportable = Array.Empty<bool>();

            int ret = _sapModel.DatabaseTables.GetAllFieldsInTable(
                tableKey,
                ref tableVersion,
                ref numberFields,
                ref fieldKey,
                ref fieldName,
                ref description,
                ref unitsString,
                ref isImportable);

            result.ReturnCode = ret;
            result.TableVersion = tableVersion;
            result.NumberOfFields = numberFields;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get fields for table '{tableKey}'. Return code: {ret}";
                throw new EtabsException(ret, "GetAllFieldsInTable", result.ErrorMessage);
            }

            for (int i = 0; i < numberFields; i++)
            {
                result.Fields.Add(new TableFieldInfo
                {
                    FieldKey = fieldKey[i],
                    FieldName = fieldName[i],
                    Description = description[i],
                    UnitsString = unitsString[i],
                    IsImportable = isImportable[i]
                });
            }

            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} fields for table '{TableKey}' (version {Version})",
                numberFields, tableKey, tableVersion);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting fields for table '{tableKey}': {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets list of obsolete table keys with notes.
    /// Wraps cSapModel.DatabaseTables.GetObsoleteTableKeyList.
    /// </summary>
    public ObsoleteTablesResult GetObsoleteTableKeyList()
    {
        var result = new ObsoleteTablesResult();

        try
        {
            int numberTableKeys = 0;
            string[] tableKeyList = Array.Empty<string>();
            string[] notesList = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetObsoleteTableKeyList(
                ref numberTableKeys,
                ref tableKeyList,
                ref notesList);

            result.ReturnCode = ret;
            result.NumberOfTables = numberTableKeys;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get obsolete tables. Return code: {ret}";
                throw new EtabsException(ret, "GetObsoleteTableKeyList", result.ErrorMessage);
            }

            for (int i = 0; i < numberTableKeys; i++)
            {
                result.ObsoleteTables.Add(new ObsoleteTableInfo
                {
                    TableKey = tableKeyList[i],
                    Notes = notesList[i]
                });
            }

            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} obsolete tables", numberTableKeys);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting obsolete tables: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    #endregion

    #region Display Options

    /// <summary>
    /// Gets load cases selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadCasesSelectedForDisplay.
    /// </summary>
    public LoadCasesSelectedResult GetLoadCasesSelectedForDisplay()
    {
        var result = new LoadCasesSelectedResult();

        try
        {
            int numberSelected = 0;
            string[] loadCaseList = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetLoadCasesSelectedForDisplay(
                ref numberSelected,
                ref loadCaseList);

            result.ReturnCode = ret;
            result.NumberSelected = numberSelected;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get selected load cases. Return code: {ret}";
                throw new EtabsException(ret, "GetLoadCasesSelectedForDisplay", result.ErrorMessage);
            }

            result.LoadCaseNames = loadCaseList.ToList();
            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} selected load cases", numberSelected);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting selected load cases: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets load combinations selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadCombinationsSelectedForDisplay.
    /// </summary>
    public LoadCombinationsSelectedResult GetLoadCombinationsSelectedForDisplay()
    {
        var result = new LoadCombinationsSelectedResult();

        try
        {
            int numberSelected = 0;
            string[] loadComboList = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetLoadCombinationsSelectedForDisplay(
                ref numberSelected,
                ref loadComboList);

            result.ReturnCode = ret;
            result.NumberSelected = numberSelected;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get selected load combinations. Return code: {ret}";
                throw new EtabsException(ret, "GetLoadCombinationsSelectedForDisplay", result.ErrorMessage);
            }

            result.LoadComboNames = loadComboList.ToList();
            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} selected load combinations", numberSelected);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting selected load combinations: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets load patterns selected for display.
    /// Wraps cSapModel.DatabaseTables.GetLoadPatternsSelectedForDisplay.
    /// </summary>
    public LoadPatternsSelectedResult GetLoadPatternsSelectedForDisplay()
    {
        var result = new LoadPatternsSelectedResult();

        try
        {
            int numberSelected = 0;
            string[] loadPatternList = Array.Empty<string>();

            int ret = _sapModel.DatabaseTables.GetLoadPatternsSelectedForDisplay(
                ref numberSelected,
                ref loadPatternList);

            result.ReturnCode = ret;
            result.NumberSelected = numberSelected;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get selected load patterns. Return code: {ret}";
                throw new EtabsException(ret, "GetLoadPatternsSelectedForDisplay", result.ErrorMessage);
            }

            result.LoadPatternNames = loadPatternList.ToList();
            result.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} selected load patterns", numberSelected);

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting selected load patterns: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets output options for display including base reaction location and mode ranges.
    /// Wraps cSapModel.DatabaseTables.GetOutputOptionsForDisplay.
    /// </summary>
    public OutputOptionsForDisplayResult GetOutputOptionsForDisplay()
    {
        var result = new OutputOptionsForDisplayResult();

        try
        {
            bool isUserBaseReactionLocation = false;
            double userBaseReactionX = 0;
            double userBaseReactionY = 0;
            double userBaseReactionZ = 0;
            bool isAllModes = false;
            int startMode = 1;
            int endMode = 12;
            bool isAllBucklingModes = false;
            int startBucklingMode = 1;
            int endBucklingMode = 1;
            int multistepStatic = 0;
            int nonlinearStatic = 0;
            int modalHistory = 0;
            int directHistory = 0;
            int combo = 0;

            int ret = _sapModel.DatabaseTables.GetOutputOptionsForDisplay(
                ref isUserBaseReactionLocation,
                ref userBaseReactionX,
                ref userBaseReactionY,
                ref userBaseReactionZ,
                ref isAllModes,
                ref startMode,
                ref endMode,
                ref isAllBucklingModes,
                ref startBucklingMode,
                ref endBucklingMode,
                ref multistepStatic,
                ref nonlinearStatic,
                ref modalHistory,
                ref directHistory,
                ref combo);

            result.ReturnCode = ret;

            if (ret != 0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = $"Failed to get output options. Return code: {ret}";
                throw new EtabsException(ret, "GetOutputOptionsForDisplay", result.ErrorMessage);
            }

            result.Options = new OutputOptionsForDisplay
            {
                IsUserBaseReactionLocation = isUserBaseReactionLocation,
                UserBaseReactionX = userBaseReactionX,
                UserBaseReactionY = userBaseReactionY,
                UserBaseReactionZ = userBaseReactionZ,
                IsAllModes = isAllModes,
                StartMode = startMode,
                EndMode = endMode,
                IsAllBucklingModes = isAllBucklingModes,
                StartBucklingMode = startBucklingMode,
                EndBucklingMode = endBucklingMode,
                MultistepStatic = multistepStatic,
                NonlinearStatic = nonlinearStatic,
                ModalHistory = modalHistory,
                DirectHistory = directHistory,
                Combo = combo
            };

            result.IsSuccess = true;
            _logger.LogDebug("Retrieved output options for display");

            return result;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            result.IsSuccess = false;
            result.ErrorMessage = $"Unexpected error getting output options: {ex.Message}";
            throw new EtabsException(result.ErrorMessage, ex);
        }
    }

    // Set methods for display options continue in next part...

    #endregion
}
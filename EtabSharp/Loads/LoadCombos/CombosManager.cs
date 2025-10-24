using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Loads;
using EtabSharp.Loads.LoadCombos.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Loads.LoadCombos;

/// <summary>
/// Manages load combinations in the ETABS model.
/// Implements the ICombos interface by wrapping cSapModel.RespCombo operations.
/// </summary>
public class CombosManager : ICombos
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the CombosManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public CombosManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Combination Creation & Management

    /// <summary>
    /// Adds a new load combination to the model.
    /// Wraps cSapModel.RespCombo.Add.
    /// </summary>
    public int Add(string name, int comboType)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(name));
            if (comboType < 0 || comboType > 4)
                throw new ArgumentException("Combo type must be 0-4", nameof(comboType));

            int ret = _sapModel.RespCombo.Add(name, comboType);

            if (ret != 0)
                throw new EtabsException(ret, "Add", $"Failed to add load combination '{name}'");

            _logger.LogDebug("Added load combination {ComboName} with type {ComboType}", name, comboType);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding combination '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds a new load combination using the enum type.
    /// </summary>
    public int Add(string name, LoadComboType comboType)
    {
        return Add(name, (int)comboType);
    }

    /// <summary>
    /// Adds default design load combinations for specified design types.
    /// Wraps cSapModel.RespCombo.AddDesignDefaultCombos.
    /// </summary>
    public int AddDesignDefaultCombos(bool designSteel, bool designConcrete, bool designAluminum, bool designColdFormed)
    {
        try
        {
            int ret = _sapModel.RespCombo.AddDesignDefaultCombos(designSteel, designConcrete, designAluminum, designColdFormed);

            if (ret != 0)
                throw new EtabsException(ret, "AddDesignDefaultCombos", "Failed to add design default combinations");

            _logger.LogDebug("Added design default combinations: Steel={Steel}, Concrete={Concrete}, Aluminum={Aluminum}, ColdFormed={ColdFormed}",
                designSteel, designConcrete, designAluminum, designColdFormed);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding design default combinations: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds default design combinations using options object.
    /// </summary>
    public int AddDesignDefaultCombos(DesignComboOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        return AddDesignDefaultCombos(options.IncludeSteel, options.IncludeConcrete,
            options.IncludeAluminum, options.IncludeColdFormed);
    }

    /// <summary>
    /// Deletes a load combination from the model.
    /// Wraps cSapModel.RespCombo.Delete.
    /// </summary>
    public int Delete(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(name));

            int ret = _sapModel.RespCombo.Delete(name);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete load combination '{name}'");

            _logger.LogDebug("Deleted load combination {ComboName}", name);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting combination '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the type of a load combination.
    /// Wraps cSapModel.RespCombo.GetTypeCombo.
    /// </summary>
    public int GetComboType(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(name));

            int comboType = 0;
            int ret = _sapModel.RespCombo.GetTypeCombo(name, ref comboType);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeCombo", $"Failed to get type for combination '{name}'");

            return comboType;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting combo type for '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the names of all defined load combinations.
    /// Wraps cSapModel.RespCombo.GetNameList.
    /// </summary>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.RespCombo.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get combination name list");

            _logger.LogDebug("Retrieved {Count} load combination names", numberNames);
            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting combination name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of load combinations in the model.
    /// </summary>
    public int Count()
    {
        try
        {
            var names = GetNameList();
            int count = names?.Length ?? 0;
            _logger.LogDebug("Load combination count: {Count}", count);
            return count;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting combination count: {ex.Message}", ex);
        }
    }

    #endregion

    #region Case Management within Combinations

    /// <summary>
    /// Adds or modifies a load case/combo within a combination.
    /// Wraps cSapModel.RespCombo.SetCaseList.
    /// </summary>
    public int SetCaseList(string comboName, eCNameType caseType, string caseName, double scaleFactor)
    {
        try
        {
            if (string.IsNullOrEmpty(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));
            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentException("Case name cannot be null or empty", nameof(caseName));

            eCNameType cType = caseType;
            int ret = _sapModel.RespCombo.SetCaseList(comboName, ref cType, caseName, scaleFactor);

            if (ret != 0)
                throw new EtabsException(ret, "SetCaseList", $"Failed to set case '{caseName}' in combination '{comboName}'");

            _logger.LogDebug("Set case {CaseName} in combo {ComboName} with factor {Factor}",
                caseName, comboName, scaleFactor);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting case list for '{comboName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds or modifies a load case/combo with mode number support.
    /// Wraps cSapModel.RespCombo.SetCaseList_1.
    /// </summary>
    public int SetCaseList(string comboName, eCNameType caseType, string caseName, int modeNumber, double scaleFactor)
    {
        try
        {
            if (string.IsNullOrEmpty(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));
            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentException("Case name cannot be null or empty", nameof(caseName));

            eCNameType cType = caseType;
            int ret = _sapModel.RespCombo.SetCaseList_1(comboName, ref cType, caseName, modeNumber, scaleFactor);

            if (ret != 0)
                throw new EtabsException(ret, "SetCaseList_1", $"Failed to set case '{caseName}' (mode {modeNumber}) in combination '{comboName}'");

            _logger.LogDebug("Set case {CaseName} (mode {Mode}) in combo {ComboName} with factor {Factor}",
                caseName, modeNumber, comboName, scaleFactor);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting case list for '{comboName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the list of cases in a load combination.
    /// Wraps cSapModel.RespCombo.GetCaseList_1.
    /// </summary>
    public List<LoadCombinationCase> GetCaseList(string comboName)
    {
        try
        {
            if (string.IsNullOrEmpty(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));

            int numberItems = 0;
            eCNameType[] caseTypes = null;
            string[] caseNames = null;
            int[] modeNumbers = null;
            double[] scaleFactors = null;

            int ret = _sapModel.RespCombo.GetCaseList_1(comboName, ref numberItems, ref caseTypes,
                ref caseNames, ref modeNumbers, ref scaleFactors);

            if (ret != 0)
                throw new EtabsException(ret, "GetCaseList_1", $"Failed to get case list for combination '{comboName}'");

            var cases = new List<LoadCombinationCase>();
            for (int i = 0; i < numberItems; i++)
            {
                cases.Add(new LoadCombinationCase
                {
                    CaseType = caseTypes[i],
                    CaseName = caseNames[i],
                    ModeNumber = modeNumbers[i],
                    ScaleFactor = scaleFactors[i]
                });
            }

            _logger.LogDebug("Retrieved {Count} cases for combination {ComboName}", numberItems, comboName);
            return cases;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting case list for '{comboName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes a specific case from a load combination.
    /// Wraps cSapModel.RespCombo.DeleteCase.
    /// </summary>
    public int DeleteCase(string comboName, eCNameType caseType, string caseName)
    {
        try
        {
            if (string.IsNullOrEmpty(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));
            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentException("Case name cannot be null or empty", nameof(caseName));

            int ret = _sapModel.RespCombo.DeleteCase(comboName, caseType, caseName);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteCase", $"Failed to delete case '{caseName}' from combination '{comboName}'");

            _logger.LogDebug("Deleted case {CaseName} from combo {ComboName}", caseName, comboName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting case from '{comboName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Complete Combination Queries

    /// <summary>
    /// Gets a complete load combination with all its cases.
    /// </summary>
    public LoadCombination GetLoadCombination(string comboName)
    {
        try
        {
            if (string.IsNullOrEmpty(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));

            var combo = new LoadCombination
            {
                Name = comboName,
                ComboType = GetComboType(comboName),
                Cases = GetCaseList(comboName)
            };

            _logger.LogDebug("Retrieved complete combination {ComboName} with {CaseCount} cases",
                comboName, combo.CaseCount);
            return combo;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting combination '{comboName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all load combinations in the model.
    /// </summary>
    public List<LoadCombination> GetAllCombinations()
    {
        try
        {
            var names = GetNameList();
            var combinations = new List<LoadCombination>();

            foreach (var name in names)
            {
                try
                {
                    var combo = GetLoadCombination(name);
                    combinations.Add(combo);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to retrieve combination {ComboName}: {Error}", name, ex.Message);
                }
            }

            _logger.LogDebug("Retrieved {Count} complete combinations", combinations.Count);
            return combinations;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all combinations: {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Creates a simple linear combination with specified cases.
    /// </summary>
    public int CreateLinearCombo(string name, params (string caseName, double factor)[] cases)
    {
        try
        {
            // Create the combination
            int ret = Add(name, LoadComboType.LinearAdd);
            if (ret != 0)
                return ret;

            // Add each case
            foreach (var (caseName, factor) in cases)
            {
                ret = SetCaseList(name, eCNameType.LoadCase, caseName, factor);
                if (ret != 0)
                {
                    _logger.LogError("Failed to add case {CaseName} to combo {ComboName}", caseName, name);
                    return ret;
                }
            }

            _logger.LogDebug("Created linear combo {ComboName} with {CaseCount} cases", name, cases.Length);
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error creating linear combo '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates an envelope combination.
    /// </summary>
    public int CreateEnvelopeCombo(string name, params string[] caseNames)
    {
        try
        {
            // Create the combination
            int ret = Add(name, LoadComboType.Envelope);
            if (ret != 0)
                return ret;

            // Add each case
            foreach (var caseName in caseNames)
            {
                ret = SetCaseList(name, eCNameType.LoadCase, caseName, 1.0);
                if (ret != 0)
                {
                    _logger.LogError("Failed to add case {CaseName} to envelope {ComboName}", caseName, name);
                    return ret;
                }
            }

            _logger.LogDebug("Created envelope combo {ComboName} with {CaseCount} cases", name, caseNames.Length);
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error creating envelope combo '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates a typical dead + live combination.
    /// </summary>
    public int CreateDeadPlusLive(string name, double deadFactor = 1.0, double liveFactor = 1.0)
    {
        return CreateLinearCombo(name, ("DEAD", deadFactor), ("LIVE", liveFactor));
    }

    /// <summary>
    /// Adds a load case to an existing combination.
    /// </summary>
    public int AddLoadCaseToCombo(string comboName, string caseName, double scaleFactor = 1.0)
    {
        return SetCaseList(comboName, eCNameType.LoadCase, caseName, scaleFactor);
    }

    /// <summary>
    /// Adds a load combination to an existing combination.
    /// </summary>
    public int AddLoadComboToCombo(string comboName, string nestedComboName, double scaleFactor = 1.0)
    {
        return SetCaseList(comboName, eCNameType.LoadCombo, nestedComboName, scaleFactor);
    }

    /// <summary>
    /// Checks if a combination exists.
    /// </summary>
    public bool Exists(string name)
    {
        try
        {
            var names = GetNameList();
            return names.Contains(name);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets combinations that include a specific load case.
    /// </summary>
    public List<string> GetCombosUsingCase(string caseName)
    {
        try
        {
            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentException("Case name cannot be null or empty", nameof(caseName));

            var combosUsingCase = new List<string>();
            var allCombos = GetNameList();

            foreach (var comboName in allCombos)
            {
                try
                {
                    var cases = GetCaseList(comboName);
                    if (cases.Any(c => c.CaseName == caseName))
                    {
                        combosUsingCase.Add(comboName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to check combo {ComboName} for case {CaseName}: {Error}",
                        comboName, caseName, ex.Message);
                }
            }

            _logger.LogDebug("Found {Count} combinations using case {CaseName}", combosUsingCase.Count, caseName);
            return combosUsingCase;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error finding combos using case '{caseName}': {ex.Message}", ex);
        }
    }

    #endregion
}
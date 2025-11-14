using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Loads;
using EtabSharp.Loads.LoadCases.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Loads.LoadCases;

/// <summary>
/// Manages load cases in the ETABS model.
/// Implements the ILoadCases interface by wrapping cSapModel.LoadCases operations.
/// </summary>
public class LoadCasesManager : ILoadCases
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the LoadCasesManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public LoadCasesManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Load Case Management

    /// <summary>
    /// Changes the name of an existing load case.
    /// Wraps cSapModel.LoadCases.ChangeName.
    /// </summary>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.LoadCases.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change load case name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed load case name from {OldName} to {NewName}", currentName, newName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing load case name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes a load case from the model.
    /// Wraps cSapModel.LoadCases.Delete.
    /// </summary>
    public int Delete(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load case name cannot be null or empty", nameof(name));

            int ret = _sapModel.LoadCases.Delete(name);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete load case '{name}'");

            _logger.LogDebug("Deleted load case {Name}", name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting load case '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of load cases in the model.
    /// Wraps cSapModel.LoadCases.Count.
    /// </summary>
    public int Count(eLoadCaseType caseType = 0)
    {
        try
        {
            int count = _sapModel.LoadCases.Count(caseType);
            _logger.LogDebug("Load case count for type {CaseType}: {Count}", caseType, count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting load case count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the names of all load cases in the model.
    /// Wraps cSapModel.LoadCases.GetNameList.
    /// </summary>
    public string[] GetNameList(eLoadCaseType caseType = 0)
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.LoadCases.GetNameList(ref numberNames, ref names, caseType);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get load case name list");

            _logger.LogDebug("Retrieved {Count} load case names for type {CaseType}", numberNames, caseType);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load case name list: {ex.Message}", ex);
        }
    }

    #endregion

    #region Load Case Properties

    /// <summary>
    /// Gets the type and subtype of a load case.
    /// Wraps cSapModel.LoadCases.GetTypeOAPI.
    /// </summary>
    public (eLoadCaseType CaseType, int SubType) GetTypeOAPI(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load case name cannot be null or empty", nameof(name));

            eLoadCaseType caseType = eLoadCaseType.LinearStatic;
            int subType = 0;

            int ret = _sapModel.LoadCases.GetTypeOAPI(name, ref caseType, ref subType);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get type for load case '{name}'");

            return (caseType, subType);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting type for load case '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets complete information about a load case.
    /// Wraps cSapModel.LoadCases.GetTypeOAPI_1.
    /// </summary>
    public (eLoadCaseType CaseType, int SubType, eLoadPatternType DesignType, int DesignTypeOption, bool IsAuto) GetTypeOAPI_1(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load case name cannot be null or empty", nameof(name));

            eLoadCaseType caseType = eLoadCaseType.LinearStatic;
            int subType = 0;
            eLoadPatternType designType = eLoadPatternType.Dead;
            int designTypeOption = 1;
            int autoInt = 0;

            int ret = _sapModel.LoadCases.GetTypeOAPI_1(name, ref caseType, ref subType, ref designType, ref designTypeOption, ref autoInt);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeOAPI_1", $"Failed to get complete info for load case '{name}'");

            bool isAuto = autoInt != 0;

            return (caseType, subType, designType, designTypeOption, isAuto);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting complete info for load case '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the design type for a load case.
    /// Wraps cSapModel.LoadCases.SetDesignType.
    /// </summary>
    public int SetDesignType(string name, int designTypeOption, eLoadPatternType designType = eLoadPatternType.Dead)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load case name cannot be null or empty", nameof(name));

            if (designTypeOption < 1 || designTypeOption > 3)
                throw new ArgumentException("Design type option must be 1 (Strength), 2 (Serviceability), or 3 (Other)", nameof(designTypeOption));

            int ret = _sapModel.LoadCases.SetDesignType(name, designTypeOption, designType);

            if (ret != 0)
                throw new EtabsException(ret, "SetDesignType", $"Failed to set design type for load case '{name}'");

            _logger.LogDebug("Set design type for case {Name}: Option={DesignTypeOption}, Type={DesignType}",
                name, designTypeOption, designType);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting design type for load case '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a complete load case with all properties.
    /// </summary>
    public LoadCase GetLoadCase(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load case name cannot be null or empty", nameof(name));

            var (caseType, subType, designType, designTypeOption, isAuto) = GetTypeOAPI_1(name);

            var loadCase = new LoadCase
            {
                Name = name,
                CaseType = caseType,
                SubType = subType,
                DesignType = designType,
                DesignTypeOption = designTypeOption,
                IsAuto = isAuto
            };

            _logger.LogDebug("Retrieved load case {Name}: {LoadCase}", name, loadCase.ToString());

            return loadCase;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load case '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all load cases in the model with their properties.
    /// </summary>
    public List<LoadCase> GetAllLoadCases(eLoadCaseType caseType = 0)
    {
        try
        {
            var caseNames = GetNameList(caseType);
            var cases = new List<LoadCase>();

            foreach (var caseName in caseNames)
            {
                try
                {
                    var loadCase = GetLoadCase(caseName);
                    cases.Add(loadCase);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to get load case {CaseName}: {Error}", caseName, ex.Message);
                }
            }

            _logger.LogDebug("Retrieved {Count} load cases", cases.Count);
            return cases;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all load cases: {ex.Message}", ex);
        }
    }

    #endregion

    #region Specific Load Case Type Managers

    /// <summary>
    /// Gets the static linear load case manager.
    /// </summary>
    public cCaseStaticLinear StaticLinear => _sapModel.LoadCases.StaticLinear;

    /// <summary>
    /// Gets the static nonlinear load case manager.
    /// </summary>
    public cCaseStaticNonlinear StaticNonlinear => _sapModel.LoadCases.StaticNonlinear;

    /// <summary>
    /// Gets the static nonlinear staged load case manager.
    /// </summary>
    public cCaseStaticNonlinearStaged StaticNonlinearStaged => _sapModel.LoadCases.StaticNonlinearStaged;

    /// <summary>
    /// Gets the modal eigen load case manager.
    /// </summary>
    public cCaseModalEigen ModalEigen => _sapModel.LoadCases.ModalEigen;

    /// <summary>
    /// Gets the modal ritz load case manager.
    /// </summary>
    public cCaseModalRitz ModalRitz => _sapModel.LoadCases.ModalRitz;

    /// <summary>
    /// Gets the response spectrum load case manager.
    /// </summary>
    public cCaseResponseSpectrum ResponseSpectrum => _sapModel.LoadCases.ResponseSpectrum;

    /// <summary>
    /// Gets the modal history linear load case manager.
    /// </summary>
    public cCaseModalHistoryLinear ModHistLinear => _sapModel.LoadCases.ModHistLinear;

    /// <summary>
    /// Gets the modal history nonlinear load case manager.
    /// </summary>
    public cCaseModalHistoryNonlinear ModHistNonlinear => _sapModel.LoadCases.ModHistNonlinear;

    /// <summary>
    /// Gets the direct history linear load case manager.
    /// </summary>
    public cCaseDirectHistoryLinear DirHistLinear => _sapModel.LoadCases.DirHistLinear;

    /// <summary>
    /// Gets the direct history nonlinear load case manager.
    /// </summary>
    public cCaseDirectHistoryNonlinear DirHistNonlinear => _sapModel.LoadCases.DirHistNonlinear;

    /// <summary>
    /// Gets the buckling load case manager.
    /// </summary>
    public cCaseBuckling Buckling => _sapModel.LoadCases.Buckling;

    /// <summary>
    /// Gets the hyperstatic load case manager.
    /// </summary>
    public cCaseHyperStatic HyperStatic => _sapModel.LoadCases.HyperStatic;

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Checks if a load case exists in the model.
    /// </summary>
    public bool LoadCaseExists(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var caseNames = GetNameList();
            return caseNames.Contains(name);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets load cases by type.
    /// </summary>
    public List<string> GetLoadCasesByType(eLoadCaseType caseType)
    {
        try
        {
            return GetNameList(caseType).ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load cases by type: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all static load cases (linear and nonlinear).
    /// </summary>
    public List<string> GetStaticLoadCases()
    {
        try
        {
            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => LoadCaseTypeHelper.IsStatic(c.CaseType))
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting static load cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all dynamic load cases (modal, response spectrum, time history).
    /// </summary>
    public List<string> GetDynamicLoadCases()
    {
        try
        {
            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => LoadCaseTypeHelper.IsDynamic(c.CaseType))
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting dynamic load cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all nonlinear load cases.
    /// </summary>
    public List<string> GetNonlinearLoadCases()
    {
        try
        {
            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => LoadCaseTypeHelper.IsNonlinear(c.CaseType))
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting nonlinear load cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all time history load cases.
    /// </summary>
    public List<string> GetTimeHistoryLoadCases()
    {
        try
        {
            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => LoadCaseTypeHelper.IsTimeHistory(c.CaseType))
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting time history load cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all auto-generated load cases.
    /// </summary>
    public List<string> GetAutoGeneratedLoadCases()
    {
        try
        {
            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => c.IsAuto)
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting auto-generated load cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a summary of all load cases in the model.
    /// </summary>
    public LoadCaseSummary GetLoadCaseSummary()
    {
        try
        {
            var allCases = GetAllLoadCases();
            var summary = new LoadCaseSummary
            {
                TotalCases = allCases.Count,
                LinearStaticCases = allCases.Count(c => c.CaseType == eLoadCaseType.LinearStatic ||
                                                       c.CaseType == eLoadCaseType.LinearStaticMultiStep),
                NonlinearStaticCases = allCases.Count(c => c.CaseType == eLoadCaseType.NonlinearStatic),
                ModalCases = allCases.Count(c => c.CaseType == eLoadCaseType.Modal),
                ResponseSpectrumCases = allCases.Count(c => c.CaseType == eLoadCaseType.ResponseSpectrum),
                LinearHistoryCases = allCases.Count(c => c.CaseType == eLoadCaseType.LinearHistory ||
                                                         c.CaseType == eLoadCaseType.LinearDynamic),
                NonlinearHistoryCases = allCases.Count(c => c.CaseType == eLoadCaseType.NonlinearHistory ||
                                                            c.CaseType == eLoadCaseType.NonlinearDynamic),
                BucklingCases = allCases.Count(c => c.CaseType == eLoadCaseType.Buckling),
                MovingLoadCases = allCases.Count(c => c.CaseType == eLoadCaseType.MovingLoad),
                AutoGeneratedCases = allCases.Count(c => c.IsAuto),
                OtherCases = allCases.Count(c => c.CaseType == eLoadCaseType.SteadyState ||
                                                c.CaseType == eLoadCaseType.PowerSpectralDensity ||
                                                c.CaseType == eLoadCaseType.HyperStatic)
            };

            _logger.LogDebug("Load case summary: {Summary}", summary.ToString());
            return summary;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load case summary: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes all load cases of a specific type.
    /// </summary>
    public int DeleteLoadCasesByType(eLoadCaseType caseType)
    {
        try
        {
            var casesToDelete = GetLoadCasesByType(caseType);
            int deletedCount = 0;

            foreach (var caseName in casesToDelete)
            {
                try
                {
                    int ret = Delete(caseName);
                    if (ret == 0)
                    {
                        deletedCount++;
                        _logger.LogDebug("Deleted load case {CaseName} of type {CaseType}", caseName, caseType);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to delete load case {CaseName}: {Error}", caseName, ex.Message);
                }
            }

            _logger.LogDebug("Deleted {Count} load cases of type {CaseType}", deletedCount, caseType);
            return deletedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting load cases by type: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets design type for multiple load cases.
    /// </summary>
    public int SetDesignTypeMultiple(string[] caseNames, int designTypeOption, eLoadPatternType designType)
    {
        try
        {
            if (caseNames == null || caseNames.Length == 0)
                throw new ArgumentException("Case names array cannot be null or empty", nameof(caseNames));

            int successCount = 0;

            foreach (var caseName in caseNames)
            {
                try
                {
                    int ret = SetDesignType(caseName, designTypeOption, designType);
                    if (ret == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to set design type for case {CaseName}: {Error}", caseName, ex.Message);
                }
            }

            _logger.LogDebug("Set design type for {SuccessCount}/{TotalCount} load cases", successCount, caseNames.Length);
            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting design type for multiple cases: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets load cases by design type option.
    /// </summary>
    public List<string> GetLoadCasesByDesignType(int designTypeOption)
    {
        try
        {
            if (designTypeOption < 1 || designTypeOption > 3)
                throw new ArgumentException("Design type option must be 1 (Strength), 2 (Serviceability), or 3 (Other)", nameof(designTypeOption));

            var allCases = GetAllLoadCases();
            return allCases
                .Where(c => c.DesignTypeOption == designTypeOption)
                .Select(c => c.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load cases by design type: {ex.Message}", ex);
        }
    }

    #endregion
}
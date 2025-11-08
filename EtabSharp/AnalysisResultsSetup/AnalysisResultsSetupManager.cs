using EtabSharp.AnalysisResultsSetup.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.AnalysisResults;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResultsSetup;

/// <summary>
/// Manages analysis results setup in the ETABS model.
/// Implements the IAnalysisResultsSetup interface by wrapping cSapModel.Results.Setup operations.
/// </summary>
public class AnalysisResultsSetupManager : IAnalysisResultsSetup
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the AnalysisResultsSetupManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public AnalysisResultsSetupManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Case and Combo Selection

    /// <summary>
    /// Deselects all load cases and combinations for output.
    /// Wraps cSapModel.Results.Setup.DeselectAllCasesAndCombosForOutput.
    /// </summary>
    public int DeselectAllCasesAndCombosForOutput()
    {
        try
        {
            int ret = _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

            if (ret != 0)
                throw new EtabsException(ret, "DeselectAllCasesAndCombosForOutput", "Failed to deselect all cases and combos");

            _logger.LogDebug("Deselected all cases and combinations for output");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deselecting all cases and combos: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets whether a load case is selected for output.
    /// Wraps cSapModel.Results.Setup.GetCaseSelectedForOutput.
    /// </summary>
    public bool GetCaseSelectedForOutput(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Case name cannot be null or empty", nameof(name));

            bool selected = false;
            int ret = _sapModel.Results.Setup.GetCaseSelectedForOutput(name, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetCaseSelectedForOutput", $"Failed to get selection status for case '{name}'");

            return selected;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting case selection status for '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets whether a load combination is selected for output.
    /// Wraps cSapModel.Results.Setup.GetComboSelectedForOutput.
    /// </summary>
    public bool GetComboSelectedForOutput(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Combo name cannot be null or empty", nameof(name));

            bool selected = false;
            int ret = _sapModel.Results.Setup.GetComboSelectedForOutput(name, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetComboSelectedForOutput", $"Failed to get selection status for combo '{name}'");

            return selected;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting combo selection status for '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets whether a load case is selected for output.
    /// Wraps cSapModel.Results.Setup.SetCaseSelectedForOutput.
    /// </summary>
    public int SetCaseSelectedForOutput(string name, bool selected = true)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Case name cannot be null or empty", nameof(name));

            int ret = _sapModel.Results.Setup.SetCaseSelectedForOutput(name, selected);

            if (ret != 0)
                throw new EtabsException(ret, "SetCaseSelectedForOutput", $"Failed to set selection for case '{name}'");

            _logger.LogDebug("Set case {Name} output selection: {Selected}", name, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting case selection for '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets whether a load combination is selected for output.
    /// Wraps cSapModel.Results.Setup.SetComboSelectedForOutput.
    /// </summary>
    public int SetComboSelectedForOutput(string name, bool selected = true)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Combo name cannot be null or empty", nameof(name));

            int ret = _sapModel.Results.Setup.SetComboSelectedForOutput(name, selected);

            if (ret != 0)
                throw new EtabsException(ret, "SetComboSelectedForOutput", $"Failed to set selection for combo '{name}'");

            _logger.LogDebug("Set combo {Name} output selection: {Selected}", name, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting combo selection for '{name}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Base Reaction Location

    /// <summary>
    /// Gets the base reaction location coordinates.
    /// Wraps cSapModel.Results.Setup.GetOptionBaseReactLoc.
    /// </summary>
    public BaseReactionLocation GetOptionBaseReactLoc()
    {
        try
        {
            double gx = 0, gy = 0, gz = 0;
            int ret = _sapModel.Results.Setup.GetOptionBaseReactLoc(ref gx, ref gy, ref gz);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionBaseReactLoc", "Failed to get base reaction location");

            return new BaseReactionLocation(gx, gy, gz);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting base reaction location: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the base reaction location coordinates.
    /// Wraps cSapModel.Results.Setup.SetOptionBaseReactLoc.
    /// </summary>
    public int SetOptionBaseReactLoc(double gx, double gy, double gz)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionBaseReactLoc(gx, gy, gz);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionBaseReactLoc", "Failed to set base reaction location");

            _logger.LogDebug("Set base reaction location: ({GX}, {GY}, {GZ})", gx, gy, gz);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting base reaction location: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the base reaction location using a model.
    /// </summary>
    public int SetOptionBaseReactLoc(BaseReactionLocation location)
    {
        if (location == null)
            throw new ArgumentNullException(nameof(location));

        return SetOptionBaseReactLoc(location.GlobalX, location.GlobalY, location.GlobalZ);
    }

    #endregion

    #region Buckling Mode Options

    /// <summary>
    /// Gets the buckling mode range for output.
    /// Wraps cSapModel.Results.Setup.GetOptionBucklingMode.
    /// </summary>
    public BucklingModeSettings GetOptionBucklingMode()
    {
        try
        {
            int modeStart = 1, modeEnd = 1;
            bool allModes = false;

            int ret = _sapModel.Results.Setup.GetOptionBucklingMode(ref modeStart, ref modeEnd, ref allModes);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionBucklingMode", "Failed to get buckling mode options");

            return new BucklingModeSettings(modeStart, modeEnd, allModes);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting buckling mode options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the buckling mode range for output.
    /// Wraps cSapModel.Results.Setup.SetOptionBucklingMode.
    /// </summary>
    public int SetOptionBucklingMode(int buckModeStart, int buckModeEnd, bool buckModeAll = false)
    {
        try
        {
            if (buckModeStart < 1)
                throw new ArgumentException("Buckling mode start must be >= 1", nameof(buckModeStart));
            if (buckModeEnd < buckModeStart)
                throw new ArgumentException("Buckling mode end must be >= start", nameof(buckModeEnd));

            int ret = _sapModel.Results.Setup.SetOptionBucklingMode(buckModeStart, buckModeEnd, buckModeAll);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionBucklingMode", "Failed to set buckling mode options");

            _logger.LogDebug("Set buckling mode range: {Start} to {End}, All={All}", buckModeStart, buckModeEnd, buckModeAll);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting buckling mode options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the buckling mode range using a model.
    /// </summary>
    public int SetOptionBucklingMode(BucklingModeSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        return SetOptionBucklingMode(settings.ModeStart, settings.ModeEnd, settings.AllModes);
    }

    #endregion

    #region Mode Shape Options

    /// <summary>
    /// Gets the mode shape range for output.
    /// Wraps cSapModel.Results.Setup.GetOptionModeShape.
    /// </summary>
    public ModeShapeSettings GetOptionModeShape()
    {
        try
        {
            int modeStart = 1, modeEnd = 12;
            bool allModes = false;

            int ret = _sapModel.Results.Setup.GetOptionModeShape(ref modeStart, ref modeEnd, ref allModes);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionModeShape", "Failed to get mode shape options");

            return new ModeShapeSettings(modeStart, modeEnd, allModes);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting mode shape options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the mode shape range for output.
    /// Wraps cSapModel.Results.Setup.SetOptionModeShape.
    /// </summary>
    public int SetOptionModeShape(int modeShapeStart, int modeShapeEnd, bool modeShapesAll = false)
    {
        try
        {
            if (modeShapeStart < 1)
                throw new ArgumentException("Mode shape start must be >= 1", nameof(modeShapeStart));
            if (modeShapeEnd < modeShapeStart)
                throw new ArgumentException("Mode shape end must be >= start", nameof(modeShapeEnd));

            int ret = _sapModel.Results.Setup.SetOptionModeShape(modeShapeStart, modeShapeEnd, modeShapesAll);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionModeShape", "Failed to set mode shape options");

            _logger.LogDebug("Set mode shape range: {Start} to {End}, All={All}", modeShapeStart, modeShapeEnd, modeShapesAll);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting mode shape options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the mode shape range using a model.
    /// </summary>
    public int SetOptionModeShape(ModeShapeSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        return SetOptionModeShape(settings.ModeStart, settings.ModeEnd, settings.AllModes);
    }

    #endregion

    #region Time History and Nonlinear Options

    /// <summary>
    /// Gets the direct history output option.
    /// Wraps cSapModel.Results.Setup.GetOptionDirectHist.
    /// </summary>
    public int GetOptionDirectHist()
    {
        try
        {
            int value = 0;
            int ret = _sapModel.Results.Setup.GetOptionDirectHist(ref value);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionDirectHist", "Failed to get direct history option");

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting direct history option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the direct history output option.
    /// Wraps cSapModel.Results.Setup.SetOptionDirectHist.
    /// </summary>
    public int SetOptionDirectHist(int value)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionDirectHist(value);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionDirectHist", "Failed to set direct history option");

            _logger.LogDebug("Set direct history option: {Value}", value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting direct history option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the modal history output option.
    /// Wraps cSapModel.Results.Setup.GetOptionModalHist.
    /// </summary>
    public int GetOptionModalHist()
    {
        try
        {
            int value = 0;
            int ret = _sapModel.Results.Setup.GetOptionModalHist(ref value);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionModalHist", "Failed to get modal history option");

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting modal history option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the modal history output option.
    /// Wraps cSapModel.Results.Setup.SetOptionModalHist.
    /// </summary>
    public int SetOptionModalHist(int value)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionModalHist(value);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionModalHist", "Failed to set modal history option");

            _logger.LogDebug("Set modal history option: {Value}", value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting modal history option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the multi-step static output option.
    /// Wraps cSapModel.Results.Setup.GetOptionMultiStepStatic.
    /// </summary>
    public int GetOptionMultiStepStatic()
    {
        try
        {
            int value = 0;
            int ret = _sapModel.Results.Setup.GetOptionMultiStepStatic(ref value);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionMultiStepStatic", "Failed to get multi-step static option");

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting multi-step static option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the multi-step static output option.
    /// Wraps cSapModel.Results.Setup.SetOptionMultiStepStatic.
    /// </summary>
    public int SetOptionMultiStepStatic(int value)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionMultiStepStatic(value);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionMultiStepStatic", "Failed to set multi-step static option");

            _logger.LogDebug("Set multi-step static option: {Value}", value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting multi-step static option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the nonlinear static output option.
    /// Wraps cSapModel.Results.Setup.GetOptionNLStatic.
    /// </summary>
    public int GetOptionNLStatic()
    {
        try
        {
            int value = 0;
            int ret = _sapModel.Results.Setup.GetOptionNLStatic(ref value);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionNLStatic", "Failed to get nonlinear static option");

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting nonlinear static option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the nonlinear static output option.
    /// Wraps cSapModel.Results.Setup.SetOptionNLStatic.
    /// </summary>
    public int SetOptionNLStatic(int value)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionNLStatic(value);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionNLStatic", "Failed to set nonlinear static option");

            _logger.LogDebug("Set nonlinear static option: {Value}", value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting nonlinear static option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the multi-valued combination output option.
    /// Wraps cSapModel.Results.Setup.GetOptionMultiValuedCombo.
    /// </summary>
    public int GetOptionMultiValuedCombo()
    {
        try
        {
            int value = 0;
            int ret = _sapModel.Results.Setup.GetOptionMultiValuedCombo(ref value);

            if (ret != 0)
                throw new EtabsException(ret, "GetOptionMultiValuedCombo", "Failed to get multi-valued combo option");

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting multi-valued combo option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the multi-valued combination output option.
    /// Wraps cSapModel.Results.Setup.SetOptionMultiValuedCombo.
    /// </summary>
    public int SetOptionMultiValuedCombo(int value)
    {
        try
        {
            int ret = _sapModel.Results.Setup.SetOptionMultiValuedCombo(value);

            if (ret != 0)
                throw new EtabsException(ret, "SetOptionMultiValuedCombo", "Failed to set multi-valued combo option");

            _logger.LogDebug("Set multi-valued combo option: {Value}", value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting multi-valued combo option: {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Selects all load cases for output.
    /// </summary>
    public int SelectCasesForOutput(string[] caseNames)
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
                    if (SetCaseSelectedForOutput(caseName, true) == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to select case {CaseName}: {Error}", caseName, ex.Message);
                }
            }

            _logger.LogDebug("Selected {Count}/{Total} cases for output", successCount, caseNames.Length);
            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error selecting cases for output: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Selects all load combinations for output.
    /// </summary>
    public int SelectCombosForOutput(string[] comboNames)
    {
        try
        {
            if (comboNames == null || comboNames.Length == 0)
                throw new ArgumentException("Combo names array cannot be null or empty", nameof(comboNames));

            int successCount = 0;
            foreach (var comboName in comboNames)
            {
                try
                {
                    if (SetComboSelectedForOutput(comboName, true) == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to select combo {ComboName}: {Error}", comboName, ex.Message);
                }
            }

            _logger.LogDebug("Selected {Count}/{Total} combos for output", successCount, comboNames.Length);
            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error selecting combos for output: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deselects specific load cases from output.
    /// </summary>
    public int DeselectCasesForOutput(string[] caseNames)
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
                    if (SetCaseSelectedForOutput(caseName, false) == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to deselect case {CaseName}: {Error}", caseName, ex.Message);
                }
            }

            _logger.LogDebug("Deselected {Count}/{Total} cases from output", successCount, caseNames.Length);
            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deselecting cases from output: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deselects specific load combinations from output.
    /// </summary>
    public int DeselectCombosForOutput(string[] comboNames)
    {
        try
        {
            if (comboNames == null || comboNames.Length == 0)
                throw new ArgumentException("Combo names array cannot be null or empty", nameof(comboNames));

            int successCount = 0;
            foreach (var comboName in comboNames)
            {
                try
                {
                    if (SetComboSelectedForOutput(comboName, false) == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to deselect combo {ComboName}: {Error}", comboName, ex.Message);
                }
            }

            _logger.LogDebug("Deselected {Count}/{Total} combos from output", successCount, comboNames.Length);
            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deselecting combos from output: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets base reaction location at origin (0, 0, 0).
    /// </summary>
    public int SetBaseReactionAtOrigin()
    {
        return SetOptionBaseReactLoc(0, 0, 0);
    }

    /// <summary>
    /// Sets output to include all buckling modes.
    /// </summary>
    public int SetAllBucklingModes()
    {
        return SetOptionBucklingMode(1, 1, true);
    }

    /// <summary>
    /// Sets output to include all mode shapes.
    /// </summary>
    public int SetAllModeShapes()
    {
        return SetOptionModeShape(1, 1, true);
    }

    /// <summary>
    /// Sets output to include first N mode shapes.
    /// </summary>
    public int SetFirstNModeShapes(int count)
    {
        if (count < 1)
            throw new ArgumentException("Mode count must be >= 1", nameof(count));

        return SetOptionModeShape(1, count, false);
    }

    /// <summary>
    /// Gets a summary of current analysis results setup.
    /// </summary>
    public AnalysisResultsSetupSummary GetSetupSummary(string[] caseNames, string[] comboNames)
    {
        try
        {
            var summary = new AnalysisResultsSetupSummary
            {
                BaseReactionLocation = GetOptionBaseReactLoc(),
                BucklingModes = GetOptionBucklingMode(),
                ModeShapes = GetOptionModeShape(),
                DirectHistoryOption = GetOptionDirectHist(),
                ModalHistoryOption = GetOptionModalHist(),
                MultiStepStaticOption = GetOptionMultiStepStatic(),
                NonlinearStaticOption = GetOptionNLStatic(),
                MultiValuedComboOption = GetOptionMultiValuedCombo()
            };

            // Count selected cases
            if (caseNames != null)
            {
                foreach (var caseName in caseNames)
                {
                    try
                    {
                        if (GetCaseSelectedForOutput(caseName))
                            summary.SelectedCasesCount++;
                    }
                    catch { }
                }
            }

            // Count selected combos
            if (comboNames != null)
            {
                foreach (var comboName in comboNames)
                {
                    try
                    {
                        if (GetComboSelectedForOutput(comboName))
                            summary.SelectedCombosCount++;
                    }
                    catch { }
                }
            }

            _logger.LogDebug("Generated analysis results setup summary");
            return summary;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting setup summary: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Configures output for typical linear static analysis.
    /// </summary>
    public int ConfigureForLinearStatic(string[] caseNames, string[] comboNames)
    {
        try
        {
            // Select all cases and combos
            SelectCasesForOutput(caseNames);
            SelectCombosForOutput(comboNames);

            // Set first 12 mode shapes (typical)
            SetFirstNModeShapes(12);

            // Set base reaction at origin
            SetBaseReactionAtOrigin();

            _logger.LogDebug("Configured output for linear static analysis");
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error configuring for linear static: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Configures output for modal analysis.
    /// </summary>
    public int ConfigureForModalAnalysis(string[] modalCaseNames, int numberOfModes = 12)
    {
        try
        {
            // Select modal cases
            SelectCasesForOutput(modalCaseNames);

            // Set mode shapes
            SetFirstNModeShapes(numberOfModes);

            _logger.LogDebug("Configured output for modal analysis with {NumberOfModes} modes", numberOfModes);
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error configuring for modal analysis: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Configures output for time history analysis.
    /// </summary>
    public int ConfigureForTimeHistory(string[] historyCaseNames, bool outputAllSteps = false)
    {
        try
        {
            // Select history cases
            SelectCasesForOutput(historyCaseNames);

            // Set output options (1 = all steps, 2 = last step)
            int outputOption = outputAllSteps ? 1 : 2;
            SetOptionDirectHist(outputOption);
            SetOptionModalHist(outputOption);

            _logger.LogDebug("Configured output for time history analysis, all steps: {AllSteps}", outputAllSteps);
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error configuring for time history: {ex.Message}", ex);
        }
    }

    #endregion
}
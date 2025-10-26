using EtabSharp.AnalysisResults.Models.AnalysisResultsSetup;

namespace EtabSharp.Interfaces.AnalysisResults;

/// <summary>
/// Interface for managing analysis results setup and output options in ETABS.
/// Controls which cases/combos are output and how results are stored.
/// </summary>
public interface IAnalysisResultsSetup
{
    #region Case and Combo Selection

    /// <summary>
    /// Deselects all load cases and combinations for output.
    /// Wraps cSapModel.Results.Setup.DeselectAllCasesAndCombosForOutput.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeselectAllCasesAndCombosForOutput();

    /// <summary>
    /// Gets whether a load case is selected for output.
    /// Wraps cSapModel.Results.Setup.GetCaseSelectedForOutput.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <returns>True if selected for output, false otherwise</returns>
    bool GetCaseSelectedForOutput(string name);

    /// <summary>
    /// Gets whether a load combination is selected for output.
    /// Wraps cSapModel.Results.Setup.GetComboSelectedForOutput.
    /// </summary>
    /// <param name="name">Name of the load combination</param>
    /// <returns>True if selected for output, false otherwise</returns>
    bool GetComboSelectedForOutput(string name);

    /// <summary>
    /// Sets whether a load case is selected for output.
    /// Wraps cSapModel.Results.Setup.SetCaseSelectedForOutput.
    /// </summary>
    /// <param name="name">Name of the load case</param>
    /// <param name="selected">True to select for output, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetCaseSelectedForOutput(string name, bool selected = true);

    /// <summary>
    /// Sets whether a load combination is selected for output.
    /// Wraps cSapModel.Results.Setup.SetComboSelectedForOutput.
    /// </summary>
    /// <param name="name">Name of the load combination</param>
    /// <param name="selected">True to select for output, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetComboSelectedForOutput(string name, bool selected = true);

    #endregion

    #region Base Reaction Location

    /// <summary>
    /// Gets the base reaction location coordinates.
    /// Wraps cSapModel.Results.Setup.GetOptionBaseReactLoc.
    /// </summary>
    /// <returns>BaseReactionLocation with coordinates</returns>
    BaseReactionLocation GetOptionBaseReactLoc();

    /// <summary>
    /// Sets the base reaction location coordinates.
    /// Wraps cSapModel.Results.Setup.SetOptionBaseReactLoc.
    /// </summary>
    /// <param name="gx">Global X coordinate</param>
    /// <param name="gy">Global Y coordinate</param>
    /// <param name="gz">Global Z coordinate</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionBaseReactLoc(double gx, double gy, double gz);

    /// <summary>
    /// Sets the base reaction location using a model.
    /// </summary>
    /// <param name="location">BaseReactionLocation model</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionBaseReactLoc(BaseReactionLocation location);

    #endregion

    #region Buckling Mode Options

    /// <summary>
    /// Gets the buckling mode range for output.
    /// Wraps cSapModel.Results.Setup.GetOptionBucklingMode.
    /// </summary>
    /// <returns>BucklingModeSettings with mode range</returns>
    BucklingModeSettings GetOptionBucklingMode();

    /// <summary>
    /// Sets the buckling mode range for output.
    /// Wraps cSapModel.Results.Setup.SetOptionBucklingMode.
    /// </summary>
    /// <param name="buckModeStart">Starting mode number</param>
    /// <param name="buckModeEnd">Ending mode number</param>
    /// <param name="buckModeAll">True to include all modes</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionBucklingMode(int buckModeStart, int buckModeEnd, bool buckModeAll = false);

    /// <summary>
    /// Sets the buckling mode range using a model.
    /// </summary>
    /// <param name="settings">BucklingModeSettings model</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionBucklingMode(BucklingModeSettings settings);

    #endregion

    #region Mode Shape Options

    /// <summary>
    /// Gets the mode shape range for output.
    /// Wraps cSapModel.Results.Setup.GetOptionModeShape.
    /// </summary>
    /// <returns>ModeShapeSettings with mode range</returns>
    ModeShapeSettings GetOptionModeShape();

    /// <summary>
    /// Sets the mode shape range for output.
    /// Wraps cSapModel.Results.Setup.SetOptionModeShape.
    /// </summary>
    /// <param name="modeShapeStart">Starting mode number</param>
    /// <param name="modeShapeEnd">Ending mode number</param>
    /// <param name="modeShapesAll">True to include all modes</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionModeShape(int modeShapeStart, int modeShapeEnd, bool modeShapesAll = false);

    /// <summary>
    /// Sets the mode shape range using a model.
    /// </summary>
    /// <param name="settings">ModeShapeSettings model</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionModeShape(ModeShapeSettings settings);

    #endregion

    #region Time History and Nonlinear Options

    /// <summary>
    /// Gets the direct history output option.
    /// Wraps cSapModel.Results.Setup.GetOptionDirectHist.
    /// </summary>
    /// <returns>Output option value</returns>
    int GetOptionDirectHist();

    /// <summary>
    /// Sets the direct history output option.
    /// Wraps cSapModel.Results.Setup.SetOptionDirectHist.
    /// </summary>
    /// <param name="value">Output option value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionDirectHist(int value);

    /// <summary>
    /// Gets the modal history output option.
    /// Wraps cSapModel.Results.Setup.GetOptionModalHist.
    /// </summary>
    /// <returns>Output option value</returns>
    int GetOptionModalHist();

    /// <summary>
    /// Sets the modal history output option.
    /// Wraps cSapModel.Results.Setup.SetOptionModalHist.
    /// </summary>
    /// <param name="value">Output option value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionModalHist(int value);

    /// <summary>
    /// Gets the multi-step static output option.
    /// Wraps cSapModel.Results.Setup.GetOptionMultiStepStatic.
    /// </summary>
    /// <returns>Output option value</returns>
    int GetOptionMultiStepStatic();

    /// <summary>
    /// Sets the multi-step static output option.
    /// Wraps cSapModel.Results.Setup.SetOptionMultiStepStatic.
    /// </summary>
    /// <param name="value">Output option value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionMultiStepStatic(int value);

    /// <summary>
    /// Gets the nonlinear static output option.
    /// Wraps cSapModel.Results.Setup.GetOptionNLStatic.
    /// </summary>
    /// <returns>Output option value</returns>
    int GetOptionNLStatic();

    /// <summary>
    /// Sets the nonlinear static output option.
    /// Wraps cSapModel.Results.Setup.SetOptionNLStatic.
    /// </summary>
    /// <param name="value">Output option value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionNLStatic(int value);

    /// <summary>
    /// Gets the multi-valued combination output option.
    /// Wraps cSapModel.Results.Setup.GetOptionMultiValuedCombo.
    /// </summary>
    /// <returns>Output option value</returns>
    int GetOptionMultiValuedCombo();

    /// <summary>
    /// Sets the multi-valued combination output option.
    /// Wraps cSapModel.Results.Setup.SetOptionMultiValuedCombo.
    /// </summary>
    /// <param name="value">Output option value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOptionMultiValuedCombo(int value);

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Selects all load cases for output.
    /// </summary>
    /// <param name="caseNames">Array of case names to select</param>
    /// <returns>Number of cases successfully selected</returns>
    int SelectCasesForOutput(string[] caseNames);

    /// <summary>
    /// Selects all load combinations for output.
    /// </summary>
    /// <param name="comboNames">Array of combo names to select</param>
    /// <returns>Number of combos successfully selected</returns>
    int SelectCombosForOutput(string[] comboNames);

    /// <summary>
    /// Deselects specific load cases from output.
    /// </summary>
    /// <param name="caseNames">Array of case names to deselect</param>
    /// <returns>Number of cases successfully deselected</returns>
    int DeselectCasesForOutput(string[] caseNames);

    /// <summary>
    /// Deselects specific load combinations from output.
    /// </summary>
    /// <param name="comboNames">Array of combo names to deselect</param>
    /// <returns>Number of combos successfully deselected</returns>
    int DeselectCombosForOutput(string[] comboNames);

    /// <summary>
    /// Sets base reaction location at origin (0, 0, 0).
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetBaseReactionAtOrigin();

    /// <summary>
    /// Sets output to include all buckling modes.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAllBucklingModes();

    /// <summary>
    /// Sets output to include all mode shapes.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAllModeShapes();

    /// <summary>
    /// Sets output to include first N mode shapes.
    /// </summary>
    /// <param name="count">Number of modes to include</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetFirstNModeShapes(int count);

    /// <summary>
    /// Gets a summary of current analysis results setup.
    /// </summary>
    /// <param name="caseNames">Array of case names to check</param>
    /// <param name="comboNames">Array of combo names to check</param>
    /// <returns>AnalysisResultsSetupSummary with current settings</returns>
    AnalysisResultsSetupSummary GetSetupSummary(string[] caseNames, string[] comboNames);

    /// <summary>
    /// Configures output for typical linear static analysis.
    /// Selects all cases, sets first 12 modes, last step only for nonlinear.
    /// </summary>
    /// <param name="caseNames">Array of case names</param>
    /// <param name="comboNames">Array of combo names</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ConfigureForLinearStatic(string[] caseNames, string[] comboNames);

    /// <summary>
    /// Configures output for modal analysis.
    /// Selects modal cases and specified number of mode shapes.
    /// </summary>
    /// <param name="modalCaseNames">Array of modal case names</param>
    /// <param name="numberOfModes">Number of mode shapes to output</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ConfigureForModalAnalysis(string[] modalCaseNames, int numberOfModes = 12);

    /// <summary>
    /// Configures output for time history analysis.
    /// Sets appropriate output intervals for history cases.
    /// </summary>
    /// <param name="historyCaseNames">Array of time history case names</param>
    /// <param name="outputAllSteps">True to output all steps, false for last step only</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ConfigureForTimeHistory(string[] historyCaseNames, bool outputAllSteps = false);

    #endregion
}
using EtabSharp.Analyzes.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Analyzes;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Analyzes;

/// <summary>
/// Manages analysis operations in the ETABS model.
/// Implements the IAnalyze interface by wrapping cSapModel.Analyze operations.
/// </summary>
public class AnalyzeManager: IAnalyze
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    public AnalyzeManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Model Creation & Analysis

    /// <summary>
    /// Creates the analysis model from the current object-based model.
    /// Wraps cSapModel.Analyze.CreateAnalysisModel.
    /// </summary>
    public int CreateAnalysisModel()
    {
        try
        {
            int ret = _sapModel.Analyze.CreateAnalysisModel();

            if (ret != 0)
                throw new EtabsException(ret, "CreateAnalysisModel", "Failed to create analysis model");

            _logger.LogInformation("Analysis model created successfully");
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error creating analysis model: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Runs the analysis for all cases set to run.
    /// Wraps cSapModel.Analyze.RunAnalysis.
    /// </summary>
    public int RunAnalysis()
    {
        try
        {
            int ret = _sapModel.Analyze.RunAnalysis();

            if (ret != 0)
                throw new EtabsException(ret, "RunAnalysis", "Failed to run analysis");

            _logger.LogInformation("Analysis completed");
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error running analysis: {ex.Message}", ex);
        }
    }

    #endregion

    #region Results Management

    /// <summary>
    /// Deletes analysis results for a specific case or all cases.
    /// Wraps cSapModel.Analyze.DeleteResults.
    /// </summary>
    public int DeleteResults(string caseName, bool all = false)
    {
        try
        {
            int ret = _sapModel.Analyze.DeleteResults(caseName ?? "", all);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteResults",
                    all ? "Failed to delete all results" : $"Failed to delete results for case '{caseName}'");

            _logger.LogDebug(all ? "Deleted all analysis results" : "Deleted results for case {CaseName}", caseName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting results: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Merges analysis results from another model file.
    /// Wraps cSapModel.Analyze.MergeAnalysisResults.
    /// </summary>
    public int MergeAnalysisResults(string sourceFileName)
    {
        try
        {
            if (string.IsNullOrEmpty(sourceFileName))
                throw new ArgumentException("Source file name cannot be null or empty", nameof(sourceFileName));

            int ret = _sapModel.Analyze.MergeAnalysisResults(sourceFileName);

            if (ret != 0)
                throw new EtabsException(ret, "MergeAnalysisResults",
                    $"Failed to merge results from '{sourceFileName}'");

            _logger.LogInformation("Merged analysis results from {FileName}", sourceFileName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error merging results: {ex.Message}", ex);
        }
    }

    #endregion

    #region Case Status & Run Flags

    /// <summary>
    /// Gets the analysis status for all load cases.
    /// Wraps cSapModel.Analyze.GetCaseStatus.
    /// </summary>
    public List<CaseStatus> GetCaseStatus()
    {
        try
        {
            int numberItems = 0;
            string[] caseNames = null;
            int[] statuses = null;

            int ret = _sapModel.Analyze.GetCaseStatus(ref numberItems, ref caseNames, ref statuses);

            if (ret != 0)
                throw new EtabsException(ret, "GetCaseStatus", "Failed to get case status");

            var statusList = new List<CaseStatus>();
            for (int i = 0; i < numberItems; i++)
            {
                statusList.Add(new CaseStatus
                {
                    CaseName = caseNames[i],
                    Status = statuses[i]
                });
            }

            _logger.LogDebug("Retrieved status for {Count} cases", numberItems);
            return statusList;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting case status: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the run flags for all load cases.
    /// Wraps cSapModel.Analyze.GetRunCaseFlag.
    /// </summary>
    public List<CaseRunFlag> GetRunCaseFlags()
    {
        try
        {
            int numberItems = 0;
            string[] caseNames = null;
            bool[] runFlags = null;

            int ret = _sapModel.Analyze.GetRunCaseFlag(ref numberItems, ref caseNames, ref runFlags);

            if (ret != 0)
                throw new EtabsException(ret, "GetRunCaseFlag", "Failed to get run case flags");

            var flagList = new List<CaseRunFlag>();
            for (int i = 0; i < numberItems; i++)
            {
                flagList.Add(new CaseRunFlag
                {
                    CaseName = caseNames[i],
                    Run = runFlags[i]
                });
            }

            _logger.LogDebug("Retrieved run flags for {Count} cases", numberItems);
            return flagList;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting run case flags: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets whether a case should run during analysis.
    /// Wraps cSapModel.Analyze.SetRunCaseFlag.
    /// </summary>
    public int SetRunCaseFlag(string caseName, bool run, bool all = false)
    {
        try
        {
            int ret = _sapModel.Analyze.SetRunCaseFlag(caseName ?? "", run, all);

            if (ret != 0)
                throw new EtabsException(ret, "SetRunCaseFlag",
                    all ? $"Failed to set all cases to {(run ? "run" : "not run")}" :
                          $"Failed to set case '{caseName}' to {(run ? "run" : "not run")}");

            _logger.LogDebug(all ? "Set all cases to {Status}" : "Set case {CaseName} to {Status}",
                run ? "run" : "not run", caseName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting run case flag: {ex.Message}", ex);
        }
    }

    #endregion

    #region Active DOF

    /// <summary>
    /// Gets the active degrees of freedom for analysis.
    /// Wraps cSapModel.Analyze.GetActiveDOF.
    /// </summary>
    public ActiveDOF GetActiveDOF()
    {
        try
        {
            bool[] dof = new bool[6];
            int ret = _sapModel.Analyze.GetActiveDOF(ref dof);

            if (ret != 0)
                throw new EtabsException(ret, "GetActiveDOF", "Failed to get active DOF");

            var activeDOF = ActiveDOF.FromArray(dof);
            _logger.LogDebug("Retrieved active DOF: {DOF}", activeDOF.ToString());
            return activeDOF;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting active DOF: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the active degrees of freedom for analysis.
    /// Wraps cSapModel.Analyze.SetActiveDOF.
    /// </summary>
    public int SetActiveDOF(ActiveDOF dof)
    {
        try
        {
            if (dof == null)
                throw new ArgumentNullException(nameof(dof));

            bool[] dofArray = dof.ToArray();
            int ret = _sapModel.Analyze.SetActiveDOF(ref dofArray);

            if (ret != 0)
                throw new EtabsException(ret, "SetActiveDOF", "Failed to set active DOF");

            _logger.LogDebug("Set active DOF: {DOF}", dof.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting active DOF: {ex.Message}", ex);
        }
    }

    #endregion

    #region Solver Options

    /// <summary>
    /// Gets basic solver options.
    /// Wraps cSapModel.Analyze.GetSolverOption.
    /// </summary>
    public (int SolverType, bool Force32Bit, string StiffCase) GetSolverOption()
    {
        try
        {
            int solverType = 0;
            bool force32Bit = false;
            string stiffCase = "";

            int ret = _sapModel.Analyze.GetSolverOption(ref solverType, ref force32Bit, ref stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "GetSolverOption", "Failed to get solver option");

            return (solverType, force32Bit, stiffCase);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting solver option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets basic solver options.
    /// Wraps cSapModel.Analyze.SetSolverOption.
    /// </summary>
    public int SetSolverOption(int solverType, bool force32BitSolver, string stiffCase = "")
    {
        try
        {
            int ret = _sapModel.Analyze.SetSolverOption(solverType, force32BitSolver, stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "SetSolverOption", "Failed to set solver option");

            _logger.LogDebug("Set solver option: Type={Type}, Force32Bit={Force32Bit}", solverType, force32BitSolver);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting solver option: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets solver options with process type.
    /// Wraps cSapModel.Analyze.GetSolverOption_1.
    /// </summary>
    public (int SolverType, int ProcessType, bool Force32Bit, string StiffCase) GetSolverOptionWithProcess()
    {
        try
        {
            int solverType = 0, processType = 0;
            bool force32Bit = false;
            string stiffCase = "";

            int ret = _sapModel.Analyze.GetSolverOption_1(ref solverType, ref processType, ref force32Bit, ref stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "GetSolverOption_1", "Failed to get solver option with process type");

            return (solverType, processType, force32Bit, stiffCase);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting solver option with process: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets solver options with process type.
    /// Wraps cSapModel.Analyze.SetSolverOption_1.
    /// </summary>
    public int SetSolverOptionWithProcess(int solverType, int solverProcessType, bool force32BitSolver, string stiffCase = "")
    {
        try
        {
            int ret = _sapModel.Analyze.SetSolverOption_1(solverType, solverProcessType, force32BitSolver, stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "SetSolverOption_1", "Failed to set solver option with process type");

            _logger.LogDebug("Set solver option: Type={Type}, ProcessType={ProcessType}", solverType, solverProcessType);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting solver option with process: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets advanced solver options with parallel runs.
    /// Wraps cSapModel.Analyze.GetSolverOption_2.
    /// </summary>
    public (int SolverType, int ProcessType, int ParallelRuns, string StiffCase) GetSolverOptionWithParallel()
    {
        try
        {
            int solverType = 0, processType = 0, parallelRuns = 0;
            string stiffCase = "";

            int ret = _sapModel.Analyze.GetSolverOption_2(ref solverType, ref processType, ref parallelRuns, ref stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "GetSolverOption_2", "Failed to get solver option with parallel runs");

            return (solverType, processType, parallelRuns, stiffCase);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting solver option with parallel: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets advanced solver options with parallel runs.
    /// Wraps cSapModel.Analyze.SetSolverOption_2.
    /// </summary>
    public int SetSolverOptionWithParallel(int solverType, int solverProcessType, int numberParallelRuns, string stiffCase = "")
    {
        try
        {
            int ret = _sapModel.Analyze.SetSolverOption_2(solverType, solverProcessType, numberParallelRuns, stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "SetSolverOption_2", "Failed to set solver option with parallel runs");

            _logger.LogDebug("Set solver option: ParallelRuns={ParallelRuns}", numberParallelRuns);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting solver option with parallel: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets complete solver options.
    /// Wraps cSapModel.Analyze.GetSolverOption_3.
    /// </summary>
    public SolverOptions GetSolverOptions()
    {
        try
        {
            int solverType = 0, processType = 0, parallelRuns = 0, responseFileSize = 0, analysisThreads = 0;
            string stiffCase = "";

            int ret = _sapModel.Analyze.GetSolverOption_3(ref solverType, ref processType, ref parallelRuns,
                ref responseFileSize, ref analysisThreads, ref stiffCase);

            if (ret != 0)
                throw new EtabsException(ret, "GetSolverOption_3", "Failed to get complete solver options");

            // Note: We don't get Force32BitSolver from GetSolverOption_3, so get it separately
            var basicOptions = GetSolverOption();

            var options = new SolverOptions
            {
                SolverType = solverType,
                SolverProcessType = processType,
                Force32BitSolver = basicOptions.Force32Bit,
                NumberParallelRuns = parallelRuns,
                ResponseFileSizeMaxMB = responseFileSize,
                NumberAnalysisThreads = analysisThreads,
                StiffnessCase = stiffCase
            };

            _logger.LogDebug("Retrieved complete solver options: {Options}", options.ToString());
            return options;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting complete solver options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets complete solver options.
    /// Wraps cSapModel.Analyze.SetSolverOption_3.
    /// </summary>
    public int SetSolverOptions(SolverOptions options)
    {
        try
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            int ret = _sapModel.Analyze.SetSolverOption_3(
                options.SolverType,
                options.SolverProcessType,
                options.NumberParallelRuns,
                options.ResponseFileSizeMaxMB,
                options.NumberAnalysisThreads,
                options.StiffnessCase);

            if (ret != 0)
                throw new EtabsException(ret, "SetSolverOption_3", "Failed to set complete solver options");

            _logger.LogDebug("Set complete solver options: {Options}", options.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting complete solver options: {ex.Message}", ex);
        }
    }

    #endregion

    #region Design & Response Options

    /// <summary>
    /// Gets design and response recovery options.
    /// Wraps cSapModel.Analyze.GetDesignResponseOption.
    /// </summary>
    public DesignResponseOptions GetDesignResponseOptions()
    {
        try
        {
            int designThreads = 0, responseThreads = 0, memoryMapped = 0;
            bool modelDifferencesOK = false;

            int ret = _sapModel.Analyze.GetDesignResponseOption(ref designThreads, ref responseThreads,
                ref memoryMapped, ref modelDifferencesOK);

            if (ret != 0)
                throw new EtabsException(ret, "GetDesignResponseOption", "Failed to get design response options");

            var options = new DesignResponseOptions
            {
                NumberDesignThreads = designThreads,
                NumberResponseRecoveryThreads = responseThreads,
                UseMemoryMappedFilesForResponseRecovery = memoryMapped,
                ModelDifferencesOKWhenMergingResults = modelDifferencesOK
            };

            _logger.LogDebug("Retrieved design response options: {Options}", options.ToString());
            return options;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting design response options: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets design and response recovery options.
    /// Wraps cSapModel.Analyze.SetDesignResponseOption.
    /// </summary>
    public int SetDesignResponseOptions(DesignResponseOptions options)
    {
        try
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            int ret = _sapModel.Analyze.SetDesignResponseOption(
                options.NumberDesignThreads,
                options.NumberResponseRecoveryThreads,
                options.UseMemoryMappedFilesForResponseRecovery,
                options.ModelDifferencesOKWhenMergingResults);

            if (ret != 0)
                throw new EtabsException(ret, "SetDesignResponseOption", "Failed to set design response options");

            _logger.LogDebug("Set design response options: {Options}", options.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting design response options: {ex.Message}", ex);
        }
    }

    #endregion

    #region Geometry Modification

    /// <summary>
    /// Modifies undeformed geometry based on case displacements.
    /// Wraps cSapModel.Analyze.ModifyUndeformedGeometry.
    /// </summary>
    public int ModifyUndeformedGeometry(string caseName, double scaleFactor, int stage = -1, bool restoreOriginal = false)
    {
        try
        {
            int ret = _sapModel.Analyze.ModifyUndeformedGeometry(caseName ?? "", scaleFactor, stage, restoreOriginal);

            if (ret != 0)
                throw new EtabsException(ret, "ModifyUndeformedGeometry",
                    restoreOriginal ? "Failed to restore original geometry" :
                    $"Failed to modify geometry from case '{caseName}'");

            if (restoreOriginal)
                _logger.LogInformation("Restored original geometry");
            else
                _logger.LogInformation("Modified geometry from case {CaseName}: SF={SF}, Stage={Stage}",
                    caseName, scaleFactor, stage);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error modifying undeformed geometry: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Modifies undeformed geometry using modification object.
    /// </summary>
    public int ModifyUndeformedGeometry(GeometryModification modification)
    {
        if (modification == null)
            throw new ArgumentNullException(nameof(modification));

        return ModifyUndeformedGeometry(modification.CaseName, modification.ScaleFactor,
            modification.Stage, modification.RestoreOriginal);
    }

    /// <summary>
    /// Modifies undeformed geometry based on mode shape.
    /// Wraps cSapModel.Analyze.ModifyUndeformedGeometryModeShape.
    /// </summary>
    public int ModifyUndeformedGeometryModeShape(string caseName, int modeNumber, double maxDisplacement,
        int direction, bool restoreOriginal = false)
    {
        try
        {
            int ret = _sapModel.Analyze.ModifyUndeformedGeometryModeShape(caseName ?? "", modeNumber,
                maxDisplacement, direction, restoreOriginal);

            if (ret != 0)
                throw new EtabsException(ret, "ModifyUndeformedGeometryModeShape",
                    restoreOriginal ? "Failed to restore original geometry" :
                    $"Failed to modify geometry from mode {modeNumber} of case '{caseName}'");

            if (restoreOriginal)
                _logger.LogInformation("Restored original geometry");
            else
                _logger.LogInformation("Modified geometry from mode {Mode} of case {CaseName}: MaxDispl={MaxDispl}, Direction={Direction}",
                    modeNumber, caseName, maxDisplacement, direction);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error modifying geometry from mode shape: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Modifies undeformed geometry using mode shape modification object.
    /// </summary>
    public int ModifyUndeformedGeometryModeShape(ModeShapeModification modification)
    {
        if (modification == null)
            throw new ArgumentNullException(nameof(modification));

        return ModifyUndeformedGeometryModeShape(modification.CaseName, modification.ModeNumber,
            modification.MaxDisplacement, modification.Direction, modification.RestoreOriginal);
    }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Runs a complete analysis sequence: create model, run analysis.
    /// </summary>
    public int RunCompleteAnalysis()
    {
        try
        {
            _logger.LogInformation("Starting complete analysis sequence");

            int ret = CreateAnalysisModel();
            if (ret != 0)
                return ret;

            ret = RunAnalysis();
            if (ret != 0)
                return ret;

            _logger.LogInformation("Complete analysis sequence finished successfully");
            return 0;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error in complete analysis: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets all cases to run.
    /// </summary>
    public int SetAllCasesToRun()
    {
        return SetRunCaseFlag("", true, true);
    }

    /// <summary>
    /// Sets all cases to not run.
    /// </summary>
    public int SetAllCasesToSkip()
    {
        return SetRunCaseFlag("", false, true);
    }

    /// <summary>
    /// Checks if all cases have finished running.
    /// </summary>
    public bool AreAllCasesFinished()
    {
        try
        {
            var statusList = GetCaseStatus();
            return statusList.All(s => s.IsFinished);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes all analysis results.
    /// </summary>
    public int DeleteAllResults()
    {
        return DeleteResults("", true);
    }

    /// <summary>
    /// Restores original undeformed geometry.
    /// </summary>
    public int RestoreOriginalGeometry()
    {
        return ModifyUndeformedGeometry("", 1.0, -1, true);
    }

    #endregion
}
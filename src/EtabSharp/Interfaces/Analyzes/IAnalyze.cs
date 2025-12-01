using EtabSharp.Analyzes.Models;

namespace EtabSharp.Interfaces.Analyzes;


/// <summary>
/// Provides methods for running analysis and managing analysis options in ETABS.
/// This interface handles model analysis, solver settings, and result management.
/// </summary>
public interface IAnalyze
{
    #region Model Creation & Analysis

    /// <summary>
    /// Creates the analysis model from the current object-based model.
    /// This must be called before running analysis.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateAnalysisModel();

    /// <summary>
    /// Runs the analysis for all cases set to run.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int RunAnalysis();

    #endregion

    #region Results Management

    /// <summary>
    /// Deletes analysis results for a specific case or all cases.
    /// </summary>
    /// <param name="caseName">Name of the case (ignored if all=true)</param>
    /// <param name="all">If true, deletes all results</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteResults(string caseName, bool all = false);

    /// <summary>
    /// Merges analysis results from another model file.
    /// </summary>
    /// <param name="sourceFileName">Path to source file with results</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int MergeAnalysisResults(string sourceFileName);

    #endregion

    #region Case Status & Run Flags

    /// <summary>
    /// Gets the analysis status for all load cases.
    /// </summary>
    /// <returns>List of CaseStatus objects</returns>
    List<CaseStatus> GetCaseStatus();

    /// <summary>
    /// Gets the run flags for all load cases.
    /// </summary>
    /// <returns>List of CaseRunFlag objects</returns>
    List<CaseRunFlag> GetRunCaseFlags();

    /// <summary>
    /// Sets whether a case should run during analysis.
    /// </summary>
    /// <param name="caseName">Name of the case (ignored if all=true)</param>
    /// <param name="run">True to run, false to skip</param>
    /// <param name="all">If true, applies to all cases</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetRunCaseFlag(string caseName, bool run, bool all = false);

    #endregion

    #region Active DOF

    /// <summary>
    /// Gets the active degrees of freedom for analysis.
    /// </summary>
    /// <returns>ActiveDOF object with DOF settings</returns>
    ActiveDOF GetActiveDOF();

    /// <summary>
    /// Sets the active degrees of freedom for analysis.
    /// </summary>
    /// <param name="dof">ActiveDOF object with DOF settings</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetActiveDOF(ActiveDOF dof);

    #endregion

    #region Solver Options

    /// <summary>
    /// Gets basic solver options.
    /// </summary>
    /// <returns>Tuple of (SolverType, Force32Bit, StiffCase)</returns>
    (int SolverType, bool Force32Bit, string StiffCase) GetSolverOption();

    /// <summary>
    /// Sets basic solver options.
    /// </summary>
    /// <param name="solverType">Type of solver</param>
    /// <param name="force32BitSolver">Force 32-bit solver</param>
    /// <param name="stiffCase">Stiffness case name</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSolverOption(int solverType, bool force32BitSolver, string stiffCase = "");

    /// <summary>
    /// Gets solver options with process type.
    /// </summary>
    /// <returns>Tuple of (SolverType, ProcessType, Force32Bit, StiffCase)</returns>
    (int SolverType, int ProcessType, bool Force32Bit, string StiffCase) GetSolverOptionWithProcess();

    /// <summary>
    /// Sets solver options with process type.
    /// </summary>
    /// <param name="solverType">Type of solver</param>
    /// <param name="solverProcessType">Process type</param>
    /// <param name="force32BitSolver">Force 32-bit solver</param>
    /// <param name="stiffCase">Stiffness case name</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSolverOptionWithProcess(int solverType, int solverProcessType, bool force32BitSolver, string stiffCase = "");

    /// <summary>
    /// Gets advanced solver options with parallel runs.
    /// </summary>
    /// <returns>Tuple of (SolverType, ProcessType, ParallelRuns, StiffCase)</returns>
    (int SolverType, int ProcessType, int ParallelRuns, string StiffCase) GetSolverOptionWithParallel();

    /// <summary>
    /// Sets advanced solver options with parallel runs.
    /// </summary>
    /// <param name="solverType">Type of solver</param>
    /// <param name="solverProcessType">Process type</param>
    /// <param name="numberParallelRuns">Number of parallel runs</param>
    /// <param name="stiffCase">Stiffness case name</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSolverOptionWithParallel(int solverType, int solverProcessType, int numberParallelRuns,
        string stiffCase = "");

    /// <summary>
    /// Gets complete solver options.
    /// </summary>
    /// <returns>SolverOptions object with all settings</returns>
    SolverOptions GetSolverOptions();

    /// <summary>
    /// Sets complete solver options.
    /// </summary>
    /// <param name="options">SolverOptions object with all settings</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSolverOptions(SolverOptions options);

    #endregion

    #region Design & Response Options

    /// <summary>
    /// Gets design and response recovery options.
    /// </summary>
    /// <returns>DesignResponseOptions object</returns>
    DesignResponseOptions GetDesignResponseOptions();

    /// <summary>
    /// Sets design and response recovery options.
    /// </summary>
    /// <param name="options">DesignResponseOptions object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDesignResponseOptions(DesignResponseOptions options);

    #endregion

    #region Geometry Modification

    /// <summary>
    /// Modifies undeformed geometry based on case displacements.
    /// </summary>
    /// <param name="caseName">Load case name</param>
    /// <param name="scaleFactor">Scale factor for displacements</param>
    /// <param name="stage">Stage number (-1 for all)</param>
    /// <param name="restoreOriginal">True to restore original geometry</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ModifyUndeformedGeometry(string caseName, double scaleFactor, int stage = -1, bool restoreOriginal = false);

    /// <summary>
    /// Modifies undeformed geometry using modification object.
    /// </summary>
    /// <param name="modification">GeometryModification object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ModifyUndeformedGeometry(GeometryModification modification);

    /// <summary>
    /// Modifies undeformed geometry based on mode shape.
    /// </summary>
    /// <param name="caseName">Modal case name</param>
    /// <param name="modeNumber">Mode number</param>
    /// <param name="maxDisplacement">Maximum displacement</param>
    /// <param name="direction">Direction (1-6 for UX,UY,UZ,RX,RY,RZ)</param>
    /// <param name="restoreOriginal">True to restore original geometry</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ModifyUndeformedGeometryModeShape(string caseName, int modeNumber, double maxDisplacement,
        int direction, bool restoreOriginal = false);

    /// <summary>
    /// Modifies undeformed geometry using mode shape modification object.
    /// </summary>
    /// <param name="modification">ModeShapeModification object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ModifyUndeformedGeometryModeShape(ModeShapeModification modification);

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Runs a complete analysis sequence: create model, run analysis.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int RunCompleteAnalysis();

    /// <summary>
    /// Sets all cases to run.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAllCasesToRun();

    /// <summary>
    /// Sets all cases to not run.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAllCasesToSkip();

    /// <summary>
    /// Checks if all cases have finished running.
    /// </summary>
    /// <returns>True if all finished, false otherwise</returns>
    bool AreAllCasesFinished();

    /// <summary>
    /// Deletes all analysis results.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteAllResults();

    /// <summary>
    /// Restores original undeformed geometry.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int RestoreOriginalGeometry();

    #endregion
}
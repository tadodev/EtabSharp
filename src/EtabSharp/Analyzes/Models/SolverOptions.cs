namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Solver options for analysis.
/// </summary>
public class SolverOptions
{
    /// <summary>
    /// Solver type
    /// </summary>
    public int SolverType { get; set; }

    /// <summary>
    /// Solver process type (how solver runs)
    /// </summary>
    public int SolverProcessType { get; set; }

    /// <summary>
    /// Force use of 32-bit solver
    /// </summary>
    public bool Force32BitSolver { get; set; }

    /// <summary>
    /// Number of parallel runs
    /// </summary>
    public int NumberParallelRuns { get; set; } = 1;

    /// <summary>
    /// Maximum response file size in MB
    /// </summary>
    public int ResponseFileSizeMaxMB { get; set; } = 2048;

    /// <summary>
    /// Number of analysis threads
    /// </summary>
    public int NumberAnalysisThreads { get; set; } = 0; // 0 = auto

    /// <summary>
    /// Stiffness case name
    /// </summary>
    public string StiffnessCase { get; set; } = "";

    /// <summary>
    /// Gets solver type description
    /// </summary>
    public string GetSolverTypeDescription()
    {
        return SolverType switch
        {
            0 => "Standard",
            1 => "Advanced",
            2 => "Multi-threaded",
            _ => $"Unknown ({SolverType})"
        };
    }

    public override string ToString()
    {
        return $"Solver: {GetSolverTypeDescription()}, Parallel Runs: {NumberParallelRuns}, Threads: {NumberAnalysisThreads}";
    }
}
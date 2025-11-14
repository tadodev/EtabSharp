namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Represents the run flag setting for a load case.
/// </summary>
public class CaseRunFlag
{
    /// <summary>
    /// Name of the load case
    /// </summary>
    public required string CaseName { get; set; }

    /// <summary>
    /// Whether the case is set to run
    /// </summary>
    public bool Run { get; set; }

    public override string ToString()
    {
        return $"{CaseName}: {(Run ? "Will Run" : "Will Not Run")}";
    }
}
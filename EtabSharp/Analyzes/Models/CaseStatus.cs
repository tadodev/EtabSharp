namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Represents the status of a load case analysis.
/// </summary>
public class CaseStatus
{
    /// <summary>
    /// Name of the load case
    /// </summary>
    public required string CaseName { get; set; }

    /// <summary>
    /// Status code for the case (1-4)
    /// 1 = Not run
    /// 2 = Could not start
    /// 3 = Not finished
    /// 4 = Finished
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets the status description
    /// </summary>
    public string GetStatusDescription()
    {
        return Status switch
        {
            1 => "Not Run",
            2 => "Could Not Start",
            3 => "Not Finished",
            4 => "Finished",
            _ => $"Unknown ({Status})"
        };
    }

    /// <summary>
    /// Whether the case has finished running
    /// </summary>
    public bool IsFinished => Status == 4;

    /// <summary>
    /// Whether the case has not been run yet
    /// </summary>
    public bool IsNotRun => Status == 1;

    /// <summary>
    /// Whether the case could not start
    /// </summary>
    public bool CouldNotStart => Status == 2;

    /// <summary>
    /// Whether the case is not finished
    /// </summary>
    public bool IsNotFinished => Status == 3;

    public override string ToString()
    {
        return $"{CaseName}: {GetStatusDescription()}";
    }
}
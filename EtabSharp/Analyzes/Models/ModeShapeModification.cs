namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Mode shape geometry modification parameters.
/// </summary>
public class ModeShapeModification
{
    /// <summary>
    /// Modal load case name
    /// </summary>
    public required string CaseName { get; set; }

    /// <summary>
    /// Mode number
    /// </summary>
    public int ModeNumber { get; set; } = 1;

    /// <summary>
    /// Maximum displacement
    /// </summary>
    public double MaxDisplacement { get; set; } = 1.0;

    /// <summary>
    /// Direction (1=X, 2=Y, 3=Z, 4=Resultant)
    /// </summary>
    public int Direction { get; set; } = 3; // Default to Z

    /// <summary>
    /// Restore original geometry
    /// </summary>
    public bool RestoreOriginal { get; set; }

    /// <summary>
    /// Gets direction description
    /// </summary>
    public string GetDirectionDescription()
    {
        return Direction switch
        {
            1 => "X",
            2 => "Y",
            3 => "Z",
            4 => "Resultant",
            _ => $"Unknown ({Direction})"
        };
    }

    public override string ToString()
    {
        if (RestoreOriginal)
            return "Restore Original Geometry";
        return $"Modify Mode {ModeNumber} ({CaseName}): Max Displ={MaxDisplacement:F4}, Direction={GetDirectionDescription()}";
    }
}
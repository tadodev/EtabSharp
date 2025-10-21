namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents wind pressure load on an area (wall).
/// </summary>
public class AreaWindPressureLoad
{
    /// <summary>
    /// Name of the area
    /// </summary>
    public required string AreaName { get; set; }

    /// <summary>
    /// Load pattern name
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Wind pressure type: 0=Windward, 1=Other
    /// </summary>
    public int WindPressureType { get; set; }

    /// <summary>
    /// Wind pressure coefficient (Cp)
    /// </summary>
    public double PressureCoefficient { get; set; }

    public override string ToString()
    {
        return $"{AreaName} [{LoadPattern}]: Cp={PressureCoefficient:F2}";
    }
}
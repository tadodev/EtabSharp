namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents temperature load on an area.
/// </summary>
public class AreaTemperatureLoad
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
    /// Temperature load type: 0=Temperature, 1=Temperature gradient
    /// </summary>
    public int TemperatureType { get; set; }

    /// <summary>
    /// Temperature value or gradient value
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Joint pattern name (if applicable)
    /// </summary>
    public string PatternName { get; set; } = "";

    public override string ToString()
    {
        string type = TemperatureType == 0 ? "Temp" : "Gradient";
        return $"{AreaName} [{LoadPattern}]: {type}={Value:F2}";
    }
}
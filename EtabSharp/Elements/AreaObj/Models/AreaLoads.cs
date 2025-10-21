namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Container for all types of loads that can be applied to an area object.
/// </summary>
public class AreaLoads
{
    /// <summary>
    /// Gets or sets the uniform loads applied to the area.
    /// </summary>
    public List<AreaUniformLoad> UniformLoads { get; set; } = new List<AreaUniformLoad>();

    /// <summary>
    /// Gets or sets the uniform loads transferred to frame objects.
    /// </summary>
    public List<AreaUniformToFrameLoad> UniformToFrameLoads { get; set; } = new List<AreaUniformToFrameLoad>();

    /// <summary>
    /// Gets or sets the wind pressure loads applied to the area.
    /// </summary>
    public List<AreaWindPressureLoad> WindPressureLoads { get; set; } = new List<AreaWindPressureLoad>();

    /// <summary>
    /// Gets or sets the temperature loads applied to the area.
    /// </summary>
    public List<AreaTemperatureLoad> TemperatureLoads { get; set; } = new List<AreaTemperatureLoad>();

    /// <summary>
    /// Gets the total number of loads applied to the area.
    /// </summary>
    public int TotalLoadCount => UniformLoads.Count + UniformToFrameLoads.Count + 
                                WindPressureLoads.Count + TemperatureLoads.Count;

    /// <summary>
    /// Checks if any loads are applied to the area.
    /// </summary>
    /// <returns>True if any loads exist</returns>
    public bool HasLoads()
    {
        return TotalLoadCount > 0;
    }

    /// <summary>
    /// Gets all load patterns used by the area loads.
    /// </summary>
    /// <returns>List of unique load pattern names</returns>
    public List<string> GetLoadPatterns()
    {
        var patterns = new HashSet<string>();
        
        foreach (var load in UniformLoads)
            patterns.Add(load.LoadPattern);
        
        foreach (var load in UniformToFrameLoads)
            patterns.Add(load.LoadPattern);
        
        foreach (var load in WindPressureLoads)
            patterns.Add(load.LoadPattern);
        
        foreach (var load in TemperatureLoads)
            patterns.Add(load.LoadPattern);

        return patterns.ToList();
    }

    /// <summary>
    /// Gets loads for a specific load pattern.
    /// </summary>
    /// <param name="loadPattern">Load pattern name</param>
    /// <returns>AreaLoads containing only loads for the specified pattern</returns>
    public AreaLoads GetLoadsForPattern(string loadPattern)
    {
        return new AreaLoads
        {
            UniformLoads = UniformLoads.Where(l => l.LoadPattern == loadPattern).ToList(),
            UniformToFrameLoads = UniformToFrameLoads.Where(l => l.LoadPattern == loadPattern).ToList(),
            WindPressureLoads = WindPressureLoads.Where(l => l.LoadPattern == loadPattern).ToList(),
            TemperatureLoads = TemperatureLoads.Where(l => l.LoadPattern == loadPattern).ToList()
        };
    }

    /// <summary>
    /// Removes all loads for a specific load pattern.
    /// </summary>
    /// <param name="loadPattern">Load pattern name</param>
    /// <returns>Number of loads removed</returns>
    public int RemoveLoadsForPattern(string loadPattern)
    {
        int removed = 0;
        
        removed += UniformLoads.RemoveAll(l => l.LoadPattern == loadPattern);
        removed += UniformToFrameLoads.RemoveAll(l => l.LoadPattern == loadPattern);
        removed += WindPressureLoads.RemoveAll(l => l.LoadPattern == loadPattern);
        removed += TemperatureLoads.RemoveAll(l => l.LoadPattern == loadPattern);

        return removed;
    }

    /// <summary>
    /// Returns a string representation of the area loads.
    /// </summary>
    /// <returns>String containing load summary</returns>
    public override string ToString()
    {
        return $"Area Loads: {UniformLoads.Count} Uniform, {UniformToFrameLoads.Count} ToFrame, " +
               $"{WindPressureLoads.Count} Wind, {TemperatureLoads.Count} Temperature";
    }
}



/// <summary>
/// Represents offsets for area object points.
/// </summary>
public class AreaOffsets
{
    public string AreaName { get; set; } = string.Empty;
    public List<double> Offsets { get; set; } = new List<double>();

    public override string ToString()
    {
        return $"Area Offsets: {Offsets.Count} points";
    }
}

/// <summary>
/// Represents curved edge information for an area object.
/// </summary>
public class AreaCurvedEdges
{
    public string AreaName { get; set; } = string.Empty;
    public List<CurvedEdge> Edges { get; set; } = new List<CurvedEdge>();

    public override string ToString()
    {
        return $"Curved Edges: {Edges.Count} edges";
    }
}

/// <summary>
/// Represents a single curved edge.
/// </summary>
public class CurvedEdge
{
    public int CurveType { get; set; } = 0;
    public double Tension { get; set; } = 0.0;
    public List<AreaCoordinate> Points { get; set; } = new List<AreaCoordinate>();

    public override string ToString()
    {
        return $"Curved Edge: Type={CurveType}, Tension={Tension:F3}, Points={Points.Count}";
    }
}
namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Container for all types of loads that can be applied to a frame object.
/// </summary>
public class FrameLoads
{
    /// <summary>
    /// Gets or sets the name of the frame object.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distributed loads applied to the frame.
    /// </summary>
    public List<FrameDistributedLoad> DistributedLoads { get; set; } = new List<FrameDistributedLoad>();

    /// <summary>
    /// Gets or sets the point loads applied to the frame.
    /// </summary>
    public List<FramePointLoad> PointLoads { get; set; } = new List<FramePointLoad>();

    /// <summary>
    /// Gets or sets the temperature loads applied to the frame.
    /// </summary>
    public List<FrameTemperatureLoad> TemperatureLoads { get; set; } = new List<FrameTemperatureLoad>();

    /// <summary>
    /// Gets the total number of loads applied to the frame.
    /// </summary>
    public int TotalLoadCount => DistributedLoads.Count + PointLoads.Count + TemperatureLoads.Count;

    /// <summary>
    /// Checks if any loads are applied to the frame.
    /// </summary>
    /// <returns>True if any loads exist</returns>
    public bool HasLoads()
    {
        return TotalLoadCount > 0;
    }

    /// <summary>
    /// Gets all load patterns used by the frame loads.
    /// </summary>
    /// <returns>List of unique load pattern names</returns>
    public List<string> GetLoadPatterns()
    {
        var patterns = new HashSet<string>();

        foreach (var load in DistributedLoads)
            patterns.Add(load.LoadPattern);

        foreach (var load in PointLoads)
            patterns.Add(load.LoadPattern);

        foreach (var load in TemperatureLoads)
            patterns.Add(load.LoadPattern);

        return patterns.ToList();
    }

    /// <summary>
    /// Gets loads for a specific load pattern.
    /// </summary>
    /// <param name="loadPattern">Load pattern name</param>
    /// <returns>FrameLoads containing only loads for the specified pattern</returns>
    public FrameLoads GetLoadsForPattern(string loadPattern)
    {
        return new FrameLoads
        {
            FrameName = FrameName,
            DistributedLoads = DistributedLoads.Where(l => l.LoadPattern == loadPattern).ToList(),
            PointLoads = PointLoads.Where(l => l.LoadPattern == loadPattern).ToList(),
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

        removed += DistributedLoads.RemoveAll(l => l.LoadPattern == loadPattern);
        removed += PointLoads.RemoveAll(l => l.LoadPattern == loadPattern);
        removed += TemperatureLoads.RemoveAll(l => l.LoadPattern == loadPattern);

        return removed;
    }

    /// <summary>
    /// Returns a string representation of the frame loads.
    /// </summary>
    /// <returns>String containing load summary</returns>
    public override string ToString()
    {
        return $"Frame Loads ({FrameName}): {DistributedLoads.Count} Distributed, " +
               $"{PointLoads.Count} Point, {TemperatureLoads.Count} Temperature";
    }
}

/// <summary>
/// Represents a temperature load applied to a frame object.
/// </summary>
public class FrameTemperatureLoad
{
    /// <summary>
    /// Gets or sets the name of the frame object.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the load pattern.
    /// </summary>
    public string LoadPattern { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the temperature load type.
    /// 1 = Temperature, 2 = Temperature gradient
    /// </summary>
    public int LoadType { get; set; } = 1;

    /// <summary>
    /// Gets or sets the temperature change value.
    /// </summary>
    public double TemperatureValue { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the joint pattern name (for gradient loads).
    /// </summary>
    public string JointPatternName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the load case step (for non-linear analysis).
    /// </summary>
    public int LoadCaseStep { get; set; } = 0;

    /// <summary>
    /// Gets a description of the load type.
    /// </summary>
    /// <returns>Load type description</returns>
    public string GetLoadTypeDescription()
    {
        return LoadType switch
        {
            1 => "Uniform Temperature",
            2 => "Temperature Gradient",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Returns a string representation of the temperature load.
    /// </summary>
    /// <returns>String containing load information</returns>
    public override string ToString()
    {
        return $"Temperature Load: {TemperatureValue:F1}° | {GetLoadTypeDescription()} | Pattern: {LoadPattern}";
    }
}
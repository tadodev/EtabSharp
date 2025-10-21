namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents a distributed load applied to a frame object.
/// Follows ETABS API specification for SetLoadDistributed method.
/// </summary>
public class FrameDistributedLoad
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
    /// Gets or sets the type of distributed load.
    /// 1 = Force per unit length [F/L]
    /// 2 = Moment per unit length [FL/L]
    /// </summary>
    public int LoadType { get; set; } = 1;

    /// <summary>
    /// Gets or sets the load direction (1-11).
    /// 1 = Local 1 axis, 2 = Local 2 axis, 3 = Local 3 axis,
    /// 4 = Global X, 5 = Global Y, 6 = Global Z,
    /// 7 = Projected Global X, 8 = Projected Global Y, 9 = Projected Global Z,
    /// 10 = Gravity, 11 = Projected Gravity
    /// </summary>
    public int Direction { get; set; } = 10; // Default to Gravity

    /// <summary>
    /// Gets or sets the distance from the I-End of the frame to the start of the distributed load.
    /// May be relative (0-1) or absolute distance depending on IsRelativeDistance.
    /// </summary>
    public double StartDistance { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the distance from the I-End of the frame to the end of the distributed load.
    /// May be relative (0-1) or absolute distance depending on IsRelativeDistance.
    /// </summary>
    public double EndDistance { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the load value at the start of the distributed load.
    /// [F/L] when LoadType is 1, [FL/L] when LoadType is 2
    /// </summary>
    public double StartLoad { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the load value at the end of the distributed load.
    /// [F/L] when LoadType is 1, [FL/L] when LoadType is 2
    /// </summary>
    public double EndLoad { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the coordinate system for the load.
    /// "Local" or the name of a defined coordinate system.
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Gets or sets whether the distances are relative (true) or absolute (false).
    /// If true, distances are relative (0 <= distance <= 1).
    /// If false, distances are actual distances.
    /// </summary>
    public bool IsRelativeDistance { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the FrameDistributedLoad class.
    /// </summary>
    public FrameDistributedLoad()
    {
    }

    /// <summary>
    /// Initializes a new instance of the FrameDistributedLoad class with specified parameters.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="startLoad">Load value at start</param>
    /// <param name="endLoad">Load value at end</param>
    /// <param name="loadType">Type of load (1=Force, 2=Moment)</param>
    /// <param name="direction">Load direction (1-11)</param>
    /// <param name="startDistance">Start distance</param>
    /// <param name="endDistance">End distance</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <param name="isRelativeDistance">Whether distances are relative</param>
    public FrameDistributedLoad(string frameName, string loadPattern, double startLoad, double endLoad,
        int loadType = 1, int direction = 10, double startDistance = 0.0, double endDistance = 1.0,
        string coordinateSystem = "Global", bool isRelativeDistance = true)
    {
        FrameName = frameName;
        LoadPattern = loadPattern;
        StartLoad = startLoad;
        EndLoad = endLoad;
        LoadType = loadType;
        Direction = direction;
        StartDistance = startDistance;
        EndDistance = endDistance;
        CoordinateSystem = coordinateSystem;
        IsRelativeDistance = isRelativeDistance;
    }

    /// <summary>
    /// Creates a uniform distributed load over the entire frame length.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Uniform load value</param>
    /// <param name="direction">Load direction (default: gravity)</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <returns>FrameDistributedLoad for uniform load</returns>
    public static FrameDistributedLoad CreateUniformLoad(string frameName, string loadPattern, double loadValue,
        int direction = 10, string coordinateSystem = "Global")
    {
        return new FrameDistributedLoad(frameName, loadPattern, loadValue, loadValue, 1, direction, 0.0, 1.0, coordinateSystem, true);
    }

    /// <summary>
    /// Creates a uniform gravity load over the entire frame length.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (positive for downward)</param>
    /// <param name="useProjected">If true, uses projected gravity (11), otherwise standard gravity (10)</param>
    /// <returns>FrameDistributedLoad for gravity load</returns>
    public static FrameDistributedLoad CreateGravityLoad(string frameName, string loadPattern, double loadValue, bool useProjected = false)
    {
        int direction = useProjected ? 11 : 10;
        return new FrameDistributedLoad(frameName, loadPattern, Math.Abs(loadValue), Math.Abs(loadValue), 1, direction, 0.0, 1.0, "Global", true);
    }

    /// <summary>
    /// Creates a triangular distributed load over the entire frame length.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="startLoad">Load value at I-end</param>
    /// <param name="endLoad">Load value at J-end</param>
    /// <param name="direction">Load direction (default: gravity)</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <returns>FrameDistributedLoad for triangular load</returns>
    public static FrameDistributedLoad CreateTriangularLoad(string frameName, string loadPattern, double startLoad, double endLoad,
        int direction = 10, string coordinateSystem = "Global")
    {
        return new FrameDistributedLoad(frameName, loadPattern, startLoad, endLoad, 1, direction, 0.0, 1.0, coordinateSystem, true);
    }

    /// <summary>
    /// Creates a partial distributed load over a specified portion of the frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="startDistance">Start distance (relative 0-1 or absolute)</param>
    /// <param name="endDistance">End distance (relative 0-1 or absolute)</param>
    /// <param name="direction">Load direction (default: gravity)</param>
    /// <param name="isRelative">Whether distances are relative</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <returns>FrameDistributedLoad for partial load</returns>
    public static FrameDistributedLoad CreatePartialLoad(string frameName, string loadPattern, double loadValue,
        double startDistance, double endDistance, int direction = 10, bool isRelative = true, string coordinateSystem = "Global")
    {
        return new FrameDistributedLoad(frameName, loadPattern, loadValue, loadValue, 1, direction, startDistance, endDistance, coordinateSystem, isRelative);
    }

    /// <summary>
    /// Gets a description of the load direction.
    /// </summary>
    /// <returns>Direction description</returns>
    public string GetDirectionDescription()
    {
        return Direction switch
        {
            1 => "Local 1 axis",
            2 => "Local 2 axis",
            3 => "Local 3 axis",
            4 => "Global X direction",
            5 => "Global Y direction",
            6 => "Global Z direction",
            7 => "Projected Global X direction",
            8 => "Projected Global Y direction",
            9 => "Projected Global Z direction",
            10 => "Gravity direction",
            11 => "Projected Gravity direction",
            _ => "Unknown direction"
        };
    }

    /// <summary>
    /// Gets a description of the load type.
    /// </summary>
    /// <returns>Load type description</returns>
    public string GetLoadTypeDescription()
    {
        return LoadType switch
        {
            1 => "Force per unit length",
            2 => "Moment per unit length",
            _ => "Unknown load type"
        };
    }

    /// <summary>
    /// Checks if this is a uniform load (StartLoad = EndLoad).
    /// </summary>
    /// <returns>True if uniform load</returns>
    public bool IsUniform()
    {
        return Math.Abs(StartLoad - EndLoad) < 1e-6;
    }

    /// <summary>
    /// Checks if this is a gravity load.
    /// </summary>
    /// <returns>True if direction is gravity (10) or projected gravity (11)</returns>
    public bool IsGravityLoad()
    {
        return Direction == 10 || Direction == 11;
    }

    /// <summary>
    /// Checks if this is a local direction load.
    /// </summary>
    /// <returns>True if direction is local (1-3)</returns>
    public bool IsLocalLoad()
    {
        return Direction >= 1 && Direction <= 3;
    }

    /// <summary>
    /// Checks if this is a global direction load.
    /// </summary>
    /// <returns>True if direction is global (4-6) or projected global (7-9)</returns>
    public bool IsGlobalLoad()
    {
        return Direction >= 4 && Direction <= 9;
    }

    /// <summary>
    /// Checks if this is a projected direction load.
    /// </summary>
    /// <returns>True if direction is projected (7-9, 11)</returns>
    public bool IsProjectedLoad()
    {
        return (Direction >= 7 && Direction <= 9) || Direction == 11;
    }

    /// <summary>
    /// Validates the load parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(FrameName) &&
               !string.IsNullOrEmpty(LoadPattern) &&
               LoadType >= 1 && LoadType <= 2 &&
               Direction >= 1 && Direction <= 11 &&
               !string.IsNullOrEmpty(CoordinateSystem) &&
               (IsRelativeDistance ? (StartDistance >= 0 && StartDistance <= 1 && EndDistance >= 0 && EndDistance <= 1) : true) &&
               StartDistance <= EndDistance;
    }

    /// <summary>
    /// Returns a string representation of the distributed load.
    /// </summary>
    /// <returns>String containing load information</returns>
    public override string ToString()
    {
        string loadDesc = IsUniform()
            ? $"Uniform: {StartLoad:F2}"
            : $"Triangular: {StartLoad:F2} → {EndLoad:F2}";
        
        string distanceDesc = IsRelativeDistance
            ? $"({StartDistance:F2}-{EndDistance:F2} rel)"
            : $"({StartDistance:F2}-{EndDistance:F2} abs)";

        return $"Distributed Load: {loadDesc} {distanceDesc} | {GetDirectionDescription()} | Pattern: {LoadPattern} | Frame: {FrameName}";
    }

    /// <summary>
    /// Creates a copy of the current load.
    /// </summary>
    /// <returns>Copy of the FrameDistributedLoad</returns>
    public FrameDistributedLoad Clone()
    {
        return new FrameDistributedLoad
        {
            FrameName = FrameName,
            LoadPattern = LoadPattern,
            LoadType = LoadType,
            Direction = Direction,
            StartDistance = StartDistance,
            EndDistance = EndDistance,
            StartLoad = StartLoad,
            EndLoad = EndLoad,
            CoordinateSystem = CoordinateSystem,
            IsRelativeDistance = IsRelativeDistance
        };
    }
}
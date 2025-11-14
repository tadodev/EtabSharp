using ETABSv1;

namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents a uniform load applied to an area object.
/// </summary>
public class AreaUniformLoad
{
    /// <summary>
    /// Gets or sets the name of the area object.
    /// </summary>
    public string AreaName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the load pattern.
    /// </summary>
    public string LoadPattern { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the load value (force per unit area).
    /// </summary>
    public double LoadValue { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the load direction (1-11).
    /// 1 = Local 1 axis, 2 = Local 2 axis, 3 = Local 3 axis,
    /// 4 = Global X, 5 = Global Y, 6 = Global Z,
    /// 7 = Projected Global X, 8 = Projected Global Y, 9 = Projected Global Z,
    /// 10 = Gravity, 11 = Projected Gravity
    /// </summary>
    public int Direction { get; set; } = 6; // Default to Global Z

    /// <summary>
    /// Gets or sets the coordinate system for the load.
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Gets or sets the load case step (for non-linear analysis).
    /// </summary>
    public int LoadCaseStep { get; set; } = 0;

    /// <summary>
    /// Initializes a new instance of the AreaUniformLoad class.
    /// </summary>
    public AreaUniformLoad()
    {
    }

    /// <summary>
    /// Initializes a new instance of the AreaUniformLoad class with specified parameters.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="direction">Load direction</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    public AreaUniformLoad(string areaName, string loadPattern, double loadValue, int direction = 6, string coordinateSystem = "Global")
    {
        AreaName = areaName;
        LoadPattern = loadPattern;
        LoadValue = loadValue;
        Direction = direction;
        CoordinateSystem = coordinateSystem;
    }

    /// <summary>
    /// Gets the direction as an enumeration value.
    /// Note: This method only supports basic directions (1-7). For projected directions (8-11), use Direction property directly.
    /// </summary>
    /// <returns>Load direction enumeration</returns>
    public EAreaLoadDirection GetDirectionEnum()
    {
        return Direction switch
        {
            1 => EAreaLoadDirection.Local1,
            2 => EAreaLoadDirection.Local2,
            3 => EAreaLoadDirection.Local3,
            4 => EAreaLoadDirection.GlobalX,
            5 => EAreaLoadDirection.GlobalY,
            6 => EAreaLoadDirection.GlobalZ,
            7 or 10 => EAreaLoadDirection.Gravity, // Both gravity directions map to same enum
            _ => EAreaLoadDirection.GlobalZ
        };
    }

    /// <summary>
    /// Sets the direction from an enumeration value.
    /// </summary>
    /// <param name="direction">Load direction enumeration</param>
    public void SetDirection(EAreaLoadDirection direction)
    {
        Direction = direction switch
        {
            EAreaLoadDirection.Local1 => 1,
            EAreaLoadDirection.Local2 => 2,
            EAreaLoadDirection.Local3 => 3,
            EAreaLoadDirection.GlobalX => 4,
            EAreaLoadDirection.GlobalY => 5,
            EAreaLoadDirection.GlobalZ => 6,
            EAreaLoadDirection.Gravity => 10, // Use standard gravity direction
            _ => 6
        };
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
    /// Creates a gravity load (downward in gravity direction).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (positive for downward)</param>
    /// <param name="useProjected">If true, uses projected gravity direction (11), otherwise standard gravity (10)</param>
    /// <returns>AreaUniformLoad for gravity</returns>
    public static AreaUniformLoad CreateGravityLoad(string areaName, string loadPattern, double loadValue, bool useProjected = false)
    {
        int direction = useProjected ? 11 : 10;
        return new AreaUniformLoad(areaName, loadPattern, Math.Abs(loadValue), direction, "Global");
    }

    /// <summary>
    /// Creates a pressure load (normal to surface).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="pressure">Pressure value (positive for outward normal)</param>
    /// <returns>AreaUniformLoad for pressure</returns>
    public static AreaUniformLoad CreatePressureLoad(string areaName, string loadPattern, double pressure)
    {
        return new AreaUniformLoad(areaName, loadPattern, pressure, 3, "Local");
    }

    /// <summary>
    /// Creates a uniform load in Global Z direction.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="useProjected">If true, uses projected Global Z direction (9), otherwise standard Global Z (6)</param>
    /// <returns>AreaUniformLoad in Global Z</returns>
    public static AreaUniformLoad CreateGlobalZLoad(string areaName, string loadPattern, double loadValue, bool useProjected = false)
    {
        int direction = useProjected ? 9 : 6;
        return new AreaUniformLoad(areaName, loadPattern, loadValue, direction, "Global");
    }

    /// <summary>
    /// Creates a uniform load in Global X direction.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="useProjected">If true, uses projected Global X direction (7), otherwise standard Global X (4)</param>
    /// <returns>AreaUniformLoad in Global X</returns>
    public static AreaUniformLoad CreateGlobalXLoad(string areaName, string loadPattern, double loadValue, bool useProjected = false)
    {
        int direction = useProjected ? 7 : 4;
        return new AreaUniformLoad(areaName, loadPattern, loadValue, direction, "Global");
    }

    /// <summary>
    /// Creates a uniform load in Global Y direction.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="useProjected">If true, uses projected Global Y direction (8), otherwise standard Global Y (5)</param>
    /// <returns>AreaUniformLoad in Global Y</returns>
    public static AreaUniformLoad CreateGlobalYLoad(string areaName, string loadPattern, double loadValue, bool useProjected = false)
    {
        int direction = useProjected ? 8 : 5;
        return new AreaUniformLoad(areaName, loadPattern, loadValue, direction, "Global");
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
    /// <returns>True if direction is local</returns>
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
        return Direction >= 7 && Direction <= 9 || Direction == 11;
    }

    /// <summary>
    /// Returns a string representation of the uniform load.
    /// </summary>
    /// <returns>String containing load information</returns>
    public override string ToString()
    {
        return $"Uniform Load: {LoadValue:F2} in {GetDirectionDescription()} | Pattern: {LoadPattern} | Area: {AreaName}";
    }

    /// <summary>
    /// Creates a copy of the current load.
    /// </summary>
    /// <returns>Copy of the AreaUniformLoad</returns>
    public AreaUniformLoad Clone()
    {
        return new AreaUniformLoad
        {
            AreaName = AreaName,
            LoadPattern = LoadPattern,
            LoadValue = LoadValue,
            Direction = Direction,
            CoordinateSystem = CoordinateSystem,
            LoadCaseStep = LoadCaseStep
        };
    }

    /// <summary>
    /// Validates the load parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(AreaName) &&
               !string.IsNullOrEmpty(LoadPattern) &&
               Direction >= 1 && Direction <= 11 &&
               !string.IsNullOrEmpty(CoordinateSystem);
    }
}
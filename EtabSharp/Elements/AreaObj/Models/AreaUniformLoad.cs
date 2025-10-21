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
    /// Gets or sets the load direction.
    /// 1 = Local 1 axis, 2 = Local 2 axis, 3 = Local 3 axis,
    /// 4 = Global X, 5 = Global Y, 6 = Global Z, 7 = Gravity
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
    /// </summary>
    /// <returns>Load direction enumeration</returns>
    public eLoadDirection GetDirectionEnum()
    {
        return Direction switch
        {
            1 => eLoadDirection.Local1,
            2 => eLoadDirection.Local2,
            3 => eLoadDirection.Local3,
            4 => eLoadDirection.GlobalX,
            5 => eLoadDirection.GlobalY,
            6 => eLoadDirection.GlobalZ,
            7 => eLoadDirection.Gravity,
            _ => eLoadDirection.GlobalZ
        };
    }

    /// <summary>
    /// Sets the direction from an enumeration value.
    /// </summary>
    /// <param name="direction">Load direction enumeration</param>
    public void SetDirection(eLoadDirection direction)
    {
        Direction = direction switch
        {
            eLoadDirection.Local1 => 1,
            eLoadDirection.Local2 => 2,
            eLoadDirection.Local3 => 3,
            eLoadDirection.GlobalX => 4,
            eLoadDirection.GlobalY => 5,
            eLoadDirection.GlobalZ => 6,
            eLoadDirection.Gravity => 7,
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
            4 => "Global X",
            5 => "Global Y",
            6 => "Global Z",
            7 => "Gravity",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a gravity load (downward in Global Z).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (positive for downward)</param>
    /// <returns>AreaUniformLoad for gravity</returns>
    public static AreaUniformLoad CreateGravityLoad(string areaName, string loadPattern, double loadValue)
    {
        return new AreaUniformLoad(areaName, loadPattern, -Math.Abs(loadValue), 7, "Global");
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
    /// <returns>AreaUniformLoad in Global Z</returns>
    public static AreaUniformLoad CreateGlobalZLoad(string areaName, string loadPattern, double loadValue)
    {
        return new AreaUniformLoad(areaName, loadPattern, loadValue, 6, "Global");
    }

    /// <summary>
    /// Checks if this is a gravity load.
    /// </summary>
    /// <returns>True if direction is gravity</returns>
    public bool IsGravityLoad()
    {
        return Direction == 7;
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
    /// <returns>True if direction is global</returns>
    public bool IsGlobalLoad()
    {
        return Direction >= 4 && Direction <= 6;
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
               Direction >= 1 && Direction <= 7 &&
               !string.IsNullOrEmpty(CoordinateSystem);
    }
}
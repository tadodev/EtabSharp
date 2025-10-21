using ETABSv1;

namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents a point object (joint/node) in the ETABS model.
/// Point objects are the fundamental connection points where frame and area elements connect,
/// loads are applied, and supports are defined.
/// </summary>
public class PointObj
{
    /// <summary>
    /// Gets or sets the unique name of the point object.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X coordinate of the point in current length units.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the point in current length units.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the Z coordinate (elevation) of the point in current length units.
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Gets or sets the coordinate system used for the point coordinates.
    /// Default is "Global".
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Gets or sets the user-defined label for the point.
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the story name where this point is located.
    /// </summary>
    public string Story { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this point is designated as a special point.
    /// Special points can exist without connections to other elements.
    /// </summary>
    public bool IsSpecialPoint { get; set; }

    /// <summary>
    /// Gets or sets the diaphragm assignment for this point.
    /// </summary>
    public PointDiaphragm? Diaphragm { get; set; }

    /// <summary>
    /// Gets or sets the restraint conditions for this point.
    /// </summary>
    public PointRestraint? Restraint { get; set; }

    /// <summary>
    /// Gets or sets the spring properties for this point.
    /// </summary>
    public PointSpring? Spring { get; set; }

    /// <summary>
    /// Gets or sets the mass properties for this point.
    /// </summary>
    public PointMass? Mass { get; set; }

    /// <summary>
    /// Gets or sets the panel zone properties for this point (if applicable).
    /// </summary>
    public PointPanelZone? PanelZone { get; set; }

    /// <summary>
    /// Gets or sets the list of groups this point is assigned to.
    /// </summary>
    public List<string> GroupAssignments { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the connectivity information for this point.
    /// </summary>
    public PointConnectivity? Connectivity { get; set; }

    /// <summary>
    /// Gets or sets whether this point is currently selected.
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets the GUID for this point object.
    /// </summary>
    public string GUID { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the element name associated with this point (if applicable).
    /// </summary>
    public string ElementName { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the PointObj class.
    /// </summary>
    public PointObj()
    {
    }

    /// <summary>
    /// Initializes a new instance of the PointObj class with coordinates.
    /// </summary>
    /// <param name="name">Point name</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <param name="coordinateSystem">Coordinate system (default: "Global")</param>
    public PointObj(string name, double x, double y, double z, string coordinateSystem = "Global")
    {
        Name = name;
        X = x;
        Y = y;
        Z = z;
        CoordinateSystem = coordinateSystem;
    }

    /// <summary>
    /// Returns a string representation of the point object.
    /// </summary>
    public override string ToString()
    {
        return $"Point {Name}: ({X:F3}, {Y:F3}, {Z:F3}) [{CoordinateSystem}]";
    }
}
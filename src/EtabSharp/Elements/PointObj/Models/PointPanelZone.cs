namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents panel zone properties at a point (typically for beam-column connections).
/// </summary>
public class PointPanelZone
{
    /// <summary>
    /// Name of the point
    /// </summary>
    public required string PointName { get; set; }

    /// <summary>
    /// Panel zone property type
    /// 0 = Elastic from column and beam properties
    /// 1 = Elastic user defined
    /// 2 = Nonlinear user defined
    /// </summary>
    public int PropertyType { get; set; }

    /// <summary>
    /// Panel zone thickness (for user defined types)
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    /// Shear stiffness modifier K1
    /// </summary>
    public double K1 { get; set; }

    /// <summary>
    /// Shear stiffness modifier K2
    /// </summary>
    public double K2 { get; set; }

    /// <summary>
    /// Link property name (for nonlinear panel zones)
    /// </summary>
    public string LinkProperty { get; set; } = "";

    /// <summary>
    /// Connectivity option
    /// 0 = Automatic from column and beam
    /// 1 = From column only
    /// 2 = From beam only
    /// </summary>
    public int Connectivity { get; set; }

    /// <summary>
    /// Local axis reference
    /// 1 = From column
    /// 2 = From beam
    /// </summary>
    public int LocalAxisFrom { get; set; }

    /// <summary>
    /// Local axis angle in degrees
    /// </summary>
    public double LocalAxisAngle { get; set; }

    /// <summary>
    /// Creates an elastic panel zone from column and beam properties
    /// </summary>
    public static PointPanelZone ElasticFromElements(string pointName) => new()
    {
        PointName = pointName,
        PropertyType = 0,
        Connectivity = 0,
        LocalAxisFrom = 1
    };

    /// <summary>
    /// Creates an elastic user-defined panel zone
    /// </summary>
    public static PointPanelZone ElasticUserDefined(string pointName, double thickness, double k1, double k2) => new()
    {
        PointName = pointName,
        PropertyType = 1,
        Thickness = thickness,
        K1 = k1,
        K2 = k2,
        Connectivity = 0,
        LocalAxisFrom = 1
    };

    /// <summary>
    /// Creates a nonlinear user-defined panel zone
    /// </summary>
    public static PointPanelZone NonlinearUserDefined(string pointName, string linkProperty) => new()
    {
        PointName = pointName,
        PropertyType = 2,
        LinkProperty = linkProperty,
        Connectivity = 0,
        LocalAxisFrom = 1
    };

    public override string ToString()
    {
        return PropertyType switch
        {
            0 => $"Panel Zone at {PointName}: Elastic from elements",
            1 => $"Panel Zone at {PointName}: Elastic user-defined (t={Thickness:F3})",
            2 => $"Panel Zone at {PointName}: Nonlinear ({LinkProperty})",
            _ => $"Panel Zone at {PointName}: Unknown type {PropertyType}"
        };
    }
}
using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Wall element properties
/// </summary>
public class PropWall
{
    /// <summary>
    /// Wall name which can be used to reference the Wall property in ETABS API
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Wall type as enumeration: 0 for Wall, 1 for specific, 2 for AutoSelectList
    /// </summary>
    public eWallPropType EWallPropType { get; set; }

    /// <summary>
    /// Shell element type as enumeration:  ShellThin 1,  ShellThick 2,  Membrane 3,  PlateThin_DO_NOT_USE 4,  PlateThick_DO_NOT_USE 5,  Layered 6
    /// </summary>
    public eShellType EShellType { get; set; }

    /// <summary>
    /// Material property name associated with the wall
    /// </summary>
    public string MatProp { get; set; }

    /// <summary>
    /// Thickness per current api unit
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    /// Color assigned to the wall property in ETABS. -1 indicates default color.
    /// </summary>
    public int Color { get; set; } = -1;

    /// <summary>
    /// Notes associated with the wall property
    /// </summary>
    public string Notes { get; set; } = "";

    /// <summary>
    /// Unique identifier (GUID) for the wall property
    /// </summary>
    public string GUID { get; set; }
}
using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Properties of Slab elements
/// </summary>
public class PropSlab
{
    /// <summary>
    /// Slab name which can be used to reference the Slab property in ETABS API
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Slab type as enumeration: 0 for Slab, 1 for Drop, 2 for Stiffener,Ribbed 3, Waffle 4, Mat 5, Footing 6
    /// </summary>
    public eSlabType ESlabType { get; set; }

    /// <summary>
    /// Shell element type as enumeration:  ShellThin 1,  ShellThick 2,  Membrane 3,  PlateThin_DO_NOT_USE 4,  PlateThick_DO_NOT_USE 5,  Layered 6
    /// </summary>
    public eShellType EShellType { get; set; }

    /// <summary>
    /// Material property name associated with the slab
    /// </summary>
    public string MatProp { get; set; }

    /// <summary>
    /// Thickness per current api unit
    /// </summary>
    public double Thickness { get; set; }

    /// <summary>
    /// Color assigned to the slab property in ETABS. -1 indicates default color.
    /// </summary>
    public int Color { get; set; } = -1;

    /// <summary>
    /// Notes associated with the slab property
    /// </summary>
    public string Notes { get; set; } = "";

    /// <summary>
    /// Unique identifier (GUID) for the slab property
    /// </summary>
    public string GUID { get; set; }
}
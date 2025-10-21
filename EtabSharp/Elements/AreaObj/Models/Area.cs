namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents an area object (slab, wall, ramp) in ETABS.
/// </summary>
public class Area
{
    /// <summary>
    /// Unique name of the area object
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Names of corner points defining the area perimeter
    /// </summary>
    public required string[] PointNames { get; set; }

    /// <summary>
    /// Area section property name (slab or wall section)
    /// </summary>
    public string SectionName { get; set; } = "Default";

    /// <summary>
    /// Label assigned to this area
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// Story level where this area exists
    /// </summary>
    public string Story { get; set; } = "";

    /// <summary>
    /// Local axis rotation angle (degrees)
    /// </summary>
    public double LocalAxisAngle { get; set; }

    /// <summary>
    /// Design orientation for reinforcement
    /// </summary>
    public int DesignOrientation { get; set; }

    /// <summary>
    /// Pier label (for shear walls)
    /// </summary>
    public string PierLabel { get; set; } = "";

    /// <summary>
    /// Spandrel label (for coupling beams)
    /// </summary>
    public string SpandrelLabel { get; set; } = "";

    /// <summary>
    /// Diaphragm assignment
    /// </summary>
    public string DiaphragmName { get; set; } = "";

    /// <summary>
    /// Whether this area is an opening (void)
    /// </summary>
    public bool IsOpening { get; set; }

    /// <summary>
    /// GUID identifier
    /// </summary>
    public string GUID { get; set; } = "";

    /// <summary>
    /// Property modifiers
    /// </summary>
    public AreaModifiers? Modifiers { get; set; }

    /// <summary>
    /// Additional mass per unit area
    /// </summary>
    public double MassPerArea { get; set; }

    /// <summary>
    /// Edge constraint status
    /// </summary>
    public bool HasEdgeConstraint { get; set; }

    /// <summary>
    /// List of groups this area belongs to
    /// </summary>
    public List<string> Groups { get; set; } = new();

    /// <summary>
    /// Number of corner points
    /// </summary>
    public int NumberOfPoints => PointNames?.Length ?? 0;

    /// <summary>
    /// Whether this is a triangular area (3 points)
    /// </summary>
    public bool IsTriangular => NumberOfPoints == 3;

    /// <summary>
    /// Whether this is a quadrilateral area (4 points)
    /// </summary>
    public bool IsQuadrilateral => NumberOfPoints == 4;

    public override string ToString()
    {
        return $"Area {Name}: {NumberOfPoints} points [{SectionName}], Story: {Story}";
    }
}
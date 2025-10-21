namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents a frame object (beam, column, brace) in ETABS.
/// </summary>
public class Frame
{
    /// <summary>
    /// Unique name of the frame object
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Name of the I-end (start) point
    /// </summary>
    public required string Point1Name { get; set; }

    /// <summary>
    /// Name of the J-end (end) point
    /// </summary>
    public required string Point2Name { get; set; }

    /// <summary>
    /// Frame section property name
    /// </summary>
    public string SectionName { get; set; } = "Default";

    /// <summary>
    /// Label assigned to this frame
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// Story level where this frame exists
    /// </summary>
    public string Story { get; set; } = "";

    /// <summary>
    /// Frame length
    /// </summary>
    public double Length { get; set; }

    /// <summary>
    /// Local axis rotation angle (degrees)
    /// </summary>
    public double LocalAxisAngle { get; set; }

    /// <summary>
    /// Design procedure: 0=Auto, 1=Column, 2=Beam, 3=Brace
    /// </summary>
    public int DesignProcedure { get; set; }

    /// <summary>
    /// Pier label (for lateral analysis - columns)
    /// </summary>
    public string PierLabel { get; set; } = "";

    /// <summary>
    /// Spandrel label (for lateral analysis - beams)
    /// </summary>
    public string SpandrelLabel { get; set; } = "";

    /// <summary>
    /// GUID identifier
    /// </summary>
    public string GUID { get; set; } = "";

    /// <summary>
    /// End releases assignment
    /// </summary>
    public FrameReleases? Releases { get; set; }

    /// <summary>
    /// End length offsets (rigid zones)
    /// </summary>
    public FrameEndOffsets? EndOffsets { get; set; }

    /// <summary>
    /// Insertion point data
    /// </summary>
    public FrameInsertionPoint? InsertionPoint { get; set; }

    /// <summary>
    /// Property modifiers
    /// </summary>
    public FrameModifiers? Modifiers { get; set; }

    /// <summary>
    /// Additional mass per unit length
    /// </summary>
    public double MassPerLength { get; set; }

    /// <summary>
    /// List of groups this frame belongs to
    /// </summary>
    public List<string> Groups { get; set; } = new();

    /// <summary>
    /// Whether this frame is curved
    /// </summary>
    public bool IsCurved { get; set; }

    public override string ToString()
    {
        return $"Frame {Name}: {Point1Name} → {Point2Name} [{SectionName}], L={Length:F2}";
    }
}
namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents end length offsets (rigid zones) for a frame.
/// </summary>
public class FrameEndOffsets
{
    /// <summary>
    /// Use automatic offset calculation
    /// </summary>
    public bool AutoOffset { get; set; }

    /// <summary>
    /// Rigid zone length at I-end
    /// </summary>
    public double Length1 { get; set; }

    /// <summary>
    /// Rigid zone length at J-end
    /// </summary>
    public double Length2 { get; set; }

    /// <summary>
    /// Rigid zone factor (typically 1.0)
    /// </summary>
    public double RigidZoneFactor { get; set; } = 1.0;

    public override string ToString()
    {
        return AutoOffset
            ? "Auto Offsets"
            : $"Offsets: I={Length1:F3}, J={Length2:F3}, RZ={RigidZoneFactor:F2}";
    }
}
using ETABSv1;

namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents a plastic hinge assignment on a frame object.
/// Used for nonlinear pushover and time-history analysis.
/// </summary>
public class FrameHinge
{
    /// <summary>
    /// Name of the frame object
    /// </summary>
    public string FrameName { get; set; } = "";

    /// <summary>
    /// Hinge identification number
    /// </summary>
    public int HingeNumber { get; set; }

    /// <summary>
    /// Name of the hinge property
    /// </summary>
    public string PropertyName { get; set; } = "";

    /// <summary>
    /// Hinge type (1-14):
    /// 1 = P, 2 = V2, 3 = V3, 4 = T, 5 = M2, 6 = M3,
    /// 7 = Interacting P-M2, 8 = Interacting P-M3, 9 = Interacting M2-M3,
    /// 10 = Interacting P-M2-M3, 11 = Fiber P-M2-M3, 12 = Fiber P-M3,
    /// 13 = Parametric Concrete P-M2-M3, 14 = Parametric Steel P-M2-M3
    /// </summary>
    public int HingeType { get; set; }

    /// <summary>
    /// Hinge behavior:
    /// 1 = Force-controlled
    /// 2 = Deformation-controlled
    /// </summary>
    public int Behavior { get; set; }

    /// <summary>
    /// Source of the hinge property ("Auto" for auto-generated hinges)
    /// </summary>
    public string Source { get; set; } = "";

    /// <summary>
    /// Location type for the hinge
    /// </summary>
    public eHingeLocationType LocationType { get; set; }

    /// <summary>
    /// Relative distance from I-end (if LocationType = RelativeDistance)
    /// </summary>
    public double RelativeDistance { get; set; }

    /// <summary>
    /// Absolute distance from I-end or J-end (if LocationType = OffsetFromIEnd or OffsetFromJEnd)
    /// </summary>
    public double AbsoluteDistance { get; set; }

    /// <summary>
    /// Gets the hinge type description
    /// </summary>
    public string HingeTypeDescription => HingeType switch
    {
        1 => "P (Axial)",
        2 => "V2 (Shear 2)",
        3 => "V3 (Shear 3)",
        4 => "T (Torsion)",
        5 => "M2 (Moment 2)",
        6 => "M3 (Moment 3)",
        7 => "Interacting P-M2",
        8 => "Interacting P-M3",
        9 => "Interacting M2-M3",
        10 => "Interacting P-M2-M3",
        11 => "Fiber P-M2-M3",
        12 => "Fiber P-M3",
        13 => "Parametric Concrete P-M2-M3",
        14 => "Parametric Steel P-M2-M3",
        _ => $"Unknown ({HingeType})"
    };

    /// <summary>
    /// Gets the behavior description
    /// </summary>
    public string BehaviorDescription => Behavior switch
    {
        1 => "Force-controlled",
        2 => "Deformation-controlled",
        _ => $"Unknown ({Behavior})"
    };

    public override string ToString()
    {
        return $"Hinge {HingeNumber} at {FrameName}: {HingeTypeDescription}, {BehaviorDescription}";
    }
}

using ETABSv1;

namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents a point load on a frame.
/// </summary>
public class FramePointLoad
{
    /// <summary>
    /// Name of the frame object.
    /// </summary>
    public required string FrameName { get; set; }

    /// <summary>
    /// Load pattern name.
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Type of load: 1 = Force, 2 = Moment.
    /// </summary>
    public eLoadType LoadType { get; set; } = eLoadType.Force;

    /// <summary>
    /// Direction of the load (1–11) as defined by ETABS API.
    /// </summary>
    public eFrameLoadDirection Direction { get; set; } = eFrameLoadDirection.Gravity;

    /// <summary>
    /// Relative distance from I-End (0–1) of the frame.
    /// </summary>
    public double RelativeDistance { get; set; }

    /// <summary>
    /// Absolute distance from I-End (same units as model length).
    /// </summary>
    public double AbsoluteDistance { get; set; }

    /// <summary>
    /// Value of the load. [F] if Force, [FL] if Moment.
    /// </summary>
    public double LoadValue { get; set; }

    /// <summary>
    /// Coordinate system in which the load is defined ("Local" or a named CSys).
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// True if RelativeDistance is used to define load position, false if AbsoluteDistance is used.
    /// </summary>
    public bool IsRelativeDistance { get; set; } = true;

    /// <summary>
    /// If true, replaces existing loads of this pattern (used when setting loads).
    /// </summary>
    public bool Replace { get; set; } = true;

    /// <summary>
    /// Item type context (Object, Group, SelectedObjects).
    /// </summary>
    public eItemType ItemType { get; set; } = eItemType.Objects;

    public override string ToString()
    {
        var distance = IsRelativeDistance ? RelativeDistance : AbsoluteDistance;
        var distType = IsRelativeDistance ? "Rel" : "Abs";

        return $"{LoadPattern}: {LoadType} {LoadValue} @ {distance} ({distType}) {Direction} [{CoordinateSystem}]";
    }
}

public enum eLoadType
{
    Force = 1,
    Moment = 2
}
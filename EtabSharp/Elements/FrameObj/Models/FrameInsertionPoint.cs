namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents insertion point and joint offsets for a frame.
/// </summary>
public class FrameInsertionPoint
{
    /// <summary>
    /// Cardinal point (1-11): 1=bottom-left, 10=centroid, etc.
    /// </summary>
    public int CardinalPoint { get; set; } = 10; // 10 = centroid

    /// <summary>
    /// Mirror about local 2-axis
    /// </summary>
    public bool Mirror2 { get; set; }

    /// <summary>
    /// Mirror about local 3-axis
    /// </summary>
    public bool Mirror3 { get; set; }

    /// <summary>
    /// Transform stiffness matrix for offsets
    /// </summary>
    public bool StiffnessTransform { get; set; }

    /// <summary>
    /// 3D offset at I-end [dx, dy, dz]
    /// </summary>
    public double[] Offset1 { get; set; } = new double[3];

    /// <summary>
    /// 3D offset at J-end [dx, dy, dz]
    /// </summary>
    public double[] Offset2 { get; set; } = new double[3];

    /// <summary>
    /// Coordinate system for offsets
    /// </summary>
    public string CoordinateSystem { get; set; } = "Local";

    public override string ToString()
    {
        return $"Cardinal Point: {CardinalPoint}, Offset1: ({Offset1[0]:F3}, {Offset1[1]:F3}, {Offset1[2]:F3})";
    }
}
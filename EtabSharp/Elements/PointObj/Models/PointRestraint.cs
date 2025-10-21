namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents restraint (support) conditions at a point.
/// Each boolean indicates whether that DOF is restrained (true) or free (false).
/// </summary>
public class PointRestraint
{
    /// <summary>
    /// Translation in X direction is restrained
    /// </summary>
    public bool Ux { get; set; }

    /// <summary>
    /// Translation in Y direction is restrained
    /// </summary>
    public bool Uy { get; set; }

    /// <summary>
    /// Translation in Z direction is restrained
    /// </summary>
    public bool Uz { get; set; }

    /// <summary>
    /// Rotation about X axis is restrained
    /// </summary>
    public bool Rx { get; set; }

    /// <summary>
    /// Rotation about Y axis is restrained
    /// </summary>
    public bool Ry { get; set; }

    /// <summary>
    /// Rotation about Z axis is restrained
    /// </summary>
    public bool Rz { get; set; }

    /// <summary>
    /// Creates a fixed support (all DOFs restrained)
    /// </summary>
    public static PointRestraint Fixed() => new()
    {
        Ux = true,
        Uy = true,
        Uz = true,
        Rx = true,
        Ry = true,
        Rz = true
    };

    /// <summary>
    /// Creates a pinned support (translations restrained, rotations free)
    /// </summary>
    public static PointRestraint Pinned() => new()
    {
        Ux = true,
        Uy = true,
        Uz = true,
        Rx = false,
        Ry = false,
        Rz = false
    };

    /// <summary>
    /// Creates a roller support in Z direction (Uz restrained only)
    /// </summary>
    public static PointRestraint RollerZ() => new()
    {
        Ux = false,
        Uy = false,
        Uz = true,
        Rx = false,
        Ry = false,
        Rz = false
    };

    /// <summary>
    /// Creates a free point (no restraints)
    /// </summary>
    public static PointRestraint Free() => new();

    /// <summary>
    /// Converts to boolean array [Ux, Uy, Uz, Rx, Ry, Rz]
    /// </summary>
    public bool[] ToArray() => new[] { Ux, Uy, Uz, Rx, Ry, Rz };

    /// <summary>
    /// Creates from boolean array [Ux, Uy, Uz, Rx, Ry, Rz]
    /// </summary>
    public static PointRestraint FromArray(bool[] values)
    {
        if (values?.Length != 6)
            throw new ArgumentException("Restraint array must have 6 elements");

        return new PointRestraint
        {
            Ux = values[0],
            Uy = values[1],
            Uz = values[2],
            Rx = values[3],
            Ry = values[4],
            Rz = values[5]
        };
    }

    /// <summary>
    /// Gets count of restrained DOFs
    /// </summary>
    public int RestrainedCount => ToArray().Count(x => x);

    public override string ToString()
    {
        var restraints = new List<string>();
        if (Ux) restraints.Add("Ux");
        if (Uy) restraints.Add("Uy");
        if (Uz) restraints.Add("Uz");
        if (Rx) restraints.Add("Rx");
        if (Ry) restraints.Add("Ry");
        if (Rz) restraints.Add("Rz");
        return restraints.Any() ? string.Join(", ", restraints) : "Free";
    }
}
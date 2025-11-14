namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Active degrees of freedom for analysis.
/// </summary>
public class ActiveDOF
{
    /// <summary>
    /// Translation in X direction is active
    /// </summary>
    public bool UX { get; set; } = true;

    /// <summary>
    /// Translation in Y direction is active
    /// </summary>
    public bool UY { get; set; } = true;

    /// <summary>
    /// Translation in Z direction is active
    /// </summary>
    public bool UZ { get; set; } = true;

    /// <summary>
    /// Rotation about X axis is active
    /// </summary>
    public bool RX { get; set; } = true;

    /// <summary>
    /// Rotation about Y axis is active
    /// </summary>
    public bool RY { get; set; } = true;

    /// <summary>
    /// Rotation about Z axis is active
    /// </summary>
    public bool RZ { get; set; } = true;

    /// <summary>
    /// Converts to boolean array [UX, UY, UZ, RX, RY, RZ]
    /// </summary>
    public bool[] ToArray() => new[] { UX, UY, UZ, RX, RY, RZ };

    /// <summary>
    /// Creates from boolean array [UX, UY, UZ, RX, RY, RZ]
    /// </summary>
    public static ActiveDOF FromArray(bool[] values)
    {
        if (values?.Length != 6)
            throw new ArgumentException("DOF array must have 6 elements");

        return new ActiveDOF
        {
            UX = values[0],
            UY = values[1],
            UZ = values[2],
            RX = values[3],
            RY = values[4],
            RZ = values[5]
        };
    }

    /// <summary>
    /// Creates all DOF active
    /// </summary>
    public static ActiveDOF All() => new();

    /// <summary>
    /// Creates 2D analysis DOF (UX, UY, RZ only)
    /// </summary>
    public static ActiveDOF TwoD() => new()
    {
        UX = true,
        UY = true,
        UZ = false,
        RX = false,
        RY = false,
        RZ = true
    };

    /// <summary>
    /// Creates 3D analysis DOF (all active)
    /// </summary>
    public static ActiveDOF ThreeD() => All();

    public override string ToString()
    {
        var active = new List<string>();
        if (UX) active.Add("UX");
        if (UY) active.Add("UY");
        if (UZ) active.Add("UZ");
        if (RX) active.Add("RX");
        if (RY) active.Add("RY");
        if (RZ) active.Add("RZ");
        return active.Any() ? string.Join(", ", active) : "None";
    }
}
namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents spring stiffness at a point (flexible support).
/// </summary>
public class PointSpring
{
    /// <summary>
    /// Spring stiffness in X direction (force/length)
    /// </summary>
    public double Kx { get; set; }

    /// <summary>
    /// Spring stiffness in Y direction (force/length)
    /// </summary>
    public double Ky { get; set; }

    /// <summary>
    /// Spring stiffness in Z direction (force/length)
    /// </summary>
    public double Kz { get; set; }

    /// <summary>
    /// Rotational spring stiffness about X axis (moment/radian)
    /// </summary>
    public double Krx { get; set; }

    /// <summary>
    /// Rotational spring stiffness about Y axis (moment/radian)
    /// </summary>
    public double Kry { get; set; }

    /// <summary>
    /// Rotational spring stiffness about Z axis (moment/radian)
    /// </summary>
    public double Krz { get; set; }

    /// <summary>
    /// Whether this is a coupled spring (full 6x6 stiffness matrix)
    /// </summary>
    public bool IsCoupled { get; set; }

    /// <summary>
    /// Full coupled stiffness matrix (21 values for upper triangle)
    /// Only used if IsCoupled = true
    /// </summary>
    public double[]? CoupledMatrix { get; set; }

    /// <summary>
    /// Name of the spring property (if using named property)
    /// </summary>
    public string PropertyName { get; set; } = "";

    /// <summary>
    /// Whether values are in local coordinate system
    /// </summary>
    public bool IsLocalCSys { get; set; } = true;

    /// <summary>
    /// Converts to array [Kx, Ky, Kz, Krx, Kry, Krz]
    /// </summary>
    public double[] ToArray() => new[] { Kx, Ky, Kz, Krx, Kry, Krz };

    /// <summary>
    /// Creates from array [Kx, Ky, Kz, Krx, Kry, Krz]
    /// </summary>
    public static PointSpring FromArray(double[] values)
    {
        if (values?.Length != 6)
            throw new ArgumentException("Spring array must have 6 elements");

        return new PointSpring
        {
            Kx = values[0],
            Ky = values[1],
            Kz = values[2],
            Krx = values[3],
            Kry = values[4],
            Krz = values[5]
        };
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(PropertyName))
            return $"Spring Property: {PropertyName}";

        return $"Spring: Kx={Kx:E2}, Ky={Ky:E2}, Kz={Kz:E2}";
    }
}
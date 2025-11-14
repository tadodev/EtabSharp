namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents lumped mass at a point.
/// </summary>
public class PointMass
{
    /// <summary>
    /// Mass in X direction
    /// </summary>
    public double Mx { get; set; }

    /// <summary>
    /// Mass in Y direction
    /// </summary>
    public double My { get; set; }

    /// <summary>
    /// Mass in Z direction
    /// </summary>
    public double Mz { get; set; }

    /// <summary>
    /// Mass moment of inertia about X axis
    /// </summary>
    public double MMx { get; set; }

    /// <summary>
    /// Mass moment of inertia about Y axis
    /// </summary>
    public double MMy { get; set; }

    /// <summary>
    /// Mass moment of inertia about Z axis
    /// </summary>
    public double MMz { get; set; }

    /// <summary>
    /// Whether values are in local coordinate system
    /// </summary>
    public bool IsLocalCSys { get; set; } = true;

    /// <summary>
    /// Converts to array [Mx, My, Mz, MMx, MMy, MMz]
    /// </summary>
    public double[] ToArray() => new[] { Mx, My, Mz, MMx, MMy, MMz };

    /// <summary>
    /// Creates from array [Mx, My, Mz, MMx, MMy, MMz]
    /// </summary>
    public static PointMass FromArray(double[] values)
    {
        if (values?.Length != 6)
            throw new ArgumentException("Mass array must have 6 elements");

        return new PointMass
        {
            Mx = values[0],
            My = values[1],
            Mz = values[2],
            MMx = values[3],
            MMy = values[4],
            MMz = values[5]
        };
    }

    /// <summary>
    /// Total translational mass
    /// </summary>
    public double TotalTranslationalMass => Mx + My + Mz;

    public override string ToString()
    {
        return $"Mass: Mx={Mx:F3}, My={My:F3}, Mz={Mz:F3}";
    }
}
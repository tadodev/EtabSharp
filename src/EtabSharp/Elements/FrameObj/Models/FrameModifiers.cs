namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents property modifiers for a frame section.
/// Values scale the section properties (1.0 = no modification).
/// </summary>
public class FrameModifiers
{
    /// <summary>
    /// Cross-sectional area modifier
    /// </summary>
    public double Area { get; set; } = 1.0;

    /// <summary>
    /// Shear area in local 2-direction modifier
    /// </summary>
    public double ShearArea2 { get; set; } = 1.0;

    /// <summary>
    /// Shear area in local 3-direction modifier
    /// </summary>
    public double ShearArea3 { get; set; } = 1.0;

    /// <summary>
    /// Torsional constant modifier
    /// </summary>
    public double Torsion { get; set; } = 1.0;

    /// <summary>
    /// Moment of inertia about local 2-axis modifier
    /// </summary>
    public double Inertia2 { get; set; } = 1.0;

    /// <summary>
    /// Moment of inertia about local 3-axis modifier
    /// </summary>
    public double Inertia3 { get; set; } = 1.0;

    /// <summary>
    /// Mass modifier
    /// </summary>
    public double Mass { get; set; } = 1.0;

    /// <summary>
    /// Weight modifier
    /// </summary>
    public double Weight { get; set; } = 1.0;

    /// <summary>
    /// Creates default modifiers (all 1.0)
    /// </summary>
    public static FrameModifiers Default() => new();

    /// <summary>
    /// Creates cracked section modifiers (reduced stiffness)
    /// Typical for reinforced concrete: I reduced to ~35%, Area/shear to ~70%
    /// </summary>
    public static FrameModifiers Cracked() => new()
    {
        Area = 0.7,
        ShearArea2 = 0.7,
        ShearArea3 = 0.7,
        Torsion = 0.35,
        Inertia2 = 0.35,
        Inertia3 = 0.35
    };

    /// <summary>
    /// Converts to array [Area, As2, As3, Torsion, I22, I33, Mass, Weight]
    /// </summary>
    public double[] ToArray() => new[]
    {
        Area, ShearArea2, ShearArea3, Torsion,
        Inertia2, Inertia3, Mass, Weight
    };

    /// <summary>
    /// Creates from array [Area, As2, As3, Torsion, I22, I33, Mass, Weight]
    /// </summary>
    public static FrameModifiers FromArray(double[] values)
    {
        if (values?.Length != 8)
            throw new ArgumentException("Modifier array must have 8 elements");

        return new FrameModifiers
        {
            Area = values[0],
            ShearArea2 = values[1],
            ShearArea3 = values[2],
            Torsion = values[3],
            Inertia2 = values[4],
            Inertia3 = values[5],
            Mass = values[6],
            Weight = values[7]
        };
    }

    public override string ToString()
    {
        return $"Modifiers: A={Area:F2}, I2={Inertia2:F2}, I3={Inertia3:F2}";
    }
}
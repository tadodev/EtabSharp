namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents property modifiers for an area section.
/// Values scale the section properties (1.0 = no modification).
/// </summary>
public class AreaModifiers
{
    /// <summary>
    /// Membrane stiffness modifier in 1-1 direction
    /// </summary>
    public double MembraneF11 { get; set; } = 1.0;

    /// <summary>
    /// Membrane stiffness modifier in 2-2 direction
    /// </summary>
    public double MembraneF22 { get; set; } = 1.0;

    /// <summary>
    /// Membrane stiffness modifier in 1-2 direction
    /// </summary>
    public double MembraneF12 { get; set; } = 1.0;

    /// <summary>
    /// Bending stiffness modifier for M11
    /// </summary>
    public double BendingM11 { get; set; } = 1.0;

    /// <summary>
    /// Bending stiffness modifier for M22
    /// </summary>
    public double BendingM22 { get; set; } = 1.0;

    /// <summary>
    /// Bending stiffness modifier for M12
    /// </summary>
    public double BendingM12 { get; set; } = 1.0;

    /// <summary>
    /// Shear stiffness modifier for V13
    /// </summary>
    public double ShearV13 { get; set; } = 1.0;

    /// <summary>
    /// Shear stiffness modifier for V23
    /// </summary>
    public double ShearV23 { get; set; } = 1.0;

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
    public static AreaModifiers Default() => new();

    /// <summary>
    /// Creates cracked section modifiers (reduced stiffness)
    /// Typical for reinforced concrete slabs/walls: bending reduced to ~25%
    /// </summary>
    public static AreaModifiers Cracked() => new()
    {
        MembraneF11 = 1.0,
        MembraneF22 = 1.0,
        MembraneF12 = 1.0,
        BendingM11 = 0.25,
        BendingM22 = 0.25,
        BendingM12 = 0.25,
        ShearV13 = 1.0,
        ShearV23 = 1.0
    };

    /// <summary>
    /// Converts to array [f11, f22, f12, m11, m22, m12, v13, v23, mass, weight]
    /// </summary>
    public double[] ToArray() => new[]
    {
        MembraneF11, MembraneF22, MembraneF12,
        BendingM11, BendingM22, BendingM12,
        ShearV13, ShearV23, Mass, Weight
    };

    /// <summary>
    /// Creates from array [f11, f22, f12, m11, m22, m12, v13, v23, mass, weight]
    /// </summary>
    public static AreaModifiers FromArray(double[] values)
    {
        if (values?.Length != 10)
            throw new ArgumentException("Modifier array must have 10 elements");

        return new AreaModifiers
        {
            MembraneF11 = values[0],
            MembraneF22 = values[1],
            MembraneF12 = values[2],
            BendingM11 = values[3],
            BendingM22 = values[4],
            BendingM12 = values[5],
            ShearV13 = values[6],
            ShearV23 = values[7],
            Mass = values[8],
            Weight = values[9]
        };
    }

    public override string ToString()
    {
        return $"Modifiers: Membrane={MembraneF11:F2}, Bending={BendingM11:F2}";
    }
}
namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents property modifiers for area properties.
/// These modifiers scale the area property values for analysis.
/// </summary>
public class AreaPropertyModifiers
{
    /// <summary>
    /// Gets or sets the membrane f11 modifier.
    /// </summary>
    public double MembraneF11 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the membrane f22 modifier.
    /// </summary>
    public double MembraneF22 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the membrane f12 modifier.
    /// </summary>
    public double MembraneF12 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the bending m11 modifier.
    /// </summary>
    public double BendingM11 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the bending m22 modifier.
    /// </summary>
    public double BendingM22 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the bending m12 modifier.
    /// </summary>
    public double BendingM12 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the shear v13 modifier.
    /// </summary>
    public double ShearV13 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the shear v23 modifier.
    /// </summary>
    public double ShearV23 { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the mass modifier.
    /// </summary>
    public double Mass { get; set; } = 1.0;

    /// <summary>
    /// Gets or sets the weight modifier.
    /// </summary>
    public double Weight { get; set; } = 1.0;

    /// <summary>
    /// Initializes a new instance of the AreaPropertyModifiers class with default values (1.0).
    /// </summary>
    public AreaPropertyModifiers()
    {
    }

    /// <summary>
    /// Initializes a new instance of the AreaPropertyModifiers class with specified values.
    /// </summary>
    /// <param name="membraneF11">Membrane f11 modifier</param>
    /// <param name="membraneF22">Membrane f22 modifier</param>
    /// <param name="membraneF12">Membrane f12 modifier</param>
    /// <param name="bendingM11">Bending m11 modifier</param>
    /// <param name="bendingM22">Bending m22 modifier</param>
    /// <param name="bendingM12">Bending m12 modifier</param>
    /// <param name="shearV13">Shear v13 modifier</param>
    /// <param name="shearV23">Shear v23 modifier</param>
    /// <param name="mass">Mass modifier</param>
    /// <param name="weight">Weight modifier</param>
    public AreaPropertyModifiers(double membraneF11, double membraneF22, double membraneF12,
                                double bendingM11, double bendingM22, double bendingM12,
                                double shearV13, double shearV23, double mass, double weight)
    {
        MembraneF11 = membraneF11;
        MembraneF22 = membraneF22;
        MembraneF12 = membraneF12;
        BendingM11 = bendingM11;
        BendingM22 = bendingM22;
        BendingM12 = bendingM12;
        ShearV13 = shearV13;
        ShearV23 = shearV23;
        Mass = mass;
        Weight = weight;
    }

    /// <summary>
    /// Converts the modifiers to an array format for ETABS API.
    /// </summary>
    /// <returns>Array of modifier values</returns>
    public double[] ToArray()
    {
        return new double[]
        {
            MembraneF11,
            MembraneF22,
            MembraneF12,
            BendingM11,
            BendingM22,
            BendingM12,
            ShearV13,
            ShearV23,
            Mass,
            Weight
        };
    }

    /// <summary>
    /// Creates AreaPropertyModifiers from an array of values.
    /// </summary>
    /// <param name="values">Array of modifier values</param>
    /// <returns>AreaPropertyModifiers instance</returns>
    public static AreaPropertyModifiers FromArray(double[] values)
    {
        if (values == null || values.Length < 10)
            throw new ArgumentException("Values array must contain at least 10 modifier values", nameof(values));

        return new AreaPropertyModifiers(
            values[0], values[1], values[2], values[3], values[4],
            values[5], values[6], values[7], values[8], values[9]
        );
    }

    /// <summary>
    /// Creates default modifiers (all values = 1.0).
    /// </summary>
    /// <returns>AreaPropertyModifiers with default values</returns>
    public static AreaPropertyModifiers Default()
    {
        return new AreaPropertyModifiers();
    }

    /// <summary>
    /// Checks if all modifiers are at default values (1.0).
    /// </summary>
    /// <returns>True if all modifiers are 1.0</returns>
    public bool IsDefault()
    {
        const double tolerance = 1e-6;
        return Math.Abs(MembraneF11 - 1.0) < tolerance &&
               Math.Abs(MembraneF22 - 1.0) < tolerance &&
               Math.Abs(MembraneF12 - 1.0) < tolerance &&
               Math.Abs(BendingM11 - 1.0) < tolerance &&
               Math.Abs(BendingM22 - 1.0) < tolerance &&
               Math.Abs(BendingM12 - 1.0) < tolerance &&
               Math.Abs(ShearV13 - 1.0) < tolerance &&
               Math.Abs(ShearV23 - 1.0) < tolerance &&
               Math.Abs(Mass - 1.0) < tolerance &&
               Math.Abs(Weight - 1.0) < tolerance;
    }

    /// <summary>
    /// Returns a string representation of the modifiers.
    /// </summary>
    /// <returns>String containing modifier values</returns>
    public override string ToString()
    {
        return $"Membrane: F11={MembraneF11:F2}, F22={MembraneF22:F2}, F12={MembraneF12:F2} | " +
               $"Bending: M11={BendingM11:F2}, M22={BendingM22:F2}, M12={BendingM12:F2} | " +
               $"Shear: V13={ShearV13:F2}, V23={ShearV23:F2} | " +
               $"Mass={Mass:F2}, Weight={Weight:F2}";
    }

    /// <summary>
    /// Creates a copy of the current modifiers.
    /// </summary>
    /// <returns>Copy of the AreaPropertyModifiers</returns>
    public AreaPropertyModifiers Clone()
    {
        return new AreaPropertyModifiers(
            MembraneF11, MembraneF22, MembraneF12,
            BendingM11, BendingM22, BendingM12,
            ShearV13, ShearV23, Mass, Weight
        );
    }
}
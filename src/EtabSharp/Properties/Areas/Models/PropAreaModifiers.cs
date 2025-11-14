namespace EtabSharp.Properties.Areas.Models;

/// <summary>
///     Modifiers for area properties such as slabs and walls
/// </summary>
public class PropAreaModifiers
{
    /// <summary>
    ///     Membrane f11 modifier
    /// </summary>
    public double MembraneF11 { get; set; }

    /// <summary>
    ///     Membrane f22 modifier
    /// </summary>
    public double MembraneF22 { get; set; }

    /// <summary>
    ///     Membrane f12 modifier
    /// </summary>
    public double MembraneF12 { get; set; }

    /// <summary>
    ///     Bending m11 modifier
    /// </summary>
    public double BendingM11 { get; set; }

    /// <summary>
    ///     Bending m22 modifier
    /// </summary>
    public double BendingM22 { get; set; }

    /// <summary>
    ///     Bending m12 modifier
    /// </summary>
    public double BendingM12 { get; set; }

    /// <summary>
    ///     Shear v13 modifier
    /// </summary>
    public double ShearV13 { get; set; }

    /// <summary>
    ///     Shear v23 modifier
    /// </summary>
    public double ShearV23 { get; set; }

    /// <summary>
    ///     Mass modifier
    /// </summary>
    public double Mass { get; set; }

    /// <summary>
    ///     Weight modifier
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    ///     Helper method to create AreaPropertyModifiers from an array of doubles
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static PropAreaModifiers FromArray(double[] values)
    {
        if (values == null || values.Length != 10)
            throw new ArgumentException("Modifiers array must have 10 elements.");

        return new PropAreaModifiers
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

    /// <summary>
    ///     Helper method to convert AreaPropertyModifiers to an array of doubles
    /// </summary>
    /// <returns></returns>
    public double[] ToArray()
    {
        return new[]
        {
            MembraneF11, MembraneF22, MembraneF12, BendingM11, BendingM22, BendingM12, ShearV13, ShearV23, Mass,
            Weight
        };
    }
}
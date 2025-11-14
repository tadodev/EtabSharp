namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Isotropic mechanical properties
/// </summary>
public class IsotropicProperties
{
    /// <summary>
    /// Modulus of elasticity
    /// </summary>
    public double E { get; set; }

    /// <summary>
    /// Poisson's ratio
    /// </summary>
    public double U { get; set; }

    /// <summary>
    /// Coefficient of thermal expansion
    /// </summary>
    public double A { get; set; }

    /// <summary>
    /// Shear modulus (optional, can be calculated from E and U)
    /// </summary>
    public double G { get; set; }

    /// <summary>
    /// Temperature for these properties
    /// </summary>
    public double Temp { get; set; } = 0.0;
}
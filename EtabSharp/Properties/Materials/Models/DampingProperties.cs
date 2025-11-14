namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Damping properties for materials
/// </summary>
public class DampingProperties
{
    /// <summary>
    /// Modal damping ratio
    /// </summary>
    public double ModalRatio { get; set; }

    /// <summary>
    /// Viscous mass coefficient
    /// </summary>
    public double ViscousMassCoeff { get; set; }

    /// <summary>
    /// Viscous stiffness coefficient
    /// </summary>
    public double ViscousStiffCoeff { get; set; }

    /// <summary>
    /// Hysteretic mass coefficient
    /// </summary>
    public double HystereticMassCoeff { get; set; }

    /// <summary>
    /// Hysteretic stiffness coefficient
    /// </summary>
    public double HystereticStiffCoeff { get; set; }

    /// <summary>
    /// Temperature for these properties
    /// </summary>
    public double Temp { get; set; } = 0.0;
}
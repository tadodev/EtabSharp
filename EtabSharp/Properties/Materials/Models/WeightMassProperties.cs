namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Weight and mass properties
/// </summary>
public class WeightMassProperties
{
    /// <summary>
    /// Weight per unit volume
    /// </summary>
    public double W { get; set; }

    /// <summary>
    /// Mass per unit volume
    /// </summary>
    public double M { get; set; }

    /// <summary>
    /// Temperature for these properties
    /// </summary>
    public double Temp { get; set; } = 0.0;
}
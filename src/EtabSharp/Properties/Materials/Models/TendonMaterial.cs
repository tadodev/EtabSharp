using ETABSv1;

namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Tendon material properties
/// </summary>
public class TendonMaterial : MaterialProperty
{
    public override eMatType MaterialType => eMatType.Tendon;

    // Isotropic properties
    public IsotropicProperties IsotropicProps { get; set; } = new();

    // Tendon-specific properties
    /// <summary>
    /// Minimum yield stress
    /// </summary>
    public double Fy { get; set; }

    /// <summary>
    /// Minimum tensile stress
    /// </summary>
    public double Fu { get; set; }

    /// <summary>
    /// Stress-strain curve type
    /// </summary>
    public int SSType { get; set; } = 1;

    /// <summary>
    /// Stress-strain hysteresis type
    /// </summary>
    public int SSHysType { get; set; } = 1;

    /// <summary>
    /// Final slope
    /// </summary>
    public double FinalSlope { get; set; } = -0.1;

    /// <summary>
    /// Temperature for tendon properties
    /// </summary>
    public double Temp { get; set; } = 0.0;

    // Optional properties
    public DampingProperties? Damping { get; set; }
    public WeightMassProperties? WeightMass { get; set; }

    public override bool IsValid()
    {
        return base.IsValid() && Fy > 0 && Fu > Fy && IsotropicProps.E > 0;
    }

    /// <summary>
    /// Creates tendon material with typical values
    /// </summary>
    public static TendonMaterial Create(string name, double fy, double fu)
    {
        return new TendonMaterial
        {
            Name = name,
            Fy = fy,
            Fu = fu,
            IsotropicProps = new IsotropicProperties
            {
                E = 28500, // ksi for prestressing steel
                U = 0.3,
                A = 6.5e-6
            }
        };
    }
}
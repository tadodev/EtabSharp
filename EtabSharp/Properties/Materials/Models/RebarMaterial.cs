using ETABSv1;

namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Rebar material properties
/// </summary>
public class RebarMaterial : MaterialProperty
{
    public override eMatType MaterialType => eMatType.Rebar;

    // Isotropic properties
    public IsotropicProperties IsotropicProps { get; set; } = new();

    // Rebar-specific properties
    /// <summary>
    /// Minimum yield stress
    /// </summary>
    public double Fy { get; set; }

    /// <summary>
    /// Minimum tensile stress
    /// </summary>
    public double Fu { get; set; }

    /// <summary>
    /// Expected yield stress
    /// </summary>
    public double EFy { get; set; }

    /// <summary>
    /// Expected tensile stress
    /// </summary>
    public double EFu { get; set; }

    /// <summary>
    /// Stress-strain curve type
    /// </summary>
    public int SSType { get; set; } = 1; // Parametric Simple

    /// <summary>
    /// Stress-strain hysteresis type
    /// </summary>
    public int SSHysType { get; set; } = 1; // Kinematic

    /// <summary>
    /// Strain at hardening
    /// </summary>
    public double StrainAtHardening { get; set; } = 0.02;

    /// <summary>
    /// Ultimate strain
    /// </summary>
    public double StrainUltimate { get; set; } = 0.1;

    /// <summary>
    /// Final slope
    /// </summary>
    public double FinalSlope { get; set; } = -0.1;

    /// <summary>
    /// Use Caltrans stress-strain defaults
    /// </summary>
    public bool UseCaltransSSDefaults { get; set; } = true;

    /// <summary>
    /// Temperature for rebar properties
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
    /// Creates rebar material with typical values
    /// </summary>
    public static RebarMaterial Create(string name, double fy, double fu)
    {
        return new RebarMaterial
        {
            Name = name,
            Fy = fy,
            Fu = fu,
            EFy = fy,
            EFu = fu,
            IsotropicProps = new IsotropicProperties
            {
                E = 29000, // ksi for US
                U = 0.3,
                A = 6.5e-6
            }
        };
    }
}
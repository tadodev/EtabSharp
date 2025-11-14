using ETABSv1;

namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Concrete material properties
/// </summary>
public class ConcreteMaterial : MaterialProperty
{
    /// <inheritdoc />
    public override eMatType MaterialType => eMatType.Concrete;

    /// <summary>
    /// Isotropic properties
    /// </summary>
    public IsotropicProperties IsotropicProps { get; set; } = new();

    // Concrete-specific properties
    /// <summary>
    /// Specified compressive strength
    /// </summary>
    public double Fc { get; set; }

    /// <summary>
    /// Is lightweight concrete
    /// </summary>
    public bool IsLightweight { get; set; } = false;

    /// <summary>
    /// Shear strength reduction factor
    /// </summary>
    public double FcsFactor { get; set; } = 0.0;

    /// <summary>
    /// Stress-strain curve type
    /// </summary>
    public int SSType { get; set; } = 2; // Mander

    /// <summary>
    /// Stress-strain hysteresis type
    /// </summary>
    public int SSHysType { get; set; } = 4; // Concrete

    /// <summary>
    /// Strain at maximum stress
    /// </summary>
    public double StrainAtFc { get; set; } = 0.0022;

    /// <summary>
    /// Ultimate strain
    /// </summary>
    public double StrainUltimate { get; set; } = 0.0052;

    /// <summary>
    /// Final slope
    /// </summary>
    public double FinalSlope { get; set; } = -0.1;

    /// <summary>
    /// Friction angle for Drucker-Prager
    /// </summary>
    public double FrictionAngle { get; set; } = 0.0;

    /// <summary>
    /// Dilatational angle for Drucker-Prager
    /// </summary>
    public double DilatationalAngle { get; set; } = 0.0;

    /// <summary>
    /// Temperature for concrete properties
    /// </summary>
    public double Temp { get; set; } = 0.0;

    // Optional properties
    public DampingProperties? Damping { get; set; }
    public WeightMassProperties? WeightMass { get; set; }

    public override bool IsValid()
    {
        return base.IsValid() && Fc > 0 && IsotropicProps.E > 0;
    }

    /// <summary>
    /// Creates concrete material with typical values
    /// </summary>
    public static ConcreteMaterial Create(string name, double fc, double ec)
    {
        return new ConcreteMaterial
        {
            Name = name,
            Fc = fc,
            IsotropicProps = new IsotropicProperties
            {
                E = ec,
                U = 0.2,
                A = 9.9e-6
            }
        };
    }
}
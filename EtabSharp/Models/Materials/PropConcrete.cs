namespace EtabSharp.Models.Materials;
public record PropConcrete
{
    public string Name { get; set; }
    public double fpc { get; set; }
    public double Ec { get; set; }

    //Optional mechanical properties with sensible defaults
    public double nu { get; set; } = 0.2; // Poisson's ratio
    public double alpha { get; set; } = 9.9e-6; // Coefficient of thermal expansion (1/°C)

    // Optional nonlinear properties with sensible defaults
    public bool IsLightWeight { get; set; } = false;
    public double StrainAtFc { get; set; } = 0.0022;   // Strain at maximum stress
    public double StrainAtU { get; set; } = 0.0052;   // Ultimate strain
}

namespace EtabSharp.Models;

public class Materials
{
    /// <summary>Represents a material</summary>
    public record Material
    {
        public required string Name { get; init; }
        public required string MaterialType { get; init; }
        public double E { get; init; } // Modulus of elasticity
        public double Nu { get; init; } // Poisson's ratio
        public double ThermalCoeff { get; init; }
        public double UnitMass { get; init; }
        public double UnitWeight { get; init; }
        public double Fy { get; init; } // Yield strength
        public double Fu { get; init; } // Ultimate strength
    }
}
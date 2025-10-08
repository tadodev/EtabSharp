namespace EtabSharp.Models;

/// <summary>Represents a frame element</summary>
public record Frame
{
    public required string Name { get; init; }
    public string? Section { get; init; }
    public string? Material { get; init; }
    public string? StartPoint { get; init; }
    public string? EndPoint { get; init; }
    public Point3D? StartCoordinates { get; init; }
    public Point3D? EndCoordinates { get; init; }
    public double Length { get; init; }
}

/// <summary>Represents an area element</summary>
public record Area
{
    public required string Name { get; init; }
    public string? Section { get; init; }
    public double Thickness { get; init; }
    public string? Material { get; init; }
}

/// <summary>Represents a load case</summary>
public record LoadCase
{
    public required string Name { get; init; }
    public string? Type { get; init; }
    public string? DesignType { get; init; }
}

/// <summary>Represents a load pattern</summary>
public record LoadPattern
{
    public required string Name { get; init; }
    public string? Type { get; init; }
    public double SelfWeightMultiplier { get; init; }
}

/// <summary>Represents a 3D point</summary>
public record Point3D(double X, double Y, double Z)
{
    public override string ToString() => $"({X:F3}, {Y:F3}, {Z:F3})";
}

namespace EtabSharp.Materials.Models;

/// <summary>
/// Minimal Rebar material properties in ETABS
/// </summary>
public record PropRebar
{
    public required string Name { get; set; }
    public double Fy { get; set; }
    public double Fu { get; set; }
    public double EFy { get; set; }
    public double EFu { get; set; }
}

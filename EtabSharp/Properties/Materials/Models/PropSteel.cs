namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Minimal Steel material properties in ETABS
/// </summary>
public record PropSteel
{
    public required string Name { get; set; }
    public double Fy { get; set; }
    public double Fu { get; set; }
    public double EFy { get; set; }
    public double EFu { get; set; }
}

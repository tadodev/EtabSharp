namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents rebar data for an area spandrel (coupling beam).
/// </summary>
public class AreaSpandrelRebarData
{
    /// <summary>
    /// Name of the area spandrel
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Number of rebar layers
    /// </summary>
    public int NumberOfLayers { get; set; }

    /// <summary>
    /// Rebar layer information
    /// </summary>
    public List<AreaRebarLayer> Layers { get; set; } = new();

    public override string ToString()
    {
        return $"Spandrel {Name}: {NumberOfLayers} rebar layers";
    }
}
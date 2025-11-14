namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents area edge selection status.
/// </summary>
public class AreaEdgeSelection
{
    /// <summary>
    /// Name of the area
    /// </summary>
    public required string AreaName { get; set; }

    /// <summary>
    /// Number of edges
    /// </summary>
    public int NumberOfEdges { get; set; }

    /// <summary>
    /// Selection status for each edge
    /// </summary>
    public bool[] EdgeSelected { get; set; } = Array.Empty<bool>();

    /// <summary>
    /// Gets indices of selected edges
    /// </summary>
    public int[] GetSelectedEdgeIndices()
    {
        return EdgeSelected
            .Select((selected, index) => new { selected, index })
            .Where(x => x.selected)
            .Select(x => x.index)
            .ToArray();
    }

    /// <summary>
    /// Count of selected edges
    /// </summary>
    public int SelectedEdgeCount => EdgeSelected.Count(x => x);

    public override string ToString()
    {
        return $"{AreaName}: {SelectedEdgeCount}/{NumberOfEdges} edges selected";
    }
}
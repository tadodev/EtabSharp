namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents connectivity information for a point - what objects connect to it.
/// </summary>
public class PointConnectivity
{
    /// <summary>
    /// Name of the point
    /// </summary>
    public required string PointName { get; set; }

    /// <summary>
    /// Names of frame objects connected to this point
    /// </summary>
    public List<string> ConnectedFrames { get; set; } = new();

    /// <summary>
    /// Names of area objects connected to this point
    /// </summary>
    public List<string> ConnectedAreas { get; set; } = new();

    /// <summary>
    /// Names of link objects connected to this point
    /// </summary>
    public List<string> ConnectedLinks { get; set; } = new();

    /// <summary>
    /// Names of cable objects connected to this point
    /// </summary>
    public List<string> ConnectedCables { get; set; } = new();

    /// <summary>
    /// Names of tendon objects connected to this point
    /// </summary>
    public List<string> ConnectedTendons { get; set; } = new();

    /// <summary>
    /// Total count of connected objects
    /// </summary>
    public int TotalConnections =>
        ConnectedFrames.Count + ConnectedAreas.Count + ConnectedLinks.Count +
        ConnectedCables.Count + ConnectedTendons.Count;

    /// <summary>
    /// Whether this point has any connections
    /// </summary>
    public bool HasConnections => TotalConnections > 0;

    public override string ToString()
    {
        return $"Point {PointName}: {ConnectedFrames.Count} frames, {ConnectedAreas.Count} areas, {TotalConnections} total connections";
    }
}
namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Area properties design parameters for shell-type area section
/// </summary>
public class PropShellDesign
{
    /// <summary>
    /// Name of the shell to be assigned design parameters
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Material property name associated with the shell
    /// </summary>
    public string MatProp { get; set; }

    /// <summary>
    /// Steel layout option as enumeration: 0 for Default, 1 for 1 layers, 2 for 2 layers
    /// </summary>
    public int SteelLayoutOption { get; set; }

    /// <summary>
    /// The cover to the centroid of the top reinforcing steel running in the local 1 axis direction of the area object. [L]
    /// This item applies only when SteelLayoutOption = 1 or 2
    /// </summary>
    public double DesignCoverTopDir1 { get; set; }

    /// <summary>
    /// The cover to the centroid of the top reinforcing steel running in the local 2 axis direction of the area object. [L]
    /// This item applies only when SteelLayoutOption = 1 or 2
    /// </summary>
    public double DesignCoverTopDir2 { get; set; }

    /// <summary>
    /// The cover to the centroid of the bottom reinforcing steel running in the local 1 axis direction of the area object. [L]
    /// This item applies only when SteelLayoutOption = 2.
    /// </summary>
    public double DesignCoverBotDir1 { get; set; }

    /// <summary>
    /// The cover to the centroid of the bottom reinforcing steel running in the local 2 axis direction of the area object. [L]
    /// This item applies only when SteelLayoutOption = 2
    /// </summary>
    public double DesignCoverBotDir2 { get; set; }
}
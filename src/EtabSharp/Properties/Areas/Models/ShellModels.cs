namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents shell design data for shell properties.
/// </summary>
public class ShellDesignData
{
    /// <summary>
    /// Gets or sets the material property name for design.
    /// </summary>
    public string MaterialProperty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the steel layout option.
    /// </summary>
    public int SteelLayoutOption { get; set; } = 1;

    /// <summary>
    /// Gets or sets the design cover for top direction 1.
    /// </summary>
    public double DesignCoverTopDir1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the design cover for top direction 2.
    /// </summary>
    public double DesignCoverTopDir2 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the design cover for bottom direction 1.
    /// </summary>
    public double DesignCoverBotDir1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the design cover for bottom direction 2.
    /// </summary>
    public double DesignCoverBotDir2 { get; set; } = 0.0;

    /// <summary>
    /// Validates the shell design data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(MaterialProperty) &&
               SteelLayoutOption >= 0 &&
               DesignCoverTopDir1 >= 0 &&
               DesignCoverTopDir2 >= 0 &&
               DesignCoverBotDir1 >= 0 &&
               DesignCoverBotDir2 >= 0;
    }

    /// <summary>
    /// Creates a copy of the shell design data.
    /// </summary>
    /// <returns>Copy of the ShellDesignData</returns>
    public ShellDesignData Clone()
    {
        return new ShellDesignData
        {
            MaterialProperty = MaterialProperty,
            SteelLayoutOption = SteelLayoutOption,
            DesignCoverTopDir1 = DesignCoverTopDir1,
            DesignCoverTopDir2 = DesignCoverTopDir2,
            DesignCoverBotDir1 = DesignCoverBotDir1,
            DesignCoverBotDir2 = DesignCoverBotDir2
        };
    }

    /// <summary>
    /// Returns a string representation of the shell design data.
    /// </summary>
    /// <returns>String containing shell design information</returns>
    public override string ToString()
    {
        return $"Shell Design: Material={MaterialProperty}, Layout={SteelLayoutOption}";
    }
}

/// <summary>
/// Represents shell layer data for layered shell properties.
/// </summary>
public class ShellLayerData
{
    /// <summary>
    /// Gets or sets the list of shell layers.
    /// </summary>
    public List<ShellLayer> Layers { get; set; } = new List<ShellLayer>();

    /// <summary>
    /// Gets the number of layers.
    /// </summary>
    public int NumberOfLayers => Layers.Count;

    /// <summary>
    /// Adds a layer to the shell.
    /// </summary>
    /// <param name="layer">Shell layer to add</param>
    public void AddLayer(ShellLayer layer)
    {
        if (layer != null)
        {
            Layers.Add(layer);
        }
    }

    /// <summary>
    /// Removes a layer from the shell.
    /// </summary>
    /// <param name="layerName">Name of the layer to remove</param>
    /// <returns>True if layer was removed, false otherwise</returns>
    public bool RemoveLayer(string layerName)
    {
        var layer = Layers.FirstOrDefault(l => l.LayerName == layerName);
        if (layer != null)
        {
            Layers.Remove(layer);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets a layer by name.
    /// </summary>
    /// <param name="layerName">Name of the layer</param>
    /// <returns>Shell layer or null if not found</returns>
    public ShellLayer? GetLayer(string layerName)
    {
        return Layers.FirstOrDefault(l => l.LayerName == layerName);
    }

    /// <summary>
    /// Validates the shell layer data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return Layers.Count > 0 && Layers.All(layer => layer.IsValid());
    }

    /// <summary>
    /// Creates a copy of the shell layer data.
    /// </summary>
    /// <returns>Copy of the ShellLayerData</returns>
    public ShellLayerData Clone()
    {
        return new ShellLayerData
        {
            Layers = Layers.Select(layer => layer.Clone()).ToList()
        };
    }

    /// <summary>
    /// Returns a string representation of the shell layer data.
    /// </summary>
    /// <returns>String containing shell layer information</returns>
    public override string ToString()
    {
        return $"Shell Layers: {NumberOfLayers} layers";
    }
}

/// <summary>
/// Represents a single shell layer.
/// </summary>
public class ShellLayer
{
    /// <summary>
    /// Gets or sets the layer name.
    /// </summary>
    public string LayerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the distance from the reference surface to the mid-height of the layer.
    /// </summary>
    public double Distance { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the layer thickness.
    /// </summary>
    public double Thickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the layer type.
    /// 1 = Shell, 2 = Membrane, 3 = Plate
    /// </summary>
    public ShellLayerType LayerType { get; set; } = ShellLayerType.Shell;

    /// <summary>
    /// Gets or sets the material property name.
    /// </summary>
    public string MaterialProperty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the material angle in degrees.
    /// </summary>
    public double MaterialAngle { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets whether the material behavior is nonlinear.
    /// Used by SetShellLayer API method.
    /// </summary>
    public bool Nonlinear { get; set; } = false;

    /// <summary>
    /// Gets or sets the material behavior type.
    /// 0 = Directional, 1 = Coupled
    /// Used by GetShellLayer_2 API method.
    /// </summary>
    public MaterialBehaviorType MaterialBehavior { get; set; } = MaterialBehaviorType.Directional;

    /// <summary>
    /// Gets or sets the number of integration points in the thickness direction.
    /// </summary>
    public int NumberOfIntegrationPoints { get; set; } = 3;

    /// <summary>
    /// Gets or sets the S11 component behavior type.
    /// 0 = Inactive, 1 = Linear, 2 = Nonlinear
    /// </summary>
    public ComponentBehaviorType S11Type { get; set; } = ComponentBehaviorType.Linear;

    /// <summary>
    /// Gets or sets the S22 component behavior type.
    /// 0 = Inactive, 1 = Linear, 2 = Nonlinear
    /// </summary>
    public ComponentBehaviorType S22Type { get; set; } = ComponentBehaviorType.Linear;

    /// <summary>
    /// Gets or sets the S12 component behavior type.
    /// 0 = Inactive, 1 = Linear, 2 = Nonlinear
    /// </summary>
    public ComponentBehaviorType S12Type { get; set; } = ComponentBehaviorType.Linear;

    /// <summary>
    /// Validates the shell layer.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(LayerName) &&
               !string.IsNullOrEmpty(MaterialProperty) &&
               Thickness > 0 &&
               NumberOfIntegrationPoints > 0;
    }

    /// <summary>
    /// Creates a copy of the shell layer.
    /// </summary>
    /// <returns>Copy of the ShellLayer</returns>
    public ShellLayer Clone()
    {
        return new ShellLayer
        {
            LayerName = LayerName,
            Distance = Distance,
            Thickness = Thickness,
            LayerType = LayerType,
            MaterialProperty = MaterialProperty,
            MaterialAngle = MaterialAngle,
            Nonlinear = Nonlinear,
            MaterialBehavior = MaterialBehavior,
            NumberOfIntegrationPoints = NumberOfIntegrationPoints,
            S11Type = S11Type,
            S22Type = S22Type,
            S12Type = S12Type
        };
    }

    /// <summary>
    /// Returns a string representation of the shell layer.
    /// </summary>
    /// <returns>String containing shell layer information</returns>
    public override string ToString()
    {
        return $"Layer: {LayerName} | Type: {LayerType} | Material: {MaterialProperty} | Thickness: {Thickness:F3}";
    }
}

/// <summary>
/// Shell layer type enumeration.
/// </summary>
public enum ShellLayerType
{
    /// <summary>Shell layer type</summary>
    Shell = 1,
    /// <summary>Membrane layer type</summary>
    Membrane = 2,
    /// <summary>Plate layer type</summary>
    Plate = 3
}

/// <summary>
/// Material behavior type enumeration.
/// </summary>
public enum MaterialBehaviorType
{
    /// <summary>Directional behavior</summary>
    Directional = 0,
    /// <summary>Coupled behavior</summary>
    Coupled = 1
}

/// <summary>
/// Component behavior type enumeration.
/// </summary>
public enum ComponentBehaviorType
{
    /// <summary>Inactive component</summary>
    Inactive = 0,
    /// <summary>Linear behavior</summary>
    Linear = 1,
    /// <summary>Nonlinear behavior</summary>
    Nonlinear = 2
}
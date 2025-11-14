using ETABSv1;

namespace EtabSharp.Properties.Materials.Models;

public abstract class MaterialProperty
{
    /// <summary>
    /// Material name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Material type
    /// </summary>
    public virtual eMatType MaterialType { get; set; }

    /// <summary>
    /// Material color (-1 for auto)
    /// </summary>
    public int Color { get; set; } = -1;

    /// <summary>
    /// Material notes
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Material GUID
    /// </summary>
    public string GUID { get; set; } = string.Empty;

    /// <summary>
    /// Validates basic material properties
    /// </summary>
    public virtual bool IsValid()
    {
        return !string.IsNullOrEmpty(Name);
    }
}
using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Base class for area properties in ETABS.
/// </summary>
public abstract class AreaProperty
{
    /// <summary>
    /// Gets or sets the name of the area property.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the material property name.
    /// </summary>
    public string MaterialProperty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the shell type.
    /// </summary>
    public eShellType ShellType { get; set; } = eShellType.ShellThin;

    /// <summary>
    /// Gets or sets the thickness of the area property.
    /// </summary>
    public double Thickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the color for display purposes.
    /// </summary>
    public int Color { get; set; } = -1;

    /// <summary>
    /// Gets or sets the notes for the property.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the GUID of the property.
    /// </summary>
    public string GUID { get; set; } = string.Empty;

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public abstract eAreaPropertyType PropertyType { get; }

    /// <summary>
    /// Gets or sets the property modifiers.
    /// </summary>
    public AreaPropertyModifiers? Modifiers { get; set; }

    /// <summary>
    /// Returns a string representation of the area property.
    /// </summary>
    /// <returns>String containing property name and basic information</returns>
    public override string ToString()
    {
        return $"{PropertyType}: {Name} | Material: {MaterialProperty} | Thickness: {Thickness:F3}";
    }

    /// <summary>
    /// Validates the area property parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public virtual bool IsValid()
    {
        return !string.IsNullOrEmpty(Name) &&
               !string.IsNullOrEmpty(MaterialProperty) &&
               Thickness > 0;
    }

    /// <summary>
    /// Gets a description of the shell type.
    /// </summary>
    /// <returns>Shell type description</returns>
    public string GetShellTypeDescription()
    {
        return ShellType switch
        {
            eShellType.ShellThin => "Thin Shell",
            eShellType.ShellThick => "Thick Shell",
            eShellType.Membrane => "Membrane",
            eShellType.PlateType => "Plate",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a copy of the current property.
    /// </summary>
    /// <returns>Copy of the area property</returns>
    public abstract AreaProperty Clone();
}

/// <summary>
/// Enumeration for area property types.
/// </summary>
public enum eAreaPropertyType
{
    /// <summary>
    /// Wall property
    /// </summary>
    Wall = 1,
    
    /// <summary>
    /// Slab property
    /// </summary>
    Slab = 2,
    
    /// <summary>
    /// Deck property
    /// </summary>
    Deck = 3,
    
    /// <summary>
    /// Shell property
    /// </summary>
    Shell = 4
}
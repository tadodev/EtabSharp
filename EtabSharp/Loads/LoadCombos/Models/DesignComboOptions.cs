namespace EtabSharp.Loads.LoadCombos.Models;

/// <summary>
/// Options for adding default design combinations
/// </summary>
public class DesignComboOptions
{
    /// <summary>Add steel design combinations</summary>
    public bool IncludeSteel { get; set; }

    /// <summary>Add concrete design combinations</summary>
    public bool IncludeConcrete { get; set; }

    /// <summary>Add aluminum design combinations</summary>
    public bool IncludeAluminum { get; set; }

    /// <summary>Add cold-formed steel design combinations</summary>
    public bool IncludeColdFormed { get; set; }

    /// <summary>
    /// Creates options for all design types
    /// </summary>
    public static DesignComboOptions All() => new()
    {
        IncludeSteel = true,
        IncludeConcrete = true,
        IncludeAluminum = true,
        IncludeColdFormed = true
    };

    /// <summary>
    /// Creates options for steel only
    /// </summary>
    public static DesignComboOptions SteelOnly() => new()
    {
        IncludeSteel = true
    };

    /// <summary>
    /// Creates options for concrete only
    /// </summary>
    public static DesignComboOptions ConcreteOnly() => new()
    {
        IncludeConcrete = true
    };
}
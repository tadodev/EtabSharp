namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Auto-select wall properties which contain a list of wall properties to be used based on wall height
/// </summary>
public class PropWallAutoSelect
{
    /// <summary>
    /// Name of the auto-select wall property
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// List of wall property names included in the auto-select list
    /// </summary>
    public string[] AutoSelectList { get; set; }

    /// <summary>
    /// Starting wall property name from the auto-select list
    /// </summary>
    public string StartingProperty { get; set; }
}
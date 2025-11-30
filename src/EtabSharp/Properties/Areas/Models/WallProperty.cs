using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents a wall area property in ETABS.
/// </summary>
public class WallProperty : AreaProperty
{
    /// <summary>
    /// Gets or sets the wall property type.
    /// </summary>
    public eWallPropType WallPropType { get; set; } = eWallPropType.Specified;

    /// <summary>
    /// Gets or sets the auto select list for the wall property.
    /// </summary>
    public string[] AutoSelectList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the starting property for auto selection.
    /// </summary>
    public string StartingProperty { get; set; } = "Median";

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public override eAreaPropertyType PropertyType => eAreaPropertyType.Wall;

    /// <summary>
    /// Initializes a new instance of the WallProperty class.
    /// </summary>
    public WallProperty()
    {
    }

    /// <summary>
    /// Initializes a new instance of the WallProperty class with specified parameters.
    /// </summary>
    /// <param name="name">Name of the wall property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="thickness">Wall thickness</param>
    /// <param name="wallPropType">Wall property type</param>
    /// <param name="shellType">Shell type</param>
    public WallProperty(string name, string materialProperty, double thickness,
                       eWallPropType wallPropType = eWallPropType.Specified,
                       eShellType shellType = eShellType.ShellThin)
    {
        Name = name;
        MaterialProperty = materialProperty;
        Thickness = thickness;
        WallPropType = wallPropType;
        ShellType = shellType;
    }

    /// <summary>
    /// Gets a description of the wall property type.
    /// </summary>
    /// <returns>Wall property type description</returns>
    public string GetWallPropTypeDescription()
    {
        return WallPropType switch
        {
            eWallPropType.Specified => "Specified",
            eWallPropType.AutoSelectList => "Auto Select List",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a standard wall property.
    /// </summary>
    /// <param name="name">Name of the wall property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="thickness">Wall thickness</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>WallProperty instance</returns>
    public static WallProperty CreateStandard(string name, string materialProperty, double thickness,
                                             eShellType shellType = eShellType.ShellThin)
    {
        return new WallProperty(name, materialProperty, thickness, eWallPropType.Specified, shellType);
    }

    /// <summary>
    /// Creates an auto select wall property.
    /// </summary>
    /// <param name="name">Name of the wall property</param>
    /// <param name="autoSelectList">Array of property names for auto selection</param>
    /// <param name="startingProperty">Starting property for auto selection</param>
    /// <returns>WallProperty instance</returns>
    public static WallProperty CreateAutoSelect(string name, string[] autoSelectList,
                                               string startingProperty = "Median")
    {
        return new WallProperty
        {
            Name = name,
            WallPropType = eWallPropType.AutoSelectList,
            AutoSelectList = autoSelectList,
            StartingProperty = startingProperty
        };
    }

    /// <summary>
    /// Validates the wall property parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public override bool IsValid()
    {
        if (WallPropType == eWallPropType.AutoSelectList)
        {
            return !string.IsNullOrEmpty(Name) &&
                   AutoSelectList != null &&
                   AutoSelectList.Length > 0 &&
                   !string.IsNullOrEmpty(StartingProperty);
        }

        return base.IsValid();
    }

    /// <summary>
    /// Creates a copy of the current wall property.
    /// </summary>
    /// <returns>Copy of the WallProperty</returns>
    public override AreaProperty Clone()
    {
        return new WallProperty
        {
            Name = Name,
            MaterialProperty = MaterialProperty,
            ShellType = ShellType,
            Thickness = Thickness,
            Color = Color,
            Notes = Notes,
            GUID = GUID,
            WallPropType = WallPropType,
            AutoSelectList = (string[])AutoSelectList.Clone(),
            StartingProperty = StartingProperty,
            Modifiers = Modifiers?.Clone()
        };
    }

    /// <summary>
    /// Returns a string representation of the wall property.
    /// </summary>
    /// <returns>String containing wall property information</returns>
    public override string ToString()
    {
        var baseString = base.ToString();
        var wallTypeString = GetWallPropTypeDescription();

        if (WallPropType == eWallPropType.AutoSelectList)
        {
            return $"{baseString} | Type: {wallTypeString} | Auto Select: {AutoSelectList.Length} options";
        }

        return $"{baseString} | Type: {wallTypeString}";
    }

    /// <summary>
    /// Checks if the wall property uses auto selection.
    /// </summary>
    /// <returns>True if auto selection is used</returns>
    public bool IsAutoSelect()
    {
        return WallPropType == eWallPropType.AutoSelectList;
    }

    /// <summary>
    /// Gets the effective thickness for the wall property.
    /// For auto select walls, this may vary during analysis.
    /// </summary>
    /// <returns>Effective thickness</returns>
    public double GetEffectiveThickness()
    {
        return Thickness;
    }
}
using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents a slab area property in ETABS.
/// </summary>
public class SlabProperty : AreaProperty
{
    /// <summary>
    /// Gets or sets the slab type.
    /// </summary>
    public eSlabType SlabType { get; set; } = eSlabType.Slab;

    /// <summary>
    /// Gets or sets the ribbed slab data (if applicable).
    /// </summary>
    public RibbedSlabData? RibbedData { get; set; }

    /// <summary>
    /// Gets or sets the waffle slab data (if applicable).
    /// </summary>
    public WaffleSlabData? WaffleData { get; set; }

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public override eAreaPropertyType PropertyType => eAreaPropertyType.Slab;

    /// <summary>
    /// Initializes a new instance of the SlabProperty class.
    /// </summary>
    public SlabProperty()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SlabProperty class with specified parameters.
    /// </summary>
    /// <param name="name">Name of the slab property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="thickness">Slab thickness</param>
    /// <param name="slabType">Slab type</param>
    /// <param name="shellType">Shell type</param>
    public SlabProperty(string name, string materialProperty, double thickness,
                       eSlabType slabType = eSlabType.Slab,
                       eShellType shellType = eShellType.ShellThin)
    {
        Name = name;
        MaterialProperty = materialProperty;
        Thickness = thickness;
        SlabType = slabType;
        ShellType = shellType;
    }

    /// <summary>
    /// Gets a description of the slab type.
    /// </summary>
    /// <returns>Slab type description</returns>
    public string GetSlabTypeDescription()
    {
        return SlabType switch
        {
            eSlabType.Slab => "Slab",
            eSlabType.Drop => "Drop Panel",
            eSlabType.Mat => "Mat Foundation",
            eSlabType.Footing => "Footing",
            eSlabType.Ribbed => "Ribbed Slab",
            eSlabType.Waffle => "Waffle Slab",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a standard flat slab property.
    /// </summary>
    /// <param name="name">Name of the slab property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="thickness">Slab thickness</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>SlabProperty instance</returns>
    public static SlabProperty CreateFlat(string name, string materialProperty, double thickness,
                                         eShellType shellType = eShellType.ShellThin)
    {
        return new SlabProperty(name, materialProperty, thickness, eSlabType.Slab, shellType);
    }

    /// <summary>
    /// Creates a ribbed slab property.
    /// </summary>
    /// <param name="name">Name of the slab property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="ribbedData">Ribbed slab parameters</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>SlabProperty instance</returns>
    public static SlabProperty CreateRibbed(string name, string materialProperty, RibbedSlabData ribbedData,
                                           eShellType shellType = eShellType.ShellThin)
    {
        return new SlabProperty
        {
            Name = name,
            MaterialProperty = materialProperty,
            SlabType = eSlabType.Ribbed,
            ShellType = shellType,
            Thickness = ribbedData.OverallDepth,
            RibbedData = ribbedData
        };
    }

    /// <summary>
    /// Creates a waffle slab property.
    /// </summary>
    /// <param name="name">Name of the slab property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="waffleData">Waffle slab parameters</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>SlabProperty instance</returns>
    public static SlabProperty CreateWaffle(string name, string materialProperty, WaffleSlabData waffleData,
                                           eShellType shellType = eShellType.ShellThin)
    {
        return new SlabProperty
        {
            Name = name,
            MaterialProperty = materialProperty,
            SlabType = eSlabType.Waffle,
            ShellType = shellType,
            Thickness = waffleData.OverallDepth,
            WaffleData = waffleData
        };
    }

    /// <summary>
    /// Validates the slab property parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public override bool IsValid()
    {
        if (!base.IsValid())
            return false;

        return SlabType switch
        {
            eSlabType.Ribbed => RibbedData != null && RibbedData.IsValid(),
            eSlabType.Waffle => WaffleData != null && WaffleData.IsValid(),
            _ => true
        };
    }

    /// <summary>
    /// Creates a copy of the current slab property.
    /// </summary>
    /// <returns>Copy of the SlabProperty</returns>
    public override AreaProperty Clone()
    {
        return new SlabProperty
        {
            Name = Name,
            MaterialProperty = MaterialProperty,
            ShellType = ShellType,
            Thickness = Thickness,
            Color = Color,
            Notes = Notes,
            GUID = GUID,
            SlabType = SlabType,
            RibbedData = RibbedData?.Clone(),
            WaffleData = WaffleData?.Clone(),
            Modifiers = Modifiers?.Clone()
        };
    }

    /// <summary>
    /// Returns a string representation of the slab property.
    /// </summary>
    /// <returns>String containing slab property information</returns>
    public override string ToString()
    {
        var baseString = base.ToString();
        var slabTypeString = GetSlabTypeDescription();

        return $"{baseString} | Type: {slabTypeString}";
    }

    /// <summary>
    /// Checks if the slab is a ribbed type.
    /// </summary>
    /// <returns>True if ribbed slab</returns>
    public bool IsRibbed()
    {
        return SlabType == eSlabType.Ribbed;
    }

    /// <summary>
    /// Checks if the slab is a waffle type.
    /// </summary>
    /// <returns>True if waffle slab</returns>
    public bool IsWaffle()
    {
        return SlabType == eSlabType.Waffle;
    }

    /// <summary>
    /// Gets the effective thickness for the slab property.
    /// For ribbed and waffle slabs, this includes the overall depth.
    /// </summary>
    /// <returns>Effective thickness</returns>
    public double GetEffectiveThickness()
    {
        return SlabType switch
        {
            eSlabType.Ribbed => RibbedData?.OverallDepth ?? Thickness,
            eSlabType.Waffle => WaffleData?.OverallDepth ?? Thickness,
            _ => Thickness
        };
    }
}

/// <summary>
/// Represents ribbed slab data.
/// </summary>
public class RibbedSlabData
{
    /// <summary>
    /// Gets or sets the overall depth of the ribbed slab.
    /// </summary>
    public double OverallDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the slab thickness (top flange).
    /// </summary>
    public double SlabThickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the stem width at the top.
    /// </summary>
    public double StemWidthTop { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the stem width at the bottom.
    /// </summary>
    public double StemWidthBot { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib spacing.
    /// </summary>
    public double RibSpacing { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the direction parallel to ribs (1 or 2).
    /// </summary>
    public int RibsParallelTo { get; set; } = 1;

    /// <summary>
    /// Validates the ribbed slab data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return OverallDepth > 0 &&
               SlabThickness > 0 &&
               StemWidthTop > 0 &&
               StemWidthBot > 0 &&
               RibSpacing > 0 &&
               (RibsParallelTo == 1 || RibsParallelTo == 2) &&
               SlabThickness < OverallDepth;
    }

    /// <summary>
    /// Creates a copy of the ribbed slab data.
    /// </summary>
    /// <returns>Copy of the RibbedSlabData</returns>
    public RibbedSlabData Clone()
    {
        return new RibbedSlabData
        {
            OverallDepth = OverallDepth,
            SlabThickness = SlabThickness,
            StemWidthTop = StemWidthTop,
            StemWidthBot = StemWidthBot,
            RibSpacing = RibSpacing,
            RibsParallelTo = RibsParallelTo
        };
    }

    /// <summary>
    /// Returns a string representation of the ribbed slab data.
    /// </summary>
    /// <returns>String containing ribbed slab information</returns>
    public override string ToString()
    {
        return $"Ribbed: Depth={OverallDepth:F3}, Slab={SlabThickness:F3}, Spacing={RibSpacing:F3}";
    }
}

/// <summary>
/// Represents waffle slab data.
/// </summary>
public class WaffleSlabData
{
    /// <summary>
    /// Gets or sets the overall depth of the waffle slab.
    /// </summary>
    public double OverallDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the slab thickness (top flange).
    /// </summary>
    public double SlabThickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the stem width at the top.
    /// </summary>
    public double StemWidthTop { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the stem width at the bottom.
    /// </summary>
    public double StemWidthBot { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib spacing in direction 1.
    /// </summary>
    public double RibSpacingDir1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib spacing in direction 2.
    /// </summary>
    public double RibSpacingDir2 { get; set; } = 0.0;

    /// <summary>
    /// Validates the waffle slab data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return OverallDepth > 0 &&
               SlabThickness > 0 &&
               StemWidthTop > 0 &&
               StemWidthBot > 0 &&
               RibSpacingDir1 > 0 &&
               RibSpacingDir2 > 0 &&
               SlabThickness < OverallDepth;
    }

    /// <summary>
    /// Creates a copy of the waffle slab data.
    /// </summary>
    /// <returns>Copy of the WaffleSlabData</returns>
    public WaffleSlabData Clone()
    {
        return new WaffleSlabData
        {
            OverallDepth = OverallDepth,
            SlabThickness = SlabThickness,
            StemWidthTop = StemWidthTop,
            StemWidthBot = StemWidthBot,
            RibSpacingDir1 = RibSpacingDir1,
            RibSpacingDir2 = RibSpacingDir2
        };
    }

    /// <summary>
    /// Returns a string representation of the waffle slab data.
    /// </summary>
    /// <returns>String containing waffle slab information</returns>
    public override string ToString()
    {
        return $"Waffle: Depth={OverallDepth:F3}, Slab={SlabThickness:F3}, Spacing1={RibSpacingDir1:F3}, Spacing2={RibSpacingDir2:F3}";
    }
}
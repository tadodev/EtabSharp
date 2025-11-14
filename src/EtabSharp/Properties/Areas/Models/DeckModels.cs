namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents filled deck data for composite deck properties.
/// </summary>
public class FilledDeckData
{
    /// <summary>
    /// Gets or sets the slab depth.
    /// </summary>
    public double SlabDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib depth.
    /// </summary>
    public double RibDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib width at top.
    /// </summary>
    public double RibWidthTop { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib width at bottom.
    /// </summary>
    public double RibWidthBot { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib spacing.
    /// </summary>
    public double RibSpacing { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear thickness.
    /// </summary>
    public double ShearThickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the unit weight.
    /// </summary>
    public double UnitWeight { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud diameter.
    /// </summary>
    public double ShearStudDiameter { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud height.
    /// </summary>
    public double ShearStudHeight { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud ultimate strength.
    /// </summary>
    public double ShearStudFu { get; set; } = 0.0;

    /// <summary>
    /// Validates the filled deck data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return SlabDepth > 0 &&
               RibDepth > 0 &&
               RibWidthTop > 0 &&
               RibWidthBot > 0 &&
               RibSpacing > 0 &&
               ShearThickness >= 0 &&
               UnitWeight >= 0;
    }

    /// <summary>
    /// Creates a copy of the filled deck data.
    /// </summary>
    /// <returns>Copy of the FilledDeckData</returns>
    public FilledDeckData Clone()
    {
        return new FilledDeckData
        {
            SlabDepth = SlabDepth,
            RibDepth = RibDepth,
            RibWidthTop = RibWidthTop,
            RibWidthBot = RibWidthBot,
            RibSpacing = RibSpacing,
            ShearThickness = ShearThickness,
            UnitWeight = UnitWeight,
            ShearStudDiameter = ShearStudDiameter,
            ShearStudHeight = ShearStudHeight,
            ShearStudFu = ShearStudFu
        };
    }

    /// <summary>
    /// Returns a string representation of the filled deck data.
    /// </summary>
    /// <returns>String containing filled deck information</returns>
    public override string ToString()
    {
        return $"Filled Deck: SlabDepth={SlabDepth:F2}, RibDepth={RibDepth:F2}, RibSpacing={RibSpacing:F2}";
    }
}

/// <summary>
/// Represents unfilled deck data for unfilled deck properties.
/// </summary>
public class UnfilledDeckData
{
    /// <summary>
    /// Gets or sets the rib depth.
    /// </summary>
    public double RibDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib width at top.
    /// </summary>
    public double RibWidthTop { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib width at bottom.
    /// </summary>
    public double RibWidthBot { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the rib spacing.
    /// </summary>
    public double RibSpacing { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear thickness.
    /// </summary>
    public double ShearThickness { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the unit weight.
    /// </summary>
    public double UnitWeight { get; set; } = 0.0;

    /// <summary>
    /// Validates the unfilled deck data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return RibDepth > 0 &&
               RibWidthTop > 0 &&
               RibWidthBot > 0 &&
               RibSpacing > 0 &&
               ShearThickness >= 0 &&
               UnitWeight >= 0;
    }

    /// <summary>
    /// Creates a copy of the unfilled deck data.
    /// </summary>
    /// <returns>Copy of the UnfilledDeckData</returns>
    public UnfilledDeckData Clone()
    {
        return new UnfilledDeckData
        {
            RibDepth = RibDepth,
            RibWidthTop = RibWidthTop,
            RibWidthBot = RibWidthBot,
            RibSpacing = RibSpacing,
            ShearThickness = ShearThickness,
            UnitWeight = UnitWeight
        };
    }

    /// <summary>
    /// Returns a string representation of the unfilled deck data.
    /// </summary>
    /// <returns>String containing unfilled deck information</returns>
    public override string ToString()
    {
        return $"Unfilled Deck: RibDepth={RibDepth:F2}, RibSpacing={RibSpacing:F2}";
    }
}

/// <summary>
/// Represents solid slab deck data for solid slab deck properties.
/// </summary>
public class SolidSlabDeckData
{
    /// <summary>
    /// Gets or sets the slab depth.
    /// </summary>
    public double SlabDepth { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud diameter.
    /// </summary>
    public double ShearStudDiameter { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud height.
    /// </summary>
    public double ShearStudHeight { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the shear stud ultimate strength.
    /// </summary>
    public double ShearStudFu { get; set; } = 0.0;

    /// <summary>
    /// Validates the solid slab deck data.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool IsValid()
    {
        return SlabDepth > 0;
    }

    /// <summary>
    /// Creates a copy of the solid slab deck data.
    /// </summary>
    /// <returns>Copy of the SolidSlabDeckData</returns>
    public SolidSlabDeckData Clone()
    {
        return new SolidSlabDeckData
        {
            SlabDepth = SlabDepth,
            ShearStudDiameter = ShearStudDiameter,
            ShearStudHeight = ShearStudHeight,
            ShearStudFu = ShearStudFu
        };
    }

    /// <summary>
    /// Returns a string representation of the solid slab deck data.
    /// </summary>
    /// <returns>String containing solid slab deck information</returns>
    public override string ToString()
    {
        return $"Solid Slab Deck: SlabDepth={SlabDepth:F2}";
    }
}
using ETABSv1;

namespace EtabSharp.Properties.Areas.Models;

/// <summary>
/// Represents a deck area property in ETABS.
/// </summary>
public class DeckProperty : AreaProperty
{
    /// <summary>
    /// Gets or sets the deck type.
    /// </summary>
    public eDeckType DeckType { get; set; } = eDeckType.Filled;

    /// <summary>
    /// Gets or sets the slab fill material property (for composite decks).
    /// </summary>
    public string SlabFillMaterialProperty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the deck material property.
    /// </summary>
    public string DeckMaterialProperty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filled deck data (if applicable).
    /// </summary>
    public FilledDeckData? FilledData { get; set; }

    /// <summary>
    /// Gets or sets the unfilled deck data (if applicable).
    /// </summary>
    public UnfilledDeckData? UnfilledData { get; set; }

    /// <summary>
    /// Gets or sets the solid slab deck data (if applicable).
    /// </summary>
    public SolidSlabDeckData? SolidSlabData { get; set; }

    /// <summary>
    /// Gets the property type.
    /// </summary>
    public override eAreaPropertyType PropertyType => eAreaPropertyType.Deck;

    /// <summary>
    /// Initializes a new instance of the DeckProperty class.
    /// </summary>
    public DeckProperty()
    {
    }

    /// <summary>
    /// Initializes a new instance of the DeckProperty class with specified parameters.
    /// </summary>
    /// <param name="name">Name of the deck property</param>
    /// <param name="materialProperty">Material property name</param>
    /// <param name="thickness">Deck thickness</param>
    /// <param name="deckType">Deck type</param>
    /// <param name="shellType">Shell type</param>
    public DeckProperty(string name, string materialProperty, double thickness, 
                       eDeckType deckType = eDeckType.Filled, 
                       eShellType shellType = eShellType.ShellThin)
    {
        Name = name;
        MaterialProperty = materialProperty;
        Thickness = thickness;
        DeckType = deckType;
        ShellType = shellType;
    }

    /// <summary>
    /// Gets a description of the deck type.
    /// </summary>
    /// <returns>Deck type description</returns>
    public string GetDeckTypeDescription()
    {
        return DeckType switch
        {
            eDeckType.Filled => "Filled",
            eDeckType.Unfilled => "Unfilled",
            eDeckType.SolidSlab => "Solid Slab",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a filled deck property.
    /// </summary>
    /// <param name="name">Name of the deck property</param>
    /// <param name="slabFillMaterial">Slab fill material property</param>
    /// <param name="deckMaterial">Deck material property</param>
    /// <param name="filledData">Filled deck parameters</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>DeckProperty instance</returns>
    public static DeckProperty CreateFilled(string name, string slabFillMaterial, string deckMaterial, 
                                           FilledDeckData filledData, eShellType shellType = eShellType.ShellThin)
    {
        return new DeckProperty
        {
            Name = name,
            SlabFillMaterialProperty = slabFillMaterial,
            DeckMaterialProperty = deckMaterial,
            DeckType = eDeckType.Filled,
            ShellType = shellType,
            Thickness = filledData.SlabDepth + filledData.RibDepth,
            FilledData = filledData
        };
    }

    /// <summary>
    /// Creates an unfilled deck property.
    /// </summary>
    /// <param name="name">Name of the deck property</param>
    /// <param name="deckMaterial">Deck material property</param>
    /// <param name="unfilledData">Unfilled deck parameters</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>DeckProperty instance</returns>
    public static DeckProperty CreateUnfilled(string name, string deckMaterial, UnfilledDeckData unfilledData, 
                                             eShellType shellType = eShellType.ShellThin)
    {
        return new DeckProperty
        {
            Name = name,
            DeckMaterialProperty = deckMaterial,
            DeckType = eDeckType.Unfilled,
            ShellType = shellType,
            Thickness = unfilledData.RibDepth,
            UnfilledData = unfilledData
        };
    }

    /// <summary>
    /// Creates a solid slab deck property.
    /// </summary>
    /// <param name="name">Name of the deck property</param>
    /// <param name="slabMaterial">Slab material property</param>
    /// <param name="solidSlabData">Solid slab deck parameters</param>
    /// <param name="shellType">Shell type</param>
    /// <returns>DeckProperty instance</returns>
    public static DeckProperty CreateSolidSlab(string name, string slabMaterial, SolidSlabDeckData solidSlabData, 
                                              eShellType shellType = eShellType.ShellThin)
    {
        return new DeckProperty
        {
            Name = name,
            SlabFillMaterialProperty = slabMaterial,
            DeckType = eDeckType.SolidSlab,
            ShellType = shellType,
            Thickness = solidSlabData.SlabDepth,
            SolidSlabData = solidSlabData
        };
    }

    /// <summary>
    /// Validates the deck property parameters.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public override bool IsValid()
    {
        if (!base.IsValid())
            return false;

        return DeckType switch
        {
            eDeckType.Filled => FilledData != null && FilledData.IsValid() && 
                               !string.IsNullOrEmpty(SlabFillMaterialProperty) && 
                               !string.IsNullOrEmpty(DeckMaterialProperty),
            eDeckType.Unfilled => UnfilledData != null && UnfilledData.IsValid() && 
                                 !string.IsNullOrEmpty(DeckMaterialProperty),
            eDeckType.SolidSlab => SolidSlabData != null && SolidSlabData.IsValid() && 
                                  !string.IsNullOrEmpty(SlabFillMaterialProperty),
            _ => false
        };
    }

    /// <summary>
    /// Creates a copy of the current deck property.
    /// </summary>
    /// <returns>Copy of the DeckProperty</returns>
    public override AreaProperty Clone()
    {
        return new DeckProperty
        {
            Name = Name,
            MaterialProperty = MaterialProperty,
            ShellType = ShellType,
            Thickness = Thickness,
            Color = Color,
            Notes = Notes,
            GUID = GUID,
            DeckType = DeckType,
            SlabFillMaterialProperty = SlabFillMaterialProperty,
            DeckMaterialProperty = DeckMaterialProperty,
            FilledData = FilledData?.Clone(),
            UnfilledData = UnfilledData?.Clone(),
            SolidSlabData = SolidSlabData?.Clone(),
            Modifiers = Modifiers?.Clone()
        };
    }

    /// <summary>
    /// Returns a string representation of the deck property.
    /// </summary>
    /// <returns>String containing deck property information</returns>
    public override string ToString()
    {
        var baseString = base.ToString();
        var deckTypeString = GetDeckTypeDescription();
        
        return $"{baseString} | Type: {deckTypeString}";
    }

    /// <summary>
    /// Checks if the deck is a composite type.
    /// </summary>
    /// <returns>True if composite deck</returns>
    public bool IsComposite()
    {
        return DeckType == eDeckType.Filled || DeckType == eDeckType.SolidSlab;
    }

    /// <summary>
    /// Gets the effective depth for structural calculations.
    /// </summary>
    /// <returns>Effective depth</returns>
    public double GetEffectiveDepth()
    {
        return DeckType switch
        {
            eDeckType.Filled => FilledData?.SlabDepth + FilledData?.RibDepth ?? Thickness,
            eDeckType.Unfilled => UnfilledData?.RibDepth ?? Thickness,
            eDeckType.SolidSlab => SolidSlabData?.SlabDepth ?? Thickness,
            _ => Thickness
        };
    }
}
using EtabSharp.Properties.Areas.Models;

namespace EtabSharp.Interfaces.Properties;

/// <summary>
/// Interface for area property operations in ETABS.
/// Provides type-safe methods for creating, modifying, and querying area properties.
/// </summary>
public interface IPropArea
{
    #region General Property Methods

    /// <summary>
    /// Changes the name of an existing area property.
    /// </summary>
    /// <param name="currentName">Current name of the property</param>
    /// <param name="newName">New name for the property</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets the count of area properties in the model.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Total number of area properties</returns>
    int Count(int propertyType = 0);

    /// <summary>
    /// Deletes an area property from the model.
    /// </summary>
    /// <param name="propertyName">Name of the property to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string propertyName);

    /// <summary>
    /// Gets the names of all area properties.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Array of property names</returns>
    string[] GetNameList(int propertyType = 0);

    /// <summary>
    /// Gets the property type for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Property type enumeration</returns>
    eAreaPropertyType GetPropertyType(string propertyName);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="modifiers">AreaPropertyModifiers model with modifier values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetModifiers(string propertyName, AreaPropertyModifiers modifiers);

    /// <summary>
    /// Gets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>AreaPropertyModifiers model with modifier values</returns>
    AreaPropertyModifiers GetModifiers(string propertyName);

    #endregion

    #region Wall Properties

    /// <summary>
    /// Sets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="wallProperty">WallProperty model with wall parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetWall(string propertyName, WallProperty wallProperty);

    /// <summary>
    /// Gets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>WallProperty model with wall parameters</returns>
    WallProperty GetWall(string propertyName);

    /// <summary>
    /// Sets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="autoSelectList">Array of property names for auto selection</param>
    /// <param name="startingProperty">Starting property for auto selection</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetWallAutoSelectList(string propertyName, string[] autoSelectList, string startingProperty = "Median");

    /// <summary>
    /// Gets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Tuple of (AutoSelectList, StartingProperty)</returns>
    (string[] AutoSelectList, string StartingProperty) GetWallAutoSelectList(string propertyName);

    #endregion

    #region Slab Properties

    /// <summary>
    /// Sets a slab property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="slabProperty">SlabProperty model with slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSlab(string propertyName, SlabProperty slabProperty);

    /// <summary>
    /// Gets a slab property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>SlabProperty model with slab parameters</returns>
    SlabProperty GetSlab(string propertyName);

    /// <summary>
    /// Sets ribbed slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="ribbedSlabData">RibbedSlabData model with ribbed slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSlabRibbed(string propertyName, RibbedSlabData ribbedSlabData);

    /// <summary>
    /// Gets ribbed slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>RibbedSlabData model with ribbed slab parameters</returns>
    RibbedSlabData GetSlabRibbed(string propertyName);

    /// <summary>
    /// Sets waffle slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="waffleSlabData">WaffleSlabData model with waffle slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSlabWaffle(string propertyName, WaffleSlabData waffleSlabData);

    /// <summary>
    /// Gets waffle slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>WaffleSlabData model with waffle slab parameters</returns>
    WaffleSlabData GetSlabWaffle(string propertyName);

    #endregion

    #region Deck Properties

    /// <summary>
    /// Sets a deck property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="deckProperty">DeckProperty model with deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDeck(string propertyName, DeckProperty deckProperty);

    /// <summary>
    /// Gets a deck property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>DeckProperty model with deck parameters</returns>
    DeckProperty GetDeck(string propertyName);

    /// <summary>
    /// Sets filled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="filledDeckData">FilledDeckData model with filled deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDeckFilled(string propertyName, FilledDeckData filledDeckData);

    /// <summary>
    /// Gets filled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>FilledDeckData model with filled deck parameters</returns>
    FilledDeckData GetDeckFilled(string propertyName);

    /// <summary>
    /// Sets unfilled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="unfilledDeckData">UnfilledDeckData model with unfilled deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDeckUnfilled(string propertyName, UnfilledDeckData unfilledDeckData);

    /// <summary>
    /// Gets unfilled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>UnfilledDeckData model with unfilled deck parameters</returns>
    UnfilledDeckData GetDeckUnfilled(string propertyName);

    /// <summary>
    /// Sets solid slab deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="solidSlabDeckData">SolidSlabDeckData model with solid slab deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDeckSolidSlab(string propertyName, SolidSlabDeckData solidSlabDeckData);

    /// <summary>
    /// Gets solid slab deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>SolidSlabDeckData model with solid slab deck parameters</returns>
    SolidSlabDeckData GetDeckSolidSlab(string propertyName);

    #endregion

    #region Shell Layer Methods

    /// <summary>
    /// Sets shell layer data for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="shellLayers">ShellLayerData model with layer information</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetShellLayer(string propertyName, ShellLayerData shellLayers);

    /// <summary>
    /// Gets shell layer data for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellLayerData model with layer information</returns>
    ShellLayerData GetShellLayer(string propertyName);

    /// <summary>
    /// Gets shell layer data for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellLayerData model with layer information</returns>
    public ShellLayerData GetShellLayer_2(string propertyName);

    #endregion

    #region Shell Design Methods

    /// <summary>
    /// Sets shell design parameters for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="shellDesign">ShellDesignData model with design parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetShellDesign(string propertyName, ShellDesignData shellDesign);

    /// <summary>
    /// Gets shell design parameters for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellDesignData model with design parameters</returns>
    ShellDesignData GetShellDesign(string propertyName);

    #endregion
}
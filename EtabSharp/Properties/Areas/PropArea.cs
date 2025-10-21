using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Properties.Areas.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// Manages area properties in the ETABS model.
/// Implements the IPropArea interface by wrapping cSapModel.PropArea operations.
/// </summary>
public class PropArea : IPropArea
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the PropArea class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public PropArea(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region General Property Methods

    /// <summary>
    /// Changes the name of an existing area property.
    /// </summary>
    /// <param name="currentName">Current name of the property</param>
    /// <param name="newName">New name for the property</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.PropArea.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change area property name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed area property name from {OldName} to {NewName}", currentName, newName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing area property name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of area properties in the model.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Total number of area properties</returns>
    public int Count(int propertyType = 0)
    {
        try
        {
            int count = _sapModel.PropArea.Count(propertyType);
            _logger.LogDebug("Area property count: {Count} (type filter: {PropertyType})", count, propertyType);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting area property count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes an area property from the model.
    /// </summary>
    /// <param name="propertyName">Name of the property to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int Delete(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int ret = _sapModel.PropArea.Delete(propertyName);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete area property '{propertyName}'");

            _logger.LogDebug("Deleted area property {PropertyName}", propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting area property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the names of all area properties.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Array of property names</returns>
    public string[] GetNameList(int propertyType = 0)
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.PropArea.GetNameList(ref numberNames, ref names, propertyType);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get area property name list");

            _logger.LogDebug("Retrieved {Count} area property names (type filter: {PropertyType})", numberNames, propertyType);
            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting area property name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the property type for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Property type enumeration</returns>
    public eAreaPropertyType GetPropertyType(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int propertyType = 0;
            int ret = _sapModel.PropArea.GetTypeOAPI(propertyName, ref propertyType);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get property type for area property '{propertyName}'");

            return (eAreaPropertyType)propertyType;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting property type for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="modifiers">AreaPropertyModifiers model with modifier values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetModifiers(string propertyName, AreaPropertyModifiers modifiers)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (modifiers == null)
                throw new ArgumentNullException(nameof(modifiers));

            double[] modifierArray = modifiers.ToArray();
            int ret = _sapModel.PropArea.SetModifiers(propertyName, ref modifierArray);

            if (ret != 0)
                throw new EtabsException(ret, "SetModifiers", $"Failed to set modifiers for area property '{propertyName}'");

            _logger.LogDebug("Set modifiers for area property {PropertyName}", propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting modifiers for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>AreaPropertyModifiers model with modifier values</returns>
    public AreaPropertyModifiers GetModifiers(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double[] modifierArray = new double[10];
            int ret = _sapModel.PropArea.GetModifiers(propertyName, ref modifierArray);

            if (ret != 0)
                throw new EtabsException(ret, "GetModifiers", $"Failed to get modifiers for area property '{propertyName}'");

            return AreaPropertyModifiers.FromArray(modifierArray);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting modifiers for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Wall Properties

    /// <summary>
    /// Sets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="wallProperty">WallProperty model with wall parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetWall(string propertyName, WallProperty wallProperty)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (wallProperty == null)
                throw new ArgumentNullException(nameof(wallProperty));

            int ret = _sapModel.PropArea.SetWall(propertyName, wallProperty.WallPropType, wallProperty.ShellType,
                wallProperty.MaterialProperty, wallProperty.Thickness, wallProperty.Color, wallProperty.Notes, wallProperty.GUID);

            if (ret != 0)
                throw new EtabsException(ret, "SetWall", $"Failed to set wall property '{propertyName}'");

            _logger.LogDebug("Set wall property {PropertyName}: {Wall}", propertyName, wallProperty.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>WallProperty model with wall parameters</returns>
    public WallProperty GetWall(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            eWallPropType wallPropType = eWallPropType.Specified;
            eShellType shellType = eShellType.ShellThin;
            string materialProperty = "";
            double thickness = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropArea.GetWall(propertyName, ref wallPropType, ref shellType, ref materialProperty,
                ref thickness, ref color, ref notes, ref guid);

            if (ret != 0)
                throw new EtabsException(ret, "GetWall", $"Failed to get wall property '{propertyName}'");

            return new WallProperty
            {
                Name = propertyName,
                WallPropType = wallPropType,
                ShellType = shellType,
                MaterialProperty = materialProperty,
                Thickness = thickness,
                Color = color,
                Notes = notes,
                GUID = guid
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="autoSelectList">Array of property names for auto selection</param>
    /// <param name="startingProperty">Starting property for auto selection</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetWallAutoSelectList(string propertyName, string[] autoSelectList, string startingProperty = "Median")
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (autoSelectList == null || autoSelectList.Length == 0)
                throw new ArgumentException("Auto select list cannot be null or empty", nameof(autoSelectList));

            int numberItems = autoSelectList.Length;
            int ret = _sapModel.PropArea.SetWallAutoSelectList(propertyName, numberItems, ref autoSelectList, startingProperty);

            if (ret != 0)
                throw new EtabsException(ret, "SetWallAutoSelectList", $"Failed to set auto select list for wall property '{propertyName}'");

            _logger.LogDebug("Set auto select list for wall property {PropertyName}: {Count} items", propertyName, numberItems);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting auto select list for wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Tuple of (AutoSelectList, StartingProperty)</returns>
    public (string[] AutoSelectList, string StartingProperty) GetWallAutoSelectList(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int numberItems = 0;
            string[] autoSelectList = null;
            string startingProperty = "";

            int ret = _sapModel.PropArea.GetWallAutoSelectList(propertyName, ref numberItems, ref autoSelectList, ref startingProperty);

            if (ret != 0)
                throw new EtabsException(ret, "GetWallAutoSelectList", $"Failed to get auto select list for wall property '{propertyName}'");

            return (autoSelectList ?? new string[0], startingProperty);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting auto select list for wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    // Additional methods for Slab, Deck, and Shell properties will be implemented in the next part...
}    #
region Slab Properties

    /// <summary>
    /// Sets a slab property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="slabProperty">SlabProperty model with slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSlab(string propertyName, SlabProperty slabProperty)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (slabProperty == null)
                throw new ArgumentNullException(nameof(slabProperty));

            int ret = _sapModel.PropArea.SetSlab(propertyName, slabProperty.SlabType, slabProperty.ShellType,
                slabProperty.MaterialProperty, slabProperty.Thickness, slabProperty.Color, slabProperty.Notes, slabProperty.GUID);

            if (ret != 0)
                throw new EtabsException(ret, "SetSlab", $"Failed to set slab property '{propertyName}'");

            _logger.LogDebug("Set slab property {PropertyName}: {Slab}", propertyName, slabProperty.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting slab property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a slab property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>SlabProperty model with slab parameters</returns>
    public SlabProperty GetSlab(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            eSlabType slabType = eSlabType.Slab;
            eShellType shellType = eShellType.ShellThin;
            string materialProperty = "";
            double thickness = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropArea.GetSlab(propertyName, ref slabType, ref shellType, ref materialProperty,
                ref thickness, ref color, ref notes, ref guid);

            if (ret != 0)
                throw new EtabsException(ret, "GetSlab", $"Failed to get slab property '{propertyName}'");

            return new SlabProperty
            {
                Name = propertyName,
                SlabType = slabType,
                ShellType = shellType,
                MaterialProperty = materialProperty,
                Thickness = thickness,
                Color = color,
                Notes = notes,
                GUID = guid
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting slab property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets ribbed slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="ribbedSlabData">RibbedSlabData model with ribbed slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSlabRibbed(string propertyName, RibbedSlabData ribbedSlabData)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (ribbedSlabData == null)
                throw new ArgumentNullException(nameof(ribbedSlabData));

            int ret = _sapModel.PropArea.SetSlabRibbed(propertyName, ribbedSlabData.OverallDepth, ribbedSlabData.SlabThickness,
                ribbedSlabData.StemWidthTop, ribbedSlabData.StemWidthBot, ribbedSlabData.RibSpacing, ribbedSlabData.RibsParallelTo);

            if (ret != 0)
                throw new EtabsException(ret, "SetSlabRibbed", $"Failed to set ribbed slab parameters for property '{propertyName}'");

            _logger.LogDebug("Set ribbed slab parameters for property {PropertyName}: {RibbedSlab}", propertyName, ribbedSlabData.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting ribbed slab parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets ribbed slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>RibbedSlabData model with ribbed slab parameters</returns>
    public RibbedSlabData GetSlabRibbed(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double overallDepth = 0, slabThickness = 0, stemWidthTop = 0, stemWidthBot = 0, ribSpacing = 0;
            int ribsParallelTo = 0;

            int ret = _sapModel.PropArea.GetSlabRibbed(propertyName, ref overallDepth, ref slabThickness,
                ref stemWidthTop, ref stemWidthBot, ref ribSpacing, ref ribsParallelTo);

            if (ret != 0)
                throw new EtabsException(ret, "GetSlabRibbed", $"Failed to get ribbed slab parameters for property '{propertyName}'");

            return new RibbedSlabData
            {
                OverallDepth = overallDepth,
                SlabThickness = slabThickness,
                StemWidthTop = stemWidthTop,
                StemWidthBot = stemWidthBot,
                RibSpacing = ribSpacing,
                RibsParallelTo = ribsParallelTo
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting ribbed slab parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets waffle slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="waffleSlabData">WaffleSlabData model with waffle slab parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSlabWaffle(string propertyName, WaffleSlabData waffleSlabData)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (waffleSlabData == null)
                throw new ArgumentNullException(nameof(waffleSlabData));

            int ret = _sapModel.PropArea.SetSlabWaffle(propertyName, waffleSlabData.OverallDepth, waffleSlabData.SlabThickness,
                waffleSlabData.StemWidthTop, waffleSlabData.StemWidthBot, waffleSlabData.RibSpacingDir1, waffleSlabData.RibSpacingDir2);

            if (ret != 0)
                throw new EtabsException(ret, "SetSlabWaffle", $"Failed to set waffle slab parameters for property '{propertyName}'");

            _logger.LogDebug("Set waffle slab parameters for property {PropertyName}: {WaffleSlab}", propertyName, waffleSlabData.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting waffle slab parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets waffle slab parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>WaffleSlabData model with waffle slab parameters</returns>
    public WaffleSlabData GetSlabWaffle(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double overallDepth = 0, slabThickness = 0, stemWidthTop = 0, stemWidthBot = 0;
            double ribSpacingDir1 = 0, ribSpacingDir2 = 0;

            int ret = _sapModel.PropArea.GetSlabWaffle(propertyName, ref overallDepth, ref slabThickness,
                ref stemWidthTop, ref stemWidthBot, ref ribSpacingDir1, ref ribSpacingDir2);

            if (ret != 0)
                throw new EtabsException(ret, "GetSlabWaffle", $"Failed to get waffle slab parameters for property '{propertyName}'");

            return new WaffleSlabData
            {
                OverallDepth = overallDepth,
                SlabThickness = slabThickness,
                StemWidthTop = stemWidthTop,
                StemWidthBot = stemWidthBot,
                RibSpacingDir1 = ribSpacingDir1,
                RibSpacingDir2 = ribSpacingDir2
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting waffle slab parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Deck Properties

    /// <summary>
    /// Sets a deck property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="deckProperty">DeckProperty model with deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDeck(string propertyName, DeckProperty deckProperty)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (deckProperty == null)
                throw new ArgumentNullException(nameof(deckProperty));

            int ret = _sapModel.PropArea.SetDeck(propertyName, deckProperty.DeckType, deckProperty.ShellType,
                deckProperty.MaterialProperty, deckProperty.Thickness, deckProperty.Color, deckProperty.Notes, deckProperty.GUID);

            if (ret != 0)
                throw new EtabsException(ret, "SetDeck", $"Failed to set deck property '{propertyName}'");

            _logger.LogDebug("Set deck property {PropertyName}: {Deck}", propertyName, deckProperty.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting deck property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a deck property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>DeckProperty model with deck parameters</returns>
    public DeckProperty GetDeck(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            eDeckType deckType = eDeckType.Filled;
            eShellType shellType = eShellType.ShellThin;
            string materialProperty = "";
            double thickness = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropArea.GetDeck(propertyName, ref deckType, ref shellType, ref materialProperty,
                ref thickness, ref color, ref notes, ref guid);

            if (ret != 0)
                throw new EtabsException(ret, "GetDeck", $"Failed to get deck property '{propertyName}'");

            return new DeckProperty
            {
                Name = propertyName,
                DeckType = deckType,
                ShellType = shellType,
                MaterialProperty = materialProperty,
                Thickness = thickness,
                Color = color,
                Notes = notes,
                GUID = guid
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting deck property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets filled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="filledDeckData">FilledDeckData model with filled deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDeckFilled(string propertyName, FilledDeckData filledDeckData)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (filledDeckData == null)
                throw new ArgumentNullException(nameof(filledDeckData));

            int ret = _sapModel.PropArea.SetDeckFilled(propertyName, filledDeckData.SlabDepth, filledDeckData.RibDepth,
                filledDeckData.RibWidthTop, filledDeckData.RibWidthBot, filledDeckData.RibSpacing, filledDeckData.ShearThickness,
                filledDeckData.UnitWeight, filledDeckData.ShearStudDiameter, filledDeckData.ShearStudHeight, filledDeckData.ShearStudFu);

            if (ret != 0)
                throw new EtabsException(ret, "SetDeckFilled", $"Failed to set filled deck parameters for property '{propertyName}'");

            _logger.LogDebug("Set filled deck parameters for property {PropertyName}: {FilledDeck}", propertyName, filledDeckData.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting filled deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets filled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>FilledDeckData model with filled deck parameters</returns>
    public FilledDeckData GetDeckFilled(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double slabDepth = 0, ribDepth = 0, ribWidthTop = 0, ribWidthBot = 0;
            double ribSpacing = 0, shearThickness = 0, unitWeight = 0;
            double shearStudDiameter = 0, shearStudHeight = 0, shearStudFu = 0;

            int ret = _sapModel.PropArea.GetDeckFilled(propertyName, ref slabDepth, ref ribDepth,
                ref ribWidthTop, ref ribWidthBot, ref ribSpacing, ref shearThickness,
                ref unitWeight, ref shearStudDiameter, ref shearStudHeight, ref shearStudFu);

            if (ret != 0)
                throw new EtabsException(ret, "GetDeckFilled", $"Failed to get filled deck parameters for property '{propertyName}'");

            return new FilledDeckData
            {
                SlabDepth = slabDepth,
                RibDepth = ribDepth,
                RibWidthTop = ribWidthTop,
                RibWidthBot = ribWidthBot,
                RibSpacing = ribSpacing,
                ShearThickness = shearThickness,
                UnitWeight = unitWeight,
                ShearStudDiameter = shearStudDiameter,
                ShearStudHeight = shearStudHeight,
                ShearStudFu = shearStudFu
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting filled deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets unfilled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="unfilledDeckData">UnfilledDeckData model with unfilled deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDeckUnfilled(string propertyName, UnfilledDeckData unfilledDeckData)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (unfilledDeckData == null)
                throw new ArgumentNullException(nameof(unfilledDeckData));

            int ret = _sapModel.PropArea.SetDeckUnfilled(propertyName, unfilledDeckData.RibDepth,
                unfilledDeckData.RibWidthTop, unfilledDeckData.RibWidthBot, unfilledDeckData.RibSpacing,
                unfilledDeckData.ShearThickness, unfilledDeckData.UnitWeight);

            if (ret != 0)
                throw new EtabsException(ret, "SetDeckUnfilled", $"Failed to set unfilled deck parameters for property '{propertyName}'");

            _logger.LogDebug("Set unfilled deck parameters for property {PropertyName}: {UnfilledDeck}", propertyName, unfilledDeckData.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting unfilled deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets unfilled deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>UnfilledDeckData model with unfilled deck parameters</returns>
    public UnfilledDeckData GetDeckUnfilled(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double ribDepth = 0, ribWidthTop = 0, ribWidthBot = 0;
            double ribSpacing = 0, shearThickness = 0, unitWeight = 0;

            int ret = _sapModel.PropArea.GetDeckUnfilled(propertyName, ref ribDepth,
                ref ribWidthTop, ref ribWidthBot, ref ribSpacing, ref shearThickness, ref unitWeight);

            if (ret != 0)
                throw new EtabsException(ret, "GetDeckUnfilled", $"Failed to get unfilled deck parameters for property '{propertyName}'");

            return new UnfilledDeckData
            {
                RibDepth = ribDepth,
                RibWidthTop = ribWidthTop,
                RibWidthBot = ribWidthBot,
                RibSpacing = ribSpacing,
                ShearThickness = shearThickness,
                UnitWeight = unitWeight
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting unfilled deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets solid slab deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="solidSlabDeckData">SolidSlabDeckData model with solid slab deck parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDeckSolidSlab(string propertyName, SolidSlabDeckData solidSlabDeckData)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (solidSlabDeckData == null)
                throw new ArgumentNullException(nameof(solidSlabDeckData));

            int ret = _sapModel.PropArea.SetDeckSolidSlab(propertyName, solidSlabDeckData.SlabDepth,
                solidSlabDeckData.ShearStudDiameter, solidSlabDeckData.ShearStudHeight, solidSlabDeckData.ShearStudFu);

            if (ret != 0)
                throw new EtabsException(ret, "SetDeckSolidSlab", $"Failed to set solid slab deck parameters for property '{propertyName}'");

            _logger.LogDebug("Set solid slab deck parameters for property {PropertyName}: {SolidSlab}", propertyName, solidSlabDeckData.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting solid slab deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets solid slab deck parameters.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>SolidSlabDeckData model with solid slab deck parameters</returns>
    public SolidSlabDeckData GetDeckSolidSlab(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double slabDepth = 0, shearStudDiameter = 0, shearStudHeight = 0, shearStudFu = 0;

            int ret = _sapModel.PropArea.GetDeckSolidSlab(propertyName, ref slabDepth,
                ref shearStudDiameter, ref shearStudHeight, ref shearStudFu);

            if (ret != 0)
                throw new EtabsException(ret, "GetDeckSolidSlab", $"Failed to get solid slab deck parameters for property '{propertyName}'");

            return new SolidSlabDeckData
            {
                SlabDepth = slabDepth,
                ShearStudDiameter = shearStudDiameter,
                ShearStudHeight = shearStudHeight,
                ShearStudFu = shearStudFu
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting solid slab deck parameters for property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Shell Layer Methods

    /// <summary>
    /// Sets shell layer data for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="shellLayers">ShellLayerData model with layer information</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetShellLayer(string propertyName, ShellLayerData shellLayers)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (shellLayers == null || shellLayers.Layers.Count == 0)
                throw new ArgumentException("Shell layers cannot be null or empty", nameof(shellLayers));

            int ret = 0;
            foreach (var layer in shellLayers.Layers)
            {
                ret = _sapModel.PropArea.SetShellLayer(propertyName, layer.LayerName, layer.Distance, layer.Thickness,
                    layer.MaterialProperty, layer.MaterialAngle, layer.MaterialBehavior, layer.NumberOfIntegrationPoints,
                    layer.S11Type, layer.S22Type, layer.S12Type);

                if (ret != 0)
                    throw new EtabsException(ret, "SetShellLayer", $"Failed to set shell layer '{layer.LayerName}' for property '{propertyName}'");
            }

            _logger.LogDebug("Set {LayerCount} shell layers for property {PropertyName}", shellLayers.NumberOfLayers, propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting shell layers for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets shell layer data for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellLayerData model with layer information</returns>
    public ShellLayerData GetShellLayer(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int numberLayers = 0;
            string[] layerNames = null;
            double[] distances = null, thicknesses = null, materialAngles = null;
            string[] materialProperties = null;
            int[] materialBehaviors = null, numberOfIntegrationPoints = null;
            int[] s11Types = null, s22Types = null, s12Types = null;

            int ret = _sapModel.PropArea.GetShellLayer(propertyName, ref numberLayers, ref layerNames,
                ref distances, ref thicknesses, ref materialProperties, ref materialAngles,
                ref materialBehaviors, ref numberOfIntegrationPoints, ref s11Types, ref s22Types, ref s12Types);

            if (ret != 0)
                throw new EtabsException(ret, "GetShellLayer", $"Failed to get shell layers for property '{propertyName}'");

            var shellLayerData = new ShellLayerData();
            for (int i = 0; i < numberLayers; i++)
            {
                shellLayerData.AddLayer(new ShellLayer
                {
                    LayerName = layerNames[i],
                    Distance = distances[i],
                    Thickness = thicknesses[i],
                    MaterialProperty = materialProperties[i],
                    MaterialAngle = materialAngles[i],
                    MaterialBehavior = materialBehaviors[i],
                    NumberOfIntegrationPoints = numberOfIntegrationPoints[i],
                    S11Type = s11Types[i],
                    S22Type = s22Types[i],
                    S12Type = s12Types[i]
                });
            }

            return shellLayerData;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting shell layers for property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Shell Design Methods

    /// <summary>
    /// Sets shell design parameters for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="shellDesign">ShellDesignData model with design parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetShellDesign(string propertyName, ShellDesignData shellDesign)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (shellDesign == null)
                throw new ArgumentNullException(nameof(shellDesign));

            int ret = _sapModel.PropArea.SetShellDesign(propertyName, shellDesign.MaterialProperty, shellDesign.SteelLayoutOption,
                shellDesign.DesignCoverTopDir1, shellDesign.DesignCoverTopDir2, shellDesign.DesignCoverBotDir1, shellDesign.DesignCoverBotDir2);

            if (ret != 0)
                throw new EtabsException(ret, "SetShellDesign", $"Failed to set shell design for property '{propertyName}'");

            _logger.LogDebug("Set shell design for property {PropertyName}: {ShellDesign}", propertyName, shellDesign.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting shell design for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets shell design parameters for a property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellDesignData model with design parameters</returns>
    public ShellDesignData GetShellDesign(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            string materialProperty = "";
            int steelLayoutOption = 0;
            double designCoverTopDir1 = 0, designCoverTopDir2 = 0;
            double designCoverBotDir1 = 0, designCoverBotDir2 = 0;

            int ret = _sapModel.PropArea.GetShellDesign(propertyName, ref materialProperty, ref steelLayoutOption,
                ref designCoverTopDir1, ref designCoverTopDir2, ref designCoverBotDir1, ref designCoverBotDir2);

            if (ret != 0)
                throw new EtabsException(ret, "GetShellDesign", $"Failed to get shell design for property '{propertyName}'");

            return new ShellDesignData
            {
                MaterialProperty = materialProperty,
                SteelLayoutOption = steelLayoutOption,
                DesignCoverTopDir1 = designCoverTopDir1,
                DesignCoverTopDir2 = designCoverTopDir2,
                DesignCoverBotDir1 = designCoverBotDir1,
                DesignCoverBotDir2 = designCoverBotDir2
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting shell design for property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion
}
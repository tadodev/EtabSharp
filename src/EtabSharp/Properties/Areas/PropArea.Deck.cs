using EtabSharp.Exceptions;
using EtabSharp.Properties.Areas.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// PropArea partial class - Deck Property Methods
/// </summary>
public partial class PropArea
{
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
}
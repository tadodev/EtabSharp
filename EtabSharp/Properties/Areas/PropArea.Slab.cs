using EtabSharp.Exceptions;
using EtabSharp.Properties.Areas.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// PropArea partial class - Slab Property Methods
/// </summary>
public partial class PropArea
{
    #region Slab Properties

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
}
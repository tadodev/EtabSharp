using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// AreaObjectManager partial class - Advanced Methods
/// Contains rebar data methods for wall design.
/// </summary>
public partial class AreaObjectManager
{
    #region Rebar Data

    /// <summary>
    /// Gets rebar data for a wall pier area object.
    /// Wraps cSapModel.AreaObj.GetRebarDataPier.
    /// </summary>
    public AreaPierRebarData GetRebarDataPier(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberRebarLayers = 0;
            string[] layerID = null;
            eWallPierRebarLayerType[] layerType = null;
            double[] clearCover = null;
            string[] barSizeName = null;
            double[] barArea = null;
            double[] barSpacing = null;
            int[] numberBars = null;
            bool[] confined = null;
            double[] endZoneLength = null;
            double[] endZoneThickness = null;
            double[] endZoneOffset = null;

            int ret = _sapModel.AreaObj.GetRebarDataPier(areaName, ref numberRebarLayers, ref layerID,
                ref layerType, ref clearCover, ref barSizeName, ref barArea, ref barSpacing, ref numberBars,
                ref confined, ref endZoneLength, ref endZoneThickness, ref endZoneOffset);

            if (ret != 0)
                throw new EtabsException(ret, "GetRebarDataPier", $"Failed to get pier rebar data for area '{areaName}'");

            var rebarData = new AreaPierRebarData
            {
                Name = areaName,
                NumberOfLayers = numberRebarLayers
            };

            // Populate layers
            for (int i = 0; i < numberRebarLayers; i++)
            {
                var layer = new AreaRebarLayer
                {
                    LayerID = layerID?[i] ?? "",
                    LayerType = (int)(layerType?[i] ?? 0),
                    ClearCover = clearCover?[i] ?? 0,
                    BarSize = barSizeName?[i] ?? "",
                    BarArea = barArea?[i] ?? 0,
                    BarSpacing = barSpacing?[i] ?? 0,
                    NumberOfBars = numberBars?[i] ?? 0,
                    IsConfined = confined?[i] ?? false
                };
                rebarData.Layers.Add(layer);
            }

            _logger.LogDebug("Retrieved pier rebar data for area {AreaName}: {LayerCount} layers",
                areaName, numberRebarLayers);

            return rebarData;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting pier rebar data for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets rebar data for a wall spandrel area object.
    /// Wraps cSapModel.AreaObj.GetRebarDataSpandrel.
    /// </summary>
    public AreaSpandrelRebarData GetRebarDataSpandrel(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberRebarLayers = 0;
            string[] layerID = null;
            eWallSpandrelRebarLayerType[] layerType = null;
            double[] clearCover = null;
            int[] barSizeIndex = null;
            double[] barArea = null;
            double[] barSpacing = null;
            int[] numberBars = null;
            bool[] confined = null;

            int ret = _sapModel.AreaObj.GetRebarDataSpandrel(areaName, ref numberRebarLayers, ref layerID,
                ref layerType, ref clearCover, ref barSizeIndex, ref barArea, ref barSpacing, ref numberBars, ref confined);

            if (ret != 0)
                throw new EtabsException(ret, "GetRebarDataSpandrel", $"Failed to get spandrel rebar data for area '{areaName}'");

            var rebarData = new AreaSpandrelRebarData
            {
                Name = areaName,
                NumberOfLayers = numberRebarLayers
            };

            // Populate layers
            for (int i = 0; i < numberRebarLayers; i++)
            {
                var layer = new AreaRebarLayer
                {
                    LayerID = layerID?[i] ?? "",
                    LayerType = (int)(layerType?[i] ?? 0),
                    ClearCover = clearCover?[i] ?? 0,
                    BarSize = $"Index {barSizeIndex?[i] ?? 0}", // Bar size by index
                    BarArea = barArea?[i] ?? 0,
                    BarSpacing = barSpacing?[i] ?? 0,
                    NumberOfBars = numberBars?[i] ?? 0,
                    IsConfined = confined?[i] ?? false
                };
                rebarData.Layers.Add(layer);
            }

            _logger.LogDebug("Retrieved spandrel rebar data for area {AreaName}: {LayerCount} layers",
                areaName, numberRebarLayers);

            return rebarData;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting spandrel rebar data for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion
}

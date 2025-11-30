using EtabSharp.Exceptions;
using EtabSharp.Properties.Areas.Models;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// PropArea partial class - Shell Design and Layer Methods
/// </summary>
public partial class PropArea
{
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

            int numberLayers = shellLayers.NumberOfLayers;

            // Prepare arrays for API call
            string[] layerNames = new string[numberLayers];
            double[] distances = new double[numberLayers];
            double[] thicknesses = new double[numberLayers];
            string[] materialProperties = new string[numberLayers];
            bool[] nonlinear = new bool[numberLayers];
            double[] materialAngles = new double[numberLayers];
            int[] numberOfIntegrationPoints = new int[numberLayers];

            // Populate arrays from layer data
            for (int i = 0; i < numberLayers; i++)
            {
                var layer = shellLayers.Layers[i];
                layerNames[i] = layer.LayerName;
                distances[i] = layer.Distance;
                thicknesses[i] = layer.Thickness;
                materialProperties[i] = layer.MaterialProperty;
                nonlinear[i] = layer.Nonlinear;
                materialAngles[i] = layer.MaterialAngle;
                numberOfIntegrationPoints[i] = layer.NumberOfIntegrationPoints;
            }

            int ret = _sapModel.PropArea.SetShellLayer(
                propertyName,
                numberLayers,
                ref layerNames,
                ref distances,
                ref thicknesses,
                ref materialProperties,
                ref nonlinear,
                ref materialAngles,
                ref numberOfIntegrationPoints);

            if (ret != 0)
                throw new EtabsException(ret, "SetShellLayer", $"Failed to set shell layers for property '{propertyName}'");

            _logger.LogDebug("Set {LayerCount} shell layers for property {PropertyName}", numberLayers, propertyName);
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
            bool[] nonlinear = null;
            int[] numberOfIntegrationPoints = null;

            int ret = _sapModel.PropArea.GetShellLayer(
                propertyName,
                ref numberLayers,
                ref layerNames,
                ref distances,
                ref thicknesses,
                ref materialProperties,
                ref nonlinear,
                ref materialAngles,
                ref numberOfIntegrationPoints);

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
                    Nonlinear = nonlinear[i],
                    NumberOfIntegrationPoints = numberOfIntegrationPoints[i]
                });
            }

            _logger.LogDebug("Retrieved {LayerCount} shell layers for property {PropertyName}", numberLayers, propertyName);
            return shellLayerData;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting shell layers for property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets shell layer data for a property (using GetShellLayer_2 API with extended properties).
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>ShellLayerData model with layer information including extended properties</returns>
    public ShellLayerData GetShellLayer_2(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int numberLayers = 0;
            string[] layerNames = null;
            double[] distances = null, thicknesses = null, materialAngles = null;
            int[] myType = null, numIntegrationPts = null, matBehavior = null;
            int[] s11Type = null, s22Type = null, s12Type = null;
            string[] matProp = null;

            int ret = _sapModel.PropArea.GetShellLayer_2(
                propertyName,
                ref numberLayers,
                ref layerNames,
                ref distances,
                ref thicknesses,
                ref myType,
                ref numIntegrationPts,
                ref matProp,
                ref materialAngles,
                ref matBehavior,
                ref s11Type,
                ref s22Type,
                ref s12Type);

            if (ret != 0)
                throw new EtabsException(ret, "GetShellLayer_2", $"Failed to get shell layers for property '{propertyName}'");

            var shellLayerData = new ShellLayerData();
            for (int i = 0; i < numberLayers; i++)
            {
                shellLayerData.AddLayer(new ShellLayer
                {
                    LayerName = layerNames[i],
                    Distance = distances[i],
                    Thickness = thicknesses[i],
                    LayerType = (ShellLayerType)myType[i],
                    MaterialProperty = matProp[i],
                    MaterialAngle = materialAngles[i],
                    MaterialBehavior = (MaterialBehaviorType)matBehavior[i],
                    NumberOfIntegrationPoints = numIntegrationPts[i],
                    S11Type = (ComponentBehaviorType)s11Type[i],
                    S22Type = (ComponentBehaviorType)s22Type[i],
                    S12Type = (ComponentBehaviorType)s12Type[i],
                    // Note: Nonlinear can be inferred from component behavior types
                    Nonlinear = s11Type[i] == 2 || s22Type[i] == 2 || s12Type[i] == 2
                });
            }

            _logger.LogDebug("Retrieved {LayerCount} shell layers (extended) for property {PropertyName}", numberLayers, propertyName);
            return shellLayerData;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting shell layers (extended) for property '{propertyName}': {ex.Message}", ex);
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
using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// FrameObjectManager partial class - Design and Analysis Methods
/// </summary>
public partial class FrameObjectManager
{
    #region Design Procedure Methods

    /// <summary>
    /// Sets the design procedure for a frame object.
    /// Wraps cSapModel.FrameObj.SetDesignProcedure.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="designType">Design procedure type (0=No Design, 1=Steel, 2=Concrete, etc.)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDesignProcedure(string frameName, int designType, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetDesignProcedure(frameName, designType, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetDesignProcedure", $"Failed to set design procedure for frame '{frameName}'");

            _logger.LogDebug("Set design procedure {DesignType} for frame {FrameName}", designType, frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting design procedure for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the design procedure for a frame object.
    /// Wraps cSapModel.FrameObj.GetDesignProcedure.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Design procedure type</returns>
    public int GetDesignProcedure(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int designType = 0;
            int ret = _sapModel.FrameObj.GetDesignProcedure(frameName, ref designType);

            if (ret != 0)
                throw new EtabsException(ret, "GetDesignProcedure", $"Failed to get design procedure for frame '{frameName}'");

            return designType;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting design procedure for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Pier and Spandrel Methods

    /// <summary>
    /// Assigns a frame object to a pier label.
    /// Wraps cSapModel.FrameObj.SetPier.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="pierLabel">Name of the pier label (empty to remove assignment)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPier(string frameName, string pierLabel, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetPier(frameName, pierLabel ?? "", itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetPier", $"Failed to set pier for frame '{frameName}'");

            _logger.LogDebug("Set pier {PierLabel} for frame {FrameName}", pierLabel ?? "None", frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting pier for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the pier label assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetPier.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Name of the pier label (empty if none)</returns>
    public string GetPier(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string pierLabel = "";
            int ret = _sapModel.FrameObj.GetPier(frameName, ref pierLabel);

            if (ret != 0)
                throw new EtabsException(ret, "GetPier", $"Failed to get pier for frame '{frameName}'");

            return pierLabel;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting pier for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Assigns a frame object to a spandrel label.
    /// Wraps cSapModel.FrameObj.SetSpandrel.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="spandrelLabel">Name of the spandrel label (empty to remove assignment)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpandrel(string frameName, string spandrelLabel, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetSpandrel(frameName, spandrelLabel ?? "", itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSpandrel", $"Failed to set spandrel for frame '{frameName}'");

            _logger.LogDebug("Set spandrel {SpandrelLabel} for frame {FrameName}", spandrelLabel ?? "None", frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting spandrel for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the spandrel label assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetSpandrel.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Name of the spandrel label (empty if none)</returns>
    public string GetSpandrel(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string spandrelLabel = "";
            int ret = _sapModel.FrameObj.GetSpandrel(frameName, ref spandrelLabel);

            if (ret != 0)
                throw new EtabsException(ret, "GetSpandrel", $"Failed to get spandrel for frame '{frameName}'");

            return spandrelLabel;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting spandrel for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Column Splice Methods

    /// <summary>
    /// Sets column splice overwrite data for a frame object.
    /// Wraps cSapModel.FrameObj.SetColumnSpliceOverwrite.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="spliceOption">Splice option (0=Program Determined, 1=User Defined)</param>
    /// <param name="height">Splice height</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetColumnSpliceOverwrite(string frameName, int spliceOption, double height, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetColumnSpliceOverwrite(frameName, spliceOption, height, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetColumnSpliceOverwrite", $"Failed to set column splice overwrite for frame '{frameName}'");

            _logger.LogDebug("Set column splice overwrite for frame {FrameName}: Option={Option}, Height={Height}",
                frameName, spliceOption, height);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting column splice overwrite for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets column splice overwrite data for a frame object.
    /// Wraps cSapModel.FrameObj.GetColumnSpliceOverwrite.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (SpliceOption, Height)</returns>
    public (int SpliceOption, double Height) GetColumnSpliceOverwrite(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int spliceOption = 0;
            double height = 0;
            int ret = _sapModel.FrameObj.GetColumnSpliceOverwrite(frameName, ref spliceOption, ref height);

            if (ret != 0)
                throw new EtabsException(ret, "GetColumnSpliceOverwrite", $"Failed to get column splice overwrite for frame '{frameName}'");

            return (spliceOption, height);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting column splice overwrite for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Tension/Compression Limits

    /// <summary>
    /// Sets tension/compression limits for a frame object.
    /// Wraps cSapModel.FrameObj.SetTCLimits.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="limitCompressionExists">True if compression limit exists</param>
    /// <param name="limitCompression">Compression limit value</param>
    /// <param name="limitTensionExists">True if tension limit exists</param>
    /// <param name="limitTension">Tension limit value</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetTCLimits(string frameName, bool limitCompressionExists, double limitCompression,
        bool limitTensionExists, double limitTension, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetTCLimits(frameName, limitCompressionExists, limitCompression,
                limitTensionExists, limitTension, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetTCLimits", $"Failed to set T/C limits for frame '{frameName}'");

            _logger.LogDebug("Set T/C limits for frame {FrameName}: Compression={CompExists}({CompLimit}), Tension={TenExists}({TenLimit})",
                frameName, limitCompressionExists, limitCompression, limitTensionExists, limitTension);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting T/C limits for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets tension/compression limits for a frame object.
    /// Wraps cSapModel.FrameObj.GetTCLimits.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (CompressionExists, CompressionLimit, TensionExists, TensionLimit)</returns>
    public (bool CompressionExists, double CompressionLimit, bool TensionExists, double TensionLimit) GetTCLimits(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            bool limitCompressionExists = false, limitTensionExists = false;
            double limitCompression = 0, limitTension = 0;

            int ret = _sapModel.FrameObj.GetTCLimits(frameName, ref limitCompressionExists, ref limitCompression,
                ref limitTensionExists, ref limitTension);

            if (ret != 0)
                throw new EtabsException(ret, "GetTCLimits", $"Failed to get T/C limits for frame '{frameName}'");

            return (limitCompressionExists, limitCompression, limitTensionExists, limitTension);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting T/C limits for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Lateral Bracing Methods

    /// <summary>
    /// Sets lateral bracing data for a frame object.
    /// Wraps cSapModel.FrameObj.SetLateralBracing.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType">Bracing type</param>
    /// <param name="location">Bracing location</param>
    /// <param name="distance1">Start distance (relative if relDist=true, actual if relDist=false)</param>
    /// <param name="distance2">End distance (relative if relDist=true, actual if relDist=false)</param>
    /// <param name="relDist">True if distances are relative, false for actual distances</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLateralBracing(string frameName, BracingType bracingType, BracingLocation location,
        double distance1, double distance2, bool relDist = true, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetLateralBracing(frameName, (int)bracingType, (int)location,
                distance1, distance2, relDist, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLateralBracing",
                    $"Failed to set lateral bracing for frame '{frameName}'");

            _logger.LogDebug("Set lateral bracing for frame {FrameName}: Type={Type}, Location={Location}, D1={D1}, D2={D2}, RelDist={RelDist}",
                frameName, bracingType, location, distance1, distance2, relDist);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting lateral bracing for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets lateral bracing data for a frame object.
    /// Wraps cSapModel.FrameObj.GetLateralBracing.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Lateral bracing data object</returns>
    public LateralBracingData GetLateralBracing(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int numberItems = 0;
            string[] frameNames = null;
            int[] myType = null;
            int[] loc = null;
            double[] rd1 = null, rd2 = null;
            double[] dist1 = null, dist2 = null;

            int ret = _sapModel.FrameObj.GetLateralBracing(frameName, ref numberItems, ref frameNames,
                ref myType, ref loc, ref rd1, ref rd2, ref dist1, ref dist2);

            if (ret != 0)
                throw new EtabsException(ret, "GetLateralBracing",
                    $"Failed to get lateral bracing for frame '{frameName}'");

            var bracingData = new LateralBracingData();
            for (int i = 0; i < numberItems; i++)
            {
                bracingData.AddEntry(new LateralBracingEntry
                {
                    FrameName = frameNames[i],
                    BracingType = (BracingType)myType[i],
                    Location = (BracingLocation)loc[i],
                    RelativeDistance1 = rd1[i],
                    RelativeDistance2 = rd2[i],
                    ActualDistance1 = dist1[i],
                    ActualDistance2 = dist2[i]
                });
            }

            _logger.LogDebug("Retrieved {Count} lateral bracing entries for frame {FrameName}",
                numberItems, frameName);
            return bracingData;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting lateral bracing for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes lateral bracing data for a frame object.
    /// Wraps cSapModel.FrameObj.DeleteLateralBracing.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType">Bracing type to delete</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLateralBracing(string frameName, BracingType bracingType,
        eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.DeleteLateralBracing(frameName, (int)bracingType, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLateralBracing",
                    $"Failed to delete lateral bracing for frame '{frameName}'");

            _logger.LogDebug("Deleted lateral bracing for frame {FrameName}: Type={Type}",
                frameName, bracingType);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting lateral bracing for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Clears all lateral bracing for a frame object.
    /// This is a convenience method that uses SetLateralBracing with MyType=3.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for clearing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ClearAllLateralBracing(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            // MyType = 3 means "Clear All" according to the original code comment
            int ret = _sapModel.FrameObj.SetLateralBracing(frameName, 3, 1, 0, 0, true, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "ClearAllLateralBracing",
                    $"Failed to clear all lateral bracing for frame '{frameName}'");

            _logger.LogDebug("Cleared all lateral bracing for frame {FrameName}", frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error clearing lateral bracing for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Output Stations

    /// <summary>
    /// Sets output station data for a frame object.
    /// Wraps cSapModel.FrameObj.SetOutputStations.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="stationType">Station type (1=MaxStaSpacing, 2=MinNumSta)</param>
    /// <param name="maxSegmentSize">Maximum segment size</param>
    /// <param name="minStations">Minimum number of stations</param>
    /// <param name="noOutputAtElementEnds">True to suppress output at element ends</param>
    /// <param name="noOutputAtPointLoads">True to suppress output at point loads</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetOutputStations(string frameName, int stationType, double maxSegmentSize, int minStations,
        bool noOutputAtElementEnds = false, bool noOutputAtPointLoads = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetOutputStations(frameName, stationType, maxSegmentSize, minStations,
                noOutputAtElementEnds, noOutputAtPointLoads, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetOutputStations", $"Failed to set output stations for frame '{frameName}'");

            _logger.LogDebug("Set output stations for frame {FrameName}: Type={Type}, MaxSeg={MaxSeg}, MinSta={MinSta}",
                frameName, stationType, maxSegmentSize, minStations);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting output stations for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets output station data for a frame object.
    /// Wraps cSapModel.FrameObj.GetOutputStations.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (StationType, MaxSegSize, MinStations, NoOutputAtEnds, NoOutputAtLoads)</returns>
    public (int StationType, double MaxSegSize, int MinStations, bool NoOutputAtEnds, bool NoOutputAtLoads) GetOutputStations(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int stationType = 0, minStations = 0;
            double maxSegmentSize = 0;
            bool noOutputAtElementEnds = false, noOutputAtPointLoads = false;

            int ret = _sapModel.FrameObj.GetOutputStations(frameName, ref stationType, ref maxSegmentSize,
                ref minStations, ref noOutputAtElementEnds, ref noOutputAtPointLoads);

            if (ret != 0)
                throw new EtabsException(ret, "GetOutputStations", $"Failed to get output stations for frame '{frameName}'");

            return (stationType, maxSegmentSize, minStations, noOutputAtElementEnds, noOutputAtPointLoads);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting output stations for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Design Settings

    /// <summary>
    /// Sets a frame for steel design.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSteelDesign(string frameName, eItemType itemType = eItemType.Objects)
    {
        return SetDesignProcedure(frameName, 1, itemType); // 1 = Steel design
    }

    /// <summary>
    /// Sets a frame for concrete design.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetConcreteDesign(string frameName, eItemType itemType = eItemType.Objects)
    {
        return SetDesignProcedure(frameName, 2, itemType); // 2 = Concrete design
    }

    /// <summary>
    /// Disables design for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetNoDesign(string frameName, eItemType itemType = eItemType.Objects)
    {
        return SetDesignProcedure(frameName, 0, itemType); // 0 = No design
    }

    /// <summary>
    /// Sets full lateral bracing for the entire length of a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType"></param>
    /// <param name="bracingLocation"></param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetFullLateralBracing(string frameName, BracingType bracingType, BracingLocation bracingLocation, eItemType itemType = eItemType.Objects)
    {
        return SetLateralBracing(frameName, bracingType, bracingLocation, 0.0, 1.0, true, itemType); // 2 = Uniform bracing
    }

    /// <summary>
    /// Sets point lateral bracing at specific locations on a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType"></param>
    /// <param name="bracingLocation"></param>
    /// <param name="distance">Distance along frame (relative)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPointLateralBracing(string frameName, BracingType bracingType, BracingLocation bracingLocation, double distance, eItemType itemType = eItemType.Objects)
    {
        return SetLateralBracing(frameName, bracingType, bracingLocation, distance, distance, true, itemType); // 1 = Point bracing
    }

    /// <summary>
    /// Clears all lateral bracing from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType"></param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ClearLateralBracing(string frameName, BracingType bracingType, eItemType itemType = eItemType.Objects)
    {
        return DeleteLateralBracing(frameName, bracingType, itemType); // 3 = Clear all
    }

    #endregion
}
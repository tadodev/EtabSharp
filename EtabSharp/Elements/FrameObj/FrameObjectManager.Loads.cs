using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// FrameObjectManager partial class - Load Assignment Methods
/// </summary>
public partial class FrameObjectManager
{
    #region Distributed Load Methods

    /// <summary>
    /// Assigns distributed loads to a frame object.
    /// Wraps cSapModel.FrameObj.SetLoadDistributed.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="load">FrameDistributedLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadDistributed(string frameName, FrameDistributedLoad load, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            int ret = _sapModel.FrameObj.SetLoadDistributed(frameName, load.LoadPattern, (int)load.LoadType, 
                (int)load.Direction, load.StartDistance, load.EndDistance, load.StartLoad, load.EndLoad, 
                load.CoordinateSystem, load.IsRelativeDistance, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadDistributed", $"Failed to set distributed load for frame '{frameName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set distributed load for frame {FrameName} in pattern {LoadPattern}: {Load}", 
                frameName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting distributed load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets distributed loads assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetLoadDistributed.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of FrameDistributedLoad models</returns>
    public List<FrameDistributedLoad> GetLoadDistributed(string frameName, string loadPattern = "")
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int numberItems = 0;
            string[] frameNames = null;
            string[] loadPatterns = null;
            int[] loadTypes = null;
            string[] csys = null;
            int[] directions = null;
            double[] startDist = null, endDist = null, startLoad = null, endLoad = null;
            bool[] relDist = null;

            int ret = _sapModel.FrameObj.GetLoadDistributed(frameName, ref numberItems, ref frameNames, 
                ref loadPatterns, ref loadTypes, ref csys, ref directions, ref startDist, ref endDist, 
                ref startLoad, ref endLoad, ref relDist);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadDistributed", $"Failed to get distributed loads for frame '{frameName}'");

            var loads = new List<FrameDistributedLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                // Filter by load pattern if specified
                if (!string.IsNullOrEmpty(loadPattern) && loadPatterns[i] != loadPattern)
                    continue;

                var load = new FrameDistributedLoad
                {
                    FrameName = frameName,
                    LoadPattern = loadPatterns[i],
                    LoadType = (eLoadType)loadTypes[i],
                    Direction = (eLoadDirection)directions[i],
                    StartDistance = startDist[i],
                    EndDistance = endDist[i],
                    StartLoad = startLoad[i],
                    EndLoad = endLoad[i],
                    CoordinateSystem = csys[i],
                    IsRelativeDistance = relDist[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} distributed loads for frame {FrameName}", loads.Count, frameName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting distributed loads for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes distributed loads from a frame object.
    /// Wraps cSapModel.FrameObj.DeleteLoadDistributed.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadDistributed(string frameName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.FrameObj.DeleteLoadDistributed(frameName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadDistributed", $"Failed to delete distributed load for frame '{frameName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted distributed load for frame {FrameName} in pattern {LoadPattern}", frameName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting distributed load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Point Load Methods

    /// <summary>
    /// Assigns point loads to a frame object.
    /// Wraps cSapModel.FrameObj.SetLoadPoint.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="load">FramePointLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadPoint(string frameName, FramePointLoad load, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            int ret = _sapModel.FrameObj.SetLoadPoint(frameName, load.LoadPattern, (int)load.LoadType, 
                (int)load.Direction, load.Distance, load.LoadValue, load.CoordinateSystem, 
                load.IsRelativeDistance, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadPoint", $"Failed to set point load for frame '{frameName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set point load for frame {FrameName} in pattern {LoadPattern}: {Load}", 
                frameName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting point load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets point loads assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetLoadPoint.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of FramePointLoad models</returns>
    public List<FramePointLoad> GetLoadPoint(string frameName, string loadPattern = "")
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int numberItems = 0;
            string[] frameNames = null;
            string[] loadPatterns = null;
            int[] loadTypes = null;
            string[] csys = null;
            int[] directions = null;
            double[] distances = null, loadValues = null;
            bool[] relDist = null;

            int ret = _sapModel.FrameObj.GetLoadPoint(frameName, ref numberItems, ref frameNames, 
                ref loadPatterns, ref loadTypes, ref csys, ref directions, ref distances, 
                ref loadValues, ref relDist);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadPoint", $"Failed to get point loads for frame '{frameName}'");

            var loads = new List<FramePointLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                // Filter by load pattern if specified
                if (!string.IsNullOrEmpty(loadPattern) && loadPatterns[i] != loadPattern)
                    continue;

                var load = new FramePointLoad
                {
                    FrameName = frameName,
                    LoadPattern = loadPatterns[i],
                    LoadType = (eLoadType)loadTypes[i],
                    Direction = (eLoadDirection)directions[i],
                    Distance = distances[i],
                    LoadValue = loadValues[i],
                    CoordinateSystem = csys[i],
                    IsRelativeDistance = relDist[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} point loads for frame {FrameName}", loads.Count, frameName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting point loads for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes point loads from a frame object.
    /// Wraps cSapModel.FrameObj.DeleteLoadPoint.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadPoint(string frameName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.FrameObj.DeleteLoadPoint(frameName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadPoint", $"Failed to delete point load for frame '{frameName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted point load for frame {FrameName} in pattern {LoadPattern}", frameName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting point load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Temperature Load Methods

    /// <summary>
    /// Assigns temperature loads to a frame object.
    /// Wraps cSapModel.FrameObj.SetLoadTemperature.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadType">Temperature load type</param>
    /// <param name="value">Temperature value</param>
    /// <param name="patternName">Joint pattern name (for gradient loads)</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadTemperature(string frameName, string loadPattern, int loadType, double value, 
        string patternName = "", bool replace = true, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.FrameObj.SetLoadTemperature(frameName, loadPattern, loadType, value, 
                patternName, replace, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadTemperature", $"Failed to set temperature load for frame '{frameName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Set temperature load for frame {FrameName} in pattern {LoadPattern}: Type={Type}, Value={Value}", 
                frameName, loadPattern, loadType, value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting temperature load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets temperature loads assigned to frame objects.
    /// Wraps cSapModel.FrameObj.GetLoadTemperature.
    /// </summary>
    /// <param name="frameName">Name of the frame object (empty for all frames)</param>
    /// <param name="itemType">Item type for retrieval</param>
    /// <returns>Temperature load data object</returns>
    public object GetLoadTemperature(string frameName = "", eItemType itemType = eItemType.Objects)
    {
        try
        {
            int numberItems = 0;
            string[] frameNames = null;
            string[] loadPatterns = null;
            int[] loadTypes = null;
            double[] values = null;
            string[] patternNames = null;

            int ret = _sapModel.FrameObj.GetLoadTemperature(frameName, ref numberItems, ref frameNames, 
                ref loadPatterns, ref loadTypes, ref values, ref patternNames, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadTemperature", $"Failed to get temperature loads for frame '{frameName}'");

            // Return structured data
            var temperatureLoads = new List<object>();
            for (int i = 0; i < numberItems; i++)
            {
                temperatureLoads.Add(new
                {
                    FrameName = frameNames[i],
                    LoadPattern = loadPatterns[i],
                    LoadType = loadTypes[i],
                    Value = values[i],
                    PatternName = patternNames[i]
                });
            }

            _logger.LogDebug("Retrieved {Count} temperature loads", numberItems);
            return temperatureLoads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting temperature loads for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes temperature loads from a frame object.
    /// Wraps cSapModel.FrameObj.DeleteLoadTemperature.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadTemperature(string frameName, string loadPattern, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.FrameObj.DeleteLoadTemperature(frameName, loadPattern, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadTemperature", $"Failed to delete temperature load for frame '{frameName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted temperature load for frame {FrameName} in pattern {LoadPattern}", frameName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting temperature load for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Load Types

    /// <summary>
    /// Applies a uniform distributed load over the entire length of a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (force per unit length)</param>
    /// <param name="direction">Load direction</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetUniformLoad(string frameName, string loadPattern, double loadValue, 
        eLoadDirection direction = eLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true)
    {
        var load = new FrameDistributedLoad
        {
            FrameName = frameName,
            LoadPattern = loadPattern,
            LoadType = eLoadType.Force,
            Direction = direction,
            StartDistance = 0.0,
            EndDistance = 1.0,
            StartLoad = loadValue,
            EndLoad = loadValue,
            CoordinateSystem = coordinateSystem,
            IsRelativeDistance = true
        };

        return SetLoadDistributed(frameName, load, replace);
    }

    /// <summary>
    /// Applies a triangular distributed load over the entire length of a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="startLoad">Load value at start (I-end)</param>
    /// <param name="endLoad">Load value at end (J-end)</param>
    /// <param name="direction">Load direction</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetTriangularLoad(string frameName, string loadPattern, double startLoad, double endLoad,
        eLoadDirection direction = eLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true)
    {
        var load = new FrameDistributedLoad
        {
            FrameName = frameName,
            LoadPattern = loadPattern,
            LoadType = eLoadType.Force,
            Direction = direction,
            StartDistance = 0.0,
            EndDistance = 1.0,
            StartLoad = startLoad,
            EndLoad = endLoad,
            CoordinateSystem = coordinateSystem,
            IsRelativeDistance = true
        };

        return SetLoadDistributed(frameName, load, replace);
    }

    /// <summary>
    /// Applies a concentrated load at the midspan of a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value</param>
    /// <param name="direction">Load direction</param>
    /// <param name="coordinateSystem">Coordinate system</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMidspanLoad(string frameName, string loadPattern, double loadValue,
        eLoadDirection direction = eLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true)
    {
        var load = new FramePointLoad
        {
            FrameName = frameName,
            LoadPattern = loadPattern,
            LoadType = eLoadType.Force,
            Direction = direction,
            Distance = 0.5,
            LoadValue = loadValue,
            CoordinateSystem = coordinateSystem,
            IsRelativeDistance = true
        };

        return SetLoadPoint(frameName, load, replace);
    }

    /// <summary>
    /// Applies a uniform temperature change to a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="temperatureChange">Temperature change value</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetUniformTemperatureLoad(string frameName, string loadPattern, double temperatureChange, bool replace = true)
    {
        return SetLoadTemperature(frameName, loadPattern, 1, temperatureChange, "", replace);
    }

    /// <summary>
    /// Gets all loads (distributed, point, and temperature) for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>FrameLoads model with all load types</returns>
    public FrameLoads GetAllLoads(string frameName, string loadPattern = "")
    {
        try
        {
            var frameLoads = new FrameLoads
            {
                FrameName = frameName,
                DistributedLoads = GetLoadDistributed(frameName, loadPattern),
                PointLoads = GetLoadPoint(frameName, loadPattern)
            };

            // Get temperature loads (returns object, would need proper model)
            var tempLoads = GetLoadTemperature(frameName);
            // frameLoads.TemperatureLoads = tempLoads; // Would need proper conversion

            return frameLoads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all loads for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion
}
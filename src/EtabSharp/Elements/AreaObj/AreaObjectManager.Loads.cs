using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// AreaObjectManager partial class - Load Assignment Methods
/// </summary>
public partial class AreaObjectManager
{
    #region Uniform Load Methods

    /// <summary>
    /// Sets uniform load on an area object.
    /// Wraps cSapModel.AreaObj.SetLoadUniform.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaUniformLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadUniform(string areaName, AreaUniformLoad load, bool replace = true, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));
            if (!load.IsValid())
                throw new ArgumentException("Load parameters are not valid", nameof(load));

            // Validate direction according to ETABS API (1-11)
            if (load.Direction < 1 || load.Direction > 11)
                throw new ArgumentException("Direction must be between 1 and 11", nameof(load));

            int ret = _sapModel.AreaObj.SetLoadUniform(areaName, load.LoadPattern, load.LoadValue, 
                load.Direction, replace, load.CoordinateSystem, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadUniform", $"Failed to set uniform load for area '{areaName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set uniform load for area {AreaName} in pattern {LoadPattern}: {Load} (ItemType: {ItemType})", 
                areaName, load.LoadPattern, load.ToString(), itemType);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting uniform load for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets uniform load on an area object using individual parameters.
    /// Wraps cSapModel.AreaObj.SetLoadUniform directly with ETABS API parameters.
    /// </summary>
    /// <param name="name">Name of an existing area object or group</param>
    /// <param name="loadPattern">Name of a defined load pattern</param>
    /// <param name="value">Uniform load value [F/L2]</param>
    /// <param name="direction">Load direction (1-11): 1-3=Local 1-3, 4-6=Global X-Z, 7-9=Projected X-Z, 10=Gravity, 11=Projected Gravity</param>
    /// <param name="replace">If true, replaces all previous uniform loads in the specified load pattern</param>
    /// <param name="coordinateSystem">Coordinate system name ("Local" or coordinate system name)</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadUniform(string name, string loadPattern, double value, int direction, 
        bool replace = true, string coordinateSystem = "Global", eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));
            if (string.IsNullOrEmpty(coordinateSystem))
                throw new ArgumentException("Coordinate system cannot be null or empty", nameof(coordinateSystem));
            
            // Validate direction according to ETABS API (1-11)
            if (direction < 1 || direction > 11)
                throw new ArgumentException("Direction must be between 1 and 11", nameof(direction));

            int ret = _sapModel.AreaObj.SetLoadUniform(name, loadPattern, value, direction, replace, coordinateSystem, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadUniform", $"Failed to set uniform load for '{name}' in pattern '{loadPattern}'");

            _logger.LogDebug("Set uniform load for {Name} in pattern {LoadPattern}: Value={Value}, Dir={Direction}, CSys={CoordinateSystem}, ItemType={ItemType}", 
                name, loadPattern, value, direction, coordinateSystem, itemType);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting uniform load for '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets uniform loads assigned to an area object.
    /// Wraps cSapModel.AreaObj.GetLoadUniform.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaUniformLoad models</returns>
    public List<AreaUniformLoad> GetLoadUniform(string areaName, string loadPattern = "")
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberItems = 0;
            string[] areaNames = null;
            string[] loadPatterns = null;
            string[] csys = null;
            int[] directions = null;
            double[] values = null;

            int ret = _sapModel.AreaObj.GetLoadUniform(areaName, ref numberItems, ref areaNames, 
                ref loadPatterns, ref csys, ref directions, ref values);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadUniform", $"Failed to get uniform loads for area '{areaName}'");

            var loads = new List<AreaUniformLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                // Filter by load pattern if specified
                if (!string.IsNullOrEmpty(loadPattern) && loadPatterns[i] != loadPattern)
                    continue;

                var load = new AreaUniformLoad
                {
                    AreaName = areaName,
                    LoadPattern = loadPatterns[i],
                    LoadValue = values[i],
                    Direction = directions[i],
                    CoordinateSystem = csys[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} uniform loads for area {AreaName}", loads.Count, areaName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting uniform loads for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes uniform loads from an area object.
    /// Wraps cSapModel.AreaObj.DeleteLoadUniform.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadUniform(string areaName, string loadPattern, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.AreaObj.DeleteLoadUniform(areaName, loadPattern, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadUniform", $"Failed to delete uniform load for area '{areaName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted uniform load for area {AreaName} in pattern {LoadPattern}", areaName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting uniform load for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Uniform to Frame Load Methods

    /// <summary>
    /// Sets uniform load transferred to frame objects.
    /// Wraps cSapModel.AreaObj.SetLoadUniformToFrame.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaUniformToFrameLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadUniformToFrame(string areaName, AreaUniformToFrameLoad load, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            int ret = _sapModel.AreaObj.SetLoadUniformToFrame(areaName, load.LoadPattern, load.Value, 
                load.Direction, load.DistributionType, replace, load.CoordinateSystem);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadUniformToFrame", $"Failed to set uniform to frame load for area '{areaName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set uniform to frame load for area {AreaName} in pattern {LoadPattern}: {Load}", 
                areaName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting uniform to frame load for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets uniform loads transferred to frame objects.
    /// Wraps cSapModel.AreaObj.GetLoadUniformToFrame.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaUniformToFrameLoad models</returns>
    public List<AreaUniformToFrameLoad> GetLoadUniformToFrame(string areaName, string loadPattern = "")
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberItems = 0;
            string[] areaNames = null;
            string[] loadPatterns = null;
            string[] csys = null;
            int[] directions = null;
            double[] values = null;
            int[] distTypes = null;

            int ret = _sapModel.AreaObj.GetLoadUniformToFrame(areaName, ref numberItems, ref areaNames, 
                ref loadPatterns, ref csys, ref directions, ref values, ref distTypes);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadUniformToFrame", $"Failed to get uniform to frame loads for area '{areaName}'");

            var loads = new List<AreaUniformToFrameLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                // Filter by load pattern if specified
                if (!string.IsNullOrEmpty(loadPattern) && loadPatterns[i] != loadPattern)
                    continue;

                var load = new AreaUniformToFrameLoad
                {
                    AreaName = areaName,
                    LoadPattern = loadPatterns[i],
                    Value = values[i],
                    Direction = directions[i],
                    DistributionType = distTypes[i],
                    CoordinateSystem = csys[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} uniform to frame loads for area {AreaName}", loads.Count, areaName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting uniform to frame loads for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes uniform loads transferred to frame objects.
    /// Wraps cSapModel.AreaObj.DeleteLoadUniformToFrame.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadUniformToFrame(string areaName, string loadPattern, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.AreaObj.DeleteLoadUniformToFrame(areaName, loadPattern, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadUniformToFrame", $"Failed to delete uniform to frame load for area '{areaName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted uniform to frame load for area {AreaName} in pattern {LoadPattern}", areaName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting uniform to frame load for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Wind Pressure Load Methods

    /// <summary>
    /// Sets wind pressure load on an area object.
    /// Wraps cSapModel.AreaObj.SetLoadWindPressure.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaWindPressureLoad model with load parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadWindPressure(string areaName, AreaWindPressureLoad load)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            int ret = _sapModel.AreaObj.SetLoadWindPressure(areaName, load.LoadPattern, load.WindPressureType, load.PressureCoefficient);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadWindPressure", $"Failed to set wind pressure load for area '{areaName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set wind pressure load for area {AreaName} in pattern {LoadPattern}: {Load}", 
                areaName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting wind pressure load for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets wind pressure loads assigned to an area object.
    /// Wraps cSapModel.AreaObj.GetLoadWindPressure.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaWindPressureLoad models</returns>
    public List<AreaWindPressureLoad> GetLoadWindPressure(string areaName, string loadPattern = "")
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberItems = 0;
            string[] areaNames = null;
            string[] loadPatterns = null;
            int[] loadTypes = null;
            double[] pressureCoefficients = null;

            int ret = _sapModel.AreaObj.GetLoadWindPressure(areaName, ref numberItems, ref areaNames, 
                ref loadPatterns, ref loadTypes, ref pressureCoefficients);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadWindPressure", $"Failed to get wind pressure loads for area '{areaName}'");

            var loads = new List<AreaWindPressureLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                // Filter by load pattern if specified
                if (!string.IsNullOrEmpty(loadPattern) && loadPatterns[i] != loadPattern)
                    continue;

                var load = new AreaWindPressureLoad
                {
                    AreaName = areaName,
                    LoadPattern = loadPatterns[i],
                    WindPressureType = loadTypes[i],
                    PressureCoefficient = pressureCoefficients[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} wind pressure loads for area {AreaName}", loads.Count, areaName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting wind pressure loads for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes wind pressure loads from an area object.
    /// Wraps cSapModel.AreaObj.DeleteLoadWindPressure.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadWindPressure(string areaName, string loadPattern, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.AreaObj.DeleteLoadWindPressure(areaName, loadPattern, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadWindPressure", $"Failed to delete wind pressure load for area '{areaName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted wind pressure load for area {AreaName} in pattern {LoadPattern}", areaName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting wind pressure load for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Temperature Load Methods

    /// <summary>
    /// Sets temperature load on an area object.
    /// Wraps cSapModel.AreaObj.SetLoadTemperature.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaTemperatureLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadTemperature(string areaName, AreaTemperatureLoad load, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            int ret = _sapModel.AreaObj.SetLoadTemperature(areaName, load.LoadPattern, load.TemperatureType, 
                load.Value, load.PatternName, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadTemperature", $"Failed to set temperature load for area '{areaName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set temperature load for area {AreaName} in pattern {LoadPattern}: {Load}", 
                areaName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting temperature load for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets temperature loads assigned to area objects.
    /// Wraps cSapModel.AreaObj.GetLoadTemperature.
    /// </summary>
    /// <param name="areaName">Name of the area object (empty for all areas)</param>
    /// <param name="itemType">Item type for retrieval</param>
    /// <returns>List of AreaTemperatureLoad models</returns>
    public List<AreaTemperatureLoad> GetLoadTemperature(string areaName = "", eItemType itemType = eItemType.Objects)
    {
        try
        {
            int numberItems = 0;
            string[] areaNames = null;
            string[] loadPatterns = null;
            int[] loadTypes = null;
            double[] values = null;
            string[] patternNames = null;

            int ret = _sapModel.AreaObj.GetLoadTemperature(areaName, ref numberItems, ref areaNames, 
                ref loadPatterns, ref loadTypes, ref values, ref patternNames, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadTemperature", $"Failed to get temperature loads for area '{areaName}'");

            var loads = new List<AreaTemperatureLoad>();

            for (int i = 0; i < numberItems; i++)
            {
                var load = new AreaTemperatureLoad
                {
                    AreaName = areaNames[i],
                    LoadPattern = loadPatterns[i],
                    TemperatureType = loadTypes[i],
                    Value = values[i],
                    PatternName = patternNames[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} temperature loads", loads.Count);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting temperature loads for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes temperature loads from an area object.
    /// Wraps cSapModel.AreaObj.DeleteLoadTemperature.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadTemperature(string areaName, string loadPattern, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.AreaObj.DeleteLoadTemperature(areaName, loadPattern, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadTemperature", $"Failed to delete temperature load for area '{areaName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted temperature load for area {AreaName} in pattern {LoadPattern}", areaName, loadPattern);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting temperature load for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Load Types

    /// <summary>
    /// Applies a uniform gravity load (downward) to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (positive for downward)</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetGravityLoad(string areaName, string loadPattern, double loadValue, bool replace = true)
    {
        var load = AreaUniformLoad.CreateGravityLoad(areaName, loadPattern, loadValue);
        return SetLoadUniform(areaName, load, replace);
    }

    /// <summary>
    /// Applies a uniform pressure load (normal to surface) to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="pressure">Pressure value (positive for outward normal)</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPressureLoad(string areaName, string loadPattern, double pressure, bool replace = true)
    {
        var load = AreaUniformLoad.CreatePressureLoad(areaName, loadPattern, pressure);
        return SetLoadUniform(areaName, load, replace);
    }

    /// <summary>
    /// Applies a uniform temperature change to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="temperatureChange">Temperature change value</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetUniformTemperatureLoad(string areaName, string loadPattern, double temperatureChange, bool replace = true)
    {
        var load = new AreaTemperatureLoad
        {
            AreaName = areaName,
            LoadPattern = loadPattern,
            TemperatureType = 1, // Uniform temperature
            Value = temperatureChange
        };

        return SetLoadTemperature(areaName, load, replace);
    }

    /// <summary>
    /// Gets all loads (uniform, wind, temperature) for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>AreaLoads model with all load types</returns>
    public AreaLoads GetAllLoads(string areaName, string loadPattern = "")
    {
        try
        {
            var areaLoads = new AreaLoads
            {
                UniformLoads = GetLoadUniform(areaName, loadPattern),
                UniformToFrameLoads = GetLoadUniformToFrame(areaName, loadPattern),
                WindPressureLoads = GetLoadWindPressure(areaName, loadPattern)
            };

            // Get temperature loads (filter by area and pattern if specified)
            var tempLoads = GetLoadTemperature(areaName);
            if (!string.IsNullOrEmpty(loadPattern))
            {
                tempLoads = tempLoads.Where(l => l.LoadPattern == loadPattern).ToList();
            }
            areaLoads.TemperatureLoads = tempLoads;

            return areaLoads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all loads for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion
}
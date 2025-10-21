using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// PointObjectManager partial class - Load Assignment Methods
/// </summary>
public partial class PointObjectManager
{
    #region Force Load Methods

    /// <summary>
    /// Assigns a concentrated force/moment to a point object.
    /// Wraps cSapModel.PointObj.SetLoadForce.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="load">PointLoad model with force/moment values and load pattern</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadForce(string pointName, PointLoad load, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (load == null)
                throw new ArgumentNullException(nameof(load));
            if (string.IsNullOrEmpty(load.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(load));

            double[] forceArray = load.ToArray();
            int ret = _sapModel.PointObj.SetLoadForce(pointName, load.LoadPattern, ref forceArray, replace, load.CoordinateSystem);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadForce", $"Failed to set force load for point '{pointName}' in pattern '{load.LoadPattern}'");

            _logger.LogDebug("Set force load for point {PointName} in pattern {LoadPattern}: {Load}", 
                pointName, load.LoadPattern, load.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting force load for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the force loads assigned to a point object for a specific load pattern.
    /// Wraps cSapModel.PointObj.GetLoadForce.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>PointLoad model with force/moment values, or null if no load found</returns>
    public PointLoad? GetLoadForce(string pointName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int numberItems = 0;
            string[] pointNames = null;
            string[] loadPatterns = null;
            int[] lcSteps = null;
            string[] csys = null;
            double[] f1 = null, f2 = null, f3 = null, m1 = null, m2 = null, m3 = null;

            int ret = _sapModel.PointObj.GetLoadForce(pointName, ref numberItems, ref pointNames, ref loadPatterns, 
                ref lcSteps, ref csys, ref f1, ref f2, ref f3, ref m1, ref m2, ref m3);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadForce", $"Failed to get force load for point '{pointName}'");

            // Find the load for the specified pattern
            for (int i = 0; i < numberItems; i++)
            {
                if (loadPatterns[i] == loadPattern)
                {
                    var load = new PointLoad
                    {
                        PointName = pointName,
                        LoadPattern = loadPattern,
                        LoadCaseStep = lcSteps[i],
                        Fx = f1[i],
                        Fy = f2[i],
                        Fz = f3[i],
                        Mx = m1[i],
                        My = m2[i],
                        Mz = m3[i],
                        CoordinateSystem = csys[i]
                    };

                    _logger.LogDebug("Retrieved force load for point {PointName} in pattern {LoadPattern}: {Load}", 
                        pointName, loadPattern, load.ToString());

                    return load;
                }
            }

            // No load found for the specified pattern
            return null;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting force load for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes all force loads from a point object for a specific load pattern.
    /// Wraps cSapModel.PointObj.DeleteLoadForce.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadForce(string pointName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.PointObj.DeleteLoadForce(pointName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadForce", $"Failed to delete force load for point '{pointName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted force load for point {PointName} in pattern {LoadPattern}", pointName, loadPattern);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting force load for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Displacement Load Methods

    /// <summary>
    /// Assigns ground displacement to a point object.
    /// Wraps cSapModel.PointObj.SetLoadDispl.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="displacement">PointDisplacement model with displacement values and load pattern</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLoadDisplacement(string pointName, PointDisplacement displacement, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (displacement == null)
                throw new ArgumentNullException(nameof(displacement));
            if (string.IsNullOrEmpty(displacement.LoadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(displacement));

            double[] displacementArray = displacement.ToArray();
            int ret = _sapModel.PointObj.SetLoadDispl(pointName, displacement.LoadPattern, ref displacementArray, 
                replace, displacement.CoordinateSystem);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadDispl", $"Failed to set displacement load for point '{pointName}' in pattern '{displacement.LoadPattern}'");

            _logger.LogDebug("Set displacement load for point {PointName} in pattern {LoadPattern}: {Displacement}", 
                pointName, displacement.LoadPattern, displacement.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting displacement load for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets ground displacement assignments for a point object.
    /// Wraps cSapModel.PointObj.GetLoadDispl.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>PointDisplacement model with displacement values, or null if no displacement found</returns>
    public PointDisplacement? GetLoadDisplacement(string pointName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int numberItems = 0;
            string[] pointNames = null;
            string[] loadPatterns = null;
            int[] lcSteps = null;
            string[] csys = null;
            double[] u1 = null, u2 = null, u3 = null, r1 = null, r2 = null, r3 = null;

            int ret = _sapModel.PointObj.GetLoadDispl(pointName, ref numberItems, ref pointNames, ref loadPatterns, 
                ref lcSteps, ref csys, ref u1, ref u2, ref u3, ref r1, ref r2, ref r3);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadDispl", $"Failed to get displacement load for point '{pointName}'");

            // Find the displacement for the specified pattern
            for (int i = 0; i < numberItems; i++)
            {
                if (loadPatterns[i] == loadPattern)
                {
                    var displacement = new PointDisplacement
                    {
                        PointName = pointName,
                        LoadPattern = loadPattern,
                        LoadCaseStep = lcSteps[i],
                        Ux = u1[i],
                        Uy = u2[i],
                        Uz = u3[i],
                        Rx = r1[i],
                        Ry = r2[i],
                        Rz = r3[i],
                        CoordinateSystem = csys[i]
                    };

                    _logger.LogDebug("Retrieved displacement load for point {PointName} in pattern {LoadPattern}: {Displacement}", 
                        pointName, loadPattern, displacement.ToString());

                    return displacement;
                }
            }

            // No displacement found for the specified pattern
            return null;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting displacement load for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes ground displacement assignments from a point object.
    /// Wraps cSapModel.PointObj.DeleteLoadDispl.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadDisplacement(string pointName, string loadPattern)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(loadPattern))
                throw new ArgumentException("Load pattern cannot be null or empty", nameof(loadPattern));

            int ret = _sapModel.PointObj.DeleteLoadDispl(pointName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteLoadDispl", $"Failed to delete displacement load for point '{pointName}' in pattern '{loadPattern}'");

            _logger.LogDebug("Deleted displacement load for point {PointName} in pattern {LoadPattern}", pointName, loadPattern);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting displacement load for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Load Types

    /// <summary>
    /// Applies a vertical downward force at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="force">Downward force magnitude (positive value)</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetVerticalLoad(string pointName, string loadPattern, double force, bool replace = true)
    {
        var load = new PointLoad
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Fz = -Math.Abs(force), // Negative for downward
            CoordinateSystem = "Global"
        };

        return SetLoadForce(pointName, load, replace);
    }

    /// <summary>
    /// Applies horizontal forces at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="fx">Force in X direction</param>
    /// <param name="fy">Force in Y direction</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetHorizontalLoad(string pointName, string loadPattern, double fx, double fy, bool replace = true)
    {
        var load = new PointLoad
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Fx = fx,
            Fy = fy,
            CoordinateSystem = "Global"
        };

        return SetLoadForce(pointName, load, replace);
    }

    /// <summary>
    /// Applies a moment about the Z-axis at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="moment">Moment magnitude</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetTorsionalMoment(string pointName, string loadPattern, double moment, bool replace = true)
    {
        var load = new PointLoad
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Mz = moment,
            CoordinateSystem = "Global"
        };

        return SetLoadForce(pointName, load, replace);
    }

    /// <summary>
    /// Applies support settlement (vertical displacement) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="settlement">Settlement magnitude (positive for downward)</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSupportSettlement(string pointName, string loadPattern, double settlement, bool replace = true)
    {
        var displacement = new PointDisplacement
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Uz = -Math.Abs(settlement), // Negative for downward settlement
            CoordinateSystem = "Global"
        };

        return SetLoadDisplacement(pointName, displacement, replace);
    }

    /// <summary>
    /// Gets all force loads for a point across all load patterns.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>List of PointLoad models for all patterns</returns>
    public List<PointLoad> GetAllForceLoads(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            var loads = new List<PointLoad>();

            int numberItems = 0;
            string[] pointNames = null;
            string[] loadPatterns = null;
            int[] lcSteps = null;
            string[] csys = null;
            double[] f1 = null, f2 = null, f3 = null, m1 = null, m2 = null, m3 = null;

            int ret = _sapModel.PointObj.GetLoadForce(pointName, ref numberItems, ref pointNames, ref loadPatterns, 
                ref lcSteps, ref csys, ref f1, ref f2, ref f3, ref m1, ref m2, ref m3);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadForce", $"Failed to get all force loads for point '{pointName}'");

            for (int i = 0; i < numberItems; i++)
            {
                var load = new PointLoad
                {
                    PointName = pointName,
                    LoadPattern = loadPatterns[i],
                    LoadCaseStep = lcSteps[i],
                    Fx = f1[i],
                    Fy = f2[i],
                    Fz = f3[i],
                    Mx = m1[i],
                    My = m2[i],
                    Mz = m3[i],
                    CoordinateSystem = csys[i]
                };
                loads.Add(load);
            }

            _logger.LogDebug("Retrieved {Count} force loads for point {PointName}", loads.Count, pointName);
            return loads;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all force loads for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all displacement loads for a point across all load patterns.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>List of PointDisplacement models for all patterns</returns>
    public List<PointDisplacement> GetAllDisplacementLoads(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            var displacements = new List<PointDisplacement>();

            int numberItems = 0;
            string[] pointNames = null;
            string[] loadPatterns = null;
            int[] lcSteps = null;
            string[] csys = null;
            double[] u1 = null, u2 = null, u3 = null, r1 = null, r2 = null, r3 = null;

            int ret = _sapModel.PointObj.GetLoadDispl(pointName, ref numberItems, ref pointNames, ref loadPatterns, 
                ref lcSteps, ref csys, ref u1, ref u2, ref u3, ref r1, ref r2, ref r3);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadDispl", $"Failed to get all displacement loads for point '{pointName}'");

            for (int i = 0; i < numberItems; i++)
            {
                var displacement = new PointDisplacement
                {
                    PointName = pointName,
                    LoadPattern = loadPatterns[i],
                    LoadCaseStep = lcSteps[i],
                    Ux = u1[i],
                    Uy = u2[i],
                    Uz = u3[i],
                    Rx = r1[i],
                    Ry = r2[i],
                    Rz = r3[i],
                    CoordinateSystem = csys[i]
                };
                displacements.Add(displacement);
            }

            _logger.LogDebug("Retrieved {Count} displacement loads for point {PointName}", displacements.Count, pointName);
            return displacements;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all displacement loads for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion
}
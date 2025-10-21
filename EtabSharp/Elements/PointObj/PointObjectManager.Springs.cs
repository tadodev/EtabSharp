using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// PointObjectManager partial class - Spring Support Methods
/// </summary>
public partial class PointObjectManager
{
    #region Spring Methods

    /// <summary>
    /// Assigns spring stiffness to a point object.
    /// Wraps cSapModel.PointObj.SetSpring or SetSpringAssignment.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="spring">PointSpring model with spring stiffness values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpring(string pointName, PointSpring spring)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (spring == null)
                throw new ArgumentNullException(nameof(spring));

            int ret;

            // If using a named spring property
            if (!string.IsNullOrEmpty(spring.PropertyName))
            {
                ret = _sapModel.PointObj.SetSpringAssignment(pointName, spring.PropertyName);
                if (ret != 0)
                    throw new EtabsException(ret, "SetSpringAssignment", $"Failed to set spring property '{spring.PropertyName}' for point '{pointName}'");

                _logger.LogDebug("Set spring property {PropertyName} for point {PointName}", spring.PropertyName, pointName);
            }
            else
            {
                // Use direct spring values
                if (spring.IsCoupled && spring.CoupledMatrix != null)
                {
                    // Use coupled spring matrix
                    double[] coupledMatrix = spring.CoupledMatrix;
                    ret = _sapModel.PointObj.SetSpringCoupled(pointName, ref coupledMatrix, eItemType.Objects, spring.IsLocalCSys);
                }
                else
                {
                    // Use uncoupled spring values
                    double[] springArray = spring.ToArray();
                    ret = _sapModel.PointObj.SetSpring(pointName, ref springArray, eItemType.Objects, spring.IsLocalCSys);
                }

                if (ret != 0)
                    throw new EtabsException(ret, "SetSpring", $"Failed to set spring values for point '{pointName}'");

                _logger.LogDebug("Set spring values for point {PointName}: {Spring}", pointName, spring.ToString());
            }

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting spring for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the spring stiffness values assigned to a point object.
    /// Wraps cSapModel.PointObj.GetSpring, GetSpringCoupled, and GetSpringAssignment.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointSpring model with spring stiffness values, or null if no springs</returns>
    public PointSpring? GetSpring(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            // First check if there's a named spring property assignment
            string springProperty = "";
            int ret = _sapModel.PointObj.GetSpringAssignment(pointName, ref springProperty);
            
            if (ret == 0 && !string.IsNullOrEmpty(springProperty))
            {
                return new PointSpring
                {
                    PropertyName = springProperty
                };
            }

            // Check if it's a coupled spring
            bool isCoupled = false;
            ret = _sapModel.PointObj.IsSpringCoupled(pointName, ref isCoupled);
            
            if (ret != 0)
            {
                // No spring assignment found
                return null;
            }

            var spring = new PointSpring
            {
                IsCoupled = isCoupled
            };

            if (isCoupled)
            {
                // Get coupled spring matrix
                double[] coupledMatrix = new double[21]; // 6x6 symmetric matrix upper triangle
                ret = _sapModel.PointObj.GetSpringCoupled(pointName, ref coupledMatrix);
                
                if (ret == 0)
                {
                    spring.CoupledMatrix = coupledMatrix;
                }
            }
            else
            {
                // Get uncoupled spring values
                double[] springArray = new double[6];
                ret = _sapModel.PointObj.GetSpring(pointName, ref springArray);
                
                if (ret == 0)
                {
                    spring.Kx = springArray[0];
                    spring.Ky = springArray[1];
                    spring.Kz = springArray[2];
                    spring.Krx = springArray[3];
                    spring.Kry = springArray[4];
                    spring.Krz = springArray[5];
                }
            }

            // Check if all spring values are zero (no effective spring)
            if (!isCoupled && spring.ToArray().All(k => Math.Abs(k) < 1e-10))
                return null;

            _logger.LogDebug("Retrieved spring for point {PointName}: {Spring}", pointName, spring.ToString());
            return spring;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting spring for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Removes all spring assignments from a point object.
    /// Wraps cSapModel.PointObj.DeleteSpring.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteSpring(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.DeleteSpring(pointName);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteSpring", $"Failed to delete spring for point '{pointName}'");

            _logger.LogDebug("Deleted spring for point {PointName}", pointName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting spring for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Spring Types

    /// <summary>
    /// Sets a spring support with equal stiffness in all translational directions.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="translationalStiffness">Stiffness in X, Y, Z directions</param>
    /// <param name="rotationalStiffness">Stiffness in RX, RY, RZ directions (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetUniformSpringSupport(string pointName, double translationalStiffness, double rotationalStiffness = 0)
    {
        var spring = new PointSpring
        {
            Kx = translationalStiffness,
            Ky = translationalStiffness,
            Kz = translationalStiffness,
            Krx = rotationalStiffness,
            Kry = rotationalStiffness,
            Krz = rotationalStiffness
        };

        return SetSpring(pointName, spring);
    }

    /// <summary>
    /// Sets a soil spring support with different stiffness in horizontal and vertical directions.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="horizontalStiffness">Stiffness in X and Y directions</param>
    /// <param name="verticalStiffness">Stiffness in Z direction</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSoilSpringSupport(string pointName, double horizontalStiffness, double verticalStiffness)
    {
        var spring = new PointSpring
        {
            Kx = horizontalStiffness,
            Ky = horizontalStiffness,
            Kz = verticalStiffness,
            Krx = 0,
            Kry = 0,
            Krz = 0
        };

        return SetSpring(pointName, spring);
    }

    #endregion
}
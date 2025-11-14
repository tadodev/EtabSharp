using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// FrameObjectManager partial class - End Release Methods
/// </summary>
public partial class FrameObjectManager
{
    #region End Release Methods

    /// <summary>
    /// Sets end releases (hinges) for a frame object.
    /// Wraps cSapModel.FrameObj.SetReleases.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="releases">FrameReleases model with release conditions</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetReleases(string frameName, FrameReleases releases, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (releases == null)
                throw new ArgumentNullException(nameof(releases));

            // Validate releases before sending to API
            if (!releases.IsValid())
                throw new ArgumentException("Invalid release combination - would cause instability", nameof(releases));

            bool[] ii = releases.IEndReleases.ToArray();
            bool[] jj = releases.JEndReleases.ToArray();
            double[] startValue = releases.IEndPartialFixity.ToArray();
            double[] endValue = releases.JEndPartialFixity.ToArray();

            int ret = _sapModel.FrameObj.SetReleases(frameName, ref ii, ref jj, ref startValue, ref endValue, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetReleases", $"Failed to set releases for frame '{frameName}'");

            _logger.LogDebug("Set releases for frame {FrameName}: I-End={IEnd}, J-End={JEnd}",
                frameName, releases.IEndReleases.ToString(), releases.JEndReleases.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting releases for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the end releases assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetReleases.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>FrameReleases model with release conditions, or null if no releases</returns>
    public FrameReleases? GetReleases(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            bool[] ii = new bool[6];
            bool[] jj = new bool[6];
            double[] startValue = new double[6];
            double[] endValue = new double[6];

            int ret = _sapModel.FrameObj.GetReleases(frameName, ref ii, ref jj, ref startValue, ref endValue);

            if (ret != 0)
                throw new EtabsException(ret, "GetReleases", $"Failed to get releases for frame '{frameName}'");

            // Check if any releases are applied
            if (ii.All(r => !r) && jj.All(r => !r))
                return null; // No releases applied

            var releases = new FrameReleases
            {
                IEndReleases = FrameEndReleases.FromArray(ii),
                JEndReleases = FrameEndReleases.FromArray(jj),
                IEndPartialFixity = FrameEndPartialFixity.FromArray(startValue),
                JEndPartialFixity = FrameEndPartialFixity.FromArray(endValue)
            };

            _logger.LogDebug("Retrieved releases for frame {FrameName}: I-End={IEnd}, J-End={JEnd}",
                frameName, releases.IEndReleases.ToString(), releases.JEndReleases.ToString());

            return releases;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting releases for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Removes all end releases from a frame object (makes it fully fixed).
    /// Wraps cSapModel.FrameObj.DeleteReleases.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteReleases(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            var releases = new FrameReleases
            {
                IEndReleases = FrameEndReleases.Fixed(),
                JEndReleases = FrameEndReleases.Fixed()
            };

            var ret = SetReleases(frameName, releases, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteReleases", $"Failed to delete releases for frame '{frameName}'");

            _logger.LogDebug("Deleted releases for frame {FrameName}", frameName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting releases for frame '{frameName}': {ex.Message}", ex);
        }
    }
    #endregion

    #region Convenience Methods for Common Release Types
    /// <summary>
    /// Sets a pinned connection at the I-end (start) of a frame (releases all moments and torsion).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetIEndPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Pinned(),
            JEndReleases = FrameEndReleases.Fixed()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a pinned connection at the J-end of a frame (releases all moments and torsion).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetJEndPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Fixed(),
            JEndReleases = FrameEndReleases.Pinned()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets pinned connections at both ends of a frame (releases all moments and torsion).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetBothEndsPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Pinned(),
            JEndReleases = FrameEndReleases.Pinned()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a roller connection at the I-end (releases axial force and all moments).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetIEndRoller(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Roller(),
            JEndReleases = FrameEndReleases.Fixed()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a roller connection at the J-end (releases axial force and all moments).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetJEndRoller(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Fixed(),
            JEndReleases = FrameEndReleases.Roller()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a torsion release at the I-end (releases R1 only).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetIEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = new FrameEndReleases { R1 = true },
            JEndReleases = FrameEndReleases.Fixed()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a torsion release at the J-end (releases R1 only).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetJEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Fixed(),
            JEndReleases = new FrameEndReleases { R1 = true }
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets moment releases only at both ends (releases R2 and R3).
    /// Common for simple beam connections.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetBothEndsMomentReleased(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = new FrameEndReleases { R2 = true, R3 = true },
            JEndReleases = new FrameEndReleases { R2 = true, R3 = true }
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets partial moment releases with specified spring stiffness values.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="iEndR2Spring">I-end R2 moment spring stiffness [FL/rad] (0 = fully released)</param>
    /// <param name="iEndR3Spring">I-end R3 moment spring stiffness [FL/rad] (0 = fully released)</param>
    /// <param name="jEndR2Spring">J-end R2 moment spring stiffness [FL/rad] (0 = fully released)</param>
    /// <param name="jEndR3Spring">J-end R3 moment spring stiffness [FL/rad] (0 = fully released)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPartialMomentReleases(string frameName,
        double iEndR2Spring, double iEndR3Spring,
        double jEndR2Spring, double jEndR3Spring,
        eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = new FrameEndReleases
            {
                R2 = true, // Must be released to apply partial fixity
                R3 = true
            },
            JEndReleases = new FrameEndReleases
            {
                R2 = true,
                R3 = true
            },
            IEndPartialFixity = new FrameEndPartialFixity
            {
                R2 = iEndR2Spring,
                R3 = iEndR3Spring
            },
            JEndPartialFixity = new FrameEndPartialFixity
            {
                R2 = jEndR2Spring,
                R3 = jEndR3Spring
            }
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a semi-rigid connection with specified rotational stiffness.
    /// Releases R2 and R3 with partial fixity springs.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="endLocation">Which end to apply to (IEnd or JEnd)</param>
    /// <param name="rotationalStiffness">Rotational spring stiffness [FL/rad]</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSemiRigidConnection(string frameName, FrameEnd endLocation,
        double rotationalStiffness, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases();

        if (endLocation == FrameEnd.IEnd)
        {
            releases.IEndReleases = new FrameEndReleases { R2 = true, R3 = true };
            releases.IEndPartialFixity = new FrameEndPartialFixity
            {
                R2 = rotationalStiffness,
                R3 = rotationalStiffness
            };
            releases.JEndReleases = FrameEndReleases.Fixed();
        }
        else
        {
            releases.IEndReleases = FrameEndReleases.Fixed();
            releases.JEndReleases = new FrameEndReleases { R2 = true, R3 = true };
            releases.JEndPartialFixity = new FrameEndPartialFixity
            {
                R2 = rotationalStiffness,
                R3 = rotationalStiffness
            };
        }

        return SetReleases(frameName, releases, itemType);
    }

    #endregion
}

/// <summary>
/// Enumeration for frame end location.
/// </summary>
public enum FrameEnd
{
    /// <summary>I-End (start of frame)</summary>
    IEnd,
    /// <summary>J-End (end of frame)</summary>
    JEnd
}
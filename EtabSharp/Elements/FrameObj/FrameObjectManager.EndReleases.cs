using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;

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

            int ret = _sapModel.FrameObj.DeleteReleases(frameName, itemType);

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
    /// Sets a pinned connection at the I-end (start) of a frame (releases all moments).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetIEndPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.MomentReleased(),
            JEndReleases = FrameEndReleases.Fixed()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a pinned connection at the J-end of a frame (releases all moments).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetJEndPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Fixed(),
            JEndReleases = FrameEndReleases.MomentReleased()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets pinned connections at both ends of a frame (releases all moments).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetBothEndsPinned(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.MomentReleased(),
            JEndReleases = FrameEndReleases.MomentReleased()
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
            IEndReleases = new FrameEndReleases
            {
                P = true,  // Axial force released
                M22 = true, // Major axis moment released
                M33 = true, // Minor axis moment released
                T = true   // Torsion released
            },
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
            JEndReleases = new FrameEndReleases
            {
                P = true,  // Axial force released
                M22 = true, // Major axis moment released
                M33 = true, // Minor axis moment released
                T = true   // Torsion released
            }
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a torsion release at the I-end (releases torsional moment only).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetIEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = new FrameEndReleases { T = true },
            JEndReleases = FrameEndReleases.Fixed()
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets a torsion release at the J-end (releases torsional moment only).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetJEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = FrameEndReleases.Fixed(),
            JEndReleases = new FrameEndReleases { T = true }
        };

        return SetReleases(frameName, releases, itemType);
    }

    /// <summary>
    /// Sets partial moment releases with specified fixity values.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="iEndM22Fixity">I-end major axis moment fixity (0.0 = released, 1.0 = fixed)</param>
    /// <param name="iEndM33Fixity">I-end minor axis moment fixity (0.0 = released, 1.0 = fixed)</param>
    /// <param name="jEndM22Fixity">J-end major axis moment fixity (0.0 = released, 1.0 = fixed)</param>
    /// <param name="jEndM33Fixity">J-end minor axis moment fixity (0.0 = released, 1.0 = fixed)</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPartialMomentReleases(string frameName, double iEndM22Fixity, double iEndM33Fixity, 
        double jEndM22Fixity, double jEndM33Fixity, eItemType itemType = eItemType.Objects)
    {
        var releases = new FrameReleases
        {
            IEndReleases = new FrameEndReleases
            {
                M22 = iEndM22Fixity < 1.0,
                M33 = iEndM33Fixity < 1.0
            },
            JEndReleases = new FrameEndReleases
            {
                M22 = jEndM22Fixity < 1.0,
                M33 = jEndM33Fixity < 1.0
            },
            IEndPartialFixity = new FrameEndPartialFixity
            {
                M22 = iEndM22Fixity,
                M33 = iEndM33Fixity
            },
            JEndPartialFixity = new FrameEndPartialFixity
            {
                M22 = jEndM22Fixity,
                M33 = jEndM33Fixity
            }
        };

        return SetReleases(frameName, releases, itemType);
    }

    #endregion
}
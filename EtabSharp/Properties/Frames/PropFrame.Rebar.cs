using EtabSharp.Exceptions;
using EtabSharp.Properties.Frames.Models;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Frames;

public partial class PropFrame
{
    #region Reinforcement

    ///<inheritdoc/>
    public int SetColumnRebarRectangular(string sectionName, PropColumnRebarRect rebarData)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        if (rebarData == null)
            throw new ArgumentNullException(nameof(rebarData));

        try
        {
            _logger.LogInformation("Setting rectangular column rebar for section '{SectionName}'", sectionName);

            int ret = _sapModel.PropFrame.SetRebarColumn(
                sectionName,
                rebarData.MatPropLong,
                rebarData.MatPropConfine,
                rebarData.Pattern, // Pattern = 1 (Rectangular)
                rebarData.ConfineType, // ConfineType = 1 (Ties)
                rebarData.Cover,
                0, // NumCircBars (not used for rectangular)
                rebarData.NumberOfBars3Dir,
                rebarData.NumberOfBars2Dir,
                rebarData.BarSize,
                rebarData.TieSize,
                rebarData.TieSpacing,
                rebarData.TieLegs2Dir,
                rebarData.TieLegs3Dir,
                rebarData.ToBeDesigned
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to set column rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "SetRebarColumn", $"Failed to set column rebar for '{sectionName}'.");
            }

            _logger.LogInformation("Successfully set column rebar for '{SectionName}'", sectionName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting column rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error setting column rebar for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetColumnRebarCircular(string sectionName, PropColumnRebarCirc rebarData)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        if (rebarData == null)
            throw new ArgumentNullException(nameof(rebarData));

        try
        {
            _logger.LogInformation("Setting circular column rebar for section '{SectionName}'", sectionName);

            int ret = _sapModel.PropFrame.SetRebarColumn(
                sectionName,
                rebarData.MatPropLong,
                rebarData.MatPropConfine,
                rebarData.Pattern, // Pattern = 2 (Circular)
                rebarData.ConfineType,
                rebarData.Cover,
                rebarData.NumberOfBars,
                0, // NumBars3Dir (not used for circular)
                0, // NumBars2Dir (not used for circular)
                rebarData.BarSize,
                rebarData.TieSize,
                rebarData.TieSpacing,
                0, // TieLegs2Dir (not used for circular)
                0, // TieLegs3Dir (not used for circular)
                rebarData.ToBeDesigned
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to set circular column rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "SetRebarColumn", $"Failed to set circular column rebar for '{sectionName}'.");
            }

            _logger.LogInformation("Successfully set circular column rebar for '{SectionName}'", sectionName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting circular column rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error setting circular column rebar for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetBeamRebar(string sectionName, PropBeamRebar rebarData)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        if (rebarData == null)
            throw new ArgumentNullException(nameof(rebarData));

        try
        {
            _logger.LogInformation("Setting beam rebar for section '{SectionName}'", sectionName);

            int ret = _sapModel.PropFrame.SetRebarBeam(
                sectionName,
                rebarData.MatPropLong,
                rebarData.MatPropConfine,
                rebarData.CoverTop,
                rebarData.CoverBottom,
                rebarData.TopLeftArea,
                rebarData.TopRightArea,
                rebarData.BottomLeftArea,
                rebarData.BottomRightArea
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to set beam rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "SetRebarBeam", $"Failed to set beam rebar for '{sectionName}'.");
            }

            _logger.LogInformation("Successfully set beam rebar for '{SectionName}'", sectionName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting beam rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error setting beam rebar for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropColumnRebarRect GetColumnRebarRectangular(string sectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        try
        {
            string matRebar = "";
            string matConf = "";
            int pattern = 0;
            int confineType = 0;
            double cover = 0;
            int numCircBars = 0;
            int numBars3 = 0;
            int numBars2 = 0;
            string barSize = "";
            string tieSize = "";
            double tieSpacing = 0;
            int numLegs2 = 0;
            int numLegs3 = 0;
            bool toBeDesigned = false;

            int ret = _sapModel.PropFrame.GetRebarColumn(
                sectionName,
                ref matRebar,
                ref matConf,
                ref pattern,
                ref confineType,
                ref cover,
                ref numCircBars,
                ref numBars3,
                ref numBars2,
                ref barSize,
                ref tieSize,
                ref tieSpacing,
                ref numLegs2,
                ref numLegs3,
                ref toBeDesigned
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to get column rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "GetRebarColumn", $"Failed to get column rebar for '{sectionName}'.");
            }

            return new PropColumnRebarRect
            {
                MatPropLong = matRebar,
                MatPropConfine = matConf,
                Cover = cover,
                NumberOfBars3Dir = numBars3,
                NumberOfBars2Dir = numBars2,
                BarSize = barSize,
                TieSize = tieSize,
                TieSpacing = tieSpacing,
                TieLegs2Dir = numLegs2,
                TieLegs3Dir = numLegs3,
                ToBeDesigned = toBeDesigned
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting column rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error getting column rebar for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropColumnRebarCirc GetColumnRebarCircular(string sectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        try
        {
            string matRebar = "";
            string matConf = "";
            int pattern = 0;
            int confineType = 0;
            double cover = 0;
            int numCircBars = 0;
            int numBars3 = 0;
            int numBars2 = 0;
            string barSize = "";
            string spiralSize = "";
            double spiralSpacing = 0;
            int numLegs2 = 0;
            int numLegs3 = 0;
            bool toBeDesigned = false;

            int ret = _sapModel.PropFrame.GetRebarColumn(
                sectionName,
                ref matRebar,
                ref matConf,
                ref pattern,
                ref confineType,
                ref cover,
                ref numCircBars,
                ref numBars3,
                ref numBars2,
                ref barSize,
                ref spiralSize,
                ref spiralSpacing,
                ref numLegs2,
                ref numLegs3,
                ref toBeDesigned
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to get circular column rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "GetRebarColumn", $"Failed to get circular column rebar for '{sectionName}'.");
            }

            return new PropColumnRebarCirc
            {
                MatPropLong = matRebar,
                MatPropConfine = matConf,
                Cover = cover,
                NumberOfBars = numCircBars,
                BarSize = barSize,
                TieSize = spiralSize,
                TieSpacing = spiralSpacing,
                ConfineType = confineType,
                ToBeDesigned = toBeDesigned
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting circular column rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error getting circular column rebar for '{sectionName}'", ex);
        }
    }

    ///<inheritdoc/>
    public PropBeamRebar GetBeamRebar(string sectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name cannot be empty.", nameof(sectionName));

        try
        {
            string matRebar = "";
            string matConf = "";
            double coverTop = 0;
            double coverBot = 0;
            double topLeftArea = 0;
            double topRightArea = 0;
            double botLeftArea = 0;
            double botRightArea = 0;

            int ret = _sapModel.PropFrame.GetRebarBeam(
                sectionName,
                ref matRebar,
                ref matConf,
                ref coverTop,
                ref coverBot,
                ref topLeftArea,
                ref topRightArea,
                ref botLeftArea,
                ref botRightArea
            );

            if (ret != 0)
            {
                _logger.LogError("Failed to get beam rebar for '{SectionName}'. Return code: {ReturnCode}", sectionName, ret);
                throw new EtabsException(ret, "GetRebarBeam", $"Failed to get beam rebar for '{sectionName}'.");
            }

            return new PropBeamRebar
            {
                MatPropLong = matRebar,
                MatPropConfine = matConf,
                CoverTop = coverTop,
                CoverBottom = coverBot,
                TopLeftArea = topLeftArea,
                TopRightArea = topRightArea,
                BottomLeftArea = botLeftArea,
                BottomRightArea = botRightArea
            };
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting beam rebar for '{SectionName}'", sectionName);
            throw new EtabsException($"Unexpected error getting beam rebar for '{sectionName}'", ex);
        }
    }

    #endregion

}
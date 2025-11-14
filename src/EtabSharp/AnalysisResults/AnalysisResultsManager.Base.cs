using EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Bucklings;
using EtabSharp.Exceptions;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for base reactions and buckling results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Base Reactions

    /// <summary>
    /// Gets base reactions.
    /// Wraps cSapModel.Results.BaseReact.
    /// </summary>
    public BaseReactionResults GetBaseReact()
    {
        var results = new BaseReactionResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] fx = Array.Empty<double>();
            double[] fy = Array.Empty<double>();
            double[] fz = Array.Empty<double>();
            double[] mx = Array.Empty<double>();
            double[] my = Array.Empty<double>();
            double[] mz = Array.Empty<double>();
            double gx = 0, gy = 0, gz = 0;  // Single values, not arrays!

            int ret = _sapModel.Results.BaseReact(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref fx, ref fy, ref fz,
                ref mx, ref my, ref mz,
                ref gx, ref gy, ref gz);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;
            results.GlobalX = gx;
            results.GlobalY = gy;
            results.GlobalZ = gz;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get base reaction results. Return code: {ret}";
                throw new EtabsException(ret, "GetBaseReact", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new BaseReactionResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    FX = fx[i],
                    FY = fy[i],
                    FZ = fz[i],
                    MX = mx[i],
                    MY = my[i],
                    MZ = mz[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} base reaction results at location ({GX}, {GY}, {GZ})",
                numberResults, gx, gy, gz);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting base reactions: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets base reactions with centroid information.
    /// Wraps cSapModel.Results.BaseReactWithCentroid.
    /// </summary>
    public BaseReactionWithCentroidResults GetBaseReactWithCentroid()
    {
        var results = new BaseReactionWithCentroidResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] fx = Array.Empty<double>();
            double[] fy = Array.Empty<double>();
            double[] fz = Array.Empty<double>();
            double[] mx = Array.Empty<double>();
            double[] my = Array.Empty<double>();
            double[] mz = Array.Empty<double>();
            double gx = 0, gy = 0, gz = 0;
            double[] xCentroidForFX = Array.Empty<double>();
            double[] yCentroidForFX = Array.Empty<double>();
            double[] zCentroidForFX = Array.Empty<double>();
            double[] xCentroidForFY = Array.Empty<double>();
            double[] yCentroidForFY = Array.Empty<double>();
            double[] zCentroidForFY = Array.Empty<double>();
            double[] xCentroidForFZ = Array.Empty<double>();
            double[] yCentroidForFZ = Array.Empty<double>();
            double[] zCentroidForFZ = Array.Empty<double>();

            int ret = _sapModel.Results.BaseReactWithCentroid(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref fx, ref fy, ref fz,
                ref mx, ref my, ref mz,
                ref gx, ref gy, ref gz,
                ref xCentroidForFX, ref yCentroidForFX, ref zCentroidForFX,
                ref xCentroidForFY, ref yCentroidForFY, ref zCentroidForFY,
                ref xCentroidForFZ, ref yCentroidForFZ, ref zCentroidForFZ);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;
            results.GlobalX = gx;
            results.GlobalY = gy;
            results.GlobalZ = gz;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get base reaction with centroid results. Return code: {ret}";
                throw new EtabsException(ret, "GetBaseReactWithCentroid", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new BaseReactionWithCentroidResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    FX = fx[i],
                    FY = fy[i],
                    FZ = fz[i],
                    MX = mx[i],
                    MY = my[i],
                    MZ = mz[i],
                    XCentroidForFX = xCentroidForFX[i],
                    YCentroidForFX = yCentroidForFX[i],
                    ZCentroidForFX = zCentroidForFX[i],
                    XCentroidForFY = xCentroidForFY[i],
                    YCentroidForFY = yCentroidForFY[i],
                    ZCentroidForFY = zCentroidForFY[i],
                    XCentroidForFZ = xCentroidForFZ[i],
                    YCentroidForFZ = yCentroidForFZ[i],
                    ZCentroidForFZ = zCentroidForFZ[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} base reaction with centroid results at location ({GX}, {GY}, {GZ})",
                numberResults, gx, gy, gz);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting base reactions with centroid: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Buckling

    /// <summary>
    /// Gets buckling factors.
    /// Wraps cSapModel.Results.BucklingFactor.
    /// </summary>
    public BucklingFactorResults GetBucklingFactor()
    {
        var results = new BucklingFactorResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] factor = Array.Empty<double>();

            int ret = _sapModel.Results.BucklingFactor(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref factor);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get buckling factor results. Return code: {ret}";
                throw new EtabsException(ret, "GetBucklingFactor", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new BucklingFactorResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    Factor = factor[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} buckling factor results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting buckling factors: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
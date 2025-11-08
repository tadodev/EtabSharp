using EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for frame element analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Frame Force

    /// <summary>
    /// Gets frame element forces.
    /// Wraps cSapModel.Results.FrameForce.
    /// </summary>
    public FrameForceResults GetFrameForce(string name, eItemTypeElm itemTypeElm)
    {
        var results = new FrameForceResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] obj = Array.Empty<string>();
            double[] objSta = Array.Empty<double>();
            string[] elm = Array.Empty<string>();
            double[] elmSta = Array.Empty<double>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] p = Array.Empty<double>();
            double[] v2 = Array.Empty<double>();
            double[] v3 = Array.Empty<double>();
            double[] t = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.FrameForce(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref objSta,
                ref elm, ref elmSta,
                ref loadCase, ref stepType, ref stepNum,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get frame force results. Return code: {ret}";
                throw new EtabsException(ret, "GetFrameForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new FrameForceResult
                {
                    ObjectName = obj[i],
                    ObjectStation = objSta[i],
                    ElementName = elm[i],
                    ElementStation = elmSta[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    P = p[i],
                    V2 = v2[i],
                    V3 = v3[i],
                    T = t[i],
                    M2 = m2[i],
                    M3 = m3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} frame force results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting frame force: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Frame Joint Force

    /// <summary>
    /// Gets frame joint forces.
    /// Wraps cSapModel.Results.FrameJointForce.
    /// </summary>
    public FrameJointForceResults GetFrameJointForce(string name, eItemTypeElm itemTypeElm)
    {
        var results = new FrameJointForceResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] obj = Array.Empty<string>();
            string[] elm = Array.Empty<string>();
            string[] pointElm = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] f1 = Array.Empty<double>();
            double[] f2 = Array.Empty<double>();
            double[] f3 = Array.Empty<double>();
            double[] m1 = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.FrameJointForce(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref f1, ref f2, ref f3,
                ref m1, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get frame joint force results. Return code: {ret}";
                throw new EtabsException(ret, "GetFrameJointForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new FrameJointForceResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    F1 = f1[i],
                    F2 = f2[i],
                    F3 = f3[i],
                    M1 = m1[i],
                    M2 = m2[i],
                    M3 = m3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} frame joint force results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting frame joint force: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
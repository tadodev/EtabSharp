using EtabSharp.AnalysisResults.Models.AnalysisResults.Links;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for link element analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Link Deformation

    /// <summary>
    /// Gets link element deformations.
    /// Wraps cSapModel.Results.LinkDeformation.
    /// </summary>
    public LinkDeformationResults GetLinkDeformation(string name, eItemTypeElm itemTypeElm)
    {
        var results = new LinkDeformationResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] obj = Array.Empty<string>();
            string[] elm = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] u1 = Array.Empty<double>();
            double[] u2 = Array.Empty<double>();
            double[] u3 = Array.Empty<double>();
            double[] r1 = Array.Empty<double>();
            double[] r2 = Array.Empty<double>();
            double[] r3 = Array.Empty<double>();

            int ret = _sapModel.Results.LinkDeformation(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm,
                ref loadCase, ref stepType, ref stepNum,
                ref u1, ref u2, ref u3,
                ref r1, ref r2, ref r3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get link deformation results. Return code: {ret}";
                throw new EtabsException(ret, "GetLinkDeformation", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new LinkDeformationResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    U1 = u1[i],
                    U2 = u2[i],
                    U3 = u3[i],
                    R1 = r1[i],
                    R2 = r2[i],
                    R3 = r3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} link deformation results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting link deformation: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Link Force

    /// <summary>
    /// Gets link element forces.
    /// Wraps cSapModel.Results.LinkForce.
    /// </summary>
    public LinkForceResults GetLinkForce(string name, eItemTypeElm itemTypeElm)
    {
        var results = new LinkForceResults();

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
            double[] p = Array.Empty<double>();
            double[] v2 = Array.Empty<double>();
            double[] v3 = Array.Empty<double>();
            double[] t = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.LinkForce(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get link force results. Return code: {ret}";
                throw new EtabsException(ret, "GetLinkForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new LinkForceResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    PointElement = pointElm[i],
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
            _logger.LogDebug("Retrieved {Count} link force results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting link force: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Link Joint Force

    /// <summary>
    /// Gets link joint forces.
    /// Wraps cSapModel.Results.LinkJointForce.
    /// </summary>
    public LinkJointForceResults GetLinkJointForce(string name, eItemTypeElm itemTypeElm)
    {
        var results = new LinkJointForceResults();

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

            int ret = _sapModel.Results.LinkJointForce(
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
                results.ErrorMessage = $"Failed to get link joint force results. Return code: {ret}";
                throw new EtabsException(ret, "GetLinkJointForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new LinkJointForceResult
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
            _logger.LogDebug("Retrieved {Count} link joint force results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting link joint force: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
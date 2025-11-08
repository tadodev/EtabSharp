using EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Piers;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Spandrels;
using EtabSharp.AnalysisResults.Models.AnalysisResults.StoryResults;
using EtabSharp.Exceptions;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for story, pier, spandrel, and drift results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Pier Force

    /// <summary>
    /// Gets pier forces.
    /// Wraps cSapModel.Results.PierForce.
    /// </summary>
    public PierForceResults GetPierForce()
    {
        var results = new PierForceResults();

        try
        {
            int numberResults = 0;
            string[] storyName = Array.Empty<string>();
            string[] pierName = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] location = Array.Empty<string>();
            double[] p = Array.Empty<double>();
            double[] v2 = Array.Empty<double>();
            double[] v3 = Array.Empty<double>();
            double[] t = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.PierForce(
                ref numberResults,
                ref storyName, ref pierName,
                ref loadCase, ref location,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get pier force results. Return code: {ret}";
                throw new EtabsException(ret, "GetPierForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new PierForceResult
                {
                    StoryName = storyName[i],
                    PierName = pierName[i],
                    LoadCase = loadCase[i],
                    Location = location[i],
                    P = p[i],
                    V2 = v2[i],
                    V3 = v3[i],
                    T = t[i],
                    M2 = m2[i],
                    M3 = m3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} pier force results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting pier forces: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Spandrel Force

    /// <summary>
    /// Gets spandrel forces.
    /// Wraps cSapModel.Results.SpandrelForce.
    /// </summary>
    public SpandrelForceResults GetSpandrelForce()
    {
        var results = new SpandrelForceResults();

        try
        {
            int numberResults = 0;
            string[] storyName = Array.Empty<string>();
            string[] spandrelName = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] location = Array.Empty<string>();
            double[] p = Array.Empty<double>();
            double[] v2 = Array.Empty<double>();
            double[] v3 = Array.Empty<double>();
            double[] t = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.SpandrelForce(
                ref numberResults,
                ref storyName, ref spandrelName,
                ref loadCase, ref location,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get spandrel force results. Return code: {ret}";
                throw new EtabsException(ret, "GetSpandrelForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new SpandrelForceResult
                {
                    StoryName = storyName[i],
                    SpandrelName = spandrelName[i],
                    LoadCase = loadCase[i],
                    Location = location[i],
                    P = p[i],
                    V2 = v2[i],
                    V3 = v3[i],
                    T = t[i],
                    M2 = m2[i],
                    M3 = m3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} spandrel force results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting spandrel forces: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Story Drifts

    /// <summary>
    /// Gets story drifts.
    /// Wraps cSapModel.Results.StoryDrifts.
    /// </summary>
    public StoryDriftResults GetStoryDrifts()
    {
        var results = new StoryDriftResults();

        try
        {
            int numberResults = 0;
            string[] story = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            string[] direction = Array.Empty<string>();
            double[] drift = Array.Empty<double>();
            string[] label = Array.Empty<string>();
            double[] x = Array.Empty<double>();
            double[] y = Array.Empty<double>();
            double[] z = Array.Empty<double>();

            int ret = _sapModel.Results.StoryDrifts(
                ref numberResults,
                ref story, ref loadCase,
                ref stepType, ref stepNum,
                ref direction, ref drift,
                ref label,
                ref x, ref y, ref z);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get story drift results. Return code: {ret}";
                throw new EtabsException(ret, "GetStoryDrifts", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new StoryDriftResult
                {
                    Story = story[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    Direction = direction[i],
                    Drift = drift[i],
                    Label = label[i],
                    X = x[i],
                    Y = y[i],
                    Z = z[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} story drift results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting story drifts: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Joint Drifts

    /// <summary>
    /// Gets joint drifts.
    /// Wraps cSapModel.Results.JointDrifts.
    /// </summary>
    public JointDriftResults GetJointDrifts()
    {
        var results = new JointDriftResults();

        try
        {
            int numberResults = 0;
            string[] story = Array.Empty<string>();
            string[] label = Array.Empty<string>();
            string[] name = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] displacementX = Array.Empty<double>();
            double[] displacementY = Array.Empty<double>();
            double[] driftX = Array.Empty<double>();
            double[] driftY = Array.Empty<double>();

            int ret = _sapModel.Results.JointDrifts(
                ref numberResults,
                ref story, ref label, ref name,
                ref loadCase, ref stepType, ref stepNum,
                ref displacementX, ref displacementY,
                ref driftX, ref driftY);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get joint drift results. Return code: {ret}";
                throw new EtabsException(ret, "GetJointDrifts", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new JointDriftResult
                {
                    Story = story[i],
                    Label = label[i],
                    Name = name[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    DisplacementX = displacementX[i],
                    DisplacementY = displacementY[i],
                    DriftX = driftX[i],
                    DriftY = driftY[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} joint drift results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting joint drifts: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
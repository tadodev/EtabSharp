using EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;
using EtabSharp.Exceptions;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for section cut analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Section Cut Analysis

    /// <summary>
    /// Gets section cut analysis forces.
    /// Wraps cSapModel.Results.SectionCutAnalysis.
    /// </summary>
    public SectionCutAnalysisResults GetSectionCutAnalysis()
    {
        var results = new SectionCutAnalysisResults();

        try
        {
            int numberResults = 0;
            string[] sectionCut = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] f1 = Array.Empty<double>();
            double[] f2 = Array.Empty<double>();
            double[] f3 = Array.Empty<double>();
            double[] m1 = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.SectionCutAnalysis(
                ref numberResults,
                ref sectionCut,
                ref loadCase, ref stepType, ref stepNum,
                ref f1, ref f2, ref f3,
                ref m1, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get section cut analysis results. Return code: {ret}";
                throw new EtabsException(ret, "GetSectionCutAnalysis", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new SectionCutAnalysisResult
                {
                    SectionCut = sectionCut[i],
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
            _logger.LogDebug("Retrieved {Count} section cut analysis results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting section cut analysis: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Section Cut Design

    /// <summary>
    /// Gets section cut design forces.
    /// Wraps cSapModel.Results.SectionCutDesign.
    /// </summary>
    public SectionCutDesignResults GetSectionCutDesign()
    {
        var results = new SectionCutDesignResults();

        try
        {
            int numberResults = 0;
            string[] sectionCut = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] p = Array.Empty<double>();
            double[] v2 = Array.Empty<double>();
            double[] v3 = Array.Empty<double>();
            double[] t = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.SectionCutDesign(
                ref numberResults,
                ref sectionCut,
                ref loadCase, ref stepType, ref stepNum,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get section cut design results. Return code: {ret}";
                throw new EtabsException(ret, "GetSectionCutDesign", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new SectionCutDesignResult
                {
                    SectionCut = sectionCut[i],
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
            _logger.LogDebug("Retrieved {Count} section cut design results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting section cut design: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
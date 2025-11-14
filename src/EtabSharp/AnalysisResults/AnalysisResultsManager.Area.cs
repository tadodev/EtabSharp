using EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;
using EtabSharp.AnalysisResults.Models.AnalysisResults.Areas.ShellLayered;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for area element analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Area Force Shell

    /// <summary>
    /// Gets shell internal forces for area elements.
    /// Wraps cSapModel.Results.AreaForceShell.
    /// </summary>
    public AreaForceShellResults GetAreaForceShell(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaForceShellResults();

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
            double[] f11 = Array.Empty<double>();
            double[] f22 = Array.Empty<double>();
            double[] f12 = Array.Empty<double>();
            double[] fMax = Array.Empty<double>();
            double[] fMin = Array.Empty<double>();
            double[] fAngle = Array.Empty<double>();
            double[] fVM = Array.Empty<double>();
            double[] m11 = Array.Empty<double>();
            double[] m22 = Array.Empty<double>();
            double[] m12 = Array.Empty<double>();
            double[] mMax = Array.Empty<double>();
            double[] mMin = Array.Empty<double>();
            double[] mAngle = Array.Empty<double>();
            double[] v13 = Array.Empty<double>();
            double[] v23 = Array.Empty<double>();
            double[] vMax = Array.Empty<double>();
            double[] vAngle = Array.Empty<double>();

            int ret = _sapModel.Results.AreaForceShell(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref f11, ref f22, ref f12,
                ref fMax, ref fMin, ref fAngle, ref fVM,
                ref m11, ref m22, ref m12,
                ref mMax, ref mMin, ref mAngle,
                ref v13, ref v23, ref vMax, ref vAngle);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get area force shell results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaForceShell", results.ErrorMessage);
            }

            // Convert arrays to result objects
            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaForceShellResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    F11 = f11[i],
                    F22 = f22[i],
                    F12 = f12[i],
                    FMax = fMax[i],
                    FMin = fMin[i],
                    FAngle = fAngle[i],
                    FVM = fVM[i],
                    M11 = m11[i],
                    M22 = m22[i],
                    M12 = m12[i],
                    MMax = mMax[i],
                    MMin = mMin[i],
                    MAngle = mAngle[i],
                    V13 = v13[i],
                    V23 = v23[i],
                    VMax = vMax[i],
                    VAngle = vAngle[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} area force shell results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area force shell: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Area Joint Force Shell

    /// <summary>
    /// Gets joint forces for area elements.
    /// Wraps cSapModel.Results.AreaJointForceShell.
    /// </summary>
    public AreaJointForceResults GetAreaJointForceShell(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaJointForceResults();

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

            int ret = _sapModel.Results.AreaJointForceShell(
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
                results.ErrorMessage = $"Failed to get area joint force shell results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaJointForceShell", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaJointForceResult
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
            _logger.LogDebug("Retrieved {Count} area joint force shell results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area joint force shell: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Area Stress Shell

    /// <summary>
    /// Gets shell stresses for area elements.
    /// Wraps cSapModel.Results.AreaStressShell.
    /// </summary>
    public AreaStressShellResults GetAreaStressShell(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaStressShellResults();

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
            double[] s11Top = Array.Empty<double>();
            double[] s22Top = Array.Empty<double>();
            double[] s12Top = Array.Empty<double>();
            double[] sMaxTop = Array.Empty<double>();
            double[] sMinTop = Array.Empty<double>();
            double[] sAngleTop = Array.Empty<double>();
            double[] svmTop = Array.Empty<double>();
            double[] s11Bot = Array.Empty<double>();
            double[] s22Bot = Array.Empty<double>();
            double[] s12Bot = Array.Empty<double>();
            double[] sMaxBot = Array.Empty<double>();
            double[] sMinBot = Array.Empty<double>();
            double[] sAngleBot = Array.Empty<double>();
            double[] svmBot = Array.Empty<double>();
            double[] s13Avg = Array.Empty<double>();
            double[] s23Avg = Array.Empty<double>();
            double[] sMaxAvg = Array.Empty<double>();
            double[] sAngleAvg = Array.Empty<double>();

            int ret = _sapModel.Results.AreaStressShell(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref s11Top, ref s22Top, ref s12Top,
                ref sMaxTop, ref sMinTop, ref sAngleTop, ref svmTop,
                ref s11Bot, ref s22Bot, ref s12Bot,
                ref sMaxBot, ref sMinBot, ref sAngleBot, ref svmBot,
                ref s13Avg, ref s23Avg, ref sMaxAvg, ref sAngleAvg);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get area stress shell results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaStressShell", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaStressShellResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    S11Top = s11Top[i],
                    S22Top = s22Top[i],
                    S12Top = s12Top[i],
                    SMaxTop = sMaxTop[i],
                    SMinTop = sMinTop[i],
                    SAngleTop = sAngleTop[i],
                    SVMTop = svmTop[i],
                    S11Bot = s11Bot[i],
                    S22Bot = s22Bot[i],
                    S12Bot = s12Bot[i],
                    SMaxBot = sMaxBot[i],
                    SMinBot = sMinBot[i],
                    SAngleBot = sAngleBot[i],
                    SVMBot = svmBot[i],
                    S13Avg = s13Avg[i],
                    S23Avg = s23Avg[i],
                    SMaxAvg = sMaxAvg[i],
                    SAngleAvg = sAngleAvg[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} area stress shell results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area stress shell: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Area Stress Shell Layered

    /// <summary>
    /// Gets layered shell stresses for area elements.
    /// Wraps cSapModel.Results.AreaStressShellLayered.
    /// </summary>
    public AreaStressShellLayeredResults GetAreaStressShellLayered(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaStressShellLayeredResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] obj = Array.Empty<string>();
            string[] elm = Array.Empty<string>();
            string[] layer = Array.Empty<string>();
            int[] intPtNum = Array.Empty<int>();
            double[] intPtLoc = Array.Empty<double>();
            string[] pointElm = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] s11 = Array.Empty<double>();
            double[] s22 = Array.Empty<double>();
            double[] s12 = Array.Empty<double>();
            double[] sMax = Array.Empty<double>();
            double[] sMin = Array.Empty<double>();
            double[] sAngle = Array.Empty<double>();
            double[] svm = Array.Empty<double>();
            double[] s13Avg = Array.Empty<double>();
            double[] s23Avg = Array.Empty<double>();
            double[] sMaxAvg = Array.Empty<double>();
            double[] sAngleAvg = Array.Empty<double>();

            int ret = _sapModel.Results.AreaStressShellLayered(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref layer,
                ref intPtNum, ref intPtLoc,
                ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref s11, ref s22, ref s12,
                ref sMax, ref sMin, ref sAngle, ref svm,
                ref s13Avg, ref s23Avg, ref sMaxAvg, ref sAngleAvg);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get area stress shell layered results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaStressShellLayered", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaStressShellLayeredResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    Layer = layer[i],
                    IntegrationPointNum = intPtNum[i],
                    IntegrationPointLoc = intPtLoc[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    S11 = s11[i],
                    S22 = s22[i],
                    S12 = s12[i],
                    SMax = sMax[i],
                    SMin = sMin[i],
                    SAngle = sAngle[i],
                    SVM = svm[i],
                    S13Avg = s13Avg[i],
                    S23Avg = s23Avg[i],
                    SMaxAvg = sMaxAvg[i],
                    SAngleAvg = sAngleAvg[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} area stress shell layered results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area stress shell layered: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Area Strain Shell

    /// <summary>
    /// Gets shell strains for area elements.
    /// Wraps cSapModel.Results.AreaStrainShell.
    /// </summary>
    public AreaStrainShellResults GetAreaStrainShell(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaStrainShellResults();

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
            double[] e11top = Array.Empty<double>();
            double[] e22top = Array.Empty<double>();
            double[] g12top = Array.Empty<double>();
            double[] emaxtop = Array.Empty<double>();
            double[] emintop = Array.Empty<double>();
            double[] eangletop = Array.Empty<double>();
            double[] evmtop = Array.Empty<double>();
            double[] e11bot = Array.Empty<double>();
            double[] e22bot = Array.Empty<double>();
            double[] g12bot = Array.Empty<double>();
            double[] emaxbot = Array.Empty<double>();
            double[] eminbot = Array.Empty<double>();
            double[] eanglebot = Array.Empty<double>();
            double[] evmbot = Array.Empty<double>();
            double[] g13avg = Array.Empty<double>();
            double[] g23avg = Array.Empty<double>();
            double[] gmaxavg = Array.Empty<double>();
            double[] gangleavg = Array.Empty<double>();

            int ret = _sapModel.Results.AreaStrainShell(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref e11top, ref e22top, ref g12top,
                ref emaxtop, ref emintop, ref eangletop, ref evmtop,
                ref e11bot, ref e22bot, ref g12bot,
                ref emaxbot, ref eminbot, ref eanglebot, ref evmbot,
                ref g13avg, ref g23avg, ref gmaxavg, ref gangleavg);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get area strain shell results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaStrainShell", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaStrainShellResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    E11Top = e11top[i],
                    E22Top = e22top[i],
                    G12Top = g12top[i],
                    EMaxTop = emaxtop[i],
                    EMinTop = emintop[i],
                    EAngleTop = eangletop[i],
                    EVMTop = evmtop[i],
                    E11Bot = e11bot[i],
                    E22Bot = e22bot[i],
                    G12Bot = g12bot[i],
                    EMaxBot = emaxbot[i],
                    EMinBot = eminbot[i],
                    EAngleBot = eanglebot[i],
                    EVMBot = evmbot[i],
                    G13Avg = g13avg[i],
                    G23Avg = g23avg[i],
                    GMaxAvg = gmaxavg[i],
                    GAngleAvg = gangleavg[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} area strain shell results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area strain shell: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Area Strain Shell Layered

    /// <summary>
    /// Gets layered shell strains for area elements.
    /// Wraps cSapModel.Results.AreaStrainShellLayered.
    /// </summary>
    public AreaStrainShellLayeredResults GetAreaStrainShellLayered(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AreaStrainShellLayeredResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] obj = Array.Empty<string>();
            string[] elm = Array.Empty<string>();
            string[] layer = Array.Empty<string>();
            int[] intPtNum = Array.Empty<int>();
            double[] intPtLoc = Array.Empty<double>();
            string[] pointElm = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] e11 = Array.Empty<double>();
            double[] e22 = Array.Empty<double>();
            double[] g12 = Array.Empty<double>();
            double[] eMax = Array.Empty<double>();
            double[] eMin = Array.Empty<double>();
            double[] eAngle = Array.Empty<double>();
            double[] evm = Array.Empty<double>();
            double[] g13avg = Array.Empty<double>();
            double[] g23avg = Array.Empty<double>();
            double[] gMaxavg = Array.Empty<double>();
            double[] gAngleavg = Array.Empty<double>();

            int ret = _sapModel.Results.AreaStrainShellLayered(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm, ref layer,
                ref intPtNum, ref intPtLoc,
                ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref e11, ref e22, ref g12,
                ref eMax, ref eMin, ref eAngle, ref evm,
                ref g13avg, ref g23avg, ref gMaxavg, ref gAngleavg);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get area strain shell layered results. Return code: {ret}";
                throw new EtabsException(ret, "GetAreaStrainShellLayered", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AreaStrainShellLayeredResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
                    Layer = layer[i],
                    IntegrationPointNum = intPtNum[i],
                    IntegrationPointLoc = intPtLoc[i],
                    PointElement = pointElm[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    E11 = e11[i],
                    E22 = e22[i],
                    G12 = g12[i],
                    EMax = eMax[i],
                    EMin = eMin[i],
                    EAngle = eAngle[i],
                    EVM = evm[i],
                    G13Avg = g13avg[i],
                    G23Avg = g23avg[i],
                    GMaxAvg = gMaxavg[i],
                    GAngleAvg = gAngleavg[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} area strain shell layered results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting area strain shell layered: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
using EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for joint analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Joint Displacement

    /// <summary>
    /// Gets joint displacements.
    /// Wraps cSapModel.Results.JointDispl.
    /// </summary>
    public JointDisplacementResults GetJointDispl(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointDisplacementInternal(name, itemTypeElm, false);
    }

    /// <summary>
    /// Gets absolute joint displacements.
    /// Wraps cSapModel.Results.JointDisplAbs.
    /// </summary>
    public JointDisplacementResults GetJointDisplAbs(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointDisplacementInternal(name, itemTypeElm, true);
    }

    private JointDisplacementResults GetJointDisplacementInternal(string name, eItemTypeElm itemTypeElm, bool absolute)
    {
        var results = new JointDisplacementResults();

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

            int ret = absolute
                ? _sapModel.Results.JointDisplAbs(
                    name, itemTypeElm,
                    ref numberResults,
                    ref obj, ref elm,
                    ref loadCase, ref stepType, ref stepNum,
                    ref u1, ref u2, ref u3,
                    ref r1, ref r2, ref r3)
                : _sapModel.Results.JointDispl(
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
                results.ErrorMessage = $"Failed to get joint displacement results. Return code: {ret}";
                throw new EtabsException(ret, absolute ? "GetJointDisplAbs" : "GetJointDispl", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new JointDisplacementResult
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
            _logger.LogDebug("Retrieved {Count} joint {Type} displacement results for '{Name}'",
                numberResults, absolute ? "absolute" : "relative", name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting joint displacement: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Joint Velocity

    /// <summary>
    /// Gets joint velocities.
    /// Wraps cSapModel.Results.JointVel.
    /// </summary>
    public JointVelocityResults GetJointVel(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointVelocityInternal(name, itemTypeElm, false);
    }

    /// <summary>
    /// Gets absolute joint velocities.
    /// Wraps cSapModel.Results.JointVelAbs.
    /// </summary>
    public JointVelocityResults GetJointVelAbs(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointVelocityInternal(name, itemTypeElm, true);
    }

    private JointVelocityResults GetJointVelocityInternal(string name, eItemTypeElm itemTypeElm, bool absolute)
    {
        var results = new JointVelocityResults();

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

            int ret = absolute
                ? _sapModel.Results.JointVelAbs(
                    name, itemTypeElm,
                    ref numberResults,
                    ref obj, ref elm,
                    ref loadCase, ref stepType, ref stepNum,
                    ref u1, ref u2, ref u3,
                    ref r1, ref r2, ref r3)
                : _sapModel.Results.JointVel(
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
                results.ErrorMessage = $"Failed to get joint velocity results. Return code: {ret}";
                throw new EtabsException(ret, absolute ? "GetJointVelAbs" : "GetJointVel", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new JointVelocityResult
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
            _logger.LogDebug("Retrieved {Count} joint {Type} velocity results for '{Name}'",
                numberResults, absolute ? "absolute" : "relative", name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting joint velocity: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Joint Acceleration

    /// <summary>
    /// Gets joint accelerations.
    /// Wraps cSapModel.Results.JointAcc.
    /// </summary>
    public JointAccelerationResults GetJointAcc(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointAccelerationInternal(name, itemTypeElm, false);
    }

    /// <summary>
    /// Gets absolute joint accelerations.
    /// Wraps cSapModel.Results.JointAccAbs.
    /// </summary>
    public JointAccelerationResults GetJointAccAbs(string name, eItemTypeElm itemTypeElm)
    {
        return GetJointAccelerationInternal(name, itemTypeElm, true);
    }

    private JointAccelerationResults GetJointAccelerationInternal(string name, eItemTypeElm itemTypeElm, bool absolute)
    {
        var results = new JointAccelerationResults();

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

            int ret = absolute
                ? _sapModel.Results.JointAccAbs(
                    name, itemTypeElm,
                    ref numberResults,
                    ref obj, ref elm,
                    ref loadCase, ref stepType, ref stepNum,
                    ref u1, ref u2, ref u3,
                    ref r1, ref r2, ref r3)
                : _sapModel.Results.JointAcc(
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
                results.ErrorMessage = $"Failed to get joint acceleration results. Return code: {ret}";
                throw new EtabsException(ret, absolute ? "GetJointAccAbs" : "GetJointAcc", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new JointAccelerationResult
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
            _logger.LogDebug("Retrieved {Count} joint {Type} acceleration results for '{Name}'",
                numberResults, absolute ? "absolute" : "relative", name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting joint acceleration: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Joint Reaction

    /// <summary>
    /// Gets joint reactions.
    /// Wraps cSapModel.Results.JointReact.
    /// </summary>
    public JointReactionResults GetJointReact(string name, eItemTypeElm itemTypeElm)
    {
        var results = new JointReactionResults();

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
            double[] f1 = Array.Empty<double>();
            double[] f2 = Array.Empty<double>();
            double[] f3 = Array.Empty<double>();
            double[] m1 = Array.Empty<double>();
            double[] m2 = Array.Empty<double>();
            double[] m3 = Array.Empty<double>();

            int ret = _sapModel.Results.JointReact(
                name, itemTypeElm,
                ref numberResults,
                ref obj, ref elm,
                ref loadCase, ref stepType, ref stepNum,
                ref f1, ref f2, ref f3,
                ref m1, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get joint reaction results. Return code: {ret}";
                throw new EtabsException(ret, "GetJointReact", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new JointReactionResult
                {
                    ObjectName = obj[i],
                    ElementName = elm[i],
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
            _logger.LogDebug("Retrieved {Count} joint reaction results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting joint reaction: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
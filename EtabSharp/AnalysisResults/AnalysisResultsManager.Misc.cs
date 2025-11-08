using EtabSharp.AnalysisResults.Models.AnalysisResults.AssembledMass;
using EtabSharp.AnalysisResults.Models.AnalysisResults.GeneralizedDisplacements;
using EtabSharp.AnalysisResults.Models.AnalysisResults.PanelZones;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for miscellaneous analysis results (panel zones, assembled mass, generalized displacement).
/// </summary>
public partial class AnalysisResultsManager
{
    #region Assembled Joint Mass

    /// <summary>
    /// Gets assembled joint mass.
    /// Wraps cSapModel.Results.AssembledJointMass.
    /// </summary>
    public AssembledJointMassResults GetAssembledJointMass(string name, eItemTypeElm itemTypeElm)
    {
        var results = new AssembledJointMassResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] pointElm = Array.Empty<string>();
            double[] u1 = Array.Empty<double>();
            double[] u2 = Array.Empty<double>();
            double[] u3 = Array.Empty<double>();
            double[] r1 = Array.Empty<double>();
            double[] r2 = Array.Empty<double>();
            double[] r3 = Array.Empty<double>();

            int ret = _sapModel.Results.AssembledJointMass(
                name, itemTypeElm,
                ref numberResults,
                ref pointElm,
                ref u1, ref u2, ref u3,
                ref r1, ref r2, ref r3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get assembled joint mass results. Return code: {ret}";
                throw new EtabsException(ret, "GetAssembledJointMass", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AssembledJointMassResult
                {
                    PointElement = pointElm[i],
                    MassSource = string.Empty, // Not provided in this version
                    U1 = u1[i],
                    U2 = u2[i],
                    U3 = u3[i],
                    R1 = r1[i],
                    R2 = r2[i],
                    R3 = r3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} assembled joint mass results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting assembled joint mass: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    /// <summary>
    /// Gets assembled joint mass with mass source information.
    /// Wraps cSapModel.Results.AssembledJointMass_1.
    /// </summary>
    public AssembledJointMassResults GetAssembledJointMass(string massSourceName, string name, eItemTypeElm itemTypeElm)
    {
        var results = new AssembledJointMassResults();

        try
        {
            if (massSourceName == null)
                throw new ArgumentNullException(nameof(massSourceName));
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
            string[] pointElm = Array.Empty<string>();
            string[] massSource = Array.Empty<string>();
            double[] u1 = Array.Empty<double>();
            double[] u2 = Array.Empty<double>();
            double[] u3 = Array.Empty<double>();
            double[] r1 = Array.Empty<double>();
            double[] r2 = Array.Empty<double>();
            double[] r3 = Array.Empty<double>();

            int ret = _sapModel.Results.AssembledJointMass_1(
                massSourceName, name, itemTypeElm,
                ref numberResults,
                ref pointElm, ref massSource,
                ref u1, ref u2, ref u3,
                ref r1, ref r2, ref r3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get assembled joint mass results with source. Return code: {ret}";
                throw new EtabsException(ret, "GetAssembledJointMass_1", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new AssembledJointMassResult
                {
                    PointElement = pointElm[i],
                    MassSource = massSource[i],
                    U1 = u1[i],
                    U2 = u2[i],
                    U3 = u3[i],
                    R1 = r1[i],
                    R2 = r2[i],
                    R3 = r3[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} assembled joint mass results for source '{Source}' and '{Name}'",
                numberResults, massSourceName, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting assembled joint mass with source: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Panel Zone Deformation

    /// <summary>
    /// Gets panel zone deformations.
    /// Wraps cSapModel.Results.PanelZoneDeformation.
    /// </summary>
    public PanelZoneDeformationResults GetPanelZoneDeformation(string name, eItemTypeElm itemTypeElm)
    {
        var results = new PanelZoneDeformationResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
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

            int ret = _sapModel.Results.PanelZoneDeformation(
                name, itemTypeElm,
                ref numberResults,
                ref elm,
                ref loadCase, ref stepType, ref stepNum,
                ref u1, ref u2, ref u3,
                ref r1, ref r2, ref r3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get panel zone deformation results. Return code: {ret}";
                throw new EtabsException(ret, "GetPanelZoneDeformation", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new PanelZoneDeformationResult
                {
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
            _logger.LogDebug("Retrieved {Count} panel zone deformation results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting panel zone deformation: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Panel Zone Force

    /// <summary>
    /// Gets panel zone forces.
    /// Wraps cSapModel.Results.PanelZoneForce.
    /// </summary>
    public PanelZoneForceResults GetPanelZoneForce(string name, eItemTypeElm itemTypeElm)
    {
        var results = new PanelZoneForceResults();

        try
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            int numberResults = 0;
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

            int ret = _sapModel.Results.PanelZoneForce(
                name, itemTypeElm,
                ref numberResults,
                ref elm, ref pointElm,
                ref loadCase, ref stepType, ref stepNum,
                ref p, ref v2, ref v3,
                ref t, ref m2, ref m3);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get panel zone force results. Return code: {ret}";
                throw new EtabsException(ret, "GetPanelZoneForce", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new PanelZoneForceResult
                {
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
            _logger.LogDebug("Retrieved {Count} panel zone force results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting panel zone force: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Generalized Displacement

    /// <summary>
    /// Gets generalized displacements.
    /// Wraps cSapModel.Results.GeneralizedDispl.
    /// </summary>
    public GeneralizedDisplacementResults GetGeneralizedDispl(string name)
    {
        var results = new GeneralizedDisplacementResults();

        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            int numberResults = 0;
            string[] gd = Array.Empty<string>();
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            string[] dType = Array.Empty<string>();
            double[] value = Array.Empty<double>();

            int ret = _sapModel.Results.GeneralizedDispl(
                name,
                ref numberResults,
                ref gd,
                ref loadCase, ref stepType, ref stepNum,
                ref dType,
                ref value);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get generalized displacement results. Return code: {ret}";
                throw new EtabsException(ret, "GetGeneralizedDispl", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new GeneralizedDisplacementResult
                {
                    GeneralizedDisplacement = gd[i],
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    DisplacementType = dType[i],
                    Value = value[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} generalized displacement results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting generalized displacement: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
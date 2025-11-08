using EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Partial class for modal analysis results.
/// </summary>
public partial class AnalysisResultsManager
{
    #region Modal Period

    /// <summary>
    /// Gets modal periods and frequencies.
    /// Wraps cSapModel.Results.ModalPeriod.
    /// </summary>
    public ModalPeriodResults GetModalPeriod()
    {
        var results = new ModalPeriodResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] period = Array.Empty<double>();
            double[] frequency = Array.Empty<double>();
            double[] circFreq = Array.Empty<double>();
            double[] eigenValue = Array.Empty<double>();

            int ret = _sapModel.Results.ModalPeriod(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref period, ref frequency, ref circFreq, ref eigenValue);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get modal period results. Return code: {ret}";
                throw new EtabsException(ret, "GetModalPeriod", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ModalPeriodResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    Period = period[i],
                    Frequency = frequency[i],
                    CircularFrequency = circFreq[i],
                    EigenValue = eigenValue[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} modal period results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting modal period: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Modal Participation Factors

    /// <summary>
    /// Gets modal participation factors.
    /// Wraps cSapModel.Results.ModalParticipationFactors.
    /// </summary>
    public ModalParticipationFactorResults GetModalParticipationFactors()
    {
        var results = new ModalParticipationFactorResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] period = Array.Empty<double>();
            double[] ux = Array.Empty<double>();
            double[] uy = Array.Empty<double>();
            double[] uz = Array.Empty<double>();
            double[] rx = Array.Empty<double>();
            double[] ry = Array.Empty<double>();
            double[] rz = Array.Empty<double>();
            double[] modalMass = Array.Empty<double>();
            double[] modalStiff = Array.Empty<double>();

            int ret = _sapModel.Results.ModalParticipationFactors(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref period,
                ref ux, ref uy, ref uz,
                ref rx, ref ry, ref rz,
                ref modalMass, ref modalStiff);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get modal participation factors. Return code: {ret}";
                throw new EtabsException(ret, "GetModalParticipationFactors", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ModalParticipationFactorResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    Period = period[i],
                    UX = ux[i],
                    UY = uy[i],
                    UZ = uz[i],
                    RX = rx[i],
                    RY = ry[i],
                    RZ = rz[i],
                    ModalMass = modalMass[i],
                    ModalStiffness = modalStiff[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} modal participation factor results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting modal participation factors: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Modal Participating Mass Ratios

    /// <summary>
    /// Gets modal participating mass ratios.
    /// Wraps cSapModel.Results.ModalParticipatingMassRatios.
    /// </summary>
    public ModalParticipatingMassRatioResults GetModalParticipatingMassRatios()
    {
        var results = new ModalParticipatingMassRatioResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] stepType = Array.Empty<string>();
            double[] stepNum = Array.Empty<double>();
            double[] period = Array.Empty<double>();
            double[] ux = Array.Empty<double>();
            double[] uy = Array.Empty<double>();
            double[] uz = Array.Empty<double>();
            double[] sumUX = Array.Empty<double>();
            double[] sumUY = Array.Empty<double>();
            double[] sumUZ = Array.Empty<double>();
            double[] rx = Array.Empty<double>();
            double[] ry = Array.Empty<double>();
            double[] rz = Array.Empty<double>();
            double[] sumRX = Array.Empty<double>();
            double[] sumRY = Array.Empty<double>();
            double[] sumRZ = Array.Empty<double>();

            int ret = _sapModel.Results.ModalParticipatingMassRatios(
                ref numberResults,
                ref loadCase, ref stepType, ref stepNum,
                ref period,
                ref ux, ref uy, ref uz,
                ref sumUX, ref sumUY, ref sumUZ,
                ref rx, ref ry, ref rz,
                ref sumRX, ref sumRY, ref sumRZ);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get modal participating mass ratios. Return code: {ret}";
                throw new EtabsException(ret, "GetModalParticipatingMassRatios", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ModalParticipatingMassRatioResult
                {
                    LoadCase = loadCase[i],
                    StepType = stepType[i],
                    StepNum = stepNum[i],
                    Period = period[i],
                    UX = ux[i],
                    UY = uy[i],
                    UZ = uz[i],
                    SumUX = sumUX[i],
                    SumUY = sumUY[i],
                    SumUZ = sumUZ[i],
                    RX = rx[i],
                    RY = ry[i],
                    RZ = rz[i],
                    SumRX = sumRX[i],
                    SumRY = sumRY[i],
                    SumRZ = sumRZ[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} modal participating mass ratio results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting modal participating mass ratios: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Modal Load Participation Ratios

    /// <summary>
    /// Gets modal load participation ratios.
    /// Wraps cSapModel.Results.ModalLoadParticipationRatios.
    /// </summary>
    public ModalLoadParticipationRatioResults GetModalLoadParticipationRatios()
    {
        var results = new ModalLoadParticipationRatioResults();

        try
        {
            int numberResults = 0;
            string[] loadCase = Array.Empty<string>();
            string[] itemType = Array.Empty<string>();
            string[] item = Array.Empty<string>();
            double[] stat = Array.Empty<double>();
            double[] dyn = Array.Empty<double>();

            int ret = _sapModel.Results.ModalLoadParticipationRatios(
                ref numberResults,
                ref loadCase,
                ref itemType,
                ref item,
                ref stat,
                ref dyn);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get modal load participation ratios. Return code: {ret}";
                throw new EtabsException(ret, "GetModalLoadParticipationRatios", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ModalLoadParticipationRatioResult
                {
                    LoadCase = loadCase[i],
                    ItemType = itemType[i],
                    Item = item[i],
                    Static = stat[i],
                    Dynamic = dyn[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} modal load participation ratio results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting modal load participation ratios: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Mode Shape

    /// <summary>
    /// Gets mode shapes for joints.
    /// Wraps cSapModel.Results.ModeShape.
    /// </summary>
    public ModeShapeResults GetModeShape(string name, eItemTypeElm itemTypeElm)
    {
        var results = new ModeShapeResults();

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

            int ret = _sapModel.Results.ModeShape(
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
                results.ErrorMessage = $"Failed to get mode shape results. Return code: {ret}";
                throw new EtabsException(ret, "GetModeShape", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ModeShapeResult
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
            _logger.LogDebug("Retrieved {Count} mode shape results for '{Name}'", numberResults, name);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting mode shape: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion
}
using EtabSharp.Design.Concrete.Codes.ACI318_14;
using EtabSharp.Design.Concrete.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Design.Concrete;
using EtabSharp.Interfaces.Design.Concrete.Codes;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Design.Concrete;

/// <summary>
/// Main manager for concrete design operations in ETABS.
/// Implements IConcreteDesign interface - all common methods across all concrete design codes.
/// </summary>
public class ConcreteDesignManager : IConcreteDesign
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private readonly Lazy<ACI318_14Manager> _aci318_14;

    public ConcreteDesignManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Lazy initialization of ACI 318-14 manager
        _aci318_14 = new Lazy<ACI318_14Manager>(() => new ACI318_14Manager(_sapModel, _logger));

        _logger.LogDebug("ConcreteDesignManager initialized");
    }

    #region Code Management

    /// <inheritdoc />
    public string GetCode()
    {
        try
        {
            string codeName = string.Empty;
            int ret = _sapModel.DesignConcrete.GetCode(ref codeName);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetCode",
                    $"Failed to get active concrete design code. Return code: {ret}");
            }

            _logger.LogDebug("Active concrete design code: {Code}", codeName);

            return codeName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting active concrete design code: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetCode(string codeName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(codeName))
                throw new ArgumentException("Code name cannot be null or empty", nameof(codeName));

            int ret = _sapModel.DesignConcrete.SetCode(codeName);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetCode",
                    $"Failed to set concrete design code to '{codeName}'. Return code: {ret}");
            }

            _logger.LogInformation("Set active concrete design code to: {Code}", codeName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting concrete design code: {ex.Message}", ex);
        }
    }

    #endregion

    #region Design Section Management

    /// <inheritdoc />
    public string GetDesignSection(string frameName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string propName = string.Empty;
            int ret = _sapModel.DesignConcrete.GetDesignSection(frameName, ref propName);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetDesignSection",
                    $"Failed to get design section for frame '{frameName}'. Return code: {ret}");
            }

            _logger.LogDebug("Design section for frame '{Frame}': {Section}", frameName, propName);

            return propName;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error getting design section for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetDesignSection(string name, string propName, bool lastAnalysis = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            if (string.IsNullOrWhiteSpace(propName))
                throw new ArgumentException("Section name cannot be null or empty", nameof(propName));

            int ret = _sapModel.DesignConcrete.SetDesignSection(name, propName, lastAnalysis, itemType);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetDesignSection",
                    $"Failed to set design section for '{name}'. Return code: {ret}");
            }

            _logger.LogDebug("Set design section for '{Name}' ({ItemType}): {Section}", name, itemType, propName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting design section for '{name}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Design Execution

    /// <inheritdoc />
    public int StartDesign()
    {
        try
        {
            int ret = _sapModel.DesignConcrete.StartDesign();

            if (ret != 0)
            {
                throw new EtabsException(ret, "StartDesign",
                    $"Failed to start concrete design. Return code: {ret}");
            }

            _logger.LogInformation("Started concrete design process");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error starting concrete design: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public bool GetResultsAvailable()
    {
        try
        {
            bool available = _sapModel.DesignConcrete.GetResultsAvailable();
            _logger.LogDebug("Design results available: {Available}", available);
            return available;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error checking if results are available: {ex.Message}", ex);
        }
    }

    #endregion

    #region Design Summary Results - Beams

    /// <inheritdoc />
    public ConcreteBeamDesignResults GetSummaryResultsBeam(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new ConcreteBeamDesignResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            double[] location = Array.Empty<double>();
            string[] topCombo = Array.Empty<string>();
            double[] topArea = Array.Empty<double>();
            string[] botCombo = Array.Empty<string>();
            double[] botArea = Array.Empty<double>();
            string[] vMajorCombo = Array.Empty<string>();
            double[] vMajorArea = Array.Empty<double>();
            string[] tlCombo = Array.Empty<string>();
            double[] tlArea = Array.Empty<double>();
            string[] ttCombo = Array.Empty<string>();
            double[] ttArea = Array.Empty<double>();
            string[] errorSummary = Array.Empty<string>();
            string[] warningSummary = Array.Empty<string>();

            int ret = _sapModel.DesignConcrete.GetSummaryResultsBeam(
                name, ref numberResults,
                ref frameName, ref location,
                ref topCombo, ref topArea,
                ref botCombo, ref botArea,
                ref vMajorCombo, ref vMajorArea,
                ref tlCombo, ref tlArea,
                ref ttCombo, ref ttArea,
                ref errorSummary, ref warningSummary,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get concrete beam design results. Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResultsBeam", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ConcreteBeamDesignResult
                {
                    FrameName = frameName[i],
                    Location = location[i],
                    TopCombo = topCombo[i],
                    TopArea = topArea[i],
                    BottomCombo = botCombo[i],
                    BottomArea = botArea[i],
                    VMajorCombo = vMajorCombo[i],
                    VMajorArea = vMajorArea[i],
                    TLCombo = tlCombo[i],
                    TLArea = tlArea[i],
                    TTCombo = ttCombo[i],
                    TTArea = ttArea[i],
                    ErrorSummary = errorSummary[i],
                    WarningSummary = warningSummary[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} concrete beam design results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting concrete beam design results: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    /// <inheritdoc />
    public ConcreteBeamDesignResults GetSummaryResultsBeam_2(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new ConcreteBeamDesignResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            double[] location = Array.Empty<double>();
            string[] topCombo = Array.Empty<string>();
            double[] topArea = Array.Empty<double>();
            double[] topAreaReq = Array.Empty<double>();
            double[] topAreaMin = Array.Empty<double>();
            double[] topAreaProvided = Array.Empty<double>();
            string[] botCombo = Array.Empty<string>();
            double[] botArea = Array.Empty<double>();
            double[] botAreaReq = Array.Empty<double>();
            double[] botAreaMin = Array.Empty<double>();
            double[] botAreaProvided = Array.Empty<double>();
            string[] vMajorCombo = Array.Empty<string>();
            double[] vMajorArea = Array.Empty<double>();
            double[] vMajorAreaReq = Array.Empty<double>();
            double[] vMajorAreaMin = Array.Empty<double>();
            double[] vMajorAreaProvided = Array.Empty<double>();
            string[] tlCombo = Array.Empty<string>();
            double[] tlArea = Array.Empty<double>();
            string[] ttCombo = Array.Empty<string>();
            double[] ttArea = Array.Empty<double>();
            string[] errorSummary = Array.Empty<string>();
            string[] warningSummary = Array.Empty<string>();

            int ret = _sapModel.DesignConcrete.GetSummaryResultsBeam_2(
                name, ref numberResults,
                ref frameName, ref location,
                ref topCombo, ref topArea, ref topAreaReq, ref topAreaMin, ref topAreaProvided,
                ref botCombo, ref botArea, ref botAreaReq, ref botAreaMin, ref botAreaProvided,
                ref vMajorCombo, ref vMajorArea, ref vMajorAreaReq, ref vMajorAreaMin, ref vMajorAreaProvided,
                ref tlCombo, ref tlArea,
                ref ttCombo, ref ttArea,
                ref errorSummary, ref warningSummary,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get concrete beam design results (detailed). Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResultsBeam_2", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ConcreteBeamDesignResult
                {
                    FrameName = frameName[i],
                    Location = location[i],
                    TopCombo = topCombo[i],
                    TopArea = topArea[i],
                    TopAreaRequired = topAreaReq[i],
                    TopAreaMin = topAreaMin[i],
                    TopAreaProvided = topAreaProvided[i],
                    BottomCombo = botCombo[i],
                    BottomArea = botArea[i],
                    BottomAreaRequired = botAreaReq[i],
                    BottomAreaMin = botAreaMin[i],
                    BottomAreaProvided = botAreaProvided[i],
                    VMajorCombo = vMajorCombo[i],
                    VMajorArea = vMajorArea[i],
                    VMajorAreaRequired = vMajorAreaReq[i],
                    VMajorAreaMin = vMajorAreaMin[i],
                    VMajorAreaProvided = vMajorAreaProvided[i],
                    TLCombo = tlCombo[i],
                    TLArea = tlArea[i],
                    TTCombo = ttCombo[i],
                    TTArea = ttArea[i],
                    ErrorSummary = errorSummary[i],
                    WarningSummary = warningSummary[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} concrete beam design results (detailed)", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting concrete beam design results: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Design Summary Results - Columns

    /// <inheritdoc />
    public ConcreteColumnDesignResults GetSummaryResultsColumn(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new ConcreteColumnDesignResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            int[] option = Array.Empty<int>();
            double[] location = Array.Empty<double>();
            string[] pmmCombo = Array.Empty<string>();
            double[] pmmArea = Array.Empty<double>();
            double[] pmmRatio = Array.Empty<double>();
            string[] vMajorCombo = Array.Empty<string>();
            double[] avMajor = Array.Empty<double>();
            string[] vMinorCombo = Array.Empty<string>();
            double[] avMinor = Array.Empty<double>();
            string[] errorSummary = Array.Empty<string>();
            string[] warningSummary = Array.Empty<string>();

            int ret = _sapModel.DesignConcrete.GetSummaryResultsColumn(
                name, ref numberResults,
                ref frameName, ref option, ref location,
                ref pmmCombo, ref pmmArea, ref pmmRatio,
                ref vMajorCombo, ref avMajor,
                ref vMinorCombo, ref avMinor,
                ref errorSummary, ref warningSummary,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get concrete column design results. Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResultsColumn", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ConcreteColumnDesignResult
                {
                    FrameName = frameName[i],
                    Option = option[i],
                    Location = location[i],
                    PMMCombo = pmmCombo[i],
                    PMMArea = pmmArea[i],
                    PMMRatio = pmmRatio[i],
                    VMajorCombo = vMajorCombo[i],
                    VMajorArea = avMajor[i],
                    VMinorCombo = vMinorCombo[i],
                    VMinorArea = avMinor[i],
                    ErrorSummary = errorSummary[i],
                    WarningSummary = warningSummary[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} concrete column design results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting concrete column design results: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Design Summary Results - Joints

    /// <inheritdoc />
    public ConcreteJointDesignResults GetSummaryResultsJoint(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new ConcreteJointDesignResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            string[] lcJSRatioMajor = Array.Empty<string>();
            double[] jsRatioMajor = Array.Empty<double>();
            string[] lcJSRatioMinor = Array.Empty<string>();
            double[] jsRatioMinor = Array.Empty<double>();
            string[] lcBCCRatioMajor = Array.Empty<string>();
            double[] bccRatioMajor = Array.Empty<double>();
            string[] lcBCCRatioMinor = Array.Empty<string>();
            double[] bccRatioMinor = Array.Empty<double>();
            string[] errorSummary = Array.Empty<string>();
            string[] warningSummary = Array.Empty<string>();

            int ret = _sapModel.DesignConcrete.GetSummaryResultsJoint(
                name, ref numberResults,
                ref frameName,
                ref lcJSRatioMajor, ref jsRatioMajor,
                ref lcJSRatioMinor, ref jsRatioMinor,
                ref lcBCCRatioMajor, ref bccRatioMajor,
                ref lcBCCRatioMinor, ref bccRatioMinor,
                ref errorSummary, ref warningSummary,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get concrete joint design results. Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResultsJoint", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new ConcreteJointDesignResult
                {
                    FrameName = frameName[i],
                    LCJSRatioMajor = lcJSRatioMajor[i],
                    JSRatioMajor = jsRatioMajor[i],
                    LCJSRatioMinor = lcJSRatioMinor[i],
                    JSRatioMinor = jsRatioMinor[i],
                    LCBCCRatioMajor = lcBCCRatioMajor[i],
                    BCCRatioMajor = bccRatioMajor[i],
                    LCBCCRatioMinor = lcBCCRatioMinor[i],
                    BCCRatioMinor = bccRatioMinor[i],
                    ErrorSummary = errorSummary[i],
                    WarningSummary = warningSummary[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} concrete joint design results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting concrete joint design results: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Load Combinations for Design

    /// <inheritdoc />
    public int SetComboStrength(string comboName, bool selected)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));

            int ret = _sapModel.DesignConcrete.SetComboStrength(comboName, selected);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetComboStrength",
                    $"Failed to set strength combination '{comboName}'. Return code: {ret}");
            }

            _logger.LogDebug("Set strength combination '{Combo}': {Selected}", comboName, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting strength combination: {ex.Message}", ex);
        }
    }

    #endregion

    #region Rebar Preferences

    /// <inheritdoc />
    public string GetRebarPrefsBeam(int item)
    {
        try
        {
            string value = string.Empty;
            int ret = _sapModel.DesignConcrete.GetRebarPrefsBeam(item, ref value);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetRebarPrefsBeam",
                    $"Failed to get rebar preference {item} for beams. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved rebar preference for beams, item {Item}: {Value}", item, value);

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting rebar preference for beams: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public string GetRebarPrefsColumn(int item)
    {
        try
        {
            string value = string.Empty;
            int ret = _sapModel.DesignConcrete.GetRebarPrefsColumn(item, ref value);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetRebarPrefsColumn",
                    $"Failed to get rebar preference {item} for columns. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved rebar preference for columns, item {Item}: {Value}", item, value);

            return value;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting rebar preference for columns: {ex.Message}", ex);
        }
    }

    #endregion

    #region Seismic Framing Type

    /// <inheritdoc />
    public (int NumberItems, string[] FrameNames, int[] FramingTypes) GetSeismicFramingType(string name = "", eItemType itemType = eItemType.Objects)
    {
        try
        {
            int numberItems = 0;
            string[] frameNames = Array.Empty<string>();
            int[] framingTypes = Array.Empty<int>();

            int ret = _sapModel.DesignConcrete.GetSeismicFramingType(
                name,
                ref numberItems,
                ref frameNames,
                ref framingTypes,
                itemType);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetSeismicFramingType",
                    $"Failed to get seismic framing type. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved seismic framing types for {Count} frames", numberItems);

            return (numberItems, frameNames, framingTypes);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting seismic framing type: {ex.Message}", ex);
        }
    }

    #endregion

    #region Code-Specific Interfaces

    public IACI318_14 ACI318_14 => _aci318_14.Value;

    #endregion
}
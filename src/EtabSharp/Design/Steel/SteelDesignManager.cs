using EtabSharp.Design.Steel.Codes.AISC360_16;
using EtabSharp.Design.Steel.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Design.Steel;
using EtabSharp.Interfaces.Design.Steel.Codes;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Design.Steel;

/// <summary>
/// Main manager for steel design operations in ETABS.
/// Implements ISteelDesign interface - all common methods across all steel design codes.
/// </summary>
public class SteelDesignManager : ISteelDesign
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private readonly Lazy<AISC360_16Manager> _aisc360_16;

    /// <summary>
    /// Initializes a new instance of the SteelDesignManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public SteelDesignManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Lazy initialization of AISC 360-16 manager
        _aisc360_16 = new Lazy<AISC360_16Manager>(() => new AISC360_16Manager(_sapModel, _logger));

        _logger.LogDebug("SteelDesignManager initialized");
    }

    #region Code Management

    /// <inheritdoc />
    public string GetCode()
    {
        try
        {
            string codeName = string.Empty;
            int ret = _sapModel.DesignSteel.GetCode(ref codeName);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetCode",
                    $"Failed to get active steel design code. Return code: {ret}");
            }

            _logger.LogDebug("Active steel design code: {Code}", codeName);

            return codeName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting active steel design code: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetCode(string codeName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(codeName))
                throw new ArgumentException("Code name cannot be null or empty", nameof(codeName));

            int ret = _sapModel.DesignSteel.SetCode(codeName);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetCode",
                    $"Failed to set steel design code to '{codeName}'. Return code: {ret}");
            }

            _logger.LogInformation("Set active steel design code to: {Code}", codeName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting steel design code: {ex.Message}", ex);
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
            int ret = _sapModel.DesignSteel.GetDesignSection(frameName, ref propName);

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

            int ret = _sapModel.DesignSteel.SetDesignSection(name, propName, lastAnalysis, itemType);

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

    /// <inheritdoc />
    public int SetAutoSelectNull(string name, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            int ret = _sapModel.DesignSteel.SetAutoSelectNull(name, itemType);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetAutoSelectNull",
                    $"Failed to set auto-select for '{name}'. Return code: {ret}");
            }

            _logger.LogDebug("Set auto-select for '{Name}' ({ItemType})", name, itemType);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting auto-select for '{name}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Design Execution

    /// <inheritdoc />
    public int StartDesign()
    {
        try
        {
            int ret = _sapModel.DesignSteel.StartDesign();

            if (ret != 0)
            {
                throw new EtabsException(ret, "StartDesign",
                    $"Failed to start steel design. Return code: {ret}");
            }

            _logger.LogInformation("Started steel design process");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error starting steel design: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int ResetOverwrites()
    {
        try
        {
            int ret = _sapModel.DesignSteel.ResetOverwrites();

            if (ret != 0)
            {
                throw new EtabsException(ret, "ResetOverwrites",
                    $"Failed to reset overwrites. Return code: {ret}");
            }

            _logger.LogDebug("Reset all steel design overwrites");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error resetting overwrites: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int DeleteResults()
    {
        try
        {
            int ret = _sapModel.DesignSteel.DeleteResults();

            if (ret != 0)
            {
                throw new EtabsException(ret, "DeleteResults",
                    $"Failed to delete design results. Return code: {ret}");
            }

            _logger.LogDebug("Deleted steel design results");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting design results: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public bool GetResultsAvailable()
    {
        try
        {
            bool available = _sapModel.DesignSteel.GetResultsAvailable();
            _logger.LogDebug("Design results available: {Available}", available);
            return available;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error checking if results are available: {ex.Message}", ex);
        }
    }

    #endregion

    #region Design Summary Results

    /// <inheritdoc />
    public SteelDesignSummaryResults GetSummaryResults(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new SteelDesignSummaryResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            double[] ratio = Array.Empty<double>();
            int[] ratioType = Array.Empty<int>();
            double[] location = Array.Empty<double>();
            string[] comboName = Array.Empty<string>();
            string[] errorSummary = Array.Empty<string>();
            string[] warningSummary = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.GetSummaryResults(
                name, ref numberResults,
                ref frameName, ref ratio, ref ratioType, ref location,
                ref comboName, ref errorSummary, ref warningSummary,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get steel design summary results. Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResults", results.ErrorMessage);
            }

            // Convert to SteelDesignSummaryResult objects (basic version)
            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new SteelDesignSummaryResult
                {
                    FrameName = frameName[i],
                    PMMCombo = comboName[i],
                    PMMRatio = ratio[i],
                    Status = errorSummary[i] == "" && warningSummary[i] == "" ? "Pass" : "Check"
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} steel design summary results", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting steel design summary: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    /// <inheritdoc />
    public SteelDesignSummaryResults GetSummaryResults_3(string name = "", eItemType itemType = eItemType.Objects)
    {
        var results = new SteelDesignSummaryResults();

        try
        {
            int numberResults = 0;
            string[] frameName = Array.Empty<string>();
            eFrameDesignOrientation[] frameType = Array.Empty<eFrameDesignOrientation>();
            string[] designSect = Array.Empty<string>();
            string[] status = Array.Empty<string>();
            string[] pmmCombo = Array.Empty<string>();
            double[] pmmRatio = Array.Empty<double>();
            double[] pRatio = Array.Empty<double>();
            double[] mMajRatio = Array.Empty<double>();
            double[] mMinRatio = Array.Empty<double>();
            string[] vMajCombo = Array.Empty<string>();
            double[] vMajRatio = Array.Empty<double>();
            string[] vMinCombo = Array.Empty<string>();
            double[] vMinRatio = Array.Empty<double>();

            int ret = _sapModel.DesignSteel.GetSummaryResults_3(
                name, ref numberResults,
                ref frameName, ref frameType, ref designSect, ref status,
                ref pmmCombo, ref pmmRatio, ref pRatio, ref mMajRatio, ref mMinRatio,
                ref vMajCombo, ref vMajRatio,
                ref vMinCombo, ref vMinRatio,
                itemType);

            results.ReturnCode = ret;
            results.NumberResults = numberResults;

            if (ret != 0)
            {
                results.IsSuccess = false;
                results.ErrorMessage = $"Failed to get steel design summary results. Return code: {ret}";
                throw new EtabsException(ret, "GetSummaryResults_3", results.ErrorMessage);
            }

            for (int i = 0; i < numberResults; i++)
            {
                results.Results.Add(new SteelDesignSummaryResult
                {
                    FrameName = frameName[i],
                    FrameType = frameType[i],
                    DesignSection = designSect[i],
                    Status = status[i],
                    PMMCombo = pmmCombo[i],
                    PMMRatio = pmmRatio[i],
                    PRatio = pRatio[i],
                    MMajorRatio = mMajRatio[i],
                    MMinorRatio = mMinRatio[i],
                    VMajorCombo = vMajCombo[i],
                    VMajorRatio = vMajRatio[i],
                    VMinorCombo = vMinCombo[i],
                    VMinorRatio = vMinRatio[i]
                });
            }

            results.IsSuccess = true;
            _logger.LogDebug("Retrieved {Count} steel design summary results (detailed)", numberResults);

            return results;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting steel design summary: {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    #endregion

    #region Verification

    /// <inheritdoc />
    public (int NumberItems, int N1, int N2, string[] FrameNames) VerifyPassed()
    {
        try
        {
            int numberItems = 0;
            int n1 = 0;
            int n2 = 0;
            string[] frameNames = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.VerifyPassed(ref numberItems, ref n1, ref n2, ref frameNames);

            if (ret != 0)
            {
                throw new EtabsException(ret, "VerifyPassed",
                    $"Failed to verify passed frames. Return code: {ret}");
            }

            _logger.LogDebug("Verified passed frames: {Count} items, N1={N1}, N2={N2}", numberItems, n1, n2);

            return (numberItems, n1, n2, frameNames);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error verifying passed frames: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public (int NumberItems, string[] SectionNames) VerifySections()
    {
        try
        {
            int numberItems = 0;
            string[] sectionNames = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.VerifySections(ref numberItems, ref sectionNames);

            if (ret != 0)
            {
                throw new EtabsException(ret, "VerifySections",
                    $"Failed to verify sections. Return code: {ret}");
            }

            _logger.LogDebug("Verified sections: {Count} items", numberItems);

            return (numberItems, sectionNames);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error verifying sections: {ex.Message}", ex);
        }
    }

    #endregion

    #region Load Combinations for Design

    /// <inheritdoc />
    public string[] GetComboDeflection()
    {
        try
        {
            int numberItems = 0;
            string[] comboNames = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.GetComboDeflection(ref numberItems, ref comboNames);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetComboDeflection",
                    $"Failed to get deflection combinations. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved {Count} deflection combinations", numberItems);

            return comboNames;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting deflection combinations: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public string[] GetComboStrength()
    {
        try
        {
            int numberItems = 0;
            string[] comboNames = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.GetComboStrength(ref numberItems, ref comboNames);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetComboStrength",
                    $"Failed to get strength combinations. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved {Count} strength combinations", numberItems);

            return comboNames;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting strength combinations: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetComboDeflection(string comboName, bool selected)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));

            int ret = _sapModel.DesignSteel.SetComboDeflection(comboName, selected);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetComboDeflection",
                    $"Failed to set deflection combination '{comboName}'. Return code: {ret}");
            }

            _logger.LogDebug("Set deflection combination '{Combo}': {Selected}", comboName, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting deflection combination: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetComboStrength(string comboName, bool selected)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(comboName))
                throw new ArgumentException("Combination name cannot be null or empty", nameof(comboName));

            int ret = _sapModel.DesignSteel.SetComboStrength(comboName, selected);

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

    #region Design Groups

    /// <inheritdoc />
    public string[] GetGroup()
    {
        try
        {
            int numberItems = 0;
            string[] groupNames = Array.Empty<string>();

            int ret = _sapModel.DesignSteel.GetGroup(ref numberItems, ref groupNames);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetGroup",
                    $"Failed to get design groups. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved {Count} design groups", numberItems);

            return groupNames;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting design groups: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetGroup(string groupName, bool selected)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            int ret = _sapModel.DesignSteel.SetGroup(groupName, selected);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetGroup",
                    $"Failed to set design group '{groupName}'. Return code: {ret}");
            }

            _logger.LogDebug("Set design group '{Group}': {Selected}", groupName, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting design group: {ex.Message}", ex);
        }
    }

    #endregion

    #region Target Displacement and Period

    /// <inheritdoc />
    public (int NumberItems, string[] LoadCases, string[] Points, double[] Displacements, bool Active) GetTargetDispl()
    {
        try
        {
            int numberItems = 0;
            string[] loadCases = Array.Empty<string>();
            string[] points = Array.Empty<string>();
            double[] displacements = Array.Empty<double>();
            bool active = false;

            int ret = _sapModel.DesignSteel.GetTargetDispl(
                ref numberItems,
                ref loadCases, ref points, ref displacements,
                ref active);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetTargetDispl",
                    $"Failed to get target displacement. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved target displacement: {Count} items, Active={Active}", numberItems, active);

            return (numberItems, loadCases, points, displacements, active);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting target displacement: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetTargetDispl(string[] loadCases, string[] points, double[] displacements, bool active = true)
    {
        try
        {
            if (loadCases == null || loadCases.Length == 0)
                throw new ArgumentException("Load cases array cannot be null or empty", nameof(loadCases));

            if (points == null || points.Length == 0)
                throw new ArgumentException("Points array cannot be null or empty", nameof(points));

            if (displacements == null || displacements.Length == 0)
                throw new ArgumentException("Displacements array cannot be null or empty", nameof(displacements));

            if (loadCases.Length != points.Length || loadCases.Length != displacements.Length)
                throw new ArgumentException("Arrays must have the same length");

            int ret = _sapModel.DesignSteel.SetTargetDispl(
                loadCases.Length,
                ref loadCases, ref points, ref displacements,
                active);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetTargetDispl",
                    $"Failed to set target displacement. Return code: {ret}");
            }

            _logger.LogDebug("Set target displacement: {Count} items, Active={Active}", loadCases.Length, active);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting target displacement: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public (int NumberItems, string ModalCase, int[] Modes, double[] Periods, bool Active) GetTargetPeriod()
    {
        try
        {
            int numberItems = 0;
            string modalCase = string.Empty;
            int[] modes = Array.Empty<int>();
            double[] periods = Array.Empty<double>();
            bool active = false;

            int ret = _sapModel.DesignSteel.GetTargetPeriod(
                ref numberItems,
                ref modalCase,
                ref modes, ref periods,
                ref active);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetTargetPeriod",
                    $"Failed to get target period. Return code: {ret}");
            }

            _logger.LogDebug("Retrieved target period: {Count} items, Modal case='{Case}', Active={Active}",
                numberItems, modalCase, active);

            return (numberItems, modalCase, modes, periods, active);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting target period: {ex.Message}", ex);
        }
    }

    /// <inheritdoc />
    public int SetTargetPeriod(string modalCase, int[] modes, double[] periods, bool active = true)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(modalCase))
                throw new ArgumentException("Modal case cannot be null or empty", nameof(modalCase));

            if (modes == null || modes.Length == 0)
                throw new ArgumentException("Modes array cannot be null or empty", nameof(modes));

            if (periods == null || periods.Length == 0)
                throw new ArgumentException("Periods array cannot be null or empty", nameof(periods));

            if (modes.Length != periods.Length)
                throw new ArgumentException("Modes and periods arrays must have the same length");

            int ret = _sapModel.DesignSteel.SetTargetPeriod(
                modes.Length,
                modalCase,
                ref modes, ref periods,
                active);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetTargetPeriod",
                    $"Failed to set target period. Return code: {ret}");
            }

            _logger.LogDebug("Set target period: {Count} items, Modal case='{Case}', Active={Active}",
                modes.Length, modalCase, active);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting target period: {ex.Message}", ex);
        }
    }

    #endregion

    #region Code-Specific Interfaces

    /// <inheritdoc />
    public IAISC360_16 AISC360_16 => _aisc360_16.Value;

    #endregion
}
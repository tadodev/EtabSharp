using EtabSharp.Elements.Story.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Elements.Stories;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.Story;

/// <summary>
/// Handles ETABS story management (creation, modification, retrieval).
/// </summary>
public class Story: IStory
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal Story(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Story Information

    ///<inheritdoc/>
    public string[] GetNameList()
    {
        try
        {
            int numberOfStories = 0;
            string[] storyNames = null;

            int ret = _sapModel.Story.GetNameList(ref numberOfStories, ref storyNames);
            if (ret != 0)
            {
                _logger.LogError("Failed to get story name list. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetNameList", "Failed to retrieve story names.");
            }

            _logger.LogDebug("Retrieved {Count} stories", numberOfStories);
            return storyNames ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting story name list");
            throw new EtabsException("Unexpected error retrieving story names", ex);
        }
    }

    ///<inheritdoc/>
    public double GetHeight(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            double height = 0;
            int ret = _sapModel.Story.GetHeight(storyName, ref height);

            if (ret != 0)
            {
                _logger.LogError("Failed to get height for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetHeight", $"Failed to get height for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' height: {Height}", storyName, height);
            return height;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting height for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error getting height for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public double GetElevation(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            double elevation = 0;
            int ret = _sapModel.Story.GetElevation(storyName, ref elevation);

            if (ret != 0)
            {
                _logger.LogError("Failed to get elevation for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetElevation", $"Failed to get elevation for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' elevation: {Elevation}", storyName, elevation);
            return elevation;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting elevation for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error getting elevation for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public StoryData GetStories()
    {
        try
        {
            double baseElevation = 0;
            int numberOfStories = 0;
            string[] storyNames = null;
            double[] storyElevations = null;
            double[] storyHeights = null;
            bool[] isMasterStory = null;
            string[] similarToStory = null;
            bool[] spliceAbove = null;
            double[] spliceHeight = null;
            int[] color = null;

            int ret = _sapModel.Story.GetStories_2(
                ref baseElevation,
                ref numberOfStories,
                ref storyNames,
                ref storyElevations,
                ref storyHeights,
                ref isMasterStory,
                ref similarToStory,
                ref spliceAbove,
                ref spliceHeight,
                ref color);

            if (ret != 0)
            {
                _logger.LogError("Failed to get story data. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetStories_2", "Failed to retrieve story data.");
            }

            var storyData = new StoryData
            {
                BaseElevation = baseElevation,
                NumberStories = numberOfStories,
                StoryNames = storyNames ?? Array.Empty<string>(),
                StoryElevations = storyElevations ?? Array.Empty<double>(),
                StoryHeights = storyHeights ?? Array.Empty<double>(),
                IsMasterStory = isMasterStory ?? Array.Empty<bool>(),
                SimilarToStory = similarToStory ?? Array.Empty<string>(),
                SpliceAbove = new bool[numberOfStories],
                SpliceHeight = spliceHeight ?? Array.Empty<double>(),
                Color = color ?? Array.Empty<int>()
            };

            // Determine which stories have splices
            if (spliceHeight != null)
            {
                for (int i = 0; i < numberOfStories; i++)
                {
                    storyData.SpliceAbove[i] = spliceHeight[i] > 0;
                }
            }

            _logger.LogInformation("Retrieved {Count} stories, base elevation: {BaseElevation}",
                numberOfStories, baseElevation);
            return storyData;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting story data");
            throw new EtabsException("Unexpected error retrieving story data", ex);
        }
    }

    #endregion

    #region Story Modification

    ///<inheritdoc/>
    public int SetHeight(string storyName, double height)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Story height must be positive.");

        try
        {
            _logger.LogInformation("Setting height for story '{StoryName}' to {Height}", storyName, height);

            int ret = _sapModel.Story.SetHeight(storyName, height);

            if (ret != 0)
            {
                _logger.LogError("Failed to set height for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetHeight", $"Failed to set height for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set height for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting height for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting height for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetElevation(string storyName, double elevation)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            _logger.LogInformation("Setting elevation for story '{StoryName}' to {Elevation}",
                storyName, elevation);

            int ret = _sapModel.Story.SetElevation(storyName, elevation);

            if (ret != 0)
            {
                _logger.LogError("Failed to set elevation for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetElevation", $"Failed to set elevation for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set elevation for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting elevation for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting elevation for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetStories(StoryData storyData)
    {
        if (storyData == null)
            throw new ArgumentNullException(nameof(storyData));

        if (!storyData.Validate())
            throw new ArgumentException("Story data is invalid. Check that all arrays have correct length and heights are positive.", nameof(storyData));

        try
        {
            _logger.LogInformation("Setting stories for building: {Count} stories, base elevation: {BaseElevation}",
                storyData.NumberStories, storyData.BaseElevation);

            var storyNames = storyData.StoryNames;
            var storyHeight = storyData.StoryHeights;
            var isMasterStory = storyData.IsMasterStory;
            var similarToStory = storyData.SimilarToStory;
            var spliceAbove = storyData.SpliceAbove;
            var spliceHeight = storyData.SpliceHeight;
            var color = storyData.Color;

            int ret = _sapModel.Story.SetStories_2(
                storyData.BaseElevation,
                storyData.NumberStories,
                ref storyNames,
                ref storyHeight,
                ref isMasterStory,
                ref similarToStory,
                ref spliceAbove,
                ref spliceHeight,
                ref color
                );

            if (ret != 0)
            {
                _logger.LogError("Failed to set stories. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "SetStories_2",
                    "Failed to set stories. Ensure no objects exist in the model before calling this function.");
            }

            _logger.LogInformation("Successfully set {Count} stories", storyData.NumberStories);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting stories");
            throw new EtabsException("Unexpected error setting stories", ex);
        }
    }

    #endregion

    #region Master Story Management

    ///<inheritdoc/>
    public bool IsMasterStory(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            bool isMaster = false;
            int ret = _sapModel.Story.GetMasterStory(storyName, ref isMaster);

            if (ret != 0)
            {
                _logger.LogError("Failed to check if story '{StoryName}' is master. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetMasterStory", $"Failed to check master status for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' is master: {IsMaster}", storyName, isMaster);
            return isMaster;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error checking master status for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error checking master status for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetMasterStory(string storyName, bool isMasterStory)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            _logger.LogInformation("Setting story '{StoryName}' as master: {IsMaster}",
                storyName, isMasterStory);

            int ret = _sapModel.Story.SetMasterStory(storyName, isMasterStory);

            if (ret != 0)
            {
                _logger.LogError("Failed to set master status for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetMasterStory", $"Failed to set master status for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set master status for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting master status for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting master status for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public (bool isMasterStory, string similarToStory) GetSimilarTo(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            bool isMaster = false;
            string similarTo = string.Empty;

            int ret = _sapModel.Story.GetSimilarTo(storyName, ref isMaster, ref similarTo);

            if (ret != 0)
            {
                _logger.LogError("Failed to get similar-to info for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetSimilarTo", $"Failed to get similar-to info for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' is master: {IsMaster}, similar to: '{SimilarTo}'",
                storyName, isMaster, similarTo);
            return (isMaster, similarTo ?? string.Empty);
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting similar-to info for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error getting similar-to info for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetSimilarTo(string storyName, string similarToStory)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            _logger.LogInformation("Setting story '{StoryName}' similar to: '{SimilarTo}'",
                storyName, similarToStory ?? "None");

            int ret = _sapModel.Story.SetSimilarTo(storyName, similarToStory ?? string.Empty);

            if (ret != 0)
            {
                _logger.LogError("Failed to set similar-to for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetSimilarTo", $"Failed to set similar-to for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set similar-to for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting similar-to for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting similar-to for story '{storyName}'", ex);
        }
    }

    #endregion

    #region Story Splice

    ///<inheritdoc/>
    public double GetSplice(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            double spliceHeight = 0;
            bool spliceAbove = false;
            int ret = _sapModel.Story.GetSplice(storyName,ref spliceAbove, ref spliceHeight);

            if (ret != 0)
            {
                _logger.LogError("Failed to get splice for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetSplice", $"Failed to get splice for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' splice height: {SpliceHeight}", storyName, spliceHeight);
            return spliceHeight;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting splice for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error getting splice for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetSplice(string storyName, bool spliceAbove, double spliceHeight)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        if (spliceHeight < 0)
            throw new ArgumentOutOfRangeException(nameof(spliceHeight), "Splice height cannot be negative.");

        try
        {
            _logger.LogInformation("Setting splice for story '{StoryName}' to {SpliceHeight}",
                storyName, spliceHeight);
            int ret = _sapModel.Story.SetSplice(storyName,spliceAbove, spliceHeight);

            if (ret != 0)
            {
                _logger.LogError("Failed to set splice for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetSplice", $"Failed to set splice for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set splice for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting splice for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting splice for story '{storyName}'", ex);
        }
    }

    #endregion

    #region GUID Management

    ///<inheritdoc/>
    public string GetGUID(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        try
        {
            string guid = string.Empty;
            int ret = _sapModel.Story.GetGUID(storyName, ref guid);

            if (ret != 0)
            {
                _logger.LogError("Failed to get GUID for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "GetGUID", $"Failed to get GUID for story '{storyName}'.");
            }

            _logger.LogDebug("Story '{StoryName}' GUID: {GUID}", storyName, guid);
            return guid ?? string.Empty;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error getting GUID for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error getting GUID for story '{storyName}'", ex);
        }
    }

    ///<inheritdoc/>
    public int SetGUID(string storyName, string guid)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            throw new ArgumentException("Story name cannot be empty.", nameof(storyName));

        if (string.IsNullOrWhiteSpace(guid))
            throw new ArgumentException("GUID cannot be empty.", nameof(guid));

        try
        {
            _logger.LogInformation("Setting GUID for story '{StoryName}' to {GUID}", storyName, guid);

            int ret = _sapModel.Story.SetGUID(storyName, guid);

            if (ret != 0)
            {
                _logger.LogError("Failed to set GUID for story '{StoryName}'. Return code: {ReturnCode}",
                    storyName, ret);
                throw new EtabsException(ret, "SetGUID", $"Failed to set GUID for story '{storyName}'.");
            }

            _logger.LogInformation("Successfully set GUID for story '{StoryName}'", storyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error setting GUID for story '{StoryName}'", storyName);
            throw new EtabsException($"Unexpected error setting GUID for story '{storyName}'", ex);
        }
    }

    #endregion

    #region Helper Methods

    ///<inheritdoc/>
    public int GetStoryCount()
    {
        try
        {
            var names = GetNameList();
            return names?.Length ?? 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting story count");
            return 0;
        }
    }

    ///<inheritdoc/>
    public bool StoryExists(string storyName)
    {
        if (string.IsNullOrWhiteSpace(storyName))
            return false;

        try
        {
            var names = GetNameList();
            return names != null && names.Contains(storyName, StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if story '{StoryName}' exists", storyName);
            return false;
        }
    }

   

    #endregion
}
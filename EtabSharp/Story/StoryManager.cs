using EtabSharp.Exceptions;
using EtabSharp.Interface;
using EtabSharp.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Story;

public class StoryManager:IStoryManager
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal StoryManager(dynamic sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<StoryData> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            try
            {
                _logger.LogDebug("Getting all stories");

                int numStories = 0;
                string[] storyNames = Array.Empty<string>();
                double[] elevations = Array.Empty<double>();
                double[] heights = Array.Empty<double>();
                bool[] isMaster = Array.Empty<bool>();
                string[] similarTo = Array.Empty<string>();
                double[] spliceAbove = Array.Empty<double>();
                double baseElev = 0;

                int ret = _sapModel.Story.GetStories_2(
                    ref numStories,
                    ref storyNames,
                    ref elevations,
                    ref heights,
                    ref isMaster,
                    ref similarTo,
                    ref spliceAbove,
                    ref baseElev);

                EtabsHelper.ThrowIfFailed(ret, "Get stories");

                _logger.LogInformation("Retrieved {Count} stories", numStories);

                return new StoryData
                {
                    NumberStories = numStories,
                    StoryNames = storyNames,
                    StoryElevations = elevations,
                    StoryHeights = heights,
                    IsMasterStory = isMaster,
                    SimilarToStory = similarTo,
                    SpliceHeights = spliceAbove,
                    BaseElevation = baseElev
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get stories");
                throw new EtabsException("Failed to get stories", ex);
            }
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Story?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Story name cannot be empty", nameof(name));

        var allStories = await GetAllAsync(cancellationToken);
        var index = Array.IndexOf(allStories.StoryNames, name);

        if (index == -1)
        {
            _logger.LogWarning("Story '{StoryName}' not found", name);
            return null;
        }

        return new Story
        {
            Name = allStories.StoryNames[index],
            Height = allStories.StoryHeights[index],
            Elevation = allStories.StoryElevations[index],
            IsMasterStory = allStories.IsMasterStory[index],
            SimilarToStory = allStories.SimilarToStory[index],
            SpliceHeight = allStories.SpliceHeights[index]
        };
    }

    /// <inheritdoc />
    public async Task CreateAsync(string name, double height, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Story name cannot be empty", nameof(name));

        if (height <= 0)
            throw new ArgumentException("Story height must be positive", nameof(height));

        await Task.Run(() =>
        {
            try
            {
                _logger.LogInformation("Creating story: {StoryName} with height: {Height}", name, height);

                int ret = _sapModel.Story.SetStory(name, height);
                EtabsHelper.ThrowIfFailed(ret, $"Create story '{name}'");

                _logger.LogInformation("Successfully created story: {StoryName}", name);
            }
            catch (Exception ex) when (ex is not EtabsException)
            {
                _logger.LogError(ex, "Failed to create story: {StoryName}", name);
                throw new EtabsException($"Failed to create story '{name}'", ex);
            }
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task CreateMultipleAsync(
        IEnumerable<(string name, double height)> stories,
        CancellationToken cancellationToken = default)
    {
        if (stories == null)
            throw new ArgumentNullException(nameof(stories));

        var storyList = stories.ToList();
        if (!storyList.Any())
            throw new ArgumentException("At least one story must be provided", nameof(stories));

        await Task.Run(() =>
        {
            try
            {
                _logger.LogInformation("Creating {Count} stories", storyList.Count);

                foreach (var (name, height) in storyList)
                {
                    if (string.IsNullOrWhiteSpace(name))
                        throw new ArgumentException($"Story name cannot be empty");

                    if (height <= 0)
                        throw new ArgumentException($"Story height must be positive for story '{name}'");

                    int ret = _sapModel.Story.SetStory(name, height);
                    EtabsHelper.ThrowIfFailed(ret, $"Create story '{name}'");

                    _logger.LogDebug("Created story: {StoryName}", name);
                }

                _logger.LogInformation("Successfully created {Count} stories", storyList.Count);
            }
            catch (Exception ex) when (ex is not EtabsException)
            {
                _logger.LogError(ex, "Failed to create multiple stories");
                throw new EtabsException("Failed to create multiple stories", ex);
            }
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Story name cannot be empty", nameof(name));

        await Task.Run(() =>
        {
            try
            {
                _logger.LogInformation("Deleting story: {StoryName}", name);

                int ret = _sapModel.Story.DeleteStory(name);
                EtabsHelper.ThrowIfFailed(ret, $"Delete story '{name}'");

                _logger.LogInformation("Successfully deleted story: {StoryName}", name);
            }
            catch (Exception ex) when (ex is not EtabsException)
            {
                _logger.LogError(ex, "Failed to delete story: {StoryName}", name);
                throw new EtabsException($"Failed to delete story '{name}'", ex);
            }
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task SetElevationAsync(string name, double elevation, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Story name cannot be empty", nameof(name));

        await Task.Run(() =>
        {
            try
            {
                _logger.LogInformation("Setting elevation for story {StoryName} to {Elevation}", name, elevation);

                int ret = _sapModel.Story.SetElevation(name, elevation);
                EtabsHelper.ThrowIfFailed(ret, $"Set elevation for story '{name}'");

                _logger.LogInformation("Successfully set elevation for story: {StoryName}", name);
            }
            catch (Exception ex) when (ex is not EtabsException)
            {
                _logger.LogError(ex, "Failed to set elevation for story: {StoryName}", name);
                throw new EtabsException($"Failed to set elevation for story '{name}'", ex);
            }
        }, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        var storyData = await GetAllAsync(cancellationToken);
        return storyData.NumberStories;
    }
}
namespace EtabSharp.Models;

public class Story
{
    /// <summary>Story name</summary>
    public required string Name { get; init; }

    /// <summary>Story height</summary>
    public required double Height { get; init; }

    /// <summary>Story elevation</summary>
    public required double Elevation { get; init; }

    /// <summary>Is this a master story</summary>
    public bool IsMasterStory { get; init; }

    /// <summary>Similar to story name</summary>
    public string? SimilarToStory { get; init; }

    /// <summary>Splice height above story</summary>
    public double SpliceHeight { get; init; }

    /// <summary>Story color</summary>
    public int Color { get; init; }
}

/// <summary>
/// Contains data about all stories in the model
/// </summary>
public record StoryData
{
    /// <summary>Base elevation of the model</summary>
    public double BaseElevation { get; init; }

    /// <summary>Number of stories</summary>
    public int NumberStories { get; init; }

    /// <summary>Array of story names</summary>
    public string[] StoryNames { get; init; } = Array.Empty<string>();

    /// <summary>Array of story elevations</summary>
    public double[] StoryElevations { get; init; } = Array.Empty<double>();

    /// <summary>Array of story heights</summary>
    public double[] StoryHeights { get; init; } = Array.Empty<double>();

    /// <summary>Array indicating master stories</summary>
    public bool[] IsMasterStory { get; init; } = Array.Empty<bool>();

    /// <summary>Array of similar-to story names</summary>
    public string[] SimilarToStory { get; init; } = Array.Empty<string>();

    /// <summary>Array of splice heights</summary>
    public double[] SpliceHeights { get; init; } = Array.Empty<double>();
}
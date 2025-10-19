namespace EtabSharp.Elements.Story;

/// <summary>
/// The story information for the current tower
/// </summary>
public class StoryData
{
    /// <summary>
    /// The elevation of the base [L]
    /// </summary>
    public double BaseElevation { get; set; }
    /// <summary>
    /// The number of stories 
    /// </summary>
    public int NumberStories { get; set; }
    /// <summary>
    /// The names of the stories  
    /// </summary>
    public string[] StoryNames { get; set; }
    /// <summary>
    /// The story elevations [L] 
    /// </summary>
    public double[] StoryElevations { get; set; }
    /// <summary>
    /// The story heights [L] 
    /// </summary>
    public double[] StoryHeights { get; set; }
    /// <summary>
    /// Whether the story is a master story 
    /// </summary>
    public bool[] IsMasterStory { get; set; }
    /// <summary>
    /// If the story is not a master story, which master story the story is similar to 
    /// </summary>
    public string SimilarToStory { get; set; }
    /// <summary>
    /// This is True if the story has a splice height, and False otherwise 
    /// </summary>
    public bool[] SpliceAbove { get; set; }
    /// <summary>
    /// The story splice height [L]
    /// </summary>
    public double[] SpliceHeight { get; set; }
    /// <summary>
    /// The display color for the story specified as an Integer
    /// </summary>
    public int Color { get; set; }
}
using System.Text;

namespace EtabSharp.Elements.Story.Models;

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
    public string[] SimilarToStory { get; set; }
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
    public int[] Color { get; set; }

    /// <summary>
    /// Validates the story data for consistency and correctness.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    public bool Validate()
    {
        // Check number of stories
        if (NumberStories <= 0)
        {
            return false;
        }

        // Check that all arrays exist and have correct length
        if (StoryNames == null || StoryNames.Length != NumberStories)
            return false;

        if (StoryHeights == null || StoryHeights.Length != NumberStories)
            return false;

        if (StoryElevations == null || StoryElevations.Length != NumberStories)
            return false;

        if (IsMasterStory == null || IsMasterStory.Length != NumberStories)
            return false;

        if (SimilarToStory == null || SimilarToStory.Length != NumberStories)
            return false;

        // Optional arrays - initialize if null
        if (SpliceAbove == null)
            SpliceAbove = new bool[NumberStories];

        if (SpliceHeight == null)
            SpliceHeight = new double[NumberStories];

        if (Color == null)
            Color = Enumerable.Repeat(-1, NumberStories).ToArray(); // -1 = auto color

        // Check array lengths for optional arrays
        if (SpliceAbove.Length != NumberStories)
            return false;

        if (SpliceHeight.Length != NumberStories)
            return false;

        if (Color.Length != NumberStories)
            return false;

        // Check that all story names are unique and non-empty
        if (StoryNames.Any(string.IsNullOrWhiteSpace))
            return false;

        if (StoryNames.Distinct().Count() != StoryNames.Length)
            return false;

        // Check that all heights are positive
        if (StoryHeights.Any(h => h <= 0))
            return false;

        // Check elevations are in ascending order
        for (int i = 1; i < NumberStories; i++)
        {
            if (StoryElevations[i] <= StoryElevations[i - 1])
                return false;
        }

        // Check similar-to relationships
        for (int i = 0; i < NumberStories; i++)
        {
            if (!IsMasterStory[i] && !string.IsNullOrEmpty(SimilarToStory[i]))
            {
                // Similar-to story must exist and be a master story
                int masterIndex = Array.IndexOf(StoryNames, SimilarToStory[i]);
                if (masterIndex == -1 || !IsMasterStory[masterIndex])
                    return false;
            }
        }

        return true;
    }

    #region Helper method for StoryBuilder

    /// <summary>
    /// Calculates story elevations based on base elevation and heights.
    /// </summary>
    public void CalculateElevations()
    {
        if (StoryHeights == null || NumberStories == 0)
            return;

        StoryElevations = new double[NumberStories];
        StoryElevations[0] = BaseElevation;

        for (int i = 1; i < NumberStories; i++)
        {
            StoryElevations[i] = StoryElevations[i - 1] + StoryHeights[i - 1];
        }
    }

    /// <summary>
    /// Calculates story heights based on elevations.
    /// </summary>
    public void CalculateHeights()
    {
        if (StoryElevations == null || NumberStories == 0)
            return;

        StoryHeights = new double[NumberStories];

        for (int i = 0; i < NumberStories - 1; i++)
        {
            StoryHeights[i] = StoryElevations[i + 1] - StoryElevations[i];
        }

        // Last story height (set to same as second-to-last or default)
        StoryHeights[NumberStories - 1] = NumberStories > 1
            ? StoryHeights[NumberStories - 2]
            : 3.5; // Default 3.5 units
    }

    /// <summary>
    /// Gets the story index by name.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Zero-based index, or -1 if not found</returns>
    public int GetStoryIndex(string storyName)
    {
        if (StoryNames == null)
            return -1;

        return Array.IndexOf(StoryNames, storyName);
    }

    /// <summary>
    /// Gets the elevation of a specific story by name.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Elevation, or double.NaN if not found</returns>
    public double GetStoryElevation(string storyName)
    {
        int index = GetStoryIndex(storyName);
        return index >= 0 ? StoryElevations[index] : double.NaN;
    }

    /// <summary>
    /// Gets the height of a specific story by name.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Height, or double.NaN if not found</returns>
    public double GetStoryHeight(string storyName)
    {
        int index = GetStoryIndex(storyName);
        return index >= 0 ? StoryHeights[index] : double.NaN;
    }

    /// <summary>
    /// Checks if a story is a master story.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>True if master story, false otherwise</returns>
    public bool IsStoryMaster(string storyName)
    {
        int index = GetStoryIndex(storyName);
        return index >= 0 && IsMasterStory[index];
    }

    /// <summary>
    /// Gets all master story names.
    /// </summary>
    /// <returns>Array of master story names</returns>
    public string[] GetMasterStories()
    {
        var masterStories = new List<string>();
        for (int i = 0; i < NumberStories; i++)
        {
            if (IsMasterStory[i])
            {
                masterStories.Add(StoryNames[i]);
            }
        }
        return masterStories.ToArray();
    }

    /// <summary>
    /// Gets stories that are similar to a specific master story.
    /// </summary>
    /// <param name="masterStoryName">Name of the master story</param>
    /// <returns>Array of story names similar to the master</returns>
    public string[] GetSimilarStories(string masterStoryName)
    {
        var similarStories = new List<string>();
        for (int i = 0; i < NumberStories; i++)
        {
            if (SimilarToStory[i] == masterStoryName)
            {
                similarStories.Add(StoryNames[i]);
            }
        }
        return similarStories.ToArray();
    }

    /// <summary>
    /// Gets the total building height (top elevation - base elevation).
    /// </summary>
    /// <returns>Total building height</returns>
    public double GetTotalHeight()
    {
        if (StoryElevations == null || NumberStories == 0)
            return 0;

        return StoryElevations[NumberStories - 1] - BaseElevation + StoryHeights[NumberStories - 1];
    }

    /// <summary>
    /// Creates a copy of the story data.
    /// </summary>
    /// <returns>Deep copy of the story data</returns>
    public StoryData Clone()
    {
        return new StoryData
        {
            BaseElevation = this.BaseElevation,
            NumberStories = this.NumberStories,
            StoryNames = (string[])this.StoryNames?.Clone(),
            StoryElevations = (double[])this.StoryElevations?.Clone(),
            StoryHeights = (double[])this.StoryHeights?.Clone(),
            IsMasterStory = (bool[])this.IsMasterStory?.Clone(),
            SimilarToStory = (string[])this.SimilarToStory?.Clone(),
            SpliceAbove = (bool[])this.SpliceAbove?.Clone(),
            SpliceHeight = (double[])this.SpliceHeight?.Clone(),
            Color = (int[])this.Color?.Clone()
        };
    }

    /// <summary>
    /// Creates story data with default values for optional parameters.
    /// </summary>
    /// <param name="baseElevation">Base elevation</param>
    /// <param name="storyNames">Array of story names (bottom to top)</param>
    /// <param name="storyHeights">Array of story heights</param>
    /// <returns>Initialized StoryData object</returns>
    public static StoryData CreateDefault(double baseElevation, string[] storyNames, double[] storyHeights)
    {
        if (storyNames == null || storyHeights == null)
            throw new ArgumentNullException("Story names and heights cannot be null");

        if (storyNames.Length != storyHeights.Length)
            throw new ArgumentException("Story names and heights must have same length");

        int count = storyNames.Length;

        var data = new StoryData
        {
            BaseElevation = baseElevation,
            NumberStories = count,
            StoryNames = storyNames,
            StoryHeights = storyHeights,
            IsMasterStory = new bool[count],
            SimilarToStory = new string[count],
            SpliceAbove = new bool[count],
            SpliceHeight = new double[count],
            Color = Enumerable.Repeat(-1, count).ToArray()
        };

        // Initialize SimilarToStory with empty strings
        for (int i = 0; i < count; i++)
        {
            data.SimilarToStory[i] = string.Empty;
        }

        // Calculate elevations
        data.CalculateElevations();

        return data;
    }

    /// <summary>
    /// Creates a typical building with uniform story heights.
    /// </summary>
    /// <param name="baseElevation">Base elevation of the building</param>
    /// <param name="numberOfStories">Number of stories</param>
    /// <param name="typicalHeight">Typical story height</param>
    /// <param name="firstStoryHeight">Height of first story (0 to use typical height)</param>
    /// <param name="storyPrefix">Prefix for story names (default: "STORY")</param>
    /// <returns>StoryData for typical building</returns>
    public static StoryData CreateTypical(
        double baseElevation,
        int numberOfStories,
        double typicalHeight,
        double firstStoryHeight = 0,
        string storyPrefix = "STORY")
    {
        if (numberOfStories <= 0)
            throw new ArgumentException("Number of stories must be positive");

        if (typicalHeight <= 0)
            throw new ArgumentException("Story height must be positive");

        if (firstStoryHeight < 0)
            throw new ArgumentException("First story height cannot be negative");

        if (firstStoryHeight == 0)
            firstStoryHeight = typicalHeight;

        var storyNames = new string[numberOfStories];
        var storyHeights = new double[numberOfStories];

        for (int i = 0; i < numberOfStories; i++)
        {
            storyNames[i] = $"{storyPrefix}{i + 1}";
            storyHeights[i] = (i == 0) ? firstStoryHeight : typicalHeight;
        }

        return CreateDefault(baseElevation, storyNames, storyHeights);
    }

    /// <summary>
    /// Creates building with basement levels.
    /// </summary>
    /// <param name="basementLevels">Number of basement levels</param>
    /// <param name="aboveGroundLevels">Number of above-ground levels</param>
    /// <param name="basementHeight">Height of basement levels</param>
    /// <param name="typicalHeight">Height of typical floors</param>
    /// <param name="groundHeight">Height of ground floor (0 to use typical height)</param>
    /// <returns>StoryData with basement and above-ground levels</returns>
    public static StoryData CreateWithBasement(
        int basementLevels,
        int aboveGroundLevels,
        double basementHeight,
        double typicalHeight,
        double groundHeight = 0)
    {
        if (groundHeight <= 0)
            groundHeight = typicalHeight;

        int totalStories = basementLevels + aboveGroundLevels;
        var storyNames = new string[totalStories];
        var storyHeights = new double[totalStories];

        // Basement levels (B1, B2, etc.)
        for (int i = 0; i < basementLevels; i++)
        {
            storyNames[i] = $"B{basementLevels - i}";
            storyHeights[i] = basementHeight;
        }

        // Above ground levels
        for (int i = 0; i < aboveGroundLevels; i++)
        {
            int index = basementLevels + i;
            if (i == 0)
            {
                storyNames[index] = "GROUND";
                storyHeights[index] = groundHeight;
            }
            else
            {
                storyNames[index] = $"LEVEL{i}";
                storyHeights[index] = typicalHeight;
            }
        }

        // Calculate base elevation (bottom of lowest basement)
        double baseElevation = -basementLevels * basementHeight;

        return CreateDefault(baseElevation, storyNames, storyHeights);
    }

    /// <summary>
    /// Returns a formatted string representation of the story data.
    /// </summary>
    public override string ToString()
    {
        if (StoryNames == null || NumberStories == 0)
            return "Empty StoryData";

        var sb = new StringBuilder();
        sb.AppendLine($"Stories: {NumberStories}, Base Elevation: {BaseElevation:F2}, Total Height: {GetTotalHeight():F2}");

        for (int i = 0; i < NumberStories; i++)
        {
            string master = IsMasterStory[i] ? " [Master]" : "";
            string similar = !string.IsNullOrEmpty(SimilarToStory[i]) ? $" (→ {SimilarToStory[i]})" : "";
            sb.AppendLine($"  {StoryNames[i]}: Elev={StoryElevations[i]:F2}, H={StoryHeights[i]:F2}{master}{similar}");
        }

        return sb.ToString();
    }

    #endregion

}

using EtabSharp.Elements.Story.Models;

namespace EtabSharp.Interfaces.Elements.Stories;

/// <summary>
/// Provides methods for managing stories in the ETABS model.
/// This interface handles creation, modification, and retrieval of story information
/// including heights, elevations, master stories, and similar-to relationships.
/// </summary>
public interface IStory
{
    #region Story Information

    /// <summary>
    /// Retrieves the names of all defined stories in the model.
    /// Stories are returned from bottom to top.
    /// </summary>
    /// <returns>An array of story names ordered from bottom to top.</returns>
    string[] GetNameList();

    /// <summary>
    /// Retrieves the height of a defined story.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <returns>The height of the story in current length units.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    double GetHeight(string storyName);

    /// <summary>
    /// Retrieves the elevation of a defined story.
    /// The elevation is measured from the global Z = 0.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <returns>The elevation of the story in current length units.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    double GetElevation(string storyName);

    /// <summary>
    /// Retrieves comprehensive story information for the current tower.
    /// This is the newer API that supersedes GetStories.
    /// </summary>
    /// <returns>
    /// A <see cref="StoryData"/> object containing all story information including
    /// names, heights, elevations, master stories, similar-to relationships, and splice data.
    /// </returns>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    StoryData GetStories();

    #endregion

    #region Story Modification

    /// <summary>
    /// Sets the height of a defined story.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <param name="height">The new height for the story. Must be positive.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If height ≤ 0.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SetHeight(string storyName, double height);

    /// <summary>
    /// Sets the elevation of a defined story.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <param name="elevation">The new elevation for the story.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SetElevation(string storyName, double elevation);

    /// <summary>
    /// Sets the stories for the current tower using the newer API.
    /// This function supersedes SetStories.
    /// WARNING: This function can only be used when no objects exist in the model.
    /// </summary>
    /// <param name="storyData">
    /// Complete story data including base elevation, story names, heights,
    /// master story flags, and similar-to relationships.
    /// </param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentNullException">If storyData is null.</exception>
    /// <exception cref="ArgumentException">If story data is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails or objects exist in model.</exception>
    int SetStories(StoryData storyData);

    #endregion

    #region Master Story Management

    /// <summary>
    /// Retrieves whether a defined story is a master story.
    /// Master stories define the typical layout that other stories can follow.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <returns>True if the story is a master story, false otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    bool IsMasterStory(string storyName);

    /// <summary>
    /// Sets whether a defined story is a master story.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <param name="isMasterStory">True to make it a master story, false otherwise.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SetMasterStory(string storyName, bool isMasterStory);

    /// <summary>
    /// Retrieves the similar-to relationship for a story.
    /// If the story is similar to a master story, it will inherit the master's layout.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <returns>
    /// A tuple containing:
    /// - isMasterStory: True if this story is itself a master story
    /// - similarToStory: The name of the master story this story is similar to (empty if master story)
    /// </returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    (bool isMasterStory, string similarToStory) GetSimilarTo(string storyName);

    /// <summary>
    /// Sets the master story that a defined story should be similar to.
    /// The story will inherit the layout from the specified master story.
    /// </summary>
    /// <param name="storyName">The name of the story to modify.</param>
    /// <param name="similarToStory">
    /// The name of the master story to be similar to.
    /// Use empty string to make the story not similar to any master story.
    /// </param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SetSimilarTo(string storyName, string similarToStory);

    #endregion

    #region Story Splice

    /// <summary>
    /// Retrieves the story splice height, if applicable.
    /// Splice height is used for column design where columns change section.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <returns>The splice height in current length units (0 if no splice defined).</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    double GetSplice(string storyName);

    /// <summary>
    /// Sets the splice height of a defined story.
    /// </summary>
    /// <param name="storyName">The name of the story.</param>
    /// <param name="spliceAbove"></param>
    /// <param name="spliceHeight">
    ///     The splice height in current length units.
    ///     Set to 0 to remove splice.
    /// </param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If story name is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If splice height is negative.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SetSplice(string storyName,bool spliceAbove, double spliceHeight);

    #endregion

}
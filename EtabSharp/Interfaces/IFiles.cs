using ETABSv1;

namespace EtabSharp.Interfaces;

/// <summary>
/// Handling file operations (open, save, export, import) and new model creation
/// </summary>
public interface IFiles
{
    /// <summary>
    /// Open an existing ETABS model(.edb) file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    int OpenFile(string filePath);

    /// <summary>
    /// Save the current ETABS model to a specified file path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    int SaveFile(string filePath);

    /// <summary>
    /// Export the current ETABS model to a specified file format (e.g., .e2k, .xml)
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    int ExportFile(string filePath, eFileTypeIO fileType);

    /// <summary>
    /// Import a model from a specified file format (e.g., .e2k, .xml) with options to create a new model or merge with opening model
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileType"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    int ImportFile(string filePath,eFileTypeIO fileType, int type);

    /// <summary>
    /// Add a new blank model to the current ETABS instance
    /// </summary>
    /// <returns></returns>
    int NewBlankModel();

    /// <summary>
    /// Add a new grid-only model to the current ETABS instance with specified parameters
    /// </summary>
    /// <param name="numberStories"></param>
    /// <param name="typicalStoryHeight"></param>
    /// <param name="bottomStoryHeight"></param>
    /// <param name="numberLineX"></param>
    /// <param name="numberLineY"></param>
    /// <param name="spacingX"></param>
    /// <param name="spacingY"></param>
    /// <returns></returns>
    int NewGridOnlyModel(int numberStories, double typicalStoryHeight, double bottomStoryHeight, int numberLineX,
        int numberLineY, double spacingX, double spacingY);

    /// <summary>
    /// Add a new steel deck model to the current ETABS instance with specified parameters
    /// </summary>
    /// <param name="numberStories"></param>
    /// <param name="typicalStoryHeight"></param>
    /// <param name="bottomStoryHeight"></param>
    /// <param name="numberLineX"></param>
    /// <param name="numberLineY"></param>
    /// <param name="spacingX"></param>
    /// <param name="spacingY"></param>
    /// <returns></returns>
    int NewSteelDeckModel(int numberStories, double typicalStoryHeight, double bottomStoryHeight, int numberLineX,
        int numberLineY,
        double spacingX, double spacingY);
}
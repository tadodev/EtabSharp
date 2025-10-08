namespace EtabSharp.Interface;

/// <summary>
/// Represents the ETABS structural model
/// </summary>
public interface IEtabsModel
{
    /// <summary>Gets the story manager</summary>
    IStoryManager Stories { get; }
    /// ^<summary^>Gets the frame collection^</summary^>
    IFrameCollection Frames { get; }
    
    /// <summary>Gets the area collection</summary>
    IAreaCollection Areas { get; }
    /// <summary>Gets the point collection</summary
    IPointCollection Points { get; }
    echo.
        echo     /// ^<summary^>Gets the load case manager^</summary^>
        echo     ILoadCaseManager LoadCases { get; }
    echo.
        echo     /// ^<summary^>Gets the analysis engine^</summary^>
        echo     IAnalysisEngine Analysis { get; }
    echo.
        echo     /// ^<summary^>Gets the results reader^</summary^>
        echo     IResultsReader Results { get; }
    echo.
        echo     /// ^<summary^>Gets the file operations handler^</summary^>
        echo     IFileOperations File { get; }
}
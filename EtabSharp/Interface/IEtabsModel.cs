using EtabSharp.Properties;

namespace EtabSharp.Interface;

/// <summary>
/// Represents the ETABS structural model
/// </summary>
public interface IEtabsModel
{
    /// <summary>Gets the story manager</summary>
    IStoryManager Stories { get; }

    /// <summary>Gets the frame collection</summary>
    IFrameCollection Frames { get; }

    /// <summary>Gets the area collection</summary>
    IAreaCollection Areas { get; }

    /// <summary>Gets the point collection</summary>
    IPointCollection Points { get; }

    /// <summary>Gets the load case manager</summary>
    ILoadCaseManager LoadCases { get; }

    /// <summary>Gets the load pattern manager</summary>
    ILoadPatternManager LoadPatterns { get; }

    /// <summary>Gets the analysis engine</summary>
    IAnalysisEngine Analysis { get; }

    /// <summary>Gets the results reader</summary>
    IResultsReader Results { get; }

    /// <summary>Gets the file operations handler</summary>
    IFileOperations File { get; }

    /// <summary>Gets the property manager</summary>
    IPropertyManager Properties { get; }

    /// <summary>Gets the material manager</summary>
    IMaterialManager Materials { get; }

    /// <summary>Gets the section manager</summary>
    ISectionManager Sections { get; }
}
using EtabSharp.Models;
using ETABSv1;
using Frame = EtabSharp.Elements.Frame;

namespace EtabSharp.Interface;

public interface IStoryManager
{
    Task<StoryData> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Models.Story?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task CreateAsync(string name, double height, CancellationToken cancellationToken = default);
    Task CreateMultipleAsync(IEnumerable<(string name, double height)> stories, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
    Task SetElevationAsync(string name, double elevation, CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}

public interface IFrameCollection
{
    Task<IEnumerable<Frame>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Frame?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<string> AddAsync(string point1, string point2, string section, string? frameName = null, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
    Task SetSectionAsync(string frameName, string sectionName, CancellationToken cancellationToken = default);
}

public interface IAreaCollection
{
    Task<IEnumerable<Area>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Area?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}

public interface IPointCollection
{
    Task<IEnumerable<Point3D>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<string> AddAsync(double x, double y, double z, string? pointName = null, CancellationToken cancellationToken = default);
    Task<Point3D?> GetCoordinatesAsync(string pointName, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}

public interface ILoadCaseManager
{
    Task<IEnumerable<LoadCase>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LoadCase?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task CreateStaticAsync(string name, string loadType, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}

public interface ILoadPatternManager
{
    Task<IEnumerable<LoadPattern>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(string name, string patternType, double selfWeightMultiplier = 0, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}

public interface IAnalysisEngine
{
    Task<bool> RunAsync(CancellationToken cancellationToken = default);
    Task<bool> RunAsync(string loadCase, CancellationToken cancellationToken = default);
    Task DeleteResultsAsync(CancellationToken cancellationToken = default);
}

public interface IResultsReader
{
    Task<FrameForces> GetFrameForcesAsync(string frameName, CancellationToken cancellationToken = default);
    Task<IEnumerable<FrameForces>> GetAllFrameForcesAsync(string loadCase, CancellationToken cancellationToken = default);
}

public interface IFileOperations
{
    Task NewBlankAsync(CancellationToken cancellationToken = default);
    Task NewGridOnlyAsync(int numXGridLines, int numYGridLines, double gridSpacing, CancellationToken cancellationToken = default);
    Task OpenAsync(string filePath, CancellationToken cancellationToken = default);
    Task SaveAsync(string? filePath = null, CancellationToken cancellationToken = default);
    Task<string?> GetFilePathAsync(CancellationToken cancellationToken = default);
}

public interface IPropertyManager
{
    IMaterialManager Materials { get; }
    ISectionManager Sections { get; }
}

public interface IMaterialManager
{
    Task<IEnumerable<Materials.Material>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Materials.Material?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task CreateAsync(Materials.Material material, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);
}

public interface ISectionManager
{
    Task<IEnumerable<FrameSection>> GetAllFrameSectionsAsync(CancellationToken cancellationToken = default);
    Task<FrameSection?> GetFrameSectionAsync(string name, CancellationToken cancellationToken = default);
    Task ImportFrameSectionAsync(string sectionName, string material, string fileName, CancellationToken cancellationToken = default);
    Task SetRectangleAsync(string sectionName, string material, double depth, double width, CancellationToken cancellationToken = default);
    Task SetIShapeAsync(string sectionName, string material, double t3, double t2, double tf, double tw, CancellationToken cancellationToken = default);
}
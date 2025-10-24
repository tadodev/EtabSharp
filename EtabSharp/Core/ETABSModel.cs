using EtabSharp.Elements.AreaObj;
using EtabSharp.Elements.FrameObj;
using EtabSharp.Elements.PointObj;
using EtabSharp.Elements.Selection;
using EtabSharp.Elements.Story;
using EtabSharp.Frames;
using EtabSharp.Groups;
using EtabSharp.Interfaces.Elements.Objects;
using EtabSharp.Interfaces.Elements.Selection;
using EtabSharp.Interfaces.Elements.Stories;
using EtabSharp.Interfaces.Groups;
using EtabSharp.Interfaces.Loads;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Interfaces.System;
using EtabSharp.Loads;
using EtabSharp.Properties.Areas;
using EtabSharp.Properties.Materials;
using EtabSharp.System;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Core;

/// <summary>
/// High-level wrapper for ETABS model operations.
/// Organizes functionality into logical managers following a clear hierarchy:
/// - System: Core model operations (Files, Info, Units)
/// - Properties: Material and section definitions (Materials, Frames, Areas)
/// - Elements: Model objects and their operations (Points, Frames, Areas, Stories,Selection)
/// - Analysis: Analysis settings and execution
/// - Results: Result extraction and post-processing
/// </summary>
public sealed class ETABSModel
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    #region System Managers (Core Operations)

    private readonly Lazy<IFiles> _files;
    private readonly Lazy<ISapModelInfor> _modelInfo;
    private readonly Lazy<IUnitSystem> _unitSystem;

    #endregion

    #region Property Managers (Definitions)

    private readonly Lazy<IPropMaterial> _materials;
    private readonly Lazy<IPropFrame> _frameProperties;
    private readonly Lazy<IPropArea> _areaProperties;
    // TODO: Add when implemented
    // private readonly Lazy<IPropLink> _linkProperties;
    // private readonly Lazy<IPropCable> _cableProperties;

    #endregion

    #region Element Managers (Model Objects)

    private readonly Lazy<ISelection> _selection;
    private readonly Lazy<IStory> _story;
    private readonly Lazy<IPoint> _points;
    private readonly Lazy<IFrame> _frames;
    private readonly Lazy<IArea> _areas;
    private readonly Lazy<IGroup> _groups;

    // TODO: Add when implemented

    #endregion

    #region Load Manager
    private readonly Lazy<ILoadPatterns> _loadPatterns;

    //TODO: Add when implemented
    // private readonly Lazy<ILoadCases> _loadCases;
    // private readonly Lazy<ILoadCombinations> _loadCombinations;

    #endregion

    #region Analysis Managers

    // TODO: Add when implemented
    // private readonly Lazy<IAnalysisSettings> _analysisSettings;


    #endregion

    #region Results Managers

    // TODO: Add when implemented
    // private readonly Lazy<IFrameResults> _frameResults;
    // private readonly Lazy<IAreaResults> _areaResults;
    // private readonly Lazy<IPointResults> _pointResults;

    #endregion

    #region Constructor

    internal ETABSModel(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Initialize System Managers
        _files = new Lazy<IFiles>(() => new Files(_sapModel, _logger));
        _modelInfo = new Lazy<ISapModelInfor>(() => new SapModelInfor(_sapModel, _logger));
        _unitSystem = new Lazy<IUnitSystem>(() => new UnitSystem(_sapModel, _logger));

        // Initialize Property Managers
        _materials = new Lazy<IPropMaterial>(() => new PropMaterial(_sapModel, _logger));
        _frameProperties = new Lazy<IPropFrame>(() => new PropFrame(_sapModel, _logger));
        _areaProperties = new Lazy<IPropArea>(() => new PropArea(_sapModel, _logger));

        // Initialize Element Managers
        _selection = new Lazy<ISelection>(() => new Selection(_sapModel, _logger));
        _story = new Lazy<IStory>(() => new Story(_sapModel, _logger));
        _points = new Lazy<IPoint>(() => new PointObjectManager(_sapModel, _logger));
        _frames = new Lazy<IFrame>(() => new FrameObjectManager(_sapModel, _logger));
        _areas = new Lazy<IArea>(() => new AreaObjectManager(_sapModel, _logger));
        _groups = new Lazy<IGroup>(() => new GroupManager(_sapModel, _logger));

        // Initialize Load Manager
        _loadPatterns = new Lazy<ILoadPatterns>(() => new LoadPatternsManager(_sapModel, _logger));
    }

    #endregion

    #region System Properties (Core Operations)

    /// <summary>
    /// File operations: open, save, import, export, and new model creation.
    /// </summary>
    public IFiles Files => _files.Value;

    /// <summary>
    /// Model information: version, filename, lock status, coordinate systems.
    /// </summary>
    public ISapModelInfor ModelInfo => _modelInfo.Value;

    /// <summary>
    /// Unit system management: get/set current units.
    /// </summary>
    public IUnitSystem Units => _unitSystem.Value;

    #endregion

    #region Property Properties (Definitions)

    /// <summary>
    /// Material property definitions: concrete, steel, rebar materials.
    /// </summary>
    public IPropMaterial Materials => _materials.Value;

    /// <summary>
    /// Frame section properties: beams, columns, braces.
    /// </summary>
    public IPropFrame PropFrame => _frameProperties.Value;

    /// <summary>
    /// Area section properties: slabs, walls, ramps.
    /// </summary>
    public IPropArea PropArea => _areaProperties.Value;

    // TODO: Implement these
    // /// <summary>
    // /// Link properties: isolators, dampers, gap/hook elements.
    // /// </summary>
    // public IPropLink LinkProperties => _linkProperties.Value;

    #endregion

    #region Element Properties (Model Objects)

    /// <summary>
    /// Selection operations: select, deselect, and retrieve selected objects.
    /// </summary>
    public ISelection Select => _selection.Value;

    /// <summary>
    /// Story definitions: levels, heights, similar-to relationships.
    /// </summary>
    public IStory Story => _story.Value;

    /// <summary>
    /// Point objects: joints, supports, connection points.
    /// </summary>
    public IPoint Points => _points.Value;

    /// <summary>
    /// Frame objects: beams, columns, braces.
    /// </summary>
    public IFrame Frames => _frames.Value;

    /// <summary>
    /// Area objects: floors, walls, ramps.
    /// </summary>
    public IArea Areas => _areas.Value;

    /// <summary>
    /// Group definitions: organize and select objects by groups.
    /// </summary>
    public IGroup Groups => _groups.Value;


    #endregion

    #region Load Manager

    /// <summary>
    /// Load pattern definitions: dead, live, wind, seismic loads.
    /// </summary>
    public ILoadPatterns LoadPatterns => _loadPatterns.Value;

    // /// <summary>
    // /// Load case definitions and run settings.
    // /// </summary>
    // public ILoadCases LoadCases => _loadCases.Value;

    // /// <summary>
    // /// Load combination definitions.
    // /// </summary>
    // public ILoadCombinations LoadCombinations => _loadCombinations.Value;

    #endregion

    #region Analysis Properties

    // TODO: Implement these
    // /// <summary>
    // /// Analysis settings and execution control.
    // /// </summary>
    // public IAnalysisSettings Analysis => _analysisSettings.Value;



    #endregion

    #region Results Properties

    // TODO: Implement these
    // /// <summary>
    // /// Frame element results: forces, stresses, displacements.
    // /// </summary>
    // public IFrameResults FrameResults => _frameResults.Value;

    // /// <summary>
    // /// Area element results: forces, stresses.
    // /// </summary>
    // public IAreaResults AreaResults => _areaResults.Value;

    // /// <summary>
    // /// Joint results: reactions, displacements.
    // /// </summary>
    // public IPointResults PointResults => _pointResults.Value;

    #endregion

    #region Advanced Access

    /// <summary>
    /// Gets the underlying SapModel for advanced usage.
    /// Use this only when you need direct API access not provided by the wrapper.
    /// </summary>
    internal cSapModel SapModel => _sapModel;

    #endregion
}
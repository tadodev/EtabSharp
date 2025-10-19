using EtabSharp.Frames;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Interfaces.System;
using EtabSharp.Properties.Materials;
using EtabSharp.System;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Core;

/// <summary>
/// High-level wrapper for ETABS model operations
/// Organizes functionality into logical managers (Materials, Frames, Stories, etc.)
/// </summary>
public sealed class ETABSModel
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    // Lazy initialization for better performance
    private readonly Lazy<IFiles> _files;
    private readonly Lazy<ISapModelInfor> _sapModelInfo;
    private readonly Lazy<IPropMaterial> _materials;
    private readonly Lazy<IUnitSystem> _unitSystem;
    public readonly Lazy<IPropFrame> _propFrame;

    //TODO Add more managers here as you create them
    // private readonly Lazy<IStoryManager> _stories;
    // private readonly Lazy<IFrameManager> _frames;

    internal ETABSModel(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Initialize managers with lazy loading
        _files = new Lazy<IFiles>(() => new Files(_sapModel, _logger));
        _sapModelInfo = new Lazy<ISapModelInfor>(() => new SapModelInfor(_sapModel, _logger));
        _materials = new Lazy<IPropMaterial>(() => new PropMaterial(_sapModel, _logger));
        _unitSystem = new Lazy<IUnitSystem>(() => new UnitSystem(_sapModel, _logger));
        _propFrame = new Lazy<IPropFrame>(() => new PropFrame(_sapModel, _logger));
    }

    /// <summary>
    /// Gets the material properties manager
    /// </summary>
    public IFiles Files => _files.Value;

    public IPropMaterial Materials => _materials.Value;

    // Add more properties as you create managers
    public ISapModelInfor SapModelInfor => _sapModelInfo.Value;
    public IUnitSystem UnitSystem => _unitSystem.Value;
    public IPropFrame PropFrame => _propFrame.Value;

    // public IStoryManager Stories => _stories.Value;
    // public IFrameManager Frames => _frames.Value;

    /// <summary>
    /// Gets the underlying SapModel for advanced usage
    /// </summary>
    internal cSapModel SapModel => _sapModel;
}
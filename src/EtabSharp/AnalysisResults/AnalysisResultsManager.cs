using EtabSharp.AnalysisResultsSetup;
using EtabSharp.Interfaces;
using EtabSharp.Interfaces.AnalysisResults;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.AnalysisResults;

/// <summary>
/// Manages analysis results retrieval from the ETABS model.
/// Implements the IAnalysisResults interface by wrapping cSapModel.Results operations.
/// This is a partial class with implementations split across multiple files by result type.
/// </summary>
public partial class AnalysisResultsManager: IAnalysisResults
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private readonly IAnalysisResultsSetup _setup;

    /// <summary>
    /// Initializes a new instance of the AnalysisResultsManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public AnalysisResultsManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Initialize the setup manager
        _setup = new AnalysisResultsSetupManager(sapModel, logger);

        _logger.LogDebug("AnalysisResultsManager initialized");
    }

    /// <summary>
    /// Gets the analysis results setup interface for configuring output options.
    /// </summary>
    public IAnalysisResultsSetup Setup => _setup;
}
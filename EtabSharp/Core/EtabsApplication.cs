using EtabSharp.Exceptions;
using EtabSharp.Interface;
using ETABSv1;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EtabSharp.Core;

/// <summary>
/// Entry point for ETABS API operations
/// </summary>
public class EtabsApplication: IDisposable
{
    private readonly ILogger<EtabsApplication> _logger;
    private dynamic? _etabsObject;
    private cSapModel _sapModel;
    private bool _disposed;

    public IEtabsModel Model { get; private set; }

    public bool IsConnected { get; private set; }

    public string Version { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the EtabsApplication
    /// </summary>
    /// <param name="createNew">Create new instance or attach to existing</param>
    /// <param name="showWindow">Show ETABS window</param>
    /// <param name="logger">Optional logger</param>
    public EtabsApplication(
        bool createNew = true,
        bool showWindow = false,
        ILogger<EtabsApplication>? logger = null)
    {
        _logger = logger ?? NullLogger<EtabsApplication>.Instance;

        try
        {
            _logger.LogInformation("Initializing ETABS application...");

            // Get ETABS object type
            var progId = Constants.DefaultProgId;
            var etabsType = Type.GetTypeFromProgID(progId)
                ?? throw new EtabsException($"Failed to get ETABS type from ProgID: {progId}. Is ETABS installed?");

            if (createNew)
            {
                // Create new instance
                _logger.LogDebug("Creating new ETABS instance");
                _etabsObject = Activator.CreateInstance(etabsType)
                    ?? throw new EtabsException("Failed to create ETABS instance");
            }
            else
            {
                // Attach to running instance
                _logger.LogDebug("Attaching to running ETABS instance");
                try
                {
                    _etabsObject = Marshal.GetActiveObject(progId)
                        ?? throw new EtabsException("Failed to attach to running ETABS instance");
                }
                catch (COMException ex)
                {
                    throw new EtabsException("No running ETABS instance found. Start ETABS first or use createNew: true", ex);
                }
            }

            // Initialize SapModel
            _sapModel = _etabsObject.SapModel;
            if (_sapModel == null)
            {
                throw new EtabsException("Failed to get SapModel from ETABS object");
            }

            // Set visibility
            if (_etabsObject != null && createNew)
            {
                _etabsObject.ApplicationStart();
                SetVisible(showWindow);
            }

            // Get version
            try
            {
                Version = _sapModel.GetVersion() ?? "Unknown";
            }
            catch
            {
                Version = "Unknown";
            }

            // Initialize model wrapper
            Model = new EtabsModel(_sapModel, _logger);

            IsConnected = true;
            _logger.LogInformation("ETABS application initialized successfully. Version: {Version}", Version);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize ETABS application");
            Cleanup();

            if (ex is EtabsException)
                throw;

            throw new EtabsException("Failed to initialize ETABS application. Ensure ETABS is installed.", ex);
        }
    }

    /// <inheritdoc />
    public void SetVisible(bool visible)
    {
        if (_etabsObject == null)
        {
            _logger.LogWarning("Cannot set visibility - ETABS object is null");
            return;
        }

        try
        {
            _etabsObject.Visible = visible;
            _logger.LogDebug("Set ETABS visibility to {Visible}", visible);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to set ETABS visibility");
        }
    }

    /// <inheritdoc />
    public void Exit(bool saveModel = false)
    {
        if (!IsConnected)
        {
            _logger.LogDebug("Exit called but not connected");
            return;
        }

        if (_sapModel != null && saveModel)
        {
            try
            {
                _logger.LogInformation("Saving model before exit");
                _sapModel.File.Save();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to save model before exit");
            }
        }

        try
        {
            _etabsObject?.ApplicationExit(false);
            _logger.LogInformation("ETABS application exited");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during ETABS exit");
        }
        finally
        {
            IsConnected = false;
        }
    }

    /// <summary>
    /// Disposes the ETABS application
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _logger.LogDebug("Disposing ETABS application");
        Cleanup();
        _disposed = true;
    }

    private void Cleanup()
    {
        try
        {
            if (_sapModel != null)
            {
                Marshal.ReleaseComObject(_sapModel);
                _sapModel = null;
            }

            if (_etabsObject != null)
            {
                Marshal.ReleaseComObject(_etabsObject);
                _etabsObject = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during cleanup");
        }
        finally
        {
            IsConnected = false;
        }
    }
}
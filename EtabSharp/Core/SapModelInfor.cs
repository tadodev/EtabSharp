using EtabSharp.Core.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Core;

public class SapModelInfor : ISapModelInfor
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal SapModelInfor(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    ///<inheritdoc cref="ISapModelInfor.GetModelFilename"/>
    public string GetModelFilename(bool includePath = false)
    {
        try
        {
            var filename = _sapModel.GetModelFilename(includePath);
            _logger.LogDebug("Model filename: {Filename} (includePath={IncludePath})",
                filename, includePath);
            return filename ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting model filename");
            throw new EtabsException("Failed to get model filename", ex);
        }
    }

    ///<inheritdoc cref="ISapModelInfor.GetModelFilepath"/>
    public string GetModelFilepath()
    {
        try
        {
            var filepath = _sapModel.GetModelFilepath();
            _logger.LogDebug("Model filepath: {Filepath}", filepath);
            return filepath ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting model filepath");
            throw new EtabsException("Failed to get model filepath", ex);
        }
    }

    ///<inheritdoc/>
    public bool IsLocked()
    {
        try
        {
            var isLocked = _sapModel.GetModelIsLocked();
            _logger.LogDebug("Model is locked: {IsLocked}", isLocked);
            return isLocked;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if model is locked");
            throw new EtabsException("Failed to check model lock status", ex);
        }
    }

    ///<inheritdoc/>
    public void SetLocked(bool isLocked)
    {
        try
        {
            _sapModel.SetModelIsLocked(isLocked);
            _logger.LogInformation("Model lock set to: {IsLocked}", isLocked);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting model lock status to {IsLocked}", isLocked);
            throw new EtabsException($"Failed to set model lock to {isLocked}", ex);
        }
    }

    ///<inheritdoc/>
    public string GetVersion()
    {
        string version = String.Empty;
        double myVersion = 0;
        try
        {
            var ret = _sapModel.GetVersion(ref version, ref myVersion);
            _logger.LogDebug("ETABS version: {Version}", version);
            return version ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting ETABS version");
            throw new EtabsException("Failed to get ETABS version", ex);
        }
    }

    ///<inheritdoc/>
    public ProgramInfo GetProgramInfo()
    {
        try
        {
            string programName = string.Empty;
            string programVersion = string.Empty;
            string programLevel = string.Empty;

            int ret = _sapModel.GetProgramInfo(ref programName, ref programVersion, ref programLevel);
            if (ret != 0)
            {
                _logger.LogError("Failed to get program info. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetProgramInfo", "Failed to get program information");
            }

            var info = new ProgramInfo
            {
                ProgramName = programName,
                ProgramVersion = programVersion,
                ProgramLevel = programLevel
            };

            _logger.LogDebug("Program info: {ProgramInfo}", info);
            return info;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting program info");
            throw new EtabsException("Unexpected error getting program information", ex);
        }
    }

    ///<inheritdoc/>
    public int InitializeNewModel(eUnits units)
    {
        try
        {
            int ret = _sapModel.InitializeNewModel(units);
            if (ret != 0)
            {
                _logger.LogError("Failed to initialize new model. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "InitializeNewModel", "Failed to initialize new model");
            }

            _logger.LogInformation("Successfully initialized new model");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error initializing new model");
            throw new EtabsException("Unexpected error initializing new model", ex);
        }
    }

    ///<inheritdoc/>
    public string GetPresentCoordSystem()
    {
        try
        {
            var coordSys = _sapModel.GetPresentCoordSystem();

            if (string.IsNullOrEmpty(coordSys))
            {
                _logger.LogError("Failed to get coordinate system.");
                throw new EtabsException("Failed to get present coordinate system");
            }

            _logger.LogDebug("Present coordinate system: {CoordSys}", coordSys);
            return coordSys ?? "GLOBAL";
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting coordinate system");
            throw new EtabsException("Unexpected error getting coordinate system", ex);
        }
    }
}
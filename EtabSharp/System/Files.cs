using EtabSharp.Exceptions;
using EtabSharp.Interfaces.System;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.System;

/// <summary>
/// Handles ETABS file operations (open, save, export, import, new models)
/// </summary>
public sealed class Files : IFiles
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal Files(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public int OpenFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}", filePath);

        try
        {
            _logger.LogInformation("Opening ETABS file: {FilePath}", filePath);

            int ret = _sapModel.File.OpenFile(filePath);

            if (ret != 0)
            {
                _logger.LogError("Failed to open file {FilePath}. Return code: {ReturnCode}", filePath, ret);
                throw new EtabsException(ret, "OpenFile", $"Failed to open file: {filePath}");
            }

            _logger.LogInformation("Successfully opened file: {FilePath}", filePath);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException &&
                                   ex is not FileNotFoundException)
        {
            _logger.LogError(ex, "Unexpected error opening file: {FilePath}", filePath);
            throw new EtabsException($"Unexpected error opening file: {filePath}", ex);
        }
    }

    /// <inheritdoc/>
    public int SaveFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        try
        {
            _logger.LogInformation("Saving ETABS file to: {FilePath}", filePath);

            int ret = _sapModel.File.Save(filePath);

            if (ret != 0)
            {
                _logger.LogError("Failed to save file to {FilePath}. Return code: {ReturnCode}", filePath, ret);
                throw new EtabsException(ret, "SaveFile", $"Failed to save file to: {filePath}");
            }

            _logger.LogInformation("Successfully saved file to: {FilePath}", filePath);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error saving file to: {FilePath}", filePath);
            throw new EtabsException($"Unexpected error saving file to: {filePath}", ex);
        }
    }

    /// <inheritdoc/>
    public int ExportFile(string filePath, eFileTypeIO fileType)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        try
        {
            _logger.LogInformation("Exporting ETABS model to: {FilePath} (Type: {FileType})", filePath, fileType);

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                _logger.LogDebug("Created directory: {Directory}", directory);
            }

            int ret = _sapModel.File.ExportFile(filePath, fileType);

            if (ret != 0)
            {
                _logger.LogError("Failed to export file to {FilePath}. Return code: {ReturnCode}", filePath, ret);
                throw new EtabsException(ret, "ExportFile", $"Failed to export file to: {filePath}");
            }

            _logger.LogInformation("Successfully exported file to: {FilePath}", filePath);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error exporting file to: {FilePath}", filePath);
            throw new EtabsException($"Unexpected error exporting file to: {filePath}", ex);
        }
    }

    /// <inheritdoc/>
    public int ImportFile(string filePath, eFileTypeIO fileType, int type)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}", filePath);

        try
        {
            _logger.LogInformation("Importing file: {FilePath} (Type: {FileType}, ImportType: {Type})",
                filePath, fileType, type);

            int ret = _sapModel.File.ImportFile(filePath, fileType, type);

            if (ret != 0)
            {
                _logger.LogError("Failed to import file {FilePath}. Return code: {ReturnCode}", filePath, ret);
                throw new EtabsException(ret, "ImportFile", $"Failed to import file: {filePath}");
            }

            _logger.LogInformation("Successfully imported file: {FilePath}", filePath);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException &&
                                   ex is not FileNotFoundException)
        {
            _logger.LogError(ex, "Unexpected error importing file: {FilePath}", filePath);
            throw new EtabsException($"Unexpected error importing file: {filePath}", ex);
        }
    }

    /// <inheritdoc/>
    public int NewBlankModel()
    {
        try
        {
            _logger.LogInformation("Creating new blank model");

            int ret = _sapModel.File.NewBlank();

            if (ret != 0)
            {
                _logger.LogError("Failed to create new blank model. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "NewBlankModel", "Failed to create new blank model");
            }

            _logger.LogInformation("Successfully created new blank model");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error creating new blank model");
            throw new EtabsException("Unexpected error creating new blank model", ex);
        }
    }

    /// <inheritdoc/>
    public int NewGridOnlyModel(int numberStories, double typicalStoryHeight, double bottomStoryHeight, int numberLineX,
        int numberLineY,
        double spacingX, double spacingY)
    {
        if (numberStories <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberStories), "Number of stories must be positive.");

        if (typicalStoryHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(typicalStoryHeight), "Story height must be positive.");
        if (bottomStoryHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(bottomStoryHeight), "Story height must be positive.");

        if (numberLineX <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberLineX), "Number of X grid lines must be positive.");

        if (numberLineY <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberLineY), "Number of Y grid lines must be positive.");

        if (spacingX <= 0)
            throw new ArgumentOutOfRangeException(nameof(spacingX), "X spacing must be positive.");

        if (spacingY <= 0)
            throw new ArgumentOutOfRangeException(nameof(spacingY), "Y spacing must be positive.");

        try
        {
            _logger.LogInformation(
                "Creating new grid-only model: Stories={Stories}, Height={Height}, GridX={GridX}, GridY={GridY}, SpacingX={SpacingX}, SpacingY={SpacingY}",
                numberStories, typicalStoryHeight, numberLineX, numberLineY, spacingX, spacingY);

            int ret = _sapModel.File.NewGridOnly(numberStories, typicalStoryHeight, bottomStoryHeight, numberLineX,
                numberLineY,
                spacingX, spacingY);

            if (ret != 0)
            {
                _logger.LogError("Failed to create new grid-only model. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "NewGridOnlyModel", "Failed to create new grid-only model");
            }

            _logger.LogInformation("Successfully created new grid-only model");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating new grid-only model");
            throw new EtabsException("Unexpected error creating new grid-only model", ex);
        }
    }

    /// <inheritdoc/>
    public int NewSteelDeckModel(int numberStories, double typicalStoryHeight, double bottomStoryHeight,
        int numberLineX, int numberLineY,
        double spacingX, double spacingY)
    {
        if (numberStories <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberStories), "Number of stories must be positive.");

        if (typicalStoryHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(typicalStoryHeight), "Story height must be positive.");
        if (bottomStoryHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(bottomStoryHeight), "Story height must be positive.");

        if (numberLineX <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberLineX), "Number of X grid lines must be positive.");

        if (numberLineY <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberLineY), "Number of Y grid lines must be positive.");

        if (spacingX <= 0)
            throw new ArgumentOutOfRangeException(nameof(spacingX), "X spacing must be positive.");

        if (spacingY <= 0)
            throw new ArgumentOutOfRangeException(nameof(spacingY), "Y spacing must be positive.");

        try
        {
            _logger.LogInformation(
                "Creating new steel deck model: Stories={Stories}, Height={Height}, GridX={GridX}, GridY={GridY}, SpacingX={SpacingX}, SpacingY={SpacingY}",
                numberStories, typicalStoryHeight, numberLineX, numberLineY, spacingX, spacingY);

            int ret = _sapModel.File.NewSteelDeck(numberStories, typicalStoryHeight, bottomStoryHeight, numberLineX,
                numberLineY,
                spacingX, spacingY);

            if (ret != 0)
            {
                _logger.LogError("Failed to create new steel deck model. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "NewSteelDeckModel", "Failed to create new steel deck model");
            }

            _logger.LogInformation("Successfully created new steel deck model");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error creating new steel deck model");
            throw new EtabsException("Unexpected error creating new steel deck model", ex);
        }
    }
}
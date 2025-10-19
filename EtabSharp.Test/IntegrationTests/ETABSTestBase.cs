using EtabSharp.Core;
using ETABSv1;

namespace EtabSharp.Test.IntegrationTests;

/// <summary>
/// Base class for ETABS integration tests.
/// Provides common setup, teardown, and utilities for testing with ETABS.
/// </summary>
public abstract class ETABSTestBase
{
    protected readonly ETABSApplication Etabs;
    protected readonly ITestOutputHelper Output;
    private bool _disposed = false;

    protected ETABSTestBase(ITestOutputHelper output)
    {
        Output = output;

        // Check if ETABS is running
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            throw new InvalidOperationException(
                "ETABS v22+ is not running. Please start ETABS before running integration tests.");
        }

        // Connect to ETABS
        Etabs = ETABSWrapper.Connect();

        if (Etabs == null)
        {
            throw new InvalidOperationException("Failed to connect to ETABS instance.");
        }

        Output.WriteLine($"Connected to ETABS v{Etabs.FullVersion}");

        // Initialize a new blank model for testing
        InitializeTestModel();
    }

    /// <summary>
    /// Initialize a clean test model
    /// </summary>
    protected virtual void InitializeTestModel()
    {
        try
        {
            // Initialize new model with US units
            Etabs.Model.ModelInfo.InitializeNewModel(eUnits.lb_in_F);

            // Create a simple grid for testing
            Etabs.Model.Files.NewGridOnlyModel(
                numberStories: 5,
                typicalStoryHeight: 12, // 12 feet
                bottomStoryHeight: 12,
                numberLineX: 4,
                numberLineY: 4,
                spacingX: 20, // 20 feet
                spacingY: 20
            );

            Output.WriteLine("Test model initialized successfully");
        }
        catch (Exception ex)
        {
            Output.WriteLine($"Error initializing test model: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Log test information
    /// </summary>
    protected void Log(string message)
    {
        Output.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    /// <summary>
    /// Log test section header
    /// </summary>
    protected void LogSection(string sectionName)
    {
        Output.WriteLine("");
        Output.WriteLine($"========== {sectionName} ==========");
    }

    /// <summary>
    /// Assert that ETABS operation succeeded
    /// </summary>
    protected void AssertSuccess(int returnCode, string operation)
    {
        if (returnCode != 0)
        {
            throw new InvalidOperationException(
                $"{operation} failed with return code: {returnCode}");
        }
        Log($"✓ {operation} succeeded");
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            // Don't close ETABS - let it stay open for inspection
            // Etabs?.Close(savePrompt: false);
            _disposed = true;
        }
    }
}
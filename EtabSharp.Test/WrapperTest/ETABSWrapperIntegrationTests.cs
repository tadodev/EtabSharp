using EtabSharp.Core;
using ETABSv1;

namespace EtabSharp.Test.WrapperTest;

/// <summary>
/// Integration tests that require ETABS to be running
/// </summary>
public class ETABSWrapperIntegrationTests
{
    [Fact(Skip = "Requires ETABS v22+ running")]
    public void FullWorkflow_ConnectAndUseModel()
    {
        // Arrange & Act
        var etabs = ETABSWrapper.Connect();

        // Assert
        Assert.NotNull(etabs);

        // Test API access
        Assert.NotNull(etabs.API);
        Assert.NotNull(etabs.SapModel);

        // Test getting units (safe operation)
        eUnits ret = 0;
        etabs.ExecuteSafely(() =>
        {
            ret = etabs.SapModel.GetPresentUnits();
        }, "GetPresentUnits");

        Assert.True(ret >= 0);
    }

    [Fact(Skip = "Requires ETABS v22+ running")]
    public void CanGetModelFilePath()
    {
        // Arrange
        var etabs = ETABSWrapper.Connect();
        Assert.NotNull(etabs);

        // Act
        string filePath = string.Empty;
        etabs.ExecuteSafely(() =>
        {
            filePath = etabs.SapModel.GetModelFilename();
        }, "GetModelFilename");

        // Assert
        Assert.NotNull(filePath);
    }

    [Fact(Skip = "Creates and closes ETABS instance")]
    public void CreateNew_StartAndClose_ShouldWork()
    {
        // Act - Create new instance
        var etabs = ETABSWrapper.CreateNew(startApplication: true);

        // Assert
        Assert.NotNull(etabs);

        // Initialize new blank model
        etabs.ExecuteSafely(() =>
        {
            etabs.SapModel.InitializeNewModel();
            etabs.SapModel.File.NewBlank();
        }, "InitializeNewModel");

        // Cleanup
        etabs.Close(savePrompt: false);
    }
}

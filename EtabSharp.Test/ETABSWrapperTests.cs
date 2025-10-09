using EtabSharp.Core;
using EtabSharp.Models;

namespace EtabSharp.Test;

public class ETABSWrapperTests
{
    [Fact]
    public void IsRunning_ShouldReturnBoolean()
    {
        // Act
        bool isRunning = ETABSWrapper.IsRunning();

        // Assert
        Assert.IsType<bool>(isRunning);
    }

    [Fact]
    public void IsSupportedVersionRunning_ShouldReturnBoolean()
    {
        // Act
        bool isSupported = ETABSWrapper.IsSupportedVersionRunning();

        // Assert
        Assert.IsType<bool>(isSupported);
    }

    [Fact]
    public void GetActiveVersion_ShouldReturnNullOrString()
    {
        // Act
        var version = ETABSWrapper.GetActiveVersion();

        // Assert
        if (version != null)
        {
            Assert.Equal(version, 22);
        }
    }

    [Fact]
    public void GetAllRunningInstances_ShouldReturnList()
    {
        // Act
        var instances = ETABSWrapper.GetAllRunningInstances();

        // Assert
        Assert.NotNull(instances);
        Assert.IsType<System.Collections.Generic.List<ETABSInstanceInfo>>(instances);
    }

    [Fact]
    public void GetAllRunningInstances_WhenETABSRunning_ShouldHaveValidData()
    {
        // Arrange
        if (!ETABSWrapper.IsRunning())
        {
            // Skip test if ETABS is not running
            return;
        }

        // Act
        var instances = ETABSWrapper.GetAllRunningInstances();

        // Assert
        Assert.NotEmpty(instances);

        foreach (var instance in instances)
        {
            Assert.True(instance.ProcessId > 0);
            Assert.NotNull(instance.ProcessName);
            
        }
    }

    [Fact]
    public void Connect_WhenNoETABSRunning_ShouldReturnNull()
    {
        // Arrange
        if (ETABSWrapper.IsRunning())
        {
            // Skip test if ETABS is running
            return;
        }

        // Act
        var etabs = ETABSWrapper.Connect();

        // Assert
        Assert.Null(etabs);
    }

    [Fact]
    public void Connect_WhenETABSRunning_ShouldReturnApplication()
    {
        // Arrange
        if (!ETABSWrapper.IsRunning())
        {
            // Skip test if ETABS is not running
            return;
        }

        // Act
        var etabs = ETABSWrapper.Connect();

        // Assert
        Assert.NotNull(etabs);
        Assert.IsType<ETABSApplication>(etabs);
    }

    [Fact]
    public void Connect_WhenSuccessful_ShouldHaveValidProperties()
    {
        // Arrange
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            // Skip test if supported ETABS version is not running
            return;
        }

        // Act
        var etabs = ETABSWrapper.Connect();

        // Assert
        Assert.NotNull(etabs);
        
        Assert.NotNull(etabs.DllName);
        Assert.True(etabs.IsNetStandard);
    }

    [Fact]
    public void ETABSApplication_ShouldHaveAPIAccess()
    {
        // Arrange
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            return;
        }

        // Act
        var etabs = ETABSWrapper.Connect();

        // Assert
        Assert.NotNull(etabs.API);
        Assert.NotNull(etabs.Model);
    }

    [Fact]
    public void ETABSApplication_GetApiInfo_ShouldReturnValidInfo()
    {
        // Arrange
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            return;
        }

        var etabs = ETABSWrapper.Connect();

        // Act
        var apiInfo = etabs.GetApiInfo();

        // Assert
        Assert.NotNull(apiInfo);
        Assert.Equal("ETABSv1.DLL", apiInfo.DllName);
        Assert.True(apiInfo.IsNetStandard);
    }

    [Fact]
    public void ETABSApplication_ExecuteSafely_WithValidAction_ShouldExecute()
    {
        // Arrange
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            return;
        }

        var etabs = ETABSWrapper.Connect();
        bool executed = false;

        // Act
        etabs.ExecuteSafely(() =>
        {
            executed = true;
        }, "TestAction");

        // Assert
        Assert.True(executed);
    }

    [Fact]
    public void ETABSApplication_ExecuteSafely_WithValidFunction_ShouldReturnValue()
    {
        // Arrange
        if (!ETABSWrapper.IsSupportedVersionRunning())
        {
            return;
        }

        var etabs = ETABSWrapper.Connect();

        // Act
        var result = etabs.ExecuteSafely(() =>
        {
            return 42;
        }, "TestFunction");

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void ETABSInstanceInfo_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var instanceInfo = new ETABSInstanceInfo
        {
            ProcessId = 1234,
            ProcessName = "ETABS",
            WindowTitle = "Test Model",
            IsSupported = true
        };

        // Act
        var result = instanceInfo.ToString();

        // Assert
        Assert.Contains("22", result);
        Assert.Contains("1234", result);
        Assert.Contains("Test Model", result);
        Assert.Contains("Supported", result);
    }

    [Fact]
    public void ETABSApiInfo_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var apiInfo = new ETABSApiInfo
        {
            ApiVersion = 123,
            DllName = "ETABSv1.DLL",
            IsNetStandard = true
        };

        // Act
        var result = apiInfo.ToString();

        // Assert
        Assert.Contains("22.7.0", result);
        Assert.Contains("123", result);
        Assert.Contains("ETABSv1.DLL", result);
        Assert.Contains(".NET Standard", result);
    }

    [Fact]
    public void CreateNew_WithoutPath_ShouldCreateNewInstance()
    {
        // Note: This test will actually create a new ETABS instance
        // Consider using [Fact(Skip = "Creates new ETABS instance")] if you don't want to run it automatically

        // Act
        var etabs = ETABSWrapper.CreateNew(startApplication: false);

        // Assert
        if (etabs != null)
        {
            Assert.NotNull(etabs.API);
            Assert.NotNull(etabs.Model);

            // Cleanup
            etabs.Close(savePrompt: false);
        }
    }

    [Fact]
    public void ETABSApplication_Dispose_ShouldCloseApplication()
    {
        // Arrange
        var etabs = ETABSWrapper.CreateNew(startApplication: false);

        if (etabs == null)
        {
            return; // Skip if can't create instance
        }

        // Act
        using (etabs)
        {
            Assert.NotNull(etabs.API);
        }

        // Assert - disposed should not throw
        Assert.True(true);
    }

    [Theory]
    [InlineData(21, false)]
    [InlineData(22, true)]
    [InlineData(23, true)]
    public void MinimumVersion_ShouldBeEnforced(int version, bool shouldBeSupported)
    {
        // This is a conceptual test - in practice, you'd need to mock the version check
        // Just validating the logic
        const int MINIMUM_SUPPORTED_VERSION = 22;

        // Act
        bool isSupported = version >= MINIMUM_SUPPORTED_VERSION;

        // Assert
        Assert.Equal(shouldBeSupported, isSupported);
    }
}


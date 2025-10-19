using EtabSharp.System.Models;

namespace EtabSharp.Test.IntegrationTests;

/// <summary>
/// Integration tests for System managers (Files, ModelInfo, Units)
/// </summary>
public class SystemTests: ETABSTestBase
{
    public SystemTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void Files_Operations_ShouldWork()
    {
        LogSection("Files Operations Test");

        // Test get model filename
        var filename = Etabs.Model.ModelInfo.GetModelFilename(includePath: false);
        Log($"Current model filename: {filename}");
        Assert.NotNull(filename);

        // Test get model filepath
        var filepath = Etabs.Model.ModelInfo.GetModelFilepath();
        Log($"Current model filepath: {filepath}");
        Assert.NotNull(filepath);
    }

    [Fact]
    public void ModelInfo_GetVersion_ShouldReturnValidVersion()
    {
        LogSection("ModelInfo Version Test");

        var version = Etabs.Model.ModelInfo.GetVersion();
        Log($"ETABS Version: {version}");

        Assert.NotNull(version);
        Assert.NotEmpty(version);
        Assert.Matches(@"^\d+\.\d+\.\d+", version);
    }

    [Fact]
    public void ModelInfo_GetProgramInfo_ShouldReturnValidInfo()
    {
        LogSection("ModelInfo Program Info Test");

        var programInfo = Etabs.Model.ModelInfo.GetProgramInfo();

        Log($"Program Name: {programInfo.ProgramName}");
        Log($"Program Version: {programInfo.ProgramVersion}");
        Log($"Program Level: {programInfo.ProgramLevel}");

        Assert.NotNull(programInfo);
        Assert.NotNull(programInfo.ProgramName);
        Assert.NotNull(programInfo.ProgramVersion);
    }

    [Fact]
    public void ModelInfo_LockStatus_ShouldWork()
    {
        LogSection("ModelInfo Lock Status Test");

        // Check initial lock status
        var isLocked = Etabs.Model.ModelInfo.IsLocked();
        Log($"Initial lock status: {isLocked}");

        // Try to unlock
        Etabs.Model.ModelInfo.SetLocked(false);
        var afterUnlock = Etabs.Model.ModelInfo.IsLocked();
        Log($"After unlock: {afterUnlock}");
        Assert.False(afterUnlock);

        // Try to lock
        Etabs.Model.ModelInfo.SetLocked(true);
        var afterLock = Etabs.Model.ModelInfo.IsLocked();
        Log($"After lock: {afterLock}");
        Assert.True(afterLock);

        // Restore original state
        Etabs.Model.ModelInfo.SetLocked(isLocked);
    }

    [Fact]
    public void Units_GetAndSet_ShouldWork()
    {
        LogSection("Units Get/Set Test");

        // Get current units
        var currentUnits = Etabs.Model.Units.GetPresentUnits();
        Log($"Current units: {currentUnits}");
        Assert.NotNull(currentUnits);

        // Test setting to US units
        var usUnits = Units.US_Kip_In;
        var ret = Etabs.Model.Units.SetPresentUnits(usUnits);
        AssertSuccess(ret, "Set US units");

        var afterSet = Etabs.Model.Units.GetPresentUnits();
        Log($"After setting US units: {afterSet}");
        Assert.Equal(usUnits.Force, afterSet.Force);
        Assert.Equal(usUnits.Length, afterSet.Length);

        // Test setting to Metric units
        var metricUnits = Units.Metric_kN_m;
        ret = Etabs.Model.Units.SetPresentUnits(metricUnits);
        AssertSuccess(ret, "Set Metric units");

        var afterMetric = Etabs.Model.Units.GetPresentUnits();
        Log($"After setting Metric units: {afterMetric}");
        Assert.Equal(metricUnits.Force, afterMetric.Force);
        Assert.Equal(metricUnits.Length, afterMetric.Length);

        // Restore original units
        Etabs.Model.Units.SetPresentUnits(currentUnits);
    }

    [Fact]
    public void ModelInfo_GetCoordSystem_ShouldReturnGlobal()
    {
        LogSection("ModelInfo Coordinate System Test");

        var coordSys = Etabs.Model.ModelInfo.GetPresentCoordSystem();
        Log($"Current coordinate system: {coordSys}");

        Assert.NotNull(coordSys);
        Assert.Equal("Global", coordSys);
    }
}
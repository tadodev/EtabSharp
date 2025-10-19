using EtabSharp.Properties.Frames.Models;
using ETABSv1;

namespace EtabSharp.Test.IntegrationTests;

/// <summary>
/// Integration tests for Frame property management
/// </summary>
public class FramePropertyTests : ETABSTestBase
{
    public FramePropertyTests(ITestOutputHelper output) : base(output)
    {
        // Create materials for frame sections
        SetupMaterials();
    }

    private void SetupMaterials()
    {
        LogSection("Setup Materials for Frame Tests");

        Etabs.Model.Materials.AddConcreteMaterial("CF4000", 4000, 3600000);
        Log("✓ Created CF4000 concrete");
    }

    [Fact]
    public void FrameProperties_AddRectangular_ShouldCreateSection()
    {
        LogSection("Rectangular Frame Section Test");

        var section = Etabs.Model.PropFrame.AddRectangularSection(
            name: "C24x24",
            materialName: "CF4000",
            depth: 24,
            width: 24
        );

        Log($"✓ Created rectangular section: {section.Name}");
        Log($"  Material: {section.Material}");
        Log($"  Depth: {section.Depth} in");
        Log($"  Width: {section.Width} in");

        Assert.NotNull(section);
        Assert.Equal("C24x24", section.Name);
        Assert.Equal("CF4000", section.Material);
        Assert.Equal(24, section.Depth);
        Assert.Equal(24, section.Width);
    }

    [Fact]
    public void FrameProperties_AddCircular_ShouldCreateSection()
    {
        LogSection("Circular Frame Section Test");

        var section = Etabs.Model.PropFrame.AddCircularSection(
            name: "C24",
            materialName: "CF4000",
            diameter: 24
        );

        Log($"✓ Created circular section: {section.Name}");
        Log($"  Material: {section.Material}");
        Log($"  Diameter: {section.Diameter} in");

        Assert.NotNull(section);
        Assert.Equal("C24", section.Name);
        Assert.Equal("CF4000", section.Material);
        Assert.Equal(24, section.Diameter);
    }

    [Fact]
    public void FrameProperties_GetNameList_ShouldReturnSections()
    {
        LogSection("Frame Section Name List Test");

        // Add several sections
        Etabs.Model.PropFrame.AddRectangularSection("C12x12", "CF4000", 12, 12);
        Etabs.Model.PropFrame.AddRectangularSection("C18x18", "CF4000", 18, 18);
        Etabs.Model.PropFrame.AddRectangularSection("C24x24", "CF4000", 24, 24);

        var sections = Etabs.Model.PropFrame.GetNameList();
        Log($"Found {sections.Length} frame sections:");
        foreach (var sec in sections)
        {
            Log($"  - {sec}");
        }

        Assert.NotNull(sections);
        Assert.True(sections.Length >= 3);
    }

    [Fact]
    public void FrameProperties_GetRectangularSection_ShouldReturnCorrectData()
    {
        LogSection("Get Rectangular Section Test");

        // Create section
        var created = Etabs.Model.PropFrame.AddRectangularSection(
            "TestColumn", "CF4000", 30, 20);

        // Retrieve it
        var retrieved = Etabs.Model.PropFrame.GetRectangularSection("TestColumn");

        Log($"Retrieved section: {retrieved.Name}");
        Log($"  Material: {retrieved.Material}");
        Log($"  Depth: {retrieved.Depth}");
        Log($"  Width: {retrieved.Width}");

        Assert.Equal(created.Name, retrieved.Name);
        Assert.Equal(created.Material, retrieved.Material);
        Assert.Equal(created.Depth, retrieved.Depth);
        Assert.Equal(created.Width, retrieved.Width);
    }

    [Fact]
    public void FrameProperties_GetCircularSection_ShouldReturnCorrectData()
    {
        LogSection("Get Circular Section Test");

        // Create section
        var created = Etabs.Model.PropFrame.AddCircularSection(
            "TestPier", "CF4000", 36);

        // Retrieve it
        var retrieved = Etabs.Model.PropFrame.GetCircularSection("TestPier");

        Log($"Retrieved section: {retrieved.Name}");
        Log($"  Material: {retrieved.Material}");
        Log($"  Diameter: {retrieved.Diameter}");

        Assert.Equal(created.Name, retrieved.Name);
        Assert.Equal(created.Material, retrieved.Material);
        Assert.Equal(created.Diameter, retrieved.Diameter);
    }

    [Fact]
    public void FrameProperties_GetSectionType_ShouldReturnCorrectType()
    {
        LogSection("Get Section Type Test");

        // Create different section types
        Etabs.Model.PropFrame.AddRectangularSection("RectSec", "CF4000", 24, 24);
        Etabs.Model.PropFrame.AddCircularSection("CircSec", "CF4000", 24);

        var rectType = Etabs.Model.PropFrame.GetSectionType("RectSec");
        var circType = Etabs.Model.PropFrame.GetSectionType("CircSec");

        Log($"RectSec type: {rectType}");
        Log($"CircSec type: {circType}");

        Assert.Equal(eFramePropType.Rectangular, rectType);
        Assert.Equal(eFramePropType.Circle, circType);
    }

    [Theory]
    [InlineData("C12x12", 12, 12)]
    [InlineData("C18x24", 18, 24)]
    [InlineData("C24x24", 24, 24)]
    [InlineData("C30x30", 30, 30)]
    public void FrameProperties_AddMultipleRectangular_ShouldWork(
        string name, double depth, double width)
    {
        LogSection($"Rectangular Section: {name}");

        var section = Etabs.Model.PropFrame.AddRectangularSection(
            name, "CF4000", depth, width);

        Log($"✓ Created {section.Name}: {depth}x{width}");

        Assert.Equal(name, section.Name);
        Assert.Equal(depth, section.Depth);
        Assert.Equal(width, section.Width);
    }

    [Fact]
    public void FrameProperties_SetModifiers_ShouldWork()
    {
        LogSection("Frame Property Modifiers Test");

        // Create section
        Etabs.Model.PropFrame.AddRectangularSection("ModTest", "CF4000", 24, 24);

        // Set modifiers
        var modifiers = new PropFrameModifiers
        {
            Area = 0.9,
            Inertia2 = 0.7,
            Inertia3 = 0.7,
            Torsion = 0.5,
            Mass = 1.0,
            Weight = 1.0
        };

        var ret = Etabs.Model.PropFrame.SetModifiers("ModTest", modifiers);
        AssertSuccess(ret, "Set modifiers");

        // Get modifiers back
        var retrieved = Etabs.Model.PropFrame.GetModifiers("ModTest");

        Log($"Modifiers set and retrieved:");
        Log($"  Area: {retrieved.Area}");
        Log($"  I2: {retrieved.Inertia2}");
        Log($"  I3: {retrieved.Inertia3}");
        Log($"  J: {retrieved.Torsion}");

        Assert.Equal(modifiers.Area, retrieved.Area);
        Assert.Equal(modifiers.Inertia2, retrieved.Inertia2);
        Assert.Equal(modifiers.Inertia3, retrieved.Inertia3);
    }

    [Fact]
    public void FrameProperties_Delete_ShouldRemoveSection()
    {
        LogSection("Delete Frame Section Test");

        // Create section
        Etabs.Model.PropFrame.AddRectangularSection("ToDelete", "CF4000", 24, 24);
        Log("✓ Created section 'ToDelete'");

        // Verify it exists
        var beforeDelete = Etabs.Model.PropFrame.GetNameList();
        Assert.Contains("ToDelete", beforeDelete);

        // Delete it
        var ret = Etabs.Model.PropFrame.Delete("ToDelete");
        AssertSuccess(ret, "Delete section");

        // Verify it's gone
        var afterDelete = Etabs.Model.PropFrame.GetNameList();
        Assert.DoesNotContain("ToDelete", afterDelete);

        Log("✓ Section successfully deleted");
    }
}
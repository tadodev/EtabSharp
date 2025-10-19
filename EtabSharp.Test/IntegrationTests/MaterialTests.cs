using ETABSv1;

namespace EtabSharp.Test.IntegrationTests;

public class MaterialTests: ETABSTestBase
{
     public MaterialTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void Materials_AddConcrete_ShouldCreateMaterial()
    {
        LogSection("Concrete Material Creation Test");

        // Create concrete material
        var concrete = Etabs.Model.Materials.AddConcreteMaterial(
            name: "TestConcrete4000",
            fpc: 4000,
            Ec: 3600000
        );

        Log($"✓ Created concrete: {concrete.Name}");
        Log($"  f'c = {concrete.fpc} psi");
        Log($"  Ec = {concrete.Ec} psi");
        Log($"  ν = {concrete.nu}");
        Log($"  α = {concrete.alpha}");

        Assert.NotNull(concrete);
        Assert.Equal("TestConcrete4000", concrete.Name);
        Assert.Equal(4000, concrete.fpc);
        Assert.Equal(3600000, concrete.Ec);
    }

    [Fact]
    public void Materials_GetNameList_ShouldReturnMaterials()
    {
        LogSection("Material Name List Test");

        // Add some materials first
        Etabs.Model.Materials.AddConcreteMaterial("C3000", 3000, 3000000);
        Etabs.Model.Materials.AddConcreteMaterial("C4000", 4000, 3600000);
        Etabs.Model.Materials.AddConcreteMaterial("C5000", 5000, 4000000);

        // Get all concrete materials
        var concreteMaterials = Etabs.Model.Materials.GetNameList(eMatType.Concrete);
        Log($"Found {concreteMaterials.Length} concrete materials:");
        foreach (var mat in concreteMaterials)
        {
            Log($"  - {mat}");
        }

        Assert.NotNull(concreteMaterials);
        Assert.True(concreteMaterials.Length >= 3);
        Assert.Contains("C3000", concreteMaterials);
        Assert.Contains("C4000", concreteMaterials);
        Assert.Contains("C5000", concreteMaterials);
    }

    [Fact]
    public void Materials_GetAllTypes_ShouldReturnAllMaterials()
    {
        LogSection("All Materials Test");

        // Get all materials (all types)
        var allMaterials = Etabs.Model.Materials.GetNameList();
        Log($"Total materials in model: {allMaterials.Length}");
        
        if (allMaterials.Length > 0)
        {
            Log("First 10 materials:");
            foreach (var mat in allMaterials.Take(10))
            {
                Log($"  - {mat}");
            }
        }

        Assert.NotNull(allMaterials);
    }

    [Fact]
    public void Materials_MultipleConcrete_ShouldAllExist()
    {
        LogSection("Multiple Concrete Materials Test");

        // Create multiple concrete grades
        var concretes = new[]
        {
            ("C2500", 2500.0, 2800000.0),
            ("C3000", 3000.0, 3000000.0),
            ("C3500", 3500.0, 3300000.0),
            ("C4000", 4000.0, 3600000.0),
            ("C5000", 5000.0, 4000000.0)
        };

        foreach (var (name, fpc, ec) in concretes)
        {
            var concrete = Etabs.Model.Materials.AddConcreteMaterial(name, fpc, ec);
            Log($"✓ Created {concrete.Name}: f'c={fpc} psi, Ec={ec} psi");
            Assert.Equal(name, concrete.Name);
        }

        // Verify all exist
        var materials = Etabs.Model.Materials.GetNameList(eMatType.Concrete);
        foreach (var (name, _, _) in concretes)
        {
            Assert.Contains(name, materials);
        }

        Log($"All {concretes.Length} concrete materials verified");
    }

    [Theory]
    [InlineData("C3000", 3000, 3000000)]
    [InlineData("C4000", 4000, 3600000)]
    [InlineData("C5000", 5000, 4000000)]
    [InlineData("C6000", 6000, 4400000)]
    public void Materials_AddConcrete_WithVariousStrengths_ShouldWork(
        string name, double fpc, double ec)
    {
        LogSection($"Concrete Material Test: {name}");

        var concrete = Etabs.Model.Materials.AddConcreteMaterial(name, fpc, ec);
        
        Log($"Created: {concrete.Name}");
        Log($"  Compressive strength: {concrete.fpc} psi");
        Log($"  Elastic modulus: {concrete.Ec} psi");

        Assert.Equal(name, concrete.Name);
        Assert.Equal(fpc, concrete.fpc);
        Assert.Equal(ec, concrete.Ec);
    }

    [Fact]
    public void Materials_InvalidParameters_ShouldThrowException()
    {
        LogSection("Invalid Material Parameters Test");

        // Test invalid name
        Assert.Throws<ArgumentException>(() =>
            Etabs.Model.Materials.AddConcreteMaterial("", 4000, 3600000));

        // Test invalid fpc
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Etabs.Model.Materials.AddConcreteMaterial("InvalidFpc", 0, 3600000));

        // Test invalid Ec
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Etabs.Model.Materials.AddConcreteMaterial("InvalidEc", 4000, 0));

        Log("✓ All validation checks passed");
    }
}
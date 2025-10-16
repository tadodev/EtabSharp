// Connect to ETABS
using EtabSharp.Core;
using EtabSharp.Frames.Models;
using EtabSharp.UnitSystem.Models;
using ETABSv1;

//var etabs = ETABSWrapper.Connect();
//if (etabs != null)
//{
//    Console.WriteLine($"Major Version: {etabs.MajorVersion}");     // 22
//    Console.WriteLine($"Full Version: {etabs.FullVersion}");       // 22.7.0
//    Console.WriteLine($"API Version: {etabs.ApiVersion}");         // e.g., 123
//}

//// Get active version
//string version = ETABSWrapper.GetActiveVersion();
//Console.WriteLine($"Active ETABS: {version}");  // 22.7.0

//// List all instances with full version
//var instances = ETABSWrapper.GetAllRunningInstances();
//foreach (var instance in instances)
//{
//    Console.WriteLine(instance);
//    // ETABS v22.7.0 (PID: 1234) - Model.edb [Supported]
//}

//// Get API info
//var apiInfo = etabs.GetApiInfo();
//Console.WriteLine(apiInfo);
// ETABS v22.7.0, API v123 - ETABSv1.DLL (.NET Standard 2.0)


//var e = etabs.Model.PropMaterial.SetMaterial("Concrete", eMatType.Concrete);

//var ret = etabs.Model.PropMaterial.SetOConcrete_1("Concrete", 5, false, 0, 1, 2, 0.0022, 0.0052, -0.1, 0, 0);

//'initialize new material property
//etabs.Model.PropMaterial.SetMaterial("Rebar", eMatType.Rebar);

//'assign other properties
//etabs.Model.PropMaterial.SetORebar_1("Rebar", 62, 93, 70, 102, 1, 1, 0.02, 0.1, -0.1, true);

var etabs = ETABSWrapper.Connect();

var ret = etabs.Model.SapModelInfor.InitializeNewModel(eUnits.kip_ft_F);
etabs.Model.Files.NewSteelDeckModel(10, 10, 10, 5, 5, 30, 30);

var unit = etabs.Model.UnitSystem.GetPresentUnits();
Console.WriteLine(unit);

try
{
    // Modern async/await pattern
    var concrete = etabs.Model.Materials.AddConcreteMaterial(
        name: "5ksi",
        fpc: 5,  // 5 ksi compressive strength
        Ec: 4664  // 47*sqrt(fc) psi elastic modulus
    );

    Console.WriteLine($"✓ Created: {concrete.Name}");
    Console.WriteLine($"  f'c = {concrete.fpc} MPa");
    Console.WriteLine($"  Ec = {concrete.Ec} MPa");
    Console.WriteLine($"  Poisson's ratio = {concrete.nu}");
    Console.WriteLine();
}
catch (Exception ex)
{
    Console.WriteLine($"✗ Error: {ex.Message}");
}

etabs.Model.PropFrame.AddRectangularSection("MyRectangularSection","5ksi",12, 20);

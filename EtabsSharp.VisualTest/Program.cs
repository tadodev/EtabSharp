// Connect to ETABS
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

using EtabSharp.Core;
using EtabSharp.Frames.Models;
using ETABSv1;

var etabs = ETABSWrapper.Connect();

var ret = etabs.Model.SapModelInfor.InitializeNewModel(eUnits.lb_in_F);
etabs.Model.Files.NewSteelDeckModel(10, 10, 10, 5, 5, 30, 30);

var unit = etabs.Model.UnitSystem.GetPresentUnits();
Console.WriteLine(unit);

try
{
    // Modern async/await pattern
    var concrete = etabs.Model.Materials.AddConcreteMaterial(
        name: "5ksi",
        fpc: 5000,  // 5 ksi compressive strength
        Ec: 47 * Math.Sqrt(5000)  // 47*sqrt(fc) psi elastic modulus
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

var column = etabs.Model.PropFrame.AddRectangularSection("C_24x24_5ksi", "5ksi", 24, 24);

var rebar = new PropColumnRebarRect
{
    MatPropLong = "A615Gr60",
    MatPropConfine = "A615Gr60",
    Cover = 2,
    NumberOfBars3Dir = 6,
    NumberOfBars2Dir = 6,
    BarSize = "#8",
    TieSize = "#4",
    TieSpacing = 12,
    TieLegs2Dir = 4,
    TieLegs3Dir = 4,
    ToBeDesigned = true
};
etabs.Model.PropFrame.SetColumnRebarRectangular(column.Name, rebar);



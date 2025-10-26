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
using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Loads.LoadPatterns.Models;
using EtabSharp.Properties.Frames.Models;
using EtabSharp.System.Models;
using ETABSv1;


var etabs = ETABSWrapper.Connect();

if (etabs == null)
{
    Console.WriteLine("No Etabs instance found. Please open etabs first");
    return;
}

try
{
    var sapModel = etabs.Model;
    //Initialize a new model
   sapModel.ModelInfo.InitializeNewModel(eUnits.kip_in_F);

    // Create a new blank model
    sapModel.Files.NewBlankModel();

    // Define material properties - Concrete
    var concrete = sapModel.Materials.AddConcreteMaterial(
        name: "5ksi",
        fpc: 5000,  // 5 ksi compressive strength
        Ec: 47 * Math.Sqrt(5000)  // 47*sqrt(fc) psi elastic modulus
    );

    Console.WriteLine($"✓ Created: {concrete.Name}");
    Console.WriteLine($"  f'c = {concrete.fpc} MPa");
    Console.WriteLine($"  Ec = {concrete.Ec} MPa");
    Console.WriteLine($"  Poisson's ratio = {concrete.nu}");
    Console.WriteLine();

    // Define rectangular frame section property
    var colSection = sapModel.PropFrame.AddRectangularSection(
        name: "C_30x30_5ksi",
        materialName: "5ksi",
        depth:30,
        width: 30
    );

    var beamSection = sapModel.PropFrame.AddRectangularSection(
        name: "B_16x34_5ksi",
        materialName: "5ksi",
        depth: 34,
        width: 16
    );

    // Assign Reinforcement to sections
    var columnRebar = PropColumnRebarRect.Create("A615Gr60", "A615Gr60",6,6);

    try
    {
        var ret = sapModel.PropFrame.SetColumnRebarRectangular(colSection.Name, columnRebar);

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    var beamRebar = PropBeamRebar.Create("A615Gr60", "A615Gr60",0,0,0,4);
    try
    {
        var ret = sapModel.PropFrame.SetBeamRebar(beamSection.Name, beamRebar);

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    var frameModifier = PropFrameModifiers.Cracked();

    sapModel.PropFrame.SetModifiers(colSection.Name, frameModifier);

    sapModel.Units.SetPresentUnits(Units.US_Kip_Ft);

    // Add frames by coordinates
    var frame1 = sapModel.Frames.AddFrameByCoordinates(0, 0, 0, 0, 0, 10, colSection.Name);
    var frame2 = sapModel.Frames.AddFrameByCoordinates(0, 0, 10, 8, 0, 16, colSection.Name);
    var frame3 = sapModel.Frames.AddFrameByCoordinates(-4, 0, 10, 0, 0, 10, beamSection.Name);
    var frames = sapModel.Frames.GetAllFrames();

    
//assign point object restraint at top
sapModel.Points.SetRestraint("3",PointRestraint.RollerZ());
sapModel.Points.SetRestraint("4",PointRestraint.RollerZ());

// add load pattern

sapModel.LoadPatterns.AddDeadLoad("SW");
sapModel.LoadPatterns.AddSuperDeadLoad("SDL");
sapModel.LoadPatterns.AddLiveLoad("LLunred");

sapModel.Frames.SetLoadDistributed(frame2,FrameDistributedLoad.CreateGravityLoad(frame2, "SDL", 0.5));

//save model

    sapModel.Files.SaveFile(@"D:\Research\test.edb");

sapModel.Analyze.RunCompleteAnalysis();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}


using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Elements.PointObj.Models;
using ETABSv1;

namespace EtabSharp.Test.IntegrationTests;

/// <summary>
/// Comprehensive integration tests for Point, Frame, and Area element managers.
/// Tests advanced features, loads, properties, and interactions.
/// </summary>
public class ElementIntegrationTests : ETABSTestBase
{
    public ElementIntegrationTests(ITestOutputHelper output) : base(output)
    {
        // Setup materials for all tests
        SetupMaterials();
    }

    private void SetupMaterials()
    {
        LogSection("Setup Materials");

        Etabs.Model.Materials.AddConcreteMaterial("C3000", 3000, 3000000);
        Etabs.Model.Materials.AddConcreteMaterial("C4000", 4000, 3600000);
        Etabs.Model.Materials.AddConcreteMaterial("C5000", 5000, 4000000);

        Etabs.Model.PropFrame.AddRectangularSection("C12x12", "C4000", 12, 12);
        Etabs.Model.PropFrame.AddRectangularSection("C18x18", "C4000", 18, 18);
        Etabs.Model.PropFrame.AddRectangularSection("C24x24", "C4000", 24, 24);

        Log("✓ Materials and sections created");
    }

    #region Point Advanced Tests

    [Fact]
    public void Point_CompletePropertiesWorkflow_ShouldWork()
    {
        LogSection("Point Complete Properties Workflow");

        // Create point
        var pointName = Etabs.Model.Points.AddPoint(0, 0, 0, "TestPt1");
        Log($"✓ Created point: {pointName}");

        // Set restraint
        var restraint = PointRestraint.Fixed();
        Etabs.Model.Points.SetRestraint(pointName, restraint);
        Log("✓ Set fixed restraint");

        // Set spring
        var spring = new PointSpring { Kz = 1000 };
        Etabs.Model.Points.SetSpring(pointName, spring);
        Log("✓ Set spring (Kz=1000)");

        // Set mass
        var mass = new PointMass { Mx = 10, My = 10, Mz = 10 };
        Etabs.Model.Points.SetMass(pointName, mass);
        Log("✓ Set mass (10 in each direction)");

        // Verify all properties
        var retrievedRestraint = Etabs.Model.Points.GetRestraint(pointName);
        var retrievedSpring = Etabs.Model.Points.GetSpring(pointName);
        var retrievedMass = Etabs.Model.Points.GetMass(pointName);

        Assert.NotNull(retrievedRestraint);
        Assert.NotNull(retrievedSpring);
        Assert.NotNull(retrievedMass);
        Assert.Equal(1000, retrievedSpring.Kz);
        Assert.Equal(10, retrievedMass.Mz);

        Log("✓ All properties verified successfully");
    }

    [Fact]
    public void Point_LoadsWorkflow_ShouldWork()
    {
        LogSection("Point Loads Workflow");

        var pointName = Etabs.Model.Points.AddPoint(10, 10, 0, "LoadPt1");

        // Set force load
        var forceLoad = new PointLoad
        {
            PointName = pointName,
            LoadPattern = "DEAD",
            Fz = -50,
            CoordinateSystem = "Global"
        };
        Etabs.Model.Points.SetLoadForce(pointName, forceLoad);
        Log("✓ Set force load (Fz=-50)");

        // Set displacement load
        var dispLoad = new PointDisplacement
        {
            PointName = pointName,
            LoadPattern = "SETTLE",
            Uz = -0.1,
            CoordinateSystem = "Global"
        };
        Etabs.Model.Points.SetLoadDisplacement(pointName, dispLoad);
        Log("✓ Set displacement load (Uz=-0.1)");

        // Verify loads
        var retrievedForce = Etabs.Model.Points.GetLoadForce(pointName, "DEAD");
        var retrievedDisp = Etabs.Model.Points.GetLoadDisplacement(pointName, "SETTLE");

        Assert.NotNull(retrievedForce);
        Assert.NotNull(retrievedDisp);
        Assert.Equal(-50, retrievedForce.Fz);
        Assert.Equal(-0.1, retrievedDisp.Uz);

        Log("✓ Loads verified successfully");
    }

    [Fact]
    public void Point_GroupAndSelection_ShouldWork()
    {
        LogSection("Point Group and Selection");

        var pointName = Etabs.Model.Points.AddPoint(20, 20, 0, "GroupPt1");

        // Assign to group
        Etabs.Model.Points.SetGroupAssignment(pointName, "TestGroup");
        Log("✓ Assigned to TestGroup");

        // Verify group assignment
        var groups = Etabs.Model.Points.GetGroupAssignment(pointName);
        Assert.Contains("TestGroup", groups);

        // Set selection
        Etabs.Model.Points.SetSelected(pointName, true);
        var isSelected = Etabs.Model.Points.IsSelected(pointName);
        Assert.True(isSelected);
        Log("✓ Selection verified");

        Log("✓ Group and selection workflow completed");
    }

    #endregion

    #region Frame Advanced Tests

    [Fact]
    public void Frame_CompletePropertiesWorkflow_ShouldWork()
    {
        LogSection("Frame Complete Properties Workflow");

        // Create frame
        var p1 = Etabs.Model.Points.AddPoint(30, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(30, 0, 120);
        var frameName = Etabs.Model.Frames.AddFrame(p1, p2, "C18x18");
        Log($"✓ Created frame: {frameName}");

        // Set local axes
        Etabs.Model.Frames.SetLocalAxes(frameName, 45);
        Log("✓ Set local axes (45°)");

        // Set modifiers
        var modifiers = new double[] { 1.0, 0.8, 0.8, 0.8, 0.7, 0.7, 1.0, 1.0 };
        Etabs.Model.Frames.SetModifiers(frameName, modifiers);
        Log("✓ Set property modifiers");

        // Set releases
        Etabs.Model.Frames.SetIEndPinned(frameName);
        Log("✓ Set I-end pinned");

        // Set insertion point
        var insertionPoint = new FrameInsertionPoint
        {
            CardinalPoint = 10, // Centroid
            Mirror2 = false,
            Mirror3 = false,
            StiffnessTransform = false,
            Offset1 = new double[] { 0, 0, 0 },
            Offset2 = new double[] { 0, 0, 0 },
            CoordinateSystem = "Local"
        };
        Etabs.Model.Frames.SetInsertionPoint(frameName, insertionPoint);
        Log("✓ Set insertion point");

        // Verify all properties
        var (angle, _) = Etabs.Model.Frames.GetLocalAxes(frameName);
        var retrievedModifiers = Etabs.Model.Frames.GetModifiers(frameName);
        var releases = Etabs.Model.Frames.GetReleases(frameName);
        var retrievedIP = Etabs.Model.Frames.GetInsertionPoint(frameName);

        Assert.Equal(45, angle);
        Assert.Equal(0.8, retrievedModifiers[1]); // ShearArea2
        Assert.NotNull(releases);
        Assert.True(releases.IEndReleases.R2);
        Assert.Equal(10, retrievedIP.CardinalPoint);

        Log("✓ All frame properties verified successfully");
    }

    [Fact]
    public void Frame_LoadsWorkflow_ShouldWork()
    {
        LogSection("Frame Loads Workflow");

        var p1 = Etabs.Model.Points.AddPoint(40, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(40, 120, 0);
        var frameName = Etabs.Model.Frames.AddFrame(p1, p2, "C18x18");

        // Set distributed load
        var distLoad = FrameDistributedLoad.CreateGravityLoad(frameName, "DEAD", 100);
        Etabs.Model.Frames.SetLoadDistributed(frameName, distLoad);
        Log("✓ Set distributed gravity load (100)");

        // Set triangular load
        var triLoad = FrameDistributedLoad.CreateTriangularLoad(frameName, "LIVE", 0, 80);
        Etabs.Model.Frames.SetLoadDistributed(frameName, triLoad, false); // Don't replace
        Log("✓ Set triangular load (0→80)");

        // Set point load
        var pointLoad = new FramePointLoad
        {
            FrameName = frameName,
            LoadPattern = "EQUIP",
            LoadType = eLoadType.Force,
            Direction = eFrameLoadDirection.Gravity,
            RelativeDistance = 0.5,
            LoadValue = 50,
            IsRelativeDistance = true,
            CoordinateSystem = "Global"
        };
        Etabs.Model.Frames.SetLoadPoint(frameName, pointLoad);
        Log("✓ Set point load at midspan (50)");

        // Verify loads
        var distLoads = Etabs.Model.Frames.GetLoadDistributed(frameName);
        var pointLoads = Etabs.Model.Frames.GetLoadPoint(frameName);

        Assert.True(distLoads.Count >= 2); // DEAD and LIVE
        Assert.True(pointLoads.Count >= 1); // EQUIP
        Assert.Contains(distLoads, l => l.LoadPattern == "DEAD" && l.StartLoad == 100);
        Assert.Contains(pointLoads, l => l.LoadPattern == "EQUIP" && l.LoadValue == 50);

        Log($"✓ Loads verified: {distLoads.Count} distributed, {pointLoads.Count} point");
    }

    [Fact]
    public void Frame_DesignProperties_ShouldWork()
    {
        LogSection("Frame Design Properties");

        var p1 = Etabs.Model.Points.AddPoint(50, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(50, 0, 120);
        var frameName = Etabs.Model.Frames.AddFrame(p1, p2, "C24x24");

        // Set design procedure
        Etabs.Model.Frames.SetConcreteDesign(frameName);
        Log("✓ Set concrete design procedure");

        // Set pier
        Etabs.Model.Frames.SetPier(frameName, "P1");
        Log("✓ Set pier label (P1)");

        // Set output stations
        Etabs.Model.Frames.SetOutputStations(frameName, 0, 0, 9); // Min 9 stations
        Log("✓ Set output stations (min 9)");

        // Verify design properties
        var designType = Etabs.Model.Frames.GetDesignProcedure(frameName);
        var pier = Etabs.Model.Frames.GetPier(frameName);
        var (stationType, maxSeg, minSta, noEnds, noLoads) = Etabs.Model.Frames.GetOutputStations(frameName);

        Assert.Equal(2, designType); // 2 = Concrete
        Assert.Equal("P1", pier);
        Assert.Equal(9, minSta);

        Log("✓ Design properties verified successfully");
    }

    [Fact]
    public void Frame_EndReleasesVariations_ShouldWork()
    {
        LogSection("Frame End Releases Variations");

        var p1 = Etabs.Model.Points.AddPoint(60, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(60, 120, 0);

        // Test 1: Both ends pinned
        var frame1 = Etabs.Model.Frames.AddFrame(p1, p2, "C18x18", "Frame1");
        Etabs.Model.Frames.SetBothEndsPinned(frame1);
        var rel1 = Etabs.Model.Frames.GetReleases(frame1);
        Assert.True(rel1.IEndReleases.R2 && rel1.JEndReleases.R2);
        Log("✓ Both ends pinned verified");

        // Test 2: I-end roller
        p1 = Etabs.Model.Points.AddPoint(70, 0, 0);
        p2 = Etabs.Model.Points.AddPoint(70, 120, 0);
        var frame2 = Etabs.Model.Frames.AddFrame(p1, p2, "C18x18", "Frame2");
        Etabs.Model.Frames.SetIEndRoller(frame2);
        var rel2 = Etabs.Model.Frames.GetReleases(frame2);
        Assert.True(rel2.IEndReleases.U1); // Axial released
        Log("✓ I-end roller verified");

        // Test 3: Moment releases only
        p1 = Etabs.Model.Points.AddPoint(80, 0, 0);
        p2 = Etabs.Model.Points.AddPoint(80, 120, 0);
        var frame3 = Etabs.Model.Frames.AddFrame(p1, p2, "C18x18", "Frame3");
        Etabs.Model.Frames.SetBothEndsMomentReleased(frame3);
        var rel3 = Etabs.Model.Frames.GetReleases(frame3);
        Assert.True(rel3.IEndReleases.R2 && rel3.IEndReleases.R3);
        Assert.False(rel3.IEndReleases.U1); // Axial not released
        Log("✓ Moment releases verified");

        Log("✓ All release variations tested successfully");
    }

    #endregion

    #region Area Advanced Tests

    [Fact]
    public void Area_CompletePropertiesWorkflow_ShouldWork()
    {
        LogSection("Area Complete Properties Workflow");

        // Create area
        var p1 = Etabs.Model.Points.AddPoint(0, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(20, 0, 0);
        var p3 = Etabs.Model.Points.AddPoint(20, 20, 0);
        var p4 = Etabs.Model.Points.AddPoint(0, 20, 0);
        var areaName = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 });
        Log($"✓ Created area: {areaName}");

        // Set local axes
        Etabs.Model.Areas.SetLocalAxes(areaName, 30);
        Log("✓ Set local axes (30°)");

        // Set modifiers
        var modifiers = new AreaModifiers
        {
            MembraneF11 = 0.9,
            MembraneF22 = 0.9,
            BendingM11 = 0.7,
            BendingM22 = 0.7,
            ShearV13 = 0.8,
            ShearV23 = 0.8
        };
        Etabs.Model.Areas.SetModifiers(areaName, modifiers);
        Log("✓ Set property modifiers");

        // Set diaphragm
        Etabs.Model.Areas.SetDiaphragm(areaName, "D1");
        Log("✓ Set diaphragm (D1)");

        // Set edge constraint
        Etabs.Model.Areas.SetEdgeConstraint(areaName, true);
        Log("✓ Set edge constraint");

        // Verify all properties
        var (angle, _) = Etabs.Model.Areas.GetLocalAxes(areaName);
        var retrievedModifiers = Etabs.Model.Areas.GetModifiers(areaName);
        var diaphragm = Etabs.Model.Areas.GetDiaphragm(areaName);
        var hasEdgeConstraint = Etabs.Model.Areas.GetEdgeConstraint(areaName);

        Assert.Equal(30, angle);
        Assert.Equal(0.9, retrievedModifiers.MembraneF11);
        Assert.Equal("D1", diaphragm);
        Assert.True(hasEdgeConstraint);

        Log("✓ All area properties verified successfully");
    }

    [Fact]
    public void Area_LoadsWorkflow_ShouldWork()
    {
        LogSection("Area Loads Workflow");

        var p1 = Etabs.Model.Points.AddPoint(30, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(50, 0, 0);
        var p3 = Etabs.Model.Points.AddPoint(50, 20, 0);
        var p4 = Etabs.Model.Points.AddPoint(30, 20, 0);
        var areaName = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 });

        // Set uniform gravity load
        var gravityLoad = AreaUniformLoad.CreateGravityLoad(areaName, "DEAD", 50);
        Etabs.Model.Areas.SetLoadUniform(areaName, gravityLoad);
        Log("✓ Set gravity load (50)");

        // Set pressure load
        var pressureLoad = AreaUniformLoad.CreatePressureLoad(areaName, "WIND", 20);
        Etabs.Model.Areas.SetLoadUniform(areaName, pressureLoad, false); // Don't replace
        Log("✓ Set pressure load (20)");

        // Set uniform to frame load
        var toFrameLoad = new AreaUniformToFrameLoad
        {
            AreaName = areaName,
            LoadPattern = "LIVE",
            Value = 30,
            Direction = 3,
            DistributionType = 1,
            CoordinateSystem = "Global"
        };
        Etabs.Model.Areas.SetLoadUniformToFrame(areaName, toFrameLoad);
        Log("✓ Set uniform to frame load (30)");

        // Verify loads
        var uniformLoads = Etabs.Model.Areas.GetLoadUniform(areaName);
        var toFrameLoads = Etabs.Model.Areas.GetLoadUniformToFrame(areaName);

        Assert.True(uniformLoads.Count >= 2); // DEAD and WIND
        Assert.True(toFrameLoads.Count >= 1); // LIVE
        Assert.Contains(uniformLoads, l => l.LoadPattern == "DEAD" && l.LoadValue == 50);
        Assert.Contains(toFrameLoads, l => l.LoadPattern == "LIVE" && l.Value == 30);

        Log($"✓ Loads verified: {uniformLoads.Count} uniform, {toFrameLoads.Count} to-frame");
    }

    [Fact]
    public void Area_DesignAssignments_ShouldWork()
    {
        LogSection("Area Design Assignments");

        var p1 = Etabs.Model.Points.AddPoint(0, 40*12, 0);
        var p2 = Etabs.Model.Points.AddPoint(20*12, 40 * 12, 0);
        var p3 = Etabs.Model.Points.AddPoint(20 * 12, 40 * 12, 120);
        var p4 = Etabs.Model.Points.AddPoint(0, 40 * 12, 120);

        // Test 1: Wall pier
        var area1 = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 }, "Wall1", "");
        Etabs.Model.Areas.SetAsWallPier(area1, "P1");
        var pier = Etabs.Model.Areas.GetPier(area1);
        Assert.Equal("P1", pier);
        Log("✓ Wall pier assignment verified");

        // Test 2: Spandrel
        p1 = Etabs.Model.Points.AddPoint(0, 20 * 12, 8 * 12);
        p2 = Etabs.Model.Points.AddPoint(20 * 12, 20 * 12, 8 * 12);
        p3 = Etabs.Model.Points.AddPoint(20 * 12, 20 * 12, 120);
        p4 = Etabs.Model.Points.AddPoint(0, 20 * 12, 120);
        var area2 = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 }, "Default", "Spandrel1");
        Etabs.Model.Areas.SetAsWallSpandrel(area2, "S1");
        var spandrel = Etabs.Model.Areas.GetSpandrel(area2);
        Assert.Equal("S1", spandrel);
        Log("✓ Spandrel assignment verified");

        // Test 3: Opening
        p1 = Etabs.Model.Points.AddPoint(120, 0, 0);
        p2 = Etabs.Model.Points.AddPoint(140, 0, 0);
        p3 = Etabs.Model.Points.AddPoint(140, 20, 0);
        p4 = Etabs.Model.Points.AddPoint(120, 20, 0);
        var area3 = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 }, "Default", "Opening1");
        Etabs.Model.Areas.SetAsOpening(area3);
        var isOpening = Etabs.Model.Areas.GetOpening(area3);
        Assert.True(isOpening);
        Log("✓ Opening assignment verified");

        Log("✓ All design assignments tested successfully");
    }

    [Fact]
    public void Area_LoadDirectionVariations_ShouldWork()
    {
        LogSection("Area Load Direction Variations");

        var p1 = Etabs.Model.Points.AddPoint(150, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(170, 0, 0);
        var p3 = Etabs.Model.Points.AddPoint(170, 20, 0);
        var p4 = Etabs.Model.Points.AddPoint(150, 20, 0);
        var areaName = Etabs.Model.Areas.AddAreaByPoints(new[] { p1, p2, p3, p4 });

        // Test different directions
        Etabs.Model.Areas.SetLoadUniform(areaName, "DEAD", 100, 10, true, "Global"); // Gravity
        Etabs.Model.Areas.SetLoadUniform(areaName, "LIVE", 50, 11, false, "Global"); // Projected gravity
        Etabs.Model.Areas.SetLoadUniform(areaName, "WIND", 25, 6, false, "Global"); // Global Z
        Etabs.Model.Areas.SetLoadUniform(areaName, "SEISMIC", 30, 9, false, "Global"); // Projected Z
        Etabs.Model.Areas.SetLoadUniform(areaName, "PRESSURE", 15, 3, false, "Local"); // Local 3

        var loads = Etabs.Model.Areas.GetLoadUniform(areaName);

        Assert.True(loads.Count >= 5);
        Assert.Contains(loads, l => l.Direction == 10); // Gravity
        Assert.Contains(loads, l => l.Direction == 11); // Projected gravity
        Assert.Contains(loads, l => l.Direction == 6);  // Global Z
        Assert.Contains(loads, l => l.Direction == 9);  // Projected Z
        Assert.Contains(loads, l => l.Direction == 3);  // Local 3

        Log($"✓ All {loads.Count} load directions verified");
    }

    #endregion

    #region Complex Integration Tests

    [Fact]
    public void ComplexStructure_MultistoryBuilding_ShouldWork()
    {
        LogSection("Complex Structure - Multistory Building");

        int numStories = 3;
        double storyHeight = 120;
        double bayWidth = 240;

        // Create grid points for all stories
        var gridPoints = new string[numStories + 1, 2, 2]; // [story, x, y]

        for (int story = 0; story <= numStories; story++)
        {
            double elevation = story * storyHeight;
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    gridPoints[story, x, y] = Etabs.Model.Points.AddPoint(
                        x * bayWidth, y * bayWidth, elevation,
                        $"P_S{story}_X{x}_Y{y}");
                }
            }
        }
        Log($"✓ Created {(numStories + 1) * 4} grid points");

        // Add restraints to base
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                Etabs.Model.Points.SetFixedSupport(gridPoints[0, x, y]);
            }
        }
        Log("✓ Added restraints to 4 base points");

        // Create columns
        int columnCount = 0;
        for (int story = 0; story < numStories; story++)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    var colName = Etabs.Model.Frames.AddFrame(
                        gridPoints[story, x, y],
                        gridPoints[story + 1, x, y],
                        "C24x24",
                        $"C_S{story}_X{x}_Y{y}");

                    // Set as column design
                    Etabs.Model.Frames.SetConcreteDesign(colName);
                    columnCount++;
                }
            }
        }
        Log($"✓ Created {columnCount} columns");

        // Create floor slabs
        int floorCount = 0;
        for (int story = 1; story <= numStories; story++)
        {
            var floorPoints = new[]
            {
                gridPoints[story, 0, 0],
                gridPoints[story, 1, 0],
                gridPoints[story, 1, 1],
                gridPoints[story, 0, 1]
            };

            var floorName = Etabs.Model.Areas.AddAreaByPoints(
                floorPoints, "Default", $"Floor_S{story}");

            // Set as floor diaphragm
            Etabs.Model.Areas.SetAsFloorDiaphragm(floorName, $"D{story}");

            // Add gravity load
            Etabs.Model.Areas.SetGravityLoad(floorName, "DEAD", 100);
            Etabs.Model.Areas.SetGravityLoad(floorName, "LIVE", 40, false);

            floorCount++;
        }
        Log($"✓ Created {floorCount} floor slabs with diaphragms and loads");

        // Verify structure
        var totalPoints = Etabs.Model.Points.Count();
        var totalFrames = Etabs.Model.Frames.Count();
        var totalAreas = Etabs.Model.Areas.Count();

        Assert.True(totalPoints >= (numStories + 1) * 4);
        Assert.True(totalFrames >= numStories * 4);
        Assert.True(totalAreas >= numStories);

        Log("✓ Multistory building structure verified:");
        Log($"  Total Points: {totalPoints}");
        Log($"  Total Columns: {totalFrames}");
        Log($"  Total Floors: {totalAreas}");
        Log($"  Stories: {numStories}");
    }

    [Fact]
    public void ComplexLoading_CombinedLoads_ShouldWork()
    {
        LogSection("Complex Loading - Combined Loads");

        // Create a frame
        var p1 = Etabs.Model.Points.AddPoint(200, 0, 0);
        var p2 = Etabs.Model.Points.AddPoint(200, 240, 0);
        var frameName = Etabs.Model.Frames.AddFrame(p1, p2, "C24x24");

        // Add base restraints
        Etabs.Model.Points.SetFixedSupport(p1);
        Etabs.Model.Points.SetPinnedSupport(p2);

        // Apply multiple load types

        // 1. Uniform distributed load
        var uniformLoad = FrameDistributedLoad.CreateGravityLoad(frameName, "DEAD", 2.0);
        Etabs.Model.Frames.SetLoadDistributed(frameName, uniformLoad);
        Log("✓ Applied uniform load (2.0 k/ft)");

        // 2. Triangular load
        var triLoad = FrameDistributedLoad.CreateTriangularLoad(frameName, "WIND", 0, 1.5);
        Etabs.Model.Frames.SetLoadDistributed(frameName, triLoad, false);
        Log("✓ Applied triangular wind load (0→1.5)");

        // 3. Partial load
        var partialLoad = FrameDistributedLoad.CreatePartialLoad(frameName, "EQUIP", 3.0, 0.25, 0.75);
        Etabs.Model.Frames.SetLoadDistributed(frameName, partialLoad, false);
        Log("✓ Applied partial equipment load (3.0 at 0.25-0.75)");

        // 4. Point load at midspan
        var pointLoad = new FramePointLoad
        {
            FrameName = frameName,
            LoadPattern = "LIVE",
            LoadType = eLoadType.Force,
            Direction = eFrameLoadDirection.Gravity,
            RelativeDistance = 0.5,
            LoadValue = 10.0,
            IsRelativeDistance = true,
            CoordinateSystem = "Global"
        };
        Etabs.Model.Frames.SetLoadPoint(frameName, pointLoad);
        Log("✓ Applied point load at midspan (10.0 k)");

        // 5. Temperature load
        Etabs.Model.Frames.SetUniformTemperatureLoad(frameName, "TEMP", 50);
        Log("✓ Applied temperature load (+50°F)");

        // Verify all loads
        var distLoads = Etabs.Model.Frames.GetLoadDistributed(frameName);
        var pointLoads = Etabs.Model.Frames.GetLoadPoint(frameName);
        var allLoads = Etabs.Model.Frames.GetAllLoads(frameName);

        Assert.True(distLoads.Count >= 3); // DEAD, WIND, EQUIP
        Assert.True(pointLoads.Count >= 1); // LIVE
        Assert.True(allLoads.TotalLoadCount >= 4);

        Log($"✓ Combined loading verified:");
        Log($"  Distributed loads: {distLoads.Count}");
        Log($"  Point loads: {pointLoads.Count}");
        Log($"  Total loads: {allLoads.TotalLoadCount}");
    }

    #endregion
}
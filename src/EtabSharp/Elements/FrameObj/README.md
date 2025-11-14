# Frame Object Manager - Implementation Summary

## Overview
Complete implementation of the IFrame interface for managing frame objects (beams, columns, braces) in ETABS models.

## Architecture
The implementation uses a **partial class pattern** with methods organized into separate files by functionality:

### File Structure
```
FrameObjectManager.cs                    - Core geometry and creation methods
FrameObjectManager.Properties.cs         - Section, material, local axes, insertion point, modifiers
FrameObjectManager.EndReleases.cs        - End releases and fixity
FrameObjectManager.Loads.cs              - Load assignment methods (distributed, point, temperature)
FrameObjectManager.Design.cs             - Design procedures, pier/spandrel, lateral bracing, output stations
FrameObjectManager.Advanced.cs           - Advanced features (NEW)
```

## Implemented Features

### 1. Core Geometry & Creation
- `AddFrame()` - Create frames between two points
- `AddFrameByCoordinates()` - Create frames by coordinates
- `ChangeName()` - Rename frame objects
- `GetFrame()` - Retrieve frame with properties
- `GetAllFrames()` - Get all frames in model
- `GetNameList()` - Get list of frame names
- `Count()` - Get total frame count
- `Delete()` - Delete frame objects

### 2. Section Properties
- `SetSection()` - Assign frame section
- `GetSection()` - Retrieve section assignment
- `GetSectionNonPrismatic()` - Get non-prismatic section data
- `SetMaterialOverwrite()` - Override material
- `GetMaterialOverwrite()` - Get material override

### 3. Local Axes & Orientation
- `SetLocalAxes()` - Set local axis angle
- `GetLocalAxes()` - Get local axis angle
- `GetDesignOrientation()` - Get design orientation

### 4. End Releases & Fixity
- `SetReleases()` - Assign end releases
- `GetReleases()` - Retrieve end releases
- `DeleteReleases()` - Remove end releases
- **Convenience methods:**
  - `SetIEndPinned()` - Pin I-end
  - `SetJEndPinned()` - Pin J-end
  - `SetBothEndsPinned()` - Pin both ends
  - `SetIEndRoller()` - Roller at I-end
  - `SetJEndRoller()` - Roller at J-end
  - `SetIEndTorsionRelease()` - Release torsion at I-end
  - `SetJEndTorsionRelease()` - Release torsion at J-end
  - `SetBothEndsMomentReleased()` - Release moments at both ends
  - `SetPartialMomentReleases()` - Partial moment releases with springs
  - `SetSemiRigidConnection()` - Semi-rigid connection

### 5. End Offsets & Insertion Point
- `SetEndLengthOffset()` - Set rigid end zones
- `GetEndLengthOffset()` - Get end offsets
- `SetInsertionPoint()` - Set insertion point and offsets
- `GetInsertionPoint()` - Get insertion point data
- `GetInsertionPointValues()` - Get insertion point as values
- `DeleteInsertionPoint()` - Remove insertion point

### 6. Distributed Loads
- `SetLoadDistributed()` - Assign distributed loads
- `GetLoadDistributed()` - Retrieve distributed loads
- `DeleteLoadDistributed()` - Remove distributed loads
- **Convenience methods:**
  - `SetUniformLoad()` - Uniform load over entire length
  - `SetTriangularLoad()` - Triangular load

### 7. Point Loads
- `SetLoadPoint()` - Assign concentrated loads
- `GetLoadPoint()` - Retrieve point loads
- `DeleteLoadPoint()` - Remove point loads
- **Convenience methods:**
  - `SetMidspanLoad()` - Load at midspan

### 8. Temperature Loads
- `SetLoadTemperature()` - Assign temperature loads
- `GetLoadTemperature()` - Retrieve temperature loads
- `DeleteLoadTemperature()` - Remove temperature loads
- **Convenience methods:**
  - `SetUniformTemperatureLoad()` - Uniform temperature change
  - `GetAllLoads()` - Get all loads for a frame

### 9. Design & Label Assignment
- `SetDesignProcedure()` - Set design type (column, beam, brace)
- `GetDesignProcedure()` - Get design procedure
- `SetPier()` - Assign pier label
- `GetPier()` - Get pier assignment
- `SetSpandrel()` - Assign spandrel label
- `GetSpandrel()` - Get spandrel assignment
- `SetColumnSpliceOverwrite()` - Set column splice
- `GetColumnSpliceOverwrite()` - Get column splice data
- **Convenience methods:**
  - `SetSteelDesign()` - Enable steel design
  - `SetConcreteDesign()` - Enable concrete design
  - `SetNoDesign()` - Disable design

### 10. Output Stations
- `SetOutputStations()` - Set output stations for results
- `GetOutputStations()` - Get output station data

### 11. Modifiers
- `SetModifiers()` - Set property modifiers
- `GetModifiers()` - Get property modifiers
- `DeleteModifiers()` - Remove modifiers

### 12. Mass Assignment (NEW)
- `SetMass()` - Assign additional mass per length
- `GetMass()` - Get mass assignment
- `DeleteMass()` - Remove mass assignment

### 13. Spring Assignment (NEW)
- `SetSpringAssignment()` - Assign line spring property
- `GetSpringAssignment()` - Get spring assignment
- `DeleteSpring()` - Remove spring assignment

### 14. Lateral Bracing
- `SetLateralBracing()` - Assign lateral bracing
- `GetLateralBracing()` - Get lateral bracing data
- `DeleteLateralBracing()` - Remove lateral bracing
- `ClearAllLateralBracing()` - Clear all bracing
- **Convenience methods:**
  - `SetFullLateralBracing()` - Full length bracing
  - `SetPointLateralBracing()` - Point bracing
  - `ClearLateralBracing()` - Clear bracing

### 15. Tension/Compression Limits
- `SetTCLimits()` - Set force limits (cables, struts)
- `GetTCLimits()` - Get force limits

### 16. Group Assignment (NEW)
- `SetGroupAssignment()` - Assign to/remove from group
- `GetGroupAssignment()` - Get group memberships

### 17. Selection State (NEW)
- `SetSelected()` - Select/deselect frame
- `IsSelected()` - Check selection state

### 18. Label and Story Methods (NEW)
- `GetLabelNameList()` - Get all labels and stories
- `GetLabelFromName()` - Get label for frame
- `GetNameFromLabel()` - Convert label to name
- `GetFramesOnStory()` - Get frames on story

### 19. Advanced Features (NEW)
- `GetTransformationMatrix()` - Get transformation matrix
- `GetHingeAssignments()` - Get plastic hinge assignments
- `GetSupports()` - Get support objects at frame ends
- `GetFrameType()` - Get frame type (straight/curved)
- `GetCurveData()` - Get curve data for curved frames

## Models

### Frame Models
- `Frame` - Main frame object with properties
- `FrameReleases` - End release conditions
- `FrameInsertionPoint` - Insertion point and offsets
- `FrameDistributedLoad` - Distributed load data
- `FramePointLoad` - Concentrated load data
- `FrameModifiers` - Property modifiers
- `FrameTCLimits` - Tension/compression limits
- `FrameOutputStations` - Output station configuration
- `LateralBracingData` - Lateral bracing information
- `FrameLoads` - Combined load data
- `FrameHinge` - Plastic hinge assignment (NEW)
- `FrameCurveData` - Curved frame data (NEW)

### Enums
- `eFrameLoadDirection` - Load direction options
- `FrameEnd` - Frame end (I or J)
- `BracingType` - Bracing type (point or uniform)
- `BracingLocation` - Bracing location (top, bottom, all)
- `FrameCurveType` - Curve type (straight, circular, spline, etc.) (NEW)

## Usage Examples

### Creating Frames
```csharp
// Between two points
string frame1 = frameManager.AddFrame("1", "2", sectionName: "W14X22");

// By coordinates
string frame2 = frameManager.AddFrameByCoordinates(
    xi: 0, yi: 0, zi: 0,
    xj: 10, yj: 0, zj: 0,
    sectionName: "W14X22"
);
```

### Assigning End Releases
```csharp
// Pinned both ends
frameManager.SetBothEndsPinned("Frame1");

// Custom releases
var releases = new FrameReleases
{
    IEndReleases = new bool[] { false, false, false, false, true, true }, // Fixed translation, released rotation
    JEndReleases = new bool[] { false, false, false, false, true, true }
};
frameManager.SetReleases("Frame1", releases);

// Semi-rigid connection
frameManager.SetSemiRigidConnection("Frame1", FrameEnd.IEnd, rotationalStiffness: 50000);
```

### Applying Loads
```csharp
// Uniform distributed load
frameManager.SetUniformLoad("Frame1", "DEAD", loadValue: -2.0, 
    direction: eFrameLoadDirection.Gravity);

// Triangular load
frameManager.SetTriangularLoad("Frame1", "LIVE", startLoad: -1.0, endLoad: -3.0);

// Point load at midspan
frameManager.SetMidspanLoad("Frame1", "LIVE", loadValue: -10.0);

// Custom distributed load
var distLoad = new FrameDistributedLoad
{
    LoadPattern = "WIND",
    LoadType = 1, // Force per length
    Direction = 4, // X direction
    StartDistance = 0.2, // 20% from I-end
    EndDistance = 0.8,   // 80% from I-end
    StartLoad = 1.0,
    EndLoad = 1.5,
    IsRelativeDistance = true
};
frameManager.SetLoadDistributed("Frame1", distLoad);
```

### Design Assignment
```csharp
// Set as steel column
frameManager.SetSteelDesign("Column1");
frameManager.SetDesignProcedure("Column1", 1); // Steel Frame Design

// Assign to pier
frameManager.SetPier("Column1", "Pier1");

// Set as concrete beam
frameManager.SetConcreteDesign("Beam1");
frameManager.SetSpandrel("Beam1", "Spandrel1");
```

### Lateral Bracing
```csharp
// Full length bracing
frameManager.SetFullLateralBracing("Beam1", 
    BracingType.Uniform, BracingLocation.Top);

// Point bracing at 1/3 points
frameManager.SetPointLateralBracing("Beam1", 
    BracingType.Point, BracingLocation.Top, distance: 0.33);
frameManager.SetPointLateralBracing("Beam1", 
    BracingType.Point, BracingLocation.Top, distance: 0.67);
```

### Advanced Features
```csharp
// Get transformation matrix
double[] matrix = frameManager.GetTransformationMatrix("Frame1");

// Get plastic hinges
var hinges = frameManager.GetHingeAssignments("Frame1");
foreach (var hinge in hinges)
{
    Console.WriteLine($"Hinge {hinge.HingeNumber}: {hinge.HingeTypeDescription}");
}

// Get supports
var (support1, type1, support2, type2) = frameManager.GetSupports("Beam1");
Console.WriteLine($"Supported by: {support1} ({type1}) and {support2} ({type2})");

// Check if curved
string frameType = frameManager.GetFrameType("Frame1");
if (frameType == "Curved")
{
    var curveData = frameManager.GetCurveData("Frame1");
    Console.WriteLine($"Curve type: {curveData.CurveType}, Points: {curveData.NumberOfPoints}");
}
```

### Mass and Springs
```csharp
// Add mass per length
frameManager.SetMass("Frame1", massPerLength: 0.5);

// Assign line spring
frameManager.SetSpringAssignment("Frame1", "ElasticFoundation");

// Get mass
double mass = frameManager.GetMass("Frame1");
```

### Querying Information
```csharp
// Get complete frame
var frame = frameManager.GetFrame("Frame1");

// Get frames on story
string[] framesOnStory = frameManager.GetFramesOnStory("Story2");

// Get label
var (label, story) = frameManager.GetLabelFromName("Frame1");

// Get all frames
var allFrames = frameManager.GetAllFrames();
```

## Error Handling
All methods throw `EtabsException` with detailed error information when ETABS API calls fail.

## Logging
All operations are logged using `ILogger` for debugging and tracking.

## Thread Safety
Not thread-safe. Use appropriate synchronization when accessing from multiple threads.

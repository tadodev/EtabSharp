# SetLoadDistributed Method Usage Examples

The `SetLoadDistributed` method assigns distributed loads to frame objects in ETABS. This implementation follows the ETABS API specification and supports all 11 load directions and both force and moment loads.

## Method Signatures

### 1. Using FrameDistributedLoad Model
```csharp
int SetLoadDistributed(string frameName, FrameDistributedLoad load, bool replace = true, eItemType itemType = eItemType.Objects)
```

### 2. Using Individual Parameters
```csharp
int SetLoadDistributed(string name, string loadPattern, int loadType, int direction,
    double startDistance, double endDistance, double startLoad, double endLoad,
    string coordinateSystem = "Global", bool isRelativeDistance = true, bool replace = true, eItemType itemType = eItemType.Objects)
```

## Load Types

| LoadType | Description | Units |
|----------|-------------|-------|
| 1 | Force per unit length | [F/L] |
| 2 | Moment per unit length | [FL/L] |

## Load Directions (1-11)

| Direction | Description |
|-----------|-------------|
| 1 | Local 1 axis (only applies when CSys is Local) |
| 2 | Local 2 axis (only applies when CSys is Local) |
| 3 | Local 3 axis (only applies when CSys is Local) |
| 4 | Global X direction |
| 5 | Global Y direction |
| 6 | Global Z direction |
| 7 | Projected Global X direction |
| 8 | Projected Global Y direction |
| 9 | Projected Global Z direction |
| 10 | Gravity direction (positive gravity is in negative Global Z) |
| 11 | Projected Gravity direction |

## Distance Parameters

- **Relative Distance (isRelativeDistance = true)**: Distances are specified as fractions (0.0 to 1.0) of the frame length
- **Absolute Distance (isRelativeDistance = false)**: Distances are specified in actual length units

## Usage Examples

### Example 1: Basic Uniform Gravity Load
```csharp
// Apply a uniform gravity load of 2.5 kip/ft over entire beam length
var result = frameManager.SetLoadDistributed("Beam1", "DEAD", 1, 10, 0.0, 1.0, 2.5, 2.5, "Global", true, true);
if (result == 0)
{
    Console.WriteLine("Uniform gravity load applied successfully");
}
```

### Example 2: Using FrameDistributedLoad Model
```csharp
// Create a uniform gravity load using the model
var gravityLoad = FrameDistributedLoad.CreateGravityLoad("Beam1", "DEAD", 2.5);
var result = frameManager.SetLoadDistributed("Beam1", gravityLoad);
```

### Example 3: Triangular Load Distribution
```csharp
// Apply triangular load: 0 at start, 3.0 kip/ft at end
var triangularLoad = FrameDistributedLoad.CreateTriangularLoad("Beam1", "LIVE", 0.0, 3.0);
var result = frameManager.SetLoadDistributed("Beam1", triangularLoad);
```

### Example 4: Partial Load (Middle Third of Beam)
```csharp
// Apply uniform load only to middle third of beam
var partialLoad = FrameDistributedLoad.CreatePartialLoad("Beam1", "EQUIPMENT", 5.0, 0.33, 0.67);
var result = frameManager.SetLoadDistributed("Beam1", partialLoad);
```

### Example 5: Absolute Distance Load
```csharp
// Apply load from 2 ft to 8 ft along a 12 ft beam (absolute distances)
var result = frameManager.SetLoadDistributed("Beam1", "LIVE", 1, 10, 2.0, 8.0, 1.5, 1.5, "Global", false, true);
```

### Example 6: Lateral Load (Local 2-axis)
```csharp
// Apply lateral load in local 2-axis direction
var result = frameManager.SetLoadDistributed("Column1", "WIND", 1, 2, 0.0, 1.0, 0.8, 0.8, "Local", true, true);
```

### Example 7: Projected Loads
```csharp
// Apply projected gravity load (follows gravity regardless of member orientation)
var result1 = frameManager.SetLoadDistributed("Brace1", "DEAD", 1, 11, 0.0, 1.0, 1.2, 1.2, "Global", true, true);

// Apply projected Global X load
var result2 = frameManager.SetLoadDistributed("Frame1", "WIND", 1, 7, 0.0, 1.0, 2.0, 2.0, "Global", true, true);
```

### Example 8: Distributed Moment Load
```csharp
// Apply distributed torsional moment
var result = frameManager.SetLoadDistributed("Beam1", "TORSION", 2, 1, 0.0, 1.0, 5.0, 5.0, "Local", true, true);
```

### Example 9: Group Assignment
```csharp
// Apply load to all frames in a group
var result = frameManager.SetLoadDistributed("BeamGroup", "LIVE", 1, 10, 0.0, 1.0, 2.0, 2.0, "Global", true, true, eItemType.Group);
```

### Example 10: Complex Load Pattern
```csharp
// Apply multiple loads to the same frame
var loads = new[]
{
    new { Pattern = "DEAD", StartLoad = 2.5, EndLoad = 2.5, Direction = 10 }, // Uniform gravity
    new { Pattern = "LIVE", StartLoad = 0.0, EndLoad = 4.0, Direction = 10 }, // Triangular gravity
    new { Pattern = "WIND_X", StartLoad = 1.0, EndLoad = 1.0, Direction = 4 }, // Uniform Global X
    new { Pattern = "WIND_Y", StartLoad = 0.5, EndLoad = 0.5, Direction = 5 }  // Uniform Global Y
};

foreach (var load in loads)
{
    var result = frameManager.SetLoadDistributed("Frame1", load.Pattern, 1, load.Direction, 
        0.0, 1.0, load.StartLoad, load.EndLoad);
    Console.WriteLine($"Applied {load.Pattern}: {(result == 0 ? "Success" : "Failed")}");
}
```

### Example 11: Using Factory Methods
```csharp
// Create different types of loads using factory methods
var uniformLoad = FrameDistributedLoad.CreateUniformLoad("Beam1", "DEAD", 2.5, 10, "Global");
var gravityLoad = FrameDistributedLoad.CreateGravityLoad("Beam1", "LIVE", 1.8, useProjected: false);
var projectedGravityLoad = FrameDistributedLoad.CreateGravityLoad("Brace1", "DEAD", 1.2, useProjected: true);
var triangularLoad = FrameDistributedLoad.CreateTriangularLoad("Beam1", "WIND", 0.0, 2.0, 6, "Global");
var partialLoad = FrameDistributedLoad.CreatePartialLoad("Beam1", "EQUIPMENT", 8.0, 0.25, 0.75, 10, true, "Global");

// Apply all loads
var loads = new[] { uniformLoad, gravityLoad, projectedGravityLoad, triangularLoad, partialLoad };
foreach (var load in loads)
{
    var result = frameManager.SetLoadDistributed("Beam1", load);
    Console.WriteLine($"Applied {load.LoadPattern} ({load.GetDirectionDescription()}): {(result == 0 ? "Success" : "Failed")}");
}
```

## Load Validation

The `FrameDistributedLoad` class includes validation methods:

```csharp
var load = new FrameDistributedLoad("Beam1", "DEAD", 2.5, 2.5, 1, 10, 0.0, 1.0, "Global", true);

// Check if load parameters are valid
if (load.IsValid())
{
    Console.WriteLine("Load parameters are valid");
}

// Check load characteristics
if (load.IsUniform())
{
    Console.WriteLine("This is a uniform load");
}

if (load.IsGravityLoad())
{
    Console.WriteLine("This is a gravity load");
}

if (load.IsProjectedLoad())
{
    Console.WriteLine("This is a projected load");
}

if (load.IsLocalLoad())
{
    Console.WriteLine("This load is in local coordinates");
}

// Get descriptions
Console.WriteLine($"Load type: {load.GetLoadTypeDescription()}");
Console.WriteLine($"Load direction: {load.GetDirectionDescription()}");
```

## Common Load Scenarios

### Beam Dead Load
```csharp
// Typical beam dead load (self-weight + superimposed)
var deadLoad = FrameDistributedLoad.CreateGravityLoad("Beam1", "DEAD", 3.2); // kip/ft
var result = frameManager.SetLoadDistributed("Beam1", deadLoad);
```

### Beam Live Load
```csharp
// Uniform live load on beam
var liveLoad = FrameDistributedLoad.CreateUniformLoad("Beam1", "LIVE", 2.0, 10, "Global");
var result = frameManager.SetLoadDistributed("Beam1", liveLoad);
```

### Wind Load on Frame
```csharp
// Wind load in Global X direction
var windLoad = FrameDistributedLoad.CreateUniformLoad("Column1", "WIND_X", 0.5, 4, "Global");
var result = frameManager.SetLoadDistributed("Column1", windLoad);
```

### Equipment Load (Partial)
```csharp
// Equipment load over middle 6 feet of 20-foot beam
var equipmentLoad = FrameDistributedLoad.CreatePartialLoad("Beam1", "EQUIPMENT", 10.0, 7.0, 13.0, 10, false, "Global");
var result = frameManager.SetLoadDistributed("Beam1", equipmentLoad);
```

### Trapezoidal Snow Load
```csharp
// Snow load varying from 1.5 to 3.0 psf
var snowLoad = FrameDistributedLoad.CreateTriangularLoad("Roof_Beam", "SNOW", 1.5, 3.0, 10, "Global");
var result = frameManager.SetLoadDistributed("Roof_Beam", snowLoad);
```

## Error Handling

```csharp
try
{
    var result = frameManager.SetLoadDistributed("Beam1", "DEAD", 1, 10, 0.0, 1.0, 2.5, 2.5);
    if (result != 0)
    {
        Console.WriteLine($"Failed to apply load. Error code: {result}");
    }
}
catch (EtabsException ex)
{
    Console.WriteLine($"ETABS Error: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid parameters: {ex.Message}");
}
```

## Best Practices

1. **Use Relative Distances**: For most applications, use relative distances (0-1) as they automatically scale with frame length
2. **Validate Loads**: Always check `load.IsValid()` before applying loads
3. **Consistent Units**: Ensure load values are in consistent units with your model
4. **Load Combinations**: Apply individual load patterns separately, let ETABS handle combinations
5. **Direction Selection**: Use gravity direction (10) for typical dead/live loads, local directions for member-specific loads
6. **Projected Loads**: Use projected directions (7-9, 11) for loads that should maintain direction regardless of member orientation

## Notes

- The positive gravity direction (Dir = 10 and 11) is in the negative Global Z direction
- When using Local coordinate system, only directions 1-3 are valid
- The `replace` parameter determines whether to replace existing loads or add to them
- Use `eItemType.Group` to apply loads to all frames in a group
- Use `eItemType.SelectedObjects` to apply loads to currently selected frames
- Distances must satisfy: startDistance ≤ endDistance
- For relative distances: 0 ≤ distance ≤ 1
# SetLoadUniform Method Usage Examples

The `SetLoadUniform` method assigns uniform loads to area objects in ETABS. This implementation follows the ETABS API specification and supports all 11 load directions.

## Method Signatures

### 1. Using AreaUniformLoad Model
```csharp
int SetLoadUniform(string areaName, AreaUniformLoad load, bool replace = true, eItemType itemType = eItemType.Objects)
```

### 2. Using Individual Parameters
```csharp
int SetLoadUniform(string name, string loadPattern, double value, int direction, 
    bool replace = true, string coordinateSystem = "Global", eItemType itemType = eItemType.Objects)
```

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

## Usage Examples

### Example 1: Basic Gravity Load
```csharp
// Apply a gravity load of 100 psf to an area
var result = areaManager.SetLoadUniform("Area1", "DEAD", 100.0, 10, true, "Global");
if (result == 0)
{
    Console.WriteLine("Gravity load applied successfully");
}
```

### Example 2: Using AreaUniformLoad Model
```csharp
// Create a gravity load using the convenience method
var gravityLoad = AreaUniformLoad.CreateGravityLoad("Area1", "DEAD", 100.0);
var result = areaManager.SetLoadUniform("Area1", gravityLoad);
```

### Example 3: Pressure Load (Normal to Surface)
```csharp
// Apply pressure load normal to the area surface
var pressureLoad = AreaUniformLoad.CreatePressureLoad("Area1", "WIND", 25.0);
var result = areaManager.SetLoadUniform("Area1", pressureLoad);
```

### Example 4: Projected Loads
```csharp
// Apply projected gravity load
var result1 = areaManager.SetLoadUniform("Area1", "LIVE", 50.0, 11, true, "Global");

// Apply projected Global Z load
var result2 = areaManager.SetLoadUniform("Area1", "WIND", 30.0, 9, true, "Global");
```

### Example 5: Local Coordinate System Load
```csharp
// Apply load in local 3-axis direction (normal to area)
var result = areaManager.SetLoadUniform("Area1", "PRESSURE", 15.0, 3, true, "Local");
```

### Example 6: Group Assignment
```csharp
// Apply load to all areas in a group
var result = areaManager.SetLoadUniform("FloorGroup", "LIVE", 40.0, 10, true, "Global", eItemType.Group);
```

### Example 7: Multiple Load Directions
```csharp
// Apply loads in different directions
var loads = new[]
{
    new { Pattern = "DEAD", Value = 100.0, Direction = 10 }, // Gravity
    new { Pattern = "LIVE", Value = 40.0, Direction = 10 },  // Gravity
    new { Pattern = "WIND_X", Value = 20.0, Direction = 4 }, // Global X
    new { Pattern = "WIND_Y", Value = 20.0, Direction = 5 }  // Global Y
};

foreach (var load in loads)
{
    var result = areaManager.SetLoadUniform("Area1", load.Pattern, load.Value, load.Direction);
    Console.WriteLine($"Applied {load.Pattern}: {(result == 0 ? "Success" : "Failed")}");
}
```

### Example 8: Using Convenience Factory Methods
```csharp
// Create different types of loads using factory methods
var gravityLoad = AreaUniformLoad.CreateGravityLoad("Area1", "DEAD", 100.0, useProjected: false);
var projectedGravityLoad = AreaUniformLoad.CreateGravityLoad("Area1", "LIVE", 50.0, useProjected: true);
var globalZLoad = AreaUniformLoad.CreateGlobalZLoad("Area1", "WIND", 25.0, useProjected: false);
var projectedGlobalZLoad = AreaUniformLoad.CreateGlobalZLoad("Area1", "SEISMIC", 30.0, useProjected: true);

// Apply all loads
var loads = new[] { gravityLoad, projectedGravityLoad, globalZLoad, projectedGlobalZLoad };
foreach (var load in loads)
{
    var result = areaManager.SetLoadUniform("Area1", load);
    Console.WriteLine($"Applied {load.LoadPattern} ({load.GetDirectionDescription()}): {(result == 0 ? "Success" : "Failed")}");
}
```

## Load Validation

The `AreaUniformLoad` class includes validation methods:

```csharp
var load = new AreaUniformLoad("Area1", "DEAD", 100.0, 10, "Global");

// Check if load parameters are valid
if (load.IsValid())
{
    Console.WriteLine("Load parameters are valid");
}

// Check load type
if (load.IsGravityLoad())
{
    Console.WriteLine("This is a gravity load");
}

if (load.IsProjectedLoad())
{
    Console.WriteLine("This is a projected load");
}

// Get description
Console.WriteLine($"Load direction: {load.GetDirectionDescription()}");
```

## Error Handling

```csharp
try
{
    var result = areaManager.SetLoadUniform("Area1", "DEAD", 100.0, 10);
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

## Notes

- The positive gravity direction (Dir = 10 and 11) is in the negative Global Z direction
- When using Local coordinate system, only directions 1-3 are valid
- The `replace` parameter determines whether to replace existing loads or add to them
- Use `eItemType.Group` to apply loads to all areas in a group
- Use `eItemType.SelectedObjects` to apply loads to currently selected areas
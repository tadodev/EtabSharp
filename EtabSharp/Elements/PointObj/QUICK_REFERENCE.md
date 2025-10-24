# Point Object Manager - Quick Reference

## Common Operations

### Creating Points
```csharp
// Basic point
string p1 = pointManager.AddPoint(x: 0, y: 0, z: 0);

// Named point
string p2 = pointManager.AddPoint(x: 10, y: 5, z: 3, userName: "Column1");
```

### Supports (Restraints)
```csharp
// Fixed support (all DOFs restrained)
pointManager.SetFixedSupport("1");

// Pinned support (translations restrained, rotations free)
pointManager.SetPinnedSupport("2");

// Roller support (Uz only)
pointManager.SetRollerSupport("3");

// Custom restraint
var restraint = new PointRestraint { Ux = true, Uy = true, Uz = true };
pointManager.SetRestraint("4", restraint);
```

### Spring Supports
```csharp
// Uniform spring (equal in all directions)
pointManager.SetUniformSpringSupport("1", translationalStiffness: 1000);

// Soil spring (different horizontal/vertical)
pointManager.SetSoilSpringSupport("2", horizontalStiffness: 500, verticalStiffness: 1000);

// Custom spring
var spring = new PointSpring { Kx = 100, Ky = 100, Kz = 500 };
pointManager.SetSpring("3", spring);
```

### Loads
```csharp
// Vertical load (downward)
pointManager.SetVerticalLoad("1", "DEAD", force: 100.0);

// Horizontal load
pointManager.SetHorizontalLoad("2", "WIND", fx: 50.0, fy: 30.0);

// Torsional moment
pointManager.SetTorsionalMoment("3", "LIVE", moment: 20.0);

// Support settlement
pointManager.SetSupportSettlement("4", "SETTLE", settlement: 0.05);

// Custom load
var load = new PointLoad
{
    LoadPattern = "CUSTOM",
    Fx = 10, Fy = 20, Fz = -50,
    Mx = 5, My = 10, Mz = 15
};
pointManager.SetLoadForce("5", load);
```

### Mass
```csharp
// Uniform mass
pointManager.SetUniformMass("1", mass: 10.0);

// Custom mass
var mass = new PointMass { Mx = 5, My = 5, Mz = 10 };
pointManager.SetMass("2", mass);

// Mass by weight
pointManager.SetMassByWeight("3", mass);
```

### Diaphragm
```csharp
// Assign to diaphragm
pointManager.SetDiaphragm("1", eDiaphragmOption.Diaphragm, "D1");

// Get diaphragm
string diaphragm = pointManager.GetDiaphragm("1");
```

### Panel Zones
```csharp
// Elastic from elements
var pz1 = PointPanelZone.ElasticFromElements("Joint1");
pointManager.SetPanelZone("Joint1", pz1);

// User-defined elastic
var pz2 = PointPanelZone.ElasticUserDefined("Joint2", thickness: 0.5, k1: 1.0, k2: 1.0);
pointManager.SetPanelZone("Joint2", pz2);

// Nonlinear
var pz3 = PointPanelZone.NonlinearUserDefined("Joint3", linkProperty: "PZ_Link");
pointManager.SetPanelZone("Joint3", pz3);
```

### Querying Information
```csharp
// Get point with coordinates
var point = pointManager.GetPoint("1");

// Get complete point (all properties)
var completePoint = pointManager.GetCompletePoint("1");

// Get all points
var allPoints = pointManager.GetAllPoints();

// Get point names
string[] names = pointManager.GetNameList();

// Count points
int count = pointManager.Count();
```

### Connectivity
```csharp
// Check if connected
bool isConnected = pointManager.IsConnected("1");

// Get connected objects
var connectivity = pointManager.GetConnectedObjects("1");
Console.WriteLine($"Frames: {connectivity.ConnectedFrames.Count}");
Console.WriteLine($"Areas: {connectivity.ConnectedAreas.Count}");
```

### Story & Label
```csharp
// Get points on story
string[] pointsOnStory = pointManager.GetPointsOnStory("Story1");

// Get name from label
string name = pointManager.GetNameFromLabel(label: "A", story: "Story1");

// Get all labels
var (names, labels, stories) = pointManager.GetLabelNameList();
```

### Groups
```csharp
// Assign to group
pointManager.SetGroupAssignment("1", "Supports");

// Get groups
string[] groups = pointManager.GetGroupAssignment("1");

// Remove from group
pointManager.SetGroupAssignment("1", "Supports", remove: true);
```

### Selection
```csharp
// Select point
pointManager.SetSelected("1", selected: true);

// Check if selected
bool isSelected = pointManager.IsSelected("1");
```

### Advanced Coordinates
```csharp
// Cylindrical coordinates
var (r, theta, z) = pointManager.GetCoordCylindrical("1");

// Spherical coordinates
var (r, a, b) = pointManager.GetCoordSpherical("1");
```

### Local Axes & Transformations
```csharp
// Get local axes angles
var (a, b, c, advanced) = pointManager.GetLocalAxes("1");

// Get transformation matrix
double[] matrix = pointManager.GetTransformationMatrix("1", isGlobal: true);
```

### Count Methods
```csharp
// Count various assignments
int restraintCount = pointManager.CountRestraint();
int springCount = pointManager.CountSpring();
int panelZoneCount = pointManager.CountPanelZone();
int forceLoadCount = pointManager.CountLoadForce();
int displLoadCount = pointManager.CountLoadDispl();

// Count for specific point/pattern
int loadCount = pointManager.CountLoadForce(pointName: "1", loadPattern: "DEAD");
```

### Deletion
```csharp
// Delete restraint
pointManager.DeleteRestraint("1");

// Delete spring
pointManager.DeleteSpring("1");

// Delete mass
pointManager.DeleteMass("1");

// Delete panel zone
pointManager.DeletePanelZone("1");

// Delete loads
pointManager.DeleteLoadForce("1", "DEAD");
pointManager.DeleteLoadDisplacement("1", "SETTLE");

// Delete unconnected special points
int deletedCount = pointManager.DeleteUnconnectedSpecialPoints();
```

## Common Patterns

### Setting Up a Foundation Point
```csharp
string foundation = pointManager.AddPoint(0, 0, 0, userName: "Foundation1");
pointManager.SetFixedSupport(foundation);
pointManager.SetSoilSpringSupport(foundation, horizontalStiffness: 1000, verticalStiffness: 2000);
```

### Setting Up a Column Base
```csharp
string columnBase = pointManager.AddPoint(5, 5, 0, userName: "ColumnBase");
pointManager.SetPinnedSupport(columnBase);
pointManager.SetVerticalLoad(columnBase, "DEAD", force: 500);
pointManager.SetUniformMass(columnBase, mass: 50);
```

### Beam-Column Joint with Panel Zone
```csharp
string joint = pointManager.AddPoint(10, 10, 12, userName: "BeamColumnJoint");
var panelZone = PointPanelZone.ElasticFromElements(joint);
pointManager.SetPanelZone(joint, panelZone);
pointManager.SetDiaphragm(joint, eDiaphragmOption.Diaphragm, "Floor3");
```

### Checking Point Status
```csharp
var point = pointManager.GetCompletePoint("1");
Console.WriteLine($"Point: {point.Name} at ({point.X}, {point.Y}, {point.Z})");
Console.WriteLine($"Restraint: {point.Restraint?.ToString() ?? "None"}");
Console.WriteLine($"Spring: {point.Spring?.ToString() ?? "None"}");
Console.WriteLine($"Mass: {point.Mass?.ToString() ?? "None"}");
Console.WriteLine($"Diaphragm: {point.DiaphragmName}");
Console.WriteLine($"Groups: {string.Join(", ", point.Groups)}");
```

## Error Handling
```csharp
try
{
    pointManager.SetFixedSupport("NonExistentPoint");
}
catch (EtabsException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine($"API Method: {ex.ApiMethod}");
    Console.WriteLine($"Return Code: {ex.ReturnCode}");
}
```

## Tips
1. Always check if a point exists before modifying it
2. Use convenience methods for common support types
3. Use `GetCompletePoint()` to get all properties at once
4. Check connectivity before deleting points
5. Use groups to organize points by function
6. Use labels and stories for better organization
7. Count methods are useful for validation and reporting

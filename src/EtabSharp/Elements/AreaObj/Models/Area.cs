using ETABSv1;

namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents an area object in the ETABS model with its properties and connectivity.
/// </summary>
public class Area
{
    /// <summary>
    /// Gets or sets the unique name of the area object.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the label of the area object.
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the story name where the area is located.
    /// </summary>
    public string Story { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the area property assigned to this area.
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the material override name (if any).
    /// </summary>
    public string MaterialOverride { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the design orientation of the area.
    /// </summary>
    public eAreaDesignOrientation DesignOrientation { get; set; } = eAreaDesignOrientation.Null;

    /// <summary>
    /// Gets or sets the local axis angle in degrees.
    /// </summary>
    public double LocalAxisAngle { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets whether advanced local axes are used.
    /// </summary>
    public bool IsAdvancedLocalAxes { get; set; } = false;

    /// <summary>
    /// Gets or sets the point names that define the area boundary.
    /// </summary>
    public List<string> PointNames { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the coordinates of the area boundary points.
    /// </summary>
    public List<AreaCoordinate> Coordinates { get; set; } = new List<AreaCoordinate>();

    /// <summary>
    /// Gets or sets the area of the object (calculated).
    /// </summary>
    public double AreaValue { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets whether the area is marked as an opening.
    /// </summary>
    public bool IsOpening { get; set; } = false;

    /// <summary>
    /// Gets or sets whether edge constraints exist.
    /// </summary>
    public bool HasEdgeConstraints { get; set; } = false;

    /// <summary>
    /// Gets or sets the pier assignment (if any).
    /// </summary>
    public string PierName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the spandrel assignment (if any).
    /// </summary>
    public string SpandrelName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the diaphragm assignment (if any).
    /// </summary>
    public string DiaphragmName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the spring property assignment (if any).
    /// </summary>
    public string SpringPropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the mass per unit area.
    /// </summary>
    public double MassPerArea { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the property modifiers for the area.
    /// </summary>
    public AreaModifiers? Modifiers { get; set; }

    /// <summary>
    /// Gets or sets the group assignments for the area.
    /// </summary>
    public List<string> Groups { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets whether the area is currently selected.
    /// </summary>
    public bool IsSelected { get; set; } = false;

    /// <summary>
    /// Gets or sets the GUID of the area object.
    /// </summary>
    public string GUID { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the loads assigned to this area.
    /// </summary>
    public AreaLoads? Loads { get; set; }

    /// <summary>
    /// Gets or sets the offsets for the area points.
    /// </summary>
    public AreaOffsets? Offsets { get; set; }

    /// <summary>
    /// Gets or sets the curved edge information.
    /// </summary>
    public AreaCurvedEdges? CurvedEdges { get; set; }

    /// <summary>
    /// Returns a string representation of the area object.
    /// </summary>
    /// <returns>String containing area name and basic properties</returns>
    public override string ToString()
    {
        return $"Area: {Name} | Property: {PropertyName} | Story: {Story} | Points: {PointNames.Count}";
    }

    /// <summary>
    /// Calculates the area value from coordinates if available.
    /// </summary>
    /// <returns>Calculated area value</returns>
    public double CalculateArea()
    {
        if (Coordinates.Count < 3)
            return 0.0;

        // Simple polygon area calculation using shoelace formula
        double area = 0.0;
        int n = Coordinates.Count;

        for (int i = 0; i < n; i++)
        {
            int j = (i + 1) % n;
            area += Coordinates[i].X * Coordinates[j].Y;
            area -= Coordinates[j].X * Coordinates[i].Y;
        }

        return Math.Abs(area) / 2.0;
    }

    /// <summary>
    /// Gets the centroid of the area from coordinates.
    /// </summary>
    /// <returns>Centroid coordinates</returns>
    public AreaCoordinate GetCentroid()
    {
        if (Coordinates.Count == 0)
            return new AreaCoordinate();

        double sumX = Coordinates.Sum(c => c.X);
        double sumY = Coordinates.Sum(c => c.Y);
        double sumZ = Coordinates.Sum(c => c.Z);
        int count = Coordinates.Count;

        return new AreaCoordinate
        {
            X = sumX / count,
            Y = sumY / count,
            Z = sumZ / count
        };
    }

    /// <summary>
    /// Checks if the area has any loads assigned.
    /// </summary>
    /// <returns>True if loads are assigned</returns>
    public bool HasLoads()
    {
        return Loads != null && (
            Loads.UniformLoads.Count > 0 ||
            Loads.UniformToFrameLoads.Count > 0 ||
            Loads.WindPressureLoads.Count > 0 ||
            Loads.TemperatureLoads.Count > 0
        );
    }

    /// <summary>
    /// Checks if the area has any design assignments.
    /// </summary>
    /// <returns>True if design assignments exist</returns>
    public bool HasDesignAssignments()
    {
        return !string.IsNullOrEmpty(PierName) ||
               !string.IsNullOrEmpty(SpandrelName) ||
               !string.IsNullOrEmpty(DiaphragmName);
    }
}
namespace EtabSharp.Elements.Selection.Models;


/// <summary>
/// Provides methods for managing object selection in the ETABS model.
/// This interface handles selection, deselection, and retrieval of selected objects
/// including points, frames, cables, tendons, areas, solids, and links.
/// </summary>
public class SelectedObjects
{
    /// <summary>
    /// Total number of selected objects across all types.
    /// </summary>
    public int NumberItems { get; set; }

    /// <summary>
    /// Array of object types for each selected object.
    /// Values: 1=Point, 2=Frame, 3=Cable, 4=Tendon, 5=Area, 6=Solid, 7=Link
    /// </summary>
    public int[] ObjectType { get; set; } = Array.Empty<int>();

    /// <summary>
    /// Array of object names corresponding to each selected object.
    /// </summary>
    public string[] ObjectName { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected point objects.
    /// </summary>
    public int NumberPoints { get; set; }

    /// <summary>
    /// Names of selected point objects.
    /// </summary>
    public string[] PointNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected frame objects.
    /// </summary>
    public int NumberFrames { get; set; }

    /// <summary>
    /// Names of selected frame objects.
    /// </summary>
    public string[] FrameNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected cable objects.
    /// </summary>
    public int NumberCables { get; set; }

    /// <summary>
    /// Names of selected cable objects.
    /// </summary>
    public string[] CableNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected tendon objects.
    /// </summary>
    public int NumberTendons { get; set; }

    /// <summary>
    /// Names of selected tendon objects.
    /// </summary>
    public string[] TendonNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected area objects.
    /// </summary>
    public int NumberAreas { get; set; }

    /// <summary>
    /// Names of selected area objects.
    /// </summary>
    public string[] AreaNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected solid objects.
    /// </summary>
    public int NumberSolids { get; set; }

    /// <summary>
    /// Names of selected solid objects.
    /// </summary>
    public string[] SolidNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Number of selected link objects.
    /// </summary>
    public int NumberLinks { get; set; }

    /// <summary>
    /// Names of selected link objects.
    /// </summary>
    public string[] LinkNames { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets an object by its index in the selection.
    /// </summary>
    /// <param name="index">Zero-based index (0 to NumberItems-1)</param>
    /// <returns>Tuple of (objectType, objectName)</returns>
    public (int ObjectType, string ObjectName) GetObjectAt(int index)
    {
        if (index < 0 || index >= NumberItems)
            throw new IndexOutOfRangeException($"Index {index} is out of range. Valid range: 0 to {NumberItems - 1}");

        return (ObjectType[index], ObjectName[index]);
    }

    /// <summary>
    /// Gets the object type name from the numeric code.
    /// </summary>
    /// <param name="objectType">Object type code (1-7)</param>
    /// <returns>Object type name</returns>
    public static string GetObjectTypeName(int objectType)
    {
        return objectType switch
        {
            1 => "Point",
            2 => "Frame",
            3 => "Cable",
            4 => "Tendon",
            5 => "Area",
            6 => "Solid",
            7 => "Link",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Returns a summary string of selected objects.
    /// </summary>
    public override string ToString()
    {
        return $"Selected: {NumberPoints} points, {NumberFrames} frames, " +
               $"{NumberCables} cables, {NumberTendons} tendons, {NumberAreas} areas, " +
               $"{NumberSolids} solids, {NumberLinks} links (Total: {NumberItems})";
    }

    /// <summary>
    /// Checks if any objects are selected.
    /// </summary>
    public bool HasSelection => NumberItems > 0;
}
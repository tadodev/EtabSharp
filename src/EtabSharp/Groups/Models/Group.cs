namespace EtabSharp.Groups.Models;

/// <summary>
/// Represents a group in the ETABS model.
/// Groups are used to organize structural elements for selection, design, and output purposes.
/// </summary>
public class Group
{
    /// <summary>
    /// Gets or sets the unique name of the group.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the color assigned to the group (RGB integer).
    /// -1 indicates automatic color assignment.
    /// </summary>
    public int Color { get; set; } = -1;

    /// <summary>
    /// Gets or sets whether this group is specified for selection purposes.
    /// </summary>
    public bool SpecifiedForSelection { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for section cut definition.
    /// </summary>
    public bool SpecifiedForSectionCutDefinition { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for steel design.
    /// </summary>
    public bool SpecifiedForSteelDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for concrete design.
    /// </summary>
    public bool SpecifiedForConcreteDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for aluminum design.
    /// </summary>
    public bool SpecifiedForAluminumDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for cold formed design.
    /// </summary>
    public bool SpecifiedForColdFormedDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for static nonlinear active stage.
    /// </summary>
    public bool SpecifiedForStaticNLActiveStage { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for bridge response output.
    /// </summary>
    public bool SpecifiedForBridgeResponseOutput { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for auto seismic output.
    /// </summary>
    public bool SpecifiedForAutoSeismicOutput { get; set; } = false;

    /// <summary>
    /// Gets or sets whether this group is specified for auto wind output.
    /// </summary>
    public bool SpecifiedForAutoWindOutput { get; set; } = false;

    /// <summary>
    /// Gets or sets whether this group is specified for mass and weight.
    /// </summary>
    public bool SpecifiedForMassAndWeight { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for steel joist design.
    /// </summary>
    public bool SpecifiedForSteelJoistDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for wall design.
    /// </summary>
    public bool SpecifiedForWallDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for base plate design.
    /// </summary>
    public bool SpecifiedForBasePlateDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this group is specified for connection design.
    /// </summary>
    public bool SpecifiedForConnectionDesign { get; set; } = true;

    /// <summary>
    /// Gets or sets the list of assignments in this group.
    /// </summary>
    public List<GroupAssignment> Assignments { get; set; } = new List<GroupAssignment>();

    /// <summary>
    /// Gets the count of objects in this group.
    /// </summary>
    public int ObjectCount => Assignments.Count;

    /// <summary>
    /// Creates a default group with standard settings.
    /// </summary>
    public static Group CreateDefault(string name)
    {
        return new Group
        {
            Name = name,
            Color = -1,
            SpecifiedForSelection = true,
            SpecifiedForSectionCutDefinition = true,
            SpecifiedForSteelDesign = true,
            SpecifiedForConcreteDesign = true,
            SpecifiedForAluminumDesign = true,
            SpecifiedForColdFormedDesign = true,
            SpecifiedForStaticNLActiveStage = true,
            SpecifiedForBridgeResponseOutput = true,
            SpecifiedForAutoSeismicOutput = false,
            SpecifiedForAutoWindOutput = false,
            SpecifiedForMassAndWeight = true,
            SpecifiedForSteelJoistDesign = true,
            SpecifiedForWallDesign = true,
            SpecifiedForBasePlateDesign = true,
            SpecifiedForConnectionDesign = true
        };
    }

    /// <summary>
    /// Creates a group for design purposes.
    /// </summary>
    public static Group CreateForDesign(string name)
    {
        return new Group
        {
            Name = name,
            Color = -1,
            SpecifiedForSelection = true,
            SpecifiedForSectionCutDefinition = false,
            SpecifiedForSteelDesign = true,
            SpecifiedForConcreteDesign = true,
            SpecifiedForAluminumDesign = true,
            SpecifiedForColdFormedDesign = true,
            SpecifiedForStaticNLActiveStage = true,
            SpecifiedForBridgeResponseOutput = false,
            SpecifiedForAutoSeismicOutput = false,
            SpecifiedForAutoWindOutput = false,
            SpecifiedForMassAndWeight = true,
            SpecifiedForSteelJoistDesign = true,
            SpecifiedForWallDesign = true,
            SpecifiedForBasePlateDesign = true,
            SpecifiedForConnectionDesign = true
        };
    }

    /// <summary>
    /// Creates a group for output purposes only.
    /// </summary>
    public static Group CreateForOutput(string name)
    {
        return new Group
        {
            Name = name,
            Color = -1,
            SpecifiedForSelection = true,
            SpecifiedForSectionCutDefinition = true,
            SpecifiedForSteelDesign = false,
            SpecifiedForConcreteDesign = false,
            SpecifiedForAluminumDesign = false,
            SpecifiedForColdFormedDesign = false,
            SpecifiedForStaticNLActiveStage = false,
            SpecifiedForBridgeResponseOutput = true,
            SpecifiedForAutoSeismicOutput = true,
            SpecifiedForAutoWindOutput = true,
            SpecifiedForMassAndWeight = true,
            SpecifiedForSteelJoistDesign = false,
            SpecifiedForWallDesign = false,
            SpecifiedForBasePlateDesign = false,
            SpecifiedForConnectionDesign = false
        };
    }

    /// <summary>
    /// Creates a group for selection purposes only.
    /// </summary>
    public static Group CreateForSelection(string name)
    {
        return new Group
        {
            Name = name,
            Color = -1,
            SpecifiedForSelection = true,
            SpecifiedForSectionCutDefinition = false,
            SpecifiedForSteelDesign = false,
            SpecifiedForConcreteDesign = false,
            SpecifiedForAluminumDesign = false,
            SpecifiedForColdFormedDesign = false,
            SpecifiedForStaticNLActiveStage = false,
            SpecifiedForBridgeResponseOutput = false,
            SpecifiedForAutoSeismicOutput = false,
            SpecifiedForAutoWindOutput = false,
            SpecifiedForMassAndWeight = false,
            SpecifiedForSteelJoistDesign = false,
            SpecifiedForWallDesign = false,
            SpecifiedForBasePlateDesign = false,
            SpecifiedForConnectionDesign = false
        };
    }

    public override string ToString()
    {
        return $"Group: {Name} ({ObjectCount} objects)";
    }
}

/// <summary>
/// Represents an object assignment within a group.
/// </summary>
public class GroupAssignment
{
    /// <summary>
    /// Gets or sets the type of object.
    /// 1 = Point, 2 = Frame, 3 = Cable, 4 = Tendon, 5 = Area, 6 = Solid, 7 = Link
    /// </summary>
    public int ObjectType { get; set; }

    /// <summary>
    /// Gets or sets the name of the object.
    /// </summary>
    public string ObjectName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the object type as a string.
    /// </summary>
    public string ObjectTypeString => ObjectType switch
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

    public override string ToString()
    {
        return $"{ObjectTypeString}: {ObjectName}";
    }
}

/// <summary>
/// Enumeration for object types in groups.
/// </summary>
public enum GroupObjectType
{
    /// <summary>
    /// Point object (joint/node)
    /// </summary>
    Point = 1,

    /// <summary>
    /// Frame object (beam/column)
    /// </summary>
    Frame = 2,

    /// <summary>
    /// Cable object
    /// </summary>
    Cable = 3,

    /// <summary>
    /// Tendon object (post-tensioning)
    /// </summary>
    Tendon = 4,

    /// <summary>
    /// Area object (wall/slab/shell)
    /// </summary>
    Area = 5,

    /// <summary>
    /// Solid object (3D element)
    /// </summary>
    Solid = 6,

    /// <summary>
    /// Link object (spring/damper/isolator)
    /// </summary>
    Link = 7
}

/// <summary>
/// Helper class for converting RGB colors to/from integers.
/// </summary>
public static class GroupColorHelper
{
    /// <summary>
    /// Converts RGB values to an integer color value.
    /// </summary>
    public static int ToColorInt(int red, int green, int blue)
    {
        return red + (green << 8) + (blue << 16);
    }

    /// <summary>
    /// Converts an integer color value to RGB components.
    /// </summary>
    public static (int Red, int Green, int Blue) FromColorInt(int color)
    {
        if (color == -1)
            return (-1, -1, -1); // Automatic color

        int red = color & 0xFF;
        int green = (color >> 8) & 0xFF;
        int blue = (color >> 16) & 0xFF;

        return (red, green, blue);
    }

    /// <summary>
    /// Common predefined colors.
    /// </summary>
    public static class Colors
    {
        public static readonly int Red = ToColorInt(255, 0, 0);
        public static readonly int Green = ToColorInt(0, 255, 0);
        public static readonly int Blue = ToColorInt(0, 0, 255);
        public static readonly int Yellow = ToColorInt(255, 255, 0);
        public static readonly int Cyan = ToColorInt(0, 255, 255);
        public static readonly int Magenta = ToColorInt(255, 0, 255);
        public static readonly int White = ToColorInt(255, 255, 255);
        public static readonly int Black = ToColorInt(0, 0, 0);
        public static readonly int Gray = ToColorInt(128, 128, 128);
        public static readonly int Orange = ToColorInt(255, 165, 0);
        public static readonly int Purple = ToColorInt(128, 0, 128);
        public static readonly int Brown = ToColorInt(165, 42, 42);
        public static readonly int Automatic = -1;
    }
}
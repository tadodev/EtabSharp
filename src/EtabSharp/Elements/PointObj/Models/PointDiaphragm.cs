using ETABSv1;

namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents diaphragm assignment for a point.
/// </summary>
public class PointDiaphragm
{
    /// <summary>
    /// Name of the point
    /// </summary>
    public string PointName { get; set; }

    /// <summary>
    /// Diaphragm option type (0 = None, 1 = FromShellObject)
    /// </summary>
    public int DiaphragmOption { get; set; }

    /// <summary>
    /// Name of the diaphragm (if DiaphragmOption is not None)
    /// </summary>
    public string DiaphragmName { get; set; } = "";

    /// <summary>
    /// Creates a diaphragm assignment
    /// </summary>
    /// <param name="pointName">Name of the point</param>
    /// <param name="diaphragmOption">Diaphragm option</param>
    /// <param name="diaphragmName">Diaphragm name</param>
    public PointDiaphragm(string pointName, int diaphragmOption, string diaphragmName = "")
    {
        PointName = pointName;
        DiaphragmOption = diaphragmOption;
        DiaphragmName = diaphragmName;
    }

    /// <summary>
    /// Creates a diaphragm assignment with no diaphragm
    /// </summary>
    public static PointDiaphragm None(string pointName) => new(pointName, 0);

    /// <summary>
    /// Creates a diaphragm assignment with a named diaphragm
    /// </summary>
    public static PointDiaphragm WithDiaphragm(string pointName, string diaphragmName) => 
        new(pointName, 1, diaphragmName);

    public override string ToString()
    {
        return DiaphragmOption == 0 
            ? $"Point {PointName}: No diaphragm"
            : $"Point {PointName}: Diaphragm '{DiaphragmName}'";
    }
}
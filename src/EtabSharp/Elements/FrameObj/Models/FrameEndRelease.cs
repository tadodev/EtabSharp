namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents release conditions at one end of a frame.
/// </summary>
public class FrameEndReleases
{
    /// <summary>
    /// Gets or sets U1 (axial) release. Index 0 in API array.
    /// </summary>
    public bool U1 { get; set; } = false;

    /// <summary>
    /// Gets or sets U2 (shear in local 2) release. Index 1 in API array.
    /// </summary>
    public bool U2 { get; set; } = false;

    /// <summary>
    /// Gets or sets U3 (shear in local 3) release. Index 2 in API array.
    /// </summary>
    public bool U3 { get; set; } = false;

    /// <summary>
    /// Gets or sets R1 (torsion) release. Index 3 in API array.
    /// </summary>
    public bool R1 { get; set; } = false;

    /// <summary>
    /// Gets or sets R2 (moment about local 2) release. Index 4 in API array.
    /// </summary>
    public bool R2 { get; set; } = false;

    /// <summary>
    /// Gets or sets R3 (moment about local 3) release. Index 5 in API array.
    /// </summary>
    public bool R3 { get; set; } = false;

    /// <summary>
    /// Converts releases to API array format.
    /// </summary>
    public bool[] ToArray()
    {
        return new[] { U1, U2, U3, R1, R2, R3 };
    }

    /// <summary>
    /// Creates releases from API array format.
    /// </summary>
    public static FrameEndReleases FromArray(bool[] array)
    {
        if (array == null || array.Length != 6)
            throw new ArgumentException("Array must have 6 elements", nameof(array));

        return new FrameEndReleases
        {
            U1 = array[0],
            U2 = array[1],
            U3 = array[2],
            R1 = array[3],
            R2 = array[4],
            R3 = array[5]
        };
    }

    /// <summary>
    /// Creates a fully fixed end (no releases).
    /// </summary>
    public static FrameEndReleases Fixed()
    {
        return new FrameEndReleases();
    }

    /// <summary>
    /// Creates a pinned end (all moments released).
    /// </summary>
    public static FrameEndReleases Pinned()
    {
        return new FrameEndReleases
        {
            R1 = true,
            R2 = true,
            R3 = true
        };
    }

    /// <summary>
    /// Creates a roller end (axial and all moments released).
    /// </summary>
    public static FrameEndReleases Roller()
    {
        return new FrameEndReleases
        {
            U1 = true,
            R1 = true,
            R2 = true,
            R3 = true
        };
    }

    public override string ToString()
    {
        var released = new List<string>();
        if (U1) released.Add("U1");
        if (U2) released.Add("U2");
        if (U3) released.Add("U3");
        if (R1) released.Add("R1");
        if (R2) released.Add("R2");
        if (R3) released.Add("R3");
        return released.Count > 0 ? string.Join(",", released) : "Fixed";
    }
}
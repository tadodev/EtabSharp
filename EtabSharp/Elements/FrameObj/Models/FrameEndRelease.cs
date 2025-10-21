namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents release conditions at one end of a frame.
/// </summary>
public class FrameEndRelease
{
    /// <summary>
    /// Axial force release (true = released/pinned)
    /// </summary>
    public bool P { get; set; }

    /// <summary>
    /// Shear force in 2-direction release
    /// </summary>
    public bool V2 { get; set; }

    /// <summary>
    /// Shear force in 3-direction release
    /// </summary>
    public bool V3 { get; set; }

    /// <summary>
    /// Torsion release
    /// </summary>
    public bool T { get; set; }

    /// <summary>
    /// Moment about 2-axis release (major axis bending for typical beam)
    /// </summary>
    public bool M2 { get; set; }

    /// <summary>
    /// Moment about 3-axis release (minor axis bending)
    /// </summary>
    public bool M3 { get; set; }

    /// <summary>
    /// Partial fixity for P (0=released, 1=fixed)
    /// </summary>
    public double FixityP { get; set; } = 1.0;

    /// <summary>
    /// Partial fixity for V2
    /// </summary>
    public double FixityV2 { get; set; } = 1.0;

    /// <summary>
    /// Partial fixity for V3
    /// </summary>
    public double FixityV3 { get; set; } = 1.0;

    /// <summary>
    /// Partial fixity for T
    /// </summary>
    public double FixityT { get; set; } = 1.0;

    /// <summary>
    /// Partial fixity for M2
    /// </summary>
    public double FixityM2 { get; set; } = 1.0;

    /// <summary>
    /// Partial fixity for M3
    /// </summary>
    public double FixityM3 { get; set; } = 1.0;

    /// <summary>
    /// Creates fixed end (no releases)
    /// </summary>
    public static FrameEndRelease Fixed() => new();

    /// <summary>
    /// Creates pinned end (M2, M3 released - typical beam connection)
    /// </summary>
    public static FrameEndRelease Pinned() => new()
    {
        M2 = true,
        M3 = true,
        FixityM2 = 0,
        FixityM3 = 0
    };

    /// <summary>
    /// Converts releases to boolean array [P, V2, V3, T, M2, M3]
    /// </summary>
    public bool[] ReleasesToArray() => new[] { P, V2, V3, T, M2, M3 };

    /// <summary>
    /// Converts fixities to double array [P, V2, V3, T, M2, M3]
    /// </summary>
    public double[] FixitiesToArray() => new[] { FixityP, FixityV2, FixityV3, FixityT, FixityM2, FixityM3 };

    public override string ToString()
    {
        var releases = new List<string>();
        if (P) releases.Add("P");
        if (V2) releases.Add("V2");
        if (V3) releases.Add("V3");
        if (T) releases.Add("T");
        if (M2) releases.Add("M2");
        if (M3) releases.Add("M3");
        return releases.Any() ? string.Join(", ", releases) : "Fixed";
    }
}
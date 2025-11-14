namespace EtabSharp.Elements.FrameObj.Models;


/// <summary>
/// Represents partial fixity values for a frame end.
/// Values are spring stiffness for released degrees of freedom.
/// Maps to ETABS API array indices: [U1, U2, U3, R1, R2, R3]
/// </summary>
public class FrameEndPartialFixity
{
    /// <summary>
    /// Gets or sets U1 (axial) partial fixity [F/L]. Index 0 in API array.
    /// </summary>
    public double U1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets U2 (shear in local 2) partial fixity [F/L]. Index 1 in API array.
    /// </summary>
    public double U2 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets U3 (shear in local 3) partial fixity [F/L]. Index 2 in API array.
    /// </summary>
    public double U3 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets R1 (torsion) partial fixity [FL/rad]. Index 3 in API array.
    /// </summary>
    public double R1 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets R2 (moment about local 2) partial fixity [FL/rad]. Index 4 in API array.
    /// </summary>
    public double R2 { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets R3 (moment about local 3) partial fixity [FL/rad]. Index 5 in API array.
    /// </summary>
    public double R3 { get; set; } = 0.0;

    /// <summary>
    /// Converts partial fixity to API array format.
    /// </summary>
    public double[] ToArray()
    {
        return new[] { U1, U2, U3, R1, R2, R3 };
    }

    /// <summary>
    /// Creates partial fixity from API array format.
    /// </summary>
    public static FrameEndPartialFixity FromArray(double[] array)
    {
        if (array == null || array.Length != 6)
            throw new ArgumentException("Array must have 6 elements", nameof(array));

        return new FrameEndPartialFixity
        {
            U1 = array[0],
            U2 = array[1],
            U3 = array[2],
            R1 = array[3],
            R2 = array[4],
            R3 = array[5]
        };
    }

    public override string ToString()
    {
        return $"U1={U1:F2}, U2={U2:F2}, U3={U3:F2}, R1={R1:F2}, R2={R2:F2}, R3={R3:F2}";
    }
}
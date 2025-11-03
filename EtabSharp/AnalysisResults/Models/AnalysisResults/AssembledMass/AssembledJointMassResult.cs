namespace EtabSharp.AnalysisResults.Models.AnalysisResults.AssembledMass;

/// <summary>
/// Represents assembled joint mass results.
/// </summary>
public class AssembledJointMassResult
{
    public string PointElement { get; set; } = string.Empty;
    public string MassSource { get; set; } = string.Empty;

    public double U1 { get; set; }
    public double U2 { get; set; }
    public double U3 { get; set; }
    public double R1 { get; set; }
    public double R2 { get; set; }
    public double R3 { get; set; }

    public override string ToString()
    {
        return $"Joint Mass: {PointElement} - U1={U1:F4}, U2={U2:F4}, U3={U3:F4}";
    }
}
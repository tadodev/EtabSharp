namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Property modifiers for frame sections.
/// Values typically range from 0 to 1, where 1 = no modification.
/// </summary>
public record PropFrameModifiers
{
    /// <summary>
    /// Cross-sectional area modifier
    /// </summary>
    public double Area { get; set; } = 1.0;

    /// <summary>
    /// Shear area in local 2-direction modifier
    /// </summary>
    public double ShearArea2 { get; set; } = 1.0;

    /// <summary>
    /// Shear area in local 3-direction modifier
    /// </summary>
    public double ShearArea3 { get; set; } = 1.0;

    /// <summary>
    /// Torsional constant modifier
    /// </summary>
    public double Torsion { get; set; } = 1.0;

    /// <summary>
    /// Moment of inertia about local 2-axis modifier
    /// </summary>
    public double Inertia2 { get; set; } = 1.0;

    /// <summary>
    /// Moment of inertia about local 3-axis modifier
    /// </summary>
    public double Inertia3 { get; set; } = 1.0;

    /// <summary>
    /// Mass modifier
    /// </summary>
    public double Mass { get; set; } = 1.0;

    /// <summary>
    /// Weight modifier
    /// </summary>
    public double Weight { get; set; } = 1.0;

    /// <summary>
    /// Creates default modifiers (all values = 1.0, no modification)
    /// </summary>
    public static PropFrameModifiers Default() => new();

    /// <summary>
    /// Creates modifiers for cracked section analysis (typical values)
    /// </summary>
    public static PropFrameModifiers Cracked() => new()
    {
        Area = 1.0,
        Inertia2 = 0.35,
        Inertia3 = 0.35,
        Torsion = 0.35
    };

    /// <summary>
    /// Creates custom modifiers with specified values
    /// </summary>
    public static PropFrameModifiers Create(
        double area = 1.0,
        double shearArea2 = 1.0,
        double shearArea3 = 1.0,
        double torsion = 1.0,
        double inertia2 = 1.0,
        double inertia3 = 1.0,
        double mass = 1.0,
        double weight = 1.0)
    {
        return new PropFrameModifiers
        {
            Area = area,
            ShearArea2 = shearArea2,
            ShearArea3 = shearArea3,
            Torsion = torsion,
            Inertia2 = inertia2,
            Inertia3 = inertia3,
            Mass = mass,
            Weight = weight
        };
    }

}
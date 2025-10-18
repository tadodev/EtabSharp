namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Represents a rectangular frame section (beam or column).
/// </summary>
public record PropFrameRectangle
{
    /// <summary>
    /// Section name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Material name
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Section depth (local 3-axis direction)
    /// </summary>
    public double Depth { get; set; }

    /// <summary>
    /// Section width (local 2-axis direction)
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Section color (-1 for auto)
    /// </summary>
    public int Color { get; set; } = -1;

}

/// <summary>
/// Represents a circular frame section (typically columns).
/// </summary>
public record PropFrameCircle
{
    /// <summary>
    /// Section name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Material name
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Outside diameter
    /// </summary>
    public double Diameter { get; set; }

    /// <summary>
    /// Section color (-1 for auto)
    /// </summary>
    public int Color { get; set; } = -1;
}

/// <summary>
/// Reinforcement data for rectangular columns.
/// </summary>
public record PropColumnRebarRect
{
    /// <summary>
    /// Longitudinal rebar material name
    /// </summary>
    public required string MatPropLong { get; set; }

    /// <summary>
    /// Confinement (tie/stirrup) rebar material name
    /// </summary>
    public required string MatPropConfine { get; set; }

    /// <summary>
    /// Rebar configuration pattern: 1=Rectangular, 2=Circular
    /// </summary>
    public int Pattern = 1; // 1=Rectangular, 2=Circular

    /// <summary>
    /// Confinement type: 1=Ties, 2=Spiral
    /// </summary>
    public int ConfineType = 1;

    /// <summary>
    /// Clear cover to reinforcement
    /// </summary>
    public double Cover { get; set; } = 2; // in or as per units

    /// <summary>
    /// Number of bars along local 3-axis (depth direction)
    /// </summary>
    public int NumberOfBars3Dir { get; set; }

    /// <summary>
    /// Number of bars along local 2-axis (width direction)
    /// </summary>
    public int NumberOfBars2Dir { get; set; }

    /// <summary>
    /// Size of longitudinal bars (e.g., "#8", "25M", "H16")
    /// </summary>
    public string BarSize { get; set; } = "#8";

    /// <summary>
    /// Size of tie/stirrup bars
    /// </summary>
    public string TieSize { get; set; } = "#4";

    /// <summary>
    /// Spacing of ties/stirrups
    /// </summary>
    public double TieSpacing { get; set; } = 12; // in or as per units

    /// <summary>
    /// Number of tie legs parallel to local 2-axis
    /// </summary>
    public int TieLegs2Dir { get; set; } = 2;

    /// <summary>
    /// Number of tie legs parallel to local 3-axis
    /// </summary>
    public int TieLegs3Dir { get; set; } = 2;

    /// <summary>
    /// Whether this column is to be designed
    /// </summary>
    public bool ToBeDesigned { get; set; } = false;

}

/// <summary>
/// Reinforcement data for circular columns.
/// </summary>
public record PropColumnRebarCirc
{
    /// <summary>
    /// Longitudinal rebar material name
    /// </summary>
    public required string MatPropLong { get; set; }

    /// <summary>
    /// Confinement (tie/stirrup) rebar material name
    /// </summary>
    public required string MatPropConfine { get; set; }

    /// <summary>
    /// Rebar configuration pattern: 1=Rectangular, 2=Circular
    /// </summary>
    public int Pattern = 2; // 1=Rectangular, 2=Circular

    /// <summary>
    /// Confinement type: 1=Ties, 2=Spiral
    /// </summary>
    public int ConfineType = 1;

    /// <summary>
    /// Clear cover to reinforcement
    /// </summary>
    public double Cover { get; set; } = 2; // in or as per units

    /// <summary>
    /// Number of bars along local 3-axis (depth direction)
    /// </summary>
    public int NumberOfBars { get; set; }

    /// <summary>
    /// Size of longitudinal bars (e.g., "#8", "25M", "H16")
    /// </summary>
    public string BarSize { get; set; } = "#8";

    /// <summary>
    /// Size of tie/stirrup bars
    /// </summary>
    public string TieSize { get; set; } = "#4";

    /// <summary>
    /// Spacing of ties/stirrups
    /// </summary>
    public double TieSpacing { get; set; } = 12; // in or as per units

    /// <summary>
    /// Number of tie legs parallel to local 2-axis
    /// </summary>
    public int TieLegs2Dir { get; set; } = 2;

    /// <summary>
    /// Number of tie legs parallel to local 3-axis
    /// </summary>
    public int TieLegs3Dir { get; set; } = 2;

    /// <summary>
    /// Whether this column is to be designed
    /// </summary>
    public bool ToBeDesigned { get; set; } = false;
}

/// <summary>
/// Reinforcement data for rectangular beams.
/// </summary>
public record PropBeamRebar
{
    /// <summary>
    /// Longitudinal rebar material name
    /// </summary>
    public required string MatPropLong { get; set; }

    /// <summary>
    /// Confinement (stirrup) rebar material name
    /// </summary>
    public required string MatPropConfine { get; set; }

    /// <summary>
    /// Top cover to reinforcement
    /// </summary>
    public double CoverTop { get; set; } = 2; // in or as per units

    /// <summary>
    /// Bottom cover to reinforcement
    /// </summary>
    public double CoverBottom { get; set; } = 2; // in or as per units

    /// <summary>
    /// Top left corner bar area
    /// </summary>
    public double TopLeftArea { get; set; } = 0.0;

    /// <summary>
    /// Top right corner bar area
    /// </summary>
    public double TopRightArea { get; set; } = 0.0;

    /// <summary>
    /// Bottom left corner bar area
    /// </summary>
    public double BottomLeftArea { get; set; } = 0.0;

    /// <summary>
    /// Bottom right corner bar area
    /// </summary>
    public double BottomRightArea { get; set; } = 0.0;
}

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
}
namespace EtabSharp.Constants;

/// <summary>
/// Default constraint values for ETABS concrete materials.
/// These correspond to the parameters of SetOConcrete_1 in the ETABS API.
/// </summary>
public static class PropConcreteDefault
{
    /// <summary>
    /// Default Poisson's ratio for concrete
    /// </summary>
    public const double U = 0.2; // Default Poisson's ratio for concrete

    /// <summary>
    /// Default coefficient of thermal expansion for concrete (1/°C)
    /// </summary>
    public const double Alpha = 9.9e-6; // Default coefficient of thermal expansion (1/°C)

    /// <summary>
    /// Whether concrete is lightweight (reduces unit weight & stiffness).
    /// </summary>
    public const bool IsLightweight = false;

    /// <summary>
    /// Shear strength factor (typically 0.0 for normal-weight concrete).
    /// </summary>
    public const double FcsFactor = 0.0;

    /// <summary>
    /// Stress–strain curve type:
    /// 0 = User-defined, 1 = Parametric Simple, 2 = Parametric Mander
    /// </summary>
    public const int SSType = 2; // Mander model – best for reinforced concrete

    /// <summary>
    /// Hysteresis type:
    /// 0 = Elastic, 1 = Kinematic, 2 = Takeda, 3 = Pivot,
    /// 4 = Concrete, 5 = BRB Hardening, 6 = Degrading, 7 = Isotropic
    /// </summary>
    public const int SSHysType = 4; // Concrete – common for cyclic concrete behavior

    /// <summary>
    /// Strain at peak compressive stress (ε_fc)
    /// </summary>
    public const double StrainAtFc = 0.0022;

    /// <summary>
    /// Ultimate unconfined strain capacity (ε_cu)
    /// </summary>
    public const double StrainUltimate = 0.0052;

    /// <summary>
    /// Final slope multiplier (E * FinalSlope gives the final tangent)
    /// </summary>
    public const double FinalSlope = -0.1;

    /// <summary>
    /// Drucker–Prager friction angle [deg]
    /// </summary>
    public const double FrictionAngle = 0.0;

    /// <summary>
    /// Drucker–Prager dilatational angle [deg]
    /// </summary>
    public const double DilatationalAngle = 0.0;
}

/// <summary>
/// Default parameter values for rebar material properties
/// </summary>
public static class PropRebarDefault
{
    /// <summary>
    /// Default Poisson's ratio for Rebar
    /// </summary>
    public const double U = 0.25; // Default Poisson's ratio for rebar

    /// <summary>
    /// Default coefficient of thermal expansion for concrete (1/°C)
    /// </summary>
    public const double Alpha = 6e-6; // Default coefficient of thermal expansion (1/°C)

    /// <summary>
    /// Stress-strain curve type:
    /// 0 = User-defined, 1 = Parametric Simple, 2 = Parametric Park
    /// </summary>
    public const int SSType = 1; // Parametric - Simple

    /// <summary>
    /// Hysteresis type:
    /// 0 = Elastic, 1 = Kinematic, 2 = Takeda, 3 = Pivot,
    /// 4 = Concrete, 5 = BRB Hardening, 6 = Degrading, 7 = Isotropic
    /// </summary>
    public const int SSHysType = 1; // Kinematic – common for rebar cyclic behavior

    /// <summary>
    /// Strain at onset of hardening
    /// </summary>
    public const double StrainAtHardening = 0.02;

    /// <summary>
    /// Ultimate strain capacity
    /// </summary>
    public const double StrainUltimate = 0.1;

    /// <summary>
    /// Final slope multiplier (E * FinalSlope gives the curve’s last slope)
    /// </summary>
    public const double FinalSlope = -0.1;

    /// <summary>
    /// Whether to use Caltrans strain defaults (bar size dependent)
    /// </summary>
    public const bool UseCaltransSSDefaults = true;
}

/// <summary>
/// Default constraint values for ETABS steel materials.
/// These correspond to the parameters of SetOSteel_1 in the ETABS API.
/// </summary>
public static class SteelDefaults
{
    /// <summary>
    /// Default Poisson's ratio for Rebar
    /// </summary>
    public const double U = 0.25; // Default Poisson's ratio for rebar

    /// <summary>
    /// Default coefficient of thermal expansion for concrete (1/°C)
    /// </summary>
    public const double Alpha = 6e-6; // Default coefficient of thermal expansion (1/°C)

    /// <summary>
    /// Stress-strain curve type:
    /// 0 = User-defined, 1 = Parametric Simple
    /// </summary>
    public const int SSType = 1; // Parametric - Simple

    /// <summary>
    /// Hysteresis type:
    /// 0 = Elastic, 1 = Kinematic, 2 = Takeda, 3 = Pivot,
    /// 4 = Concrete, 5 = BRB Hardening, 6 = Degrading, 7 = Isotropic
    /// </summary>
    public const int SSHysType = 1; // Kinematic – good general cyclic steel model

    /// <summary>
    /// Strain at onset of strain hardening (ε_sh)
    /// </summary>
    public const double StrainAtHardening = 0.02;

    /// <summary>
    /// Strain at maximum stress (ε_su)
    /// </summary>
    public const double StrainAtMaxStress = 0.1;

    /// <summary>
    /// Strain at rupture (ε_ru)
    /// </summary>
    public const double StrainAtRupture = 0.2;

    /// <summary>
    /// Final slope multiplier (E * FinalSlope gives the curve’s final slope)
    /// </summary>
    public const double FinalSlope = -0.1;
}

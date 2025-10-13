using ETABSv1;

namespace EtabSharp.Models.UnitSystem;

/// <summary>
/// 
/// </summary>
public class Units
{
    /// <summary>
    /// Force unit (lb, kip, N, kN, kgf, tonf)
    /// </summary>
    public eForce Force { get; set; }

    /// <summary>
    /// Length unit (inch, ft, mm, cm, m)
    /// </summary>
    public eLength Length { get; set; }

    /// <summary>
    /// Temperature unit (F, C)
    /// </summary>
    public eTemperature Temperature { get; set; }

    /// <summary>
    /// Creates a unit system with default values
    /// </summary>
    public Units()
    {
        Force = eForce.kN;
        Length = eLength.m;
        Temperature = eTemperature.C;
    }

    /// <summary>
    /// Creates a unit system based on US or Metric defaults
    /// </summary>
    /// <param name="isUsDefault">If true, uses US units (kip, ft, F); otherwise Metric (kN, m, C)</param>
    public Units(bool isUsDefault)
    {
        if (isUsDefault)
        {
            Force = eForce.kip;
            Length = eLength.ft;
            Temperature = eTemperature.F;
        }
        else
        {
            Force = eForce.kN;
            Length = eLength.m;
            Temperature = eTemperature.C;
        }
    }

    /// <summary>
    /// Creates a unit system with specific units
    /// </summary>
    public Units(eForce force, eLength length, eTemperature temperature)
    {
        Force = force;
        Length = length;
        Temperature = temperature;
    }

    /// <summary>
    /// Checks if this is a metric system (uses Celsius)
    /// </summary>
    public bool IsMetric => Force == eForce.kN && Length == eLength.m && Temperature == eTemperature.C;

    /// <summary>
    /// Checks if this is a US/Imperial system (uses Fahrenheit)
    /// </summary>
    public bool IsUS => Force == eForce.kip && Length == eLength.ft && Temperature == eTemperature.F;

    /// <summary>
    /// Returns a display string for the unit system
    /// </summary>
    public override string ToString()
    {
        return $"{Force}, {Length}, {Temperature}°";
    }

    /// <summary>
    /// Common US unit system: kip, in, F
    /// </summary>
    public static Units US_Kip_In => new(eForce.kip, eLength.inch, eTemperature.F);

    /// <summary>
    /// Common US unit system: kip, ft, F
    /// </summary>
    public static Units US_Kip_Ft => new(eForce.kip, eLength.ft, eTemperature.F);

    /// <summary>
    /// Common metric unit system: kN, m, C
    /// </summary>
    public static Units Metric_kN_m => new(eForce.kN, eLength.m, eTemperature.C);

    /// <summary>
    /// Common metric unit system: kN, mm, C
    /// </summary>
    public static Units Metric_kN_mm => new(eForce.kN, eLength.mm, eTemperature.C);

    /// <summary>
    /// Common metric unit system: N, mm, C
    /// </summary>
    public static Units Metric_N_mm => new(eForce.N, eLength.mm, eTemperature.C);

    /// <summary>
    /// Common metric unit system: N, m, C
    /// </summary>
    public static Units Metric_N_m => new(eForce.N, eLength.m, eTemperature.C);
}
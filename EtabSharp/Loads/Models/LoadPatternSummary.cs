namespace EtabSharp.Loads.Models;

public class LoadPatternSummary
{
    /// <summary>
    /// Total number of load patterns.
    /// </summary>
    public int TotalPatterns { get; set; }

    /// <summary>
    /// Number of dead load patterns.
    /// </summary>
    public int DeadLoads { get; set; }

    /// <summary>
    /// Number of live load patterns.
    /// </summary>
    public int LiveLoads { get; set; }

    /// <summary>
    /// Number of seismic load patterns.
    /// </summary>
    public int SeismicLoads { get; set; }

    /// <summary>
    /// Number of wind load patterns.
    /// </summary>
    public int WindLoads { get; set; }

    /// <summary>
    /// Number of snow load patterns.
    /// </summary>
    public int SnowLoads { get; set; }

    /// <summary>
    /// Number of temperature load patterns.
    /// </summary>
    public int TemperatureLoads { get; set; }

    /// <summary>
    /// Number of patterns with self-weight.
    /// </summary>
    public int PatternsWithSelfWeight { get; set; }

    /// <summary>
    /// Number of auto seismic patterns.
    /// </summary>
    public int AutoSeismicPatterns { get; set; }

    /// <summary>
    /// Number of auto wind patterns.
    /// </summary>
    public int AutoWindPatterns { get; set; }

    /// <summary>
    /// Other load pattern types.
    /// </summary>
    public int OtherLoads { get; set; }

    public override string ToString()
    {
        return $"Load Patterns Summary: Total={TotalPatterns}, Dead={DeadLoads}, Live={LiveLoads}, " +
               $"Seismic={SeismicLoads}, Wind={WindLoads}, Snow={SnowLoads}, Temp={TemperatureLoads}, " +
               $"Other={OtherLoads}, WithSW={PatternsWithSelfWeight}, AutoSeismic={AutoSeismicPatterns}, AutoWind={AutoWindPatterns}";
    }
}
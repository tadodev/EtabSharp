namespace EtabSharp.Models;

/// <summary>
/// Contains detailed ETABS API information
/// </summary>
public class ETABSApiInfo
{
    public int MajorVersion { get; set; }
    public string FullVersion { get; set; }
    public double ApiVersion { get; set; }
    public string DllName { get; set; }
    public bool IsNetStandard { get; set; }

    public override string ToString()
    {
        var apiVersionStr = ApiVersion > 0 ? $", API v{ApiVersion}" : "";
        return $"ETABS v{FullVersion}{apiVersionStr} - {DllName} (.NET Standard 2.0)";
    }
}
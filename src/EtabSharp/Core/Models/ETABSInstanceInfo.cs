namespace EtabSharp.Core.Models;

/// <summary>
/// Public class containing ETABS instance information
/// </summary>
public class ETABSInstanceInfo
{
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public int MajorVersion { get; set; }
    public string FullVersion { get; set; }
    public bool HasMainWindow { get; set; }
    public string WindowTitle { get; set; }
    public bool IsSupported { get; set; }

    public override string ToString()
    {
        var supportedText = IsSupported ? "Supported" : "Not Supported";
        return $"ETABS v{FullVersion} (PID: {ProcessId}) - {WindowTitle} [{supportedText}]";
    }
}
namespace EtabSharp.Design.Steel.Codes.AISC360_16.Models;

/// <summary>
/// Collection of steel design overwrites for one or more frame elements.
/// </summary>
public class SteelOverwriteResults
{
    /// <summary>
    /// List of overwrite records.
    /// </summary>
    public List<SteelOverwrite> Overwrites { get; set; } = new();

    /// <summary>
    /// Indicates if the operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Return code from the ETABS API (0 = success).
    /// </summary>
    public int ReturnCode { get; set; }

    /// <summary>
    /// Error message if operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Number of overwrites retrieved.
    /// </summary>
    public int Count => Overwrites.Count;

    /// <summary>
    /// Gets overwrites for a specific frame.
    /// </summary>
    public List<SteelOverwrite> GetForFrame(string frameName)
    {
        return Overwrites.Where(o => o.FrameName.Equals(frameName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets overwrites for a specific item type.
    /// </summary>
    public List<SteelOverwrite> GetForItem(SteelOverwriteItem item)
    {
        return Overwrites.Where(o => o.Item == item).ToList();
    }

    /// <summary>
    /// Gets only user-specified overwrites (not program determined).
    /// </summary>
    public List<SteelOverwrite> GetUserSpecified()
    {
        return Overwrites.Where(o => !o.IsProgramDetermined).ToList();
    }

    /// <summary>
    /// Gets only program-determined overwrites.
    /// </summary>
    public List<SteelOverwrite> GetProgramDetermined()
    {
        return Overwrites.Where(o => o.IsProgramDetermined).ToList();
    }

    public override string ToString()
    {
        return $"Steel Overwrites: {Count} records | Success: {IsSuccess}";
    }
}
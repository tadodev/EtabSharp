namespace EtabSharp.Design.Concrete.Codes.ACI318_14.Models;

/// <summary>
/// Collection of concrete design overwrites for one or more frame elements.
/// </summary>
public class ConcreteOverwriteResults
{
    /// <summary>
    /// List of overwrite records.
    /// </summary>
    public List<ConcreteOverwrite> Overwrites { get; set; } = new();

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
    public List<ConcreteOverwrite> GetForFrame(string frameName)
    {
        return Overwrites.Where(o => o.FrameName.Equals(frameName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// Gets overwrites for a specific item type.
    /// </summary>
    public List<ConcreteOverwrite> GetForItem(ConcreteOverwriteItem item)
    {
        return Overwrites.Where(o => o.Item == item).ToList();
    }

    /// <summary>
    /// Gets only user-specified overwrites (not program determined).
    /// </summary>
    public List<ConcreteOverwrite> GetUserSpecified()
    {
        return Overwrites.Where(o => !o.IsProgramDetermined).ToList();
    }

    /// <summary>
    /// Gets only program-determined overwrites.
    /// </summary>
    public List<ConcreteOverwrite> GetProgramDetermined()
    {
        return Overwrites.Where(o => o.IsProgramDetermined).ToList();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"Concrete Overwrites: {Count} records | Success: {IsSuccess}";
    }
}
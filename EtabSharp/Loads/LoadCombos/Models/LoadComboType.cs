namespace EtabSharp.Loads.LoadCombos.Models;

/// <summary>
/// Enumeration for load combination types
/// </summary>
public enum LoadComboType
{
    /// <summary>Linear Add - simple addition of cases with scale factors</summary>
    LinearAdd = 0,

    /// <summary>Envelope - takes maximum and minimum values</summary>
    Envelope = 1,

    /// <summary>Absolute Add - adds absolute values</summary>
    AbsoluteAdd = 2,

    /// <summary>SRSS - Square Root Sum of Squares</summary>
    SRSS = 3,

    /// <summary>Range Add - adds the range of values</summary>
    RangeAdd = 4
}
namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Enum for table import types.
/// </summary>
public enum TableImportType
{
    /// <summary>
    /// Not importable (read-only).
    /// </summary>
    NotImportable = 0,

    /// <summary>
    /// Importable but not interactively.
    /// </summary>
    ImportableNotInteractive = 1,

    /// <summary>
    /// Interactively importable when model is unlocked.
    /// </summary>
    InteractiveWhenUnlocked = 2,

    /// <summary>
    /// Interactively importable when model is unlocked or locked.
    /// </summary>
    InteractiveAlways = 3
}
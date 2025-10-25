namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Design and response recovery options.
/// </summary>
public class DesignResponseOptions
{
    /// <summary>
    /// Number of design threads (positive=user specified, negative=program determined, 0=auto)
    /// </summary>
    public int NumberDesignThreads { get; set; } = 0; // 0 = auto

    /// <summary>
    /// Number of response recovery threads (positive=user specified, negative=program determined, 0=auto)
    /// </summary>
    public int NumberResponseRecoveryThreads { get; set; } = 0; // 0 = auto

    /// <summary>
    /// Use memory mapped files for response recovery
    /// -2 = Not using (Program Determined)
    /// -1 = Not using (user specified)
    /// 1 = Using (user specified)
    /// 2 = Using (Program Determined)
    /// </summary>
    public int UseMemoryMappedFilesForResponseRecovery { get; set; } = -2;

    /// <summary>
    /// Allow model differences when merging results
    /// </summary>
    public bool ModelDifferencesOKWhenMergingResults { get; set; }

    /// <summary>
    /// Gets memory mapped files description
    /// </summary>
    public string GetMemoryMappedFilesDescription()
    {
        return UseMemoryMappedFilesForResponseRecovery switch
        {
            -2 => "Not Using (Program Determined)",
            -1 => "Not Using (User Specified)",
            1 => "Using (User Specified)",
            2 => "Using (Program Determined)",
            _ => $"Unknown ({UseMemoryMappedFilesForResponseRecovery})"
        };
    }

    public override string ToString()
    {
        return $"Design Threads: {NumberDesignThreads}, Response Threads: {NumberResponseRecoveryThreads}, Memory Mapped: {GetMemoryMappedFilesDescription()}";
    }
}
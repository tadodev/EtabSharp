using EtabSharp.Core.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EtabSharp.Core;

/// <summary>
/// Wrapper for connecting to and interacting with ETABS v22 and newer
/// Uses ETABSv1.DLL with .NET Standard 2.0 API
/// </summary>
public class ETABSWrapper
{
    private const string ETABS_PROCESS_NAME = "ETABS";
    private const string ETABS_PROGID = "CSI.ETABS.API.ETABSObject";
    private const int MINIMUM_SUPPORTED_VERSION = 22;

    /// <summary>
    /// Connects to running ETABS instance (v22+)
    /// </summary>
    /// <param name="logger">Optional logger for diagnostics</param>
    /// <returns>ETABS application wrapper with typed access to API, or null if none found</returns>
    public static ETABSApplication Connect(ILogger<ETABSApplication>? logger = null)
    {
        var etabsProcesses = GetETABSProcesses();

        if (!etabsProcesses.Any())
        {
            Console.WriteLine("No running ETABS instances found.");
            return null;
        }

        var activeProcess = FindActiveProcess(etabsProcesses);

        if (activeProcess == null)
        {
            Console.WriteLine("No ETABS instance with active window found.");
            return null;
        }

        return ConnectToETABS(activeProcess, logger);
    }

    /// <summary>
    /// Creates a new ETABS instance
    /// </summary>
    /// <param name="programPath">Optional path to ETABS.exe. If null, uses latest installed version</param>
    /// <param name="startApplication">Whether to start the application UI</param>
    /// <param name="logger">Optional logger for diagnostics</param>
    /// <returns>ETABS application wrapper</returns>
    public static ETABSApplication CreateNew(string programPath = null, bool startApplication = true,
        ILogger<ETABSApplication>? logger = null)
    {
        try
        {
            // Create API helper object
            ETABSv1.cHelper helper = new ETABSv1.Helper();
            ETABSv1.cOAPI api;

            if (!string.IsNullOrEmpty(programPath))
            {
                // Create instance from specified path
                api = helper.CreateObject(programPath);
            }
            else
            {
                // Create instance from latest installed ETABS
                api = helper.CreateObjectProgID(ETABS_PROGID);
            }

            if (startApplication)
            {
                // Start ETABS application UI
                int ret = api.ApplicationStart();
                if (ret != 0)
                {
                    Console.WriteLine("Warning: ApplicationStart returned non-zero value");
                }
            }

            // Get version info
            var versionInfo = GetVersionFromAPI(api);
            int version = versionInfo.majorVersion;
            string fullVersion = versionInfo.fullVersion;
            double apiVersion = GetApiVersionNumber(helper);

            Console.WriteLine($"Created new ETABS instance v{fullVersion}, API Version: {apiVersion}");

            return new ETABSApplication(api, version, apiVersion, fullVersion, logger);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating new ETABS instance: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets all running ETABS processes
    /// </summary>
    private static List<Process> GetETABSProcesses()
    {
        return Process.GetProcessesByName(ETABS_PROCESS_NAME).ToList();
    }

    /// <summary>
    /// Finds the first ETABS process with an active main window
    /// </summary>
    private static ETABSProcessInfo FindActiveProcess(List<Process> processes)
    {
        foreach (var process in processes)
        {
            if (process.MainWindowHandle != IntPtr.Zero)
            {
                try
                {
                    var fileVersionInfo = process.MainModule.FileVersionInfo;

                    return new ETABSProcessInfo
                    {
                        Process = process,
                        MajorVersion = fileVersionInfo.FileMajorPart,
                        MinorVersion = fileVersionInfo.FileMinorPart,
                        BuildVersion = fileVersionInfo.FileBuildPart,
                        FullVersion =
                            $"{fileVersionInfo.FileMajorPart}.{fileVersionInfo.FileMinorPart}.{fileVersionInfo.FileBuildPart}",
                        ProcessName = process.ProcessName
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading process info: {ex.Message}");
                    continue;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Connects to the ETABS process and returns wrapper
    /// </summary>
    private static ETABSApplication ConnectToETABS(ETABSProcessInfo processInfo, ILogger<ETABSApplication>? logger)
    {
        if (processInfo.MajorVersion == 0)
        {
            logger?.LogWarning("Unable to determine ETABS version.");
            return null;
        }

        if (processInfo.MajorVersion < MINIMUM_SUPPORTED_VERSION)
        {
            logger?.LogWarning($"ETABS v{processInfo.FullVersion} is not supported.");
            logger?.LogWarning($"This wrapper requires ETABS v{MINIMUM_SUPPORTED_VERSION} or newer.");
            logger?.LogWarning("Please upgrade your ETABS installation.");
            return null;
        }

        try
        {
            logger?.LogInformation($"Connecting to ETABS v{processInfo.FullVersion}...");
            return CreateETABSApplication(processInfo.MajorVersion, processInfo.FullVersion, logger);
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, $"Error connecting to ETABS v{processInfo.FullVersion}");
            return null;
        }
    }

    /// <summary>
    /// Creates ETABS application wrapper for v22+ by attaching to running instance
    /// </summary>
    private static ETABSApplication CreateETABSApplication(int majorVersion, string fullVersion,
        ILogger<ETABSApplication>? logger)
    {
        try
        {
            // Create helper object
            ETABSv1.cHelper helper = new ETABSv1.Helper();

            // Get the active ETABS object
            ETABSv1.cOAPI api = helper.GetObject(ETABS_PROGID);

            // Get the API version number
            double apiVersion = GetApiVersionNumber(helper);

            logger?.LogInformation($"Connected to ETABS v{fullVersion}, API Version: {apiVersion}");
            Console.WriteLine($"Connected to ETABS v{fullVersion}, API Version: {apiVersion}");

            return new ETABSApplication(api, majorVersion, apiVersion, fullVersion, logger);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to attach to ETABS: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Gets the API version number from helper
    /// </summary>
    private static double GetApiVersionNumber(ETABSv1.cHelper helper)
    {
        try
        {
            return helper.GetOAPIVersionNumber();
        }
        catch
        {
            return 0.0;
        }
    }

    /// <summary>
    /// Gets version from API object by examining process
    /// </summary>
    private static (int majorVersion, string fullVersion) GetVersionFromAPI(ETABSv1.cOAPI api)
    {
        try
        {
            var processes = GetETABSProcesses();
            var activeProcess = FindActiveProcess(processes);
            if (activeProcess != null)
            {
                return (activeProcess.MajorVersion, activeProcess.FullVersion);
            }

            return (22, "22.0.0"); // Default to 22.0.0 if can't determine
        }
        catch
        {
            return (22, "22.0.0"); // Default to 22.0.0
        }
    }

    /// <summary>
    /// Gets list of all running ETABS instances with their version info
    /// </summary>
    public static List<ETABSInstanceInfo> GetAllRunningInstances()
    {
        var instances = new List<ETABSInstanceInfo>();
        var processes = GetETABSProcesses();

        foreach (var process in processes)
        {
            try
            {
                var fileVersionInfo = process.MainModule.FileVersionInfo;
                int majorVersion = fileVersionInfo.FileMajorPart;
                string fullVersion =
                    $"{fileVersionInfo.FileMajorPart}.{fileVersionInfo.FileMinorPart}.{fileVersionInfo.FileBuildPart}";

                instances.Add(new ETABSInstanceInfo
                {
                    ProcessId = process.Id,
                    ProcessName = process.ProcessName,
                    MajorVersion = majorVersion,
                    FullVersion = fullVersion,
                    HasMainWindow = process.MainWindowHandle != IntPtr.Zero,
                    WindowTitle = process.MainWindowTitle,
                    IsSupported = majorVersion >= MINIMUM_SUPPORTED_VERSION
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading process {process.Id}: {ex.Message}");
            }
        }

        return instances;
    }

    /// <summary>
    /// Checks if ETABS is currently running
    /// </summary>
    public static bool IsRunning()
    {
        return GetETABSProcesses().Any();
    }

    /// <summary>
    /// Checks if a supported version of ETABS (v22+) is running
    /// </summary>
    public static bool IsSupportedVersionRunning()
    {
        var processes = GetETABSProcesses();
        var activeProcess = FindActiveProcess(processes);
        return activeProcess != null && activeProcess.MajorVersion >= MINIMUM_SUPPORTED_VERSION;
    }

    /// <summary>
    /// Gets the version of the active ETABS instance
    /// </summary>
    public static string GetActiveVersion()
    {
        var processes = GetETABSProcesses();
        var activeProcess = FindActiveProcess(processes);
        return activeProcess?.FullVersion;
    }
}

/// <summary>
/// Internal class to store ETABS process information
/// </summary>
internal class ETABSProcessInfo
{
    public Process? Process { get; set; }
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
    public int BuildVersion { get; set; }
    public string? FullVersion { get; set; }
    public string? ProcessName { get; set; }
}
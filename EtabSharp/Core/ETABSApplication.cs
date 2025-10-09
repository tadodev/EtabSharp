using EtabSharp.Models;

namespace EtabSharp.Core;

/// <summary>
/// ETABS application wrapper for v22 and newer
/// Provides strongly-typed access to ETABS API using ETABSv1.DLL (.NET Standard 2.0)
/// </summary>
public class ETABSApplication: IDisposable
{
    private readonly ETABSv1.cOAPI _api;
    private readonly ETABSv1.cSapModel _model;
    private readonly int _version;
    private readonly double _apiVersion;
    private bool _disposed = false;

    /// <summary>
    /// Gets the ETABS version (22+)
    /// </summary>
    public int Version => _version;

    /// <summary>
    /// Gets the API version number
    /// </summary>
    public double ApiVersion => _apiVersion;

    /// <summary>
    /// Gets the DLL name being used
    /// </summary>
    public string DllName => "ETABSv1.DLL";

    /// <summary>
    /// Always true for v22+ (.NET Standard 2.0)
    /// </summary>
    public bool IsNetStandard => true;

    /// <summary>
    /// Gets strongly-typed cOAPI object for ETABS v22+
    /// Use this for application-level operations (file, program control, etc.)
    /// </summary>
    public ETABSv1.cOAPI API => _api;

    /// <summary>
    /// Gets strongly-typed cSapModel object for ETABS v22+
    /// Use this for model operations (geometry, loads, analysis, results, etc.)
    /// </summary>
    public ETABSv1.cSapModel Model => _model;

    internal ETABSApplication(ETABSv1.cOAPI api, int version, double apiVersion)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _model = api.SapModel;
        _version = version;
        _apiVersion = apiVersion;
    }

    /// <summary>
    /// Gets API information
    /// </summary>
    public ETABSApiInfo GetApiInfo()
    {
        return new ETABSApiInfo
        {
            Version = Version,
            ApiVersion = ApiVersion,
            DllName = DllName,
            IsNetStandard = IsNetStandard
        };
    }

    /// <summary>
    /// Safely executes an API function with error handling
    /// ETABS v22+ throws catchable exceptions for unsupported functions
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="apiCall">The API function to execute</param>
    /// <param name="functionName">Optional name for better error messages</param>
    /// <returns>Result of the API call</returns>
    public T ExecuteSafely<T>(Func<T> apiCall, string functionName = null)
    {
        try
        {
            return apiCall();
        }
        catch (Exception ex)
        {
            var funcName = functionName ?? "API function";
            Console.WriteLine($"Error calling {funcName}: {ex.Message}");
            Console.WriteLine("This function may not be supported in your version of ETABS.");
            throw;
        }
    }

    /// <summary>
    /// Safely executes an API action with error handling (void return)
    /// ETABS v22+ throws catchable exceptions for unsupported functions
    /// </summary>
    /// <param name="apiCall">The API action to execute</param>
    /// <param name="functionName">Optional name for better error messages</param>
    public void ExecuteSafely(Action apiCall, string functionName = null)
    {
        try
        {
            apiCall();
        }
        catch (Exception ex)
        {
            var funcName = functionName ?? "API function";
            Console.WriteLine($"Error calling {funcName}: {ex.Message}");
            Console.WriteLine("This function may not be supported in your version of ETABS.");
            throw;
        }
    }

    /// <summary>
    /// Gets the underlying cOAPI object (for advanced usage)
    /// </summary>
    public object GetRawAPI() => _api;

    /// <summary>
    /// Gets the underlying cSapModel object (for advanced usage)
    /// </summary>
    public object GetRawModel() => _model;

    /// <summary>
    /// Closes the ETABS application
    /// </summary>
    public void Close(bool savePrompt = false)
    {
        if (!_disposed)
        {
            try
            {
                _api.ApplicationExit(savePrompt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing ETABS: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Implements IDisposable to close ETABS when disposed
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            Close(false);
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
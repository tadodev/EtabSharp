namespace EtabSharp.Exceptions;

/// <summary>
/// Base exception class for all ETABS-related errors in EtabSharp
/// </summary>
public class EtabsException : Exception
{
    /// <summary>
    /// Gets the ETABS API return code (non-zero indicates error)
    /// </summary>
    public int ReturnCode { get; }

    /// <summary>
    /// Gets the name of the operation that failed
    /// </summary>
    public string? Operation { get; }

    /// <summary>
    /// Initializes a new instance of the EtabsException class
    /// </summary>
    public EtabsException(string message) : base(message)
    {
        ReturnCode = -1;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsException class with a return code
    /// </summary>
    public EtabsException(int returnCode, string message) : base(message)
    {
        ReturnCode = returnCode;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsException class with full details
    /// </summary>
    public EtabsException(int returnCode, string operation, string message)
        : base($"[{operation}] {message} (Return code: {returnCode})")
    {
        ReturnCode = returnCode;
        Operation = operation;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsException class with an inner exception
    /// </summary>
    public EtabsException(string message, Exception innerException)
        : base(message, innerException)
    {
        ReturnCode = -1;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsException class with full details and inner exception
    /// </summary>
    public EtabsException(int returnCode, string operation, string message, Exception innerException)
        : base($"[{operation}] {message} (Return code: {returnCode})", innerException)
    {
        ReturnCode = returnCode;
        Operation = operation;
    }
}

/// <summary>
/// Exception thrown when attempting to connect to ETABS but no instance is running
/// </summary>
public class EtabsNotRunningException : EtabsException
{
    public EtabsNotRunningException()
        : base("No running ETABS instance found. Please start ETABS before connecting.")
    {
    }

    public EtabsNotRunningException(string message) : base(message)
    {
    }
}

/// <summary>
/// Exception thrown when ETABS version is not supported
/// </summary>
public class EtabsVersionNotSupportedException : EtabsException
{
    /// <summary>
    /// Gets the detected ETABS version
    /// </summary>
    public string? DetectedVersion { get; }

    /// <summary>
    /// Gets the minimum required version
    /// </summary>
    public string RequiredVersion { get; }

    public EtabsVersionNotSupportedException(string detectedVersion, string requiredVersion)
        : base($"ETABS version {detectedVersion} is not supported. Minimum required version: {requiredVersion}")
    {
        DetectedVersion = detectedVersion;
        RequiredVersion = requiredVersion;
    }
}

/// <summary>
/// Exception thrown when an ETABS API function is not supported in the current version
/// </summary>
public class EtabsFeatureNotSupportedException : EtabsException
{
    /// <summary>
    /// Gets the name of the unsupported feature/function
    /// </summary>
    public string? FeatureName { get; }

    public EtabsFeatureNotSupportedException(string featureName, string? currentVersion = null)
        : base(currentVersion != null
            ? $"Feature '{featureName}' is not supported in ETABS {currentVersion}"
            : $"Feature '{featureName}' is not supported in this version of ETABS")
    {
        FeatureName = featureName;
    }
}

/// <summary>
/// Exception thrown when an invalid parameter is passed to an ETABS API function
/// </summary>
public class EtabsInvalidParameterException : EtabsException
{
    /// <summary>
    /// Gets the name of the invalid parameter
    /// </summary>
    public string? ParameterName { get; }

    /// <summary>
    /// Gets the invalid value that was passed
    /// </summary>
    public object? InvalidValue { get; }

    public EtabsInvalidParameterException(string parameterName, object? invalidValue, string message)
        : base($"Invalid parameter '{parameterName}': {message}")
    {
        ParameterName = parameterName;
        InvalidValue = invalidValue;
    }
}
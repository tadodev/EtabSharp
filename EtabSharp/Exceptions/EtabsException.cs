namespace EtabSharp.Exceptions;

public class EtabsException:Exception
{
    /// <summary>Gets the ETABS error code</summary>
    public int ErrorCode { get; }

    public EtabsException(string message) : base(message)
    {
        ErrorCode = -1;
    }

    public EtabsException(string message, int errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public EtabsException(string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = -1;
    }
}

/// <summary>
/// Thrown when no ETABS model is loaded
/// </summary>
public class ModelNotLoadedException : EtabsException
{
    public ModelNotLoadedException()
        : base("No ETABS model is currently loaded") { }

    public ModelNotLoadedException(string message)
        : base(message) { }
}

/// <summary>
/// Thrown when analysis fails
/// </summary>
public class AnalysisFailedException : EtabsException
{
    public AnalysisFailedException(string message)
        : base($"Analysis failed: {message}") { }

    public AnalysisFailedException(string message, int errorCode)
        : base($"Analysis failed: {message}", errorCode) { }
}

/// <summary>
/// Thrown when connection to ETABS fails
/// </summary>
public class ConnectionException : EtabsException
{
    public ConnectionException(string message)
        : base($"Connection failed: {message}") { }

    public ConnectionException(string message, Exception innerException)
        : base($"Connection failed: {message}", innerException) { }
}

/// <summary>
/// Thrown when an element is not found
/// </summary>
public class ElementNotFoundException : EtabsException
{
    public ElementNotFoundException(string elementType, string elementName)
        : base($"{elementType} '{elementName}' not found") { }
}
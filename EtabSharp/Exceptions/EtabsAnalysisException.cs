namespace EtabSharp.Exceptions;

/// <summary>
/// Exception thrown when an analysis operation fails
/// </summary>
public class EtabsAnalysisException : EtabsException
{
    /// <summary>
    /// Gets the name of the analysis case that caused the error
    /// </summary>
    public string? CaseName { get; }

    /// <summary>
    /// Gets the type of analysis (Static, Modal, Response Spectrum, etc.)
    /// </summary>
    public string? AnalysisType { get; }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class
    /// </summary>
    public EtabsAnalysisException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with analysis type
    /// </summary>
    public EtabsAnalysisException(string analysisType, string message)
        : base($"Analysis [{analysisType}]: {message}")
    {
        AnalysisType = analysisType;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with case name
    /// </summary>
    public EtabsAnalysisException(string analysisType, string caseName, string message)
        : base($"Analysis [{analysisType}] Case '{caseName}': {message}")
    {
        AnalysisType = analysisType;
        CaseName = caseName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with return code
    /// </summary>
    public EtabsAnalysisException(int returnCode, string operation, string message)
        : base(returnCode, operation, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with full details
    /// </summary>
    public EtabsAnalysisException(int returnCode, string operation, string analysisType, string caseName, string message)
        : base(returnCode, operation, $"Analysis [{analysisType}] Case '{caseName}': {message}")
    {
        AnalysisType = analysisType;
        CaseName = caseName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with inner exception
    /// </summary>
    public EtabsAnalysisException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsAnalysisException class with full details and inner exception
    /// </summary>
    public EtabsAnalysisException(int returnCode, string operation, string analysisType, string caseName, string message, Exception innerException)
        : base(returnCode, operation, $"Analysis [{analysisType}] Case '{caseName}': {message}", innerException)
    {
        AnalysisType = analysisType;
        CaseName = caseName;
    }
}

/// <summary>
/// Exception thrown when analysis model validation fails
/// </summary>
public class EtabsModelValidationException : EtabsAnalysisException
{
    /// <summary>
    /// Gets the list of validation errors
    /// </summary>
    public List<string> ValidationErrors { get; }

    public EtabsModelValidationException(List<string> validationErrors)
        : base($"Model validation failed with {validationErrors.Count} error(s): {string.Join("; ", validationErrors)}")
    {
        ValidationErrors = validationErrors;
    }

    public EtabsModelValidationException(string singleError)
        : base($"Model validation failed: {singleError}")
    {
        ValidationErrors = new List<string> { singleError };
    }
}

/// <summary>
/// Exception thrown when analysis execution fails
/// </summary>
public class EtabsAnalysisExecutionException : EtabsAnalysisException
{
    /// <summary>
    /// Gets the analysis progress when the failure occurred (0.0 to 1.0)
    /// </summary>
    public double Progress { get; }

    public EtabsAnalysisExecutionException(string analysisType, string message, double progress = 0.0)
        : base(analysisType, $"{message} (Progress: {progress:P0})")
    {
        Progress = progress;
    }

    public EtabsAnalysisExecutionException(int returnCode, string operation, string analysisType, string caseName, string message, double progress = 0.0)
        : base(returnCode, operation, analysisType, caseName, $"{message} (Progress: {progress:P0})")
    {
        Progress = progress;
    }
}

/// <summary>
/// Exception thrown when analysis results are not available or invalid
/// </summary>
public class EtabsResultsException : EtabsAnalysisException
{
    /// <summary>
    /// Gets the type of results that were requested
    /// </summary>
    public string? ResultType { get; }

    public EtabsResultsException(string resultType, string message)
        : base($"Results [{resultType}]: {message}")
    {
        ResultType = resultType;
    }

    public EtabsResultsException(int returnCode, string operation, string resultType, string message)
        : base(returnCode, operation, $"Results [{resultType}]: {message}")
    {
        ResultType = resultType;
    }

    public EtabsResultsException(string resultType, string caseName, string message)
        : base($"Results [{resultType}] for case '{caseName}': {message}")
    {
        ResultType = resultType;
        // CaseName is inherited from EtabsAnalysisException and is read-only
        // We cannot set it directly here, but the message already includes the case name
    }
}
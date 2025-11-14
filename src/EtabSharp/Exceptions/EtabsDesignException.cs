namespace EtabSharp.Exceptions;

/// <summary>
/// Exception thrown when a design operation fails
/// </summary>
public class EtabsDesignException : EtabsException
{
    /// <summary>
    /// Gets the design code being used (AISC, ACI, Eurocode, etc.)
    /// </summary>
    public string? DesignCode { get; }

    /// <summary>
    /// Gets the type of design (Steel, Concrete, Shear Wall, etc.)
    /// </summary>
    public string? DesignType { get; }

    /// <summary>
    /// Gets the name of the element being designed
    /// </summary>
    public string? ElementName { get; }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class
    /// </summary>
    public EtabsDesignException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with design type
    /// </summary>
    public EtabsDesignException(string designType, string message)
        : base($"Design [{designType}]: {message}")
    {
        DesignType = designType;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with design type and code
    /// </summary>
    public EtabsDesignException(string designType, string designCode, string message)
        : base($"Design [{designType} - {designCode}]: {message}")
    {
        DesignType = designType;
        DesignCode = designCode;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with element details
    /// </summary>
    public EtabsDesignException(string designType, string designCode, string elementName, string message)
        : base($"Design [{designType} - {designCode}] Element '{elementName}': {message}")
    {
        DesignType = designType;
        DesignCode = designCode;
        ElementName = elementName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with return code
    /// </summary>
    public EtabsDesignException(int returnCode, string operation, string message)
        : base(returnCode, operation, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with full details
    /// </summary>
    public EtabsDesignException(int returnCode, string operation, string designType, string designCode, string elementName, string message)
        : base(returnCode, operation, $"Design [{designType} - {designCode}] Element '{elementName}': {message}")
    {
        DesignType = designType;
        DesignCode = designCode;
        ElementName = elementName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with inner exception
    /// </summary>
    public EtabsDesignException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EtabsDesignException class with full details and inner exception
    /// </summary>
    public EtabsDesignException(int returnCode, string operation, string designType, string designCode, string elementName, string message, Exception innerException)
        : base(returnCode, operation, $"Design [{designType} - {designCode}] Element '{elementName}': {message}", innerException)
    {
        DesignType = designType;
        DesignCode = designCode;
        ElementName = elementName;
    }
}

/// <summary>
/// Exception thrown when steel design operations fail
/// </summary>
public class EtabsSteelDesignException : EtabsDesignException
{
    public EtabsSteelDesignException(string designCode, string message)
        : base("Steel", designCode, message)
    {
    }

    public EtabsSteelDesignException(string designCode, string elementName, string message)
        : base("Steel", designCode, elementName, message)
    {
    }

    public EtabsSteelDesignException(int returnCode, string operation, string designCode, string elementName, string message)
        : base(returnCode, operation, "Steel", designCode, elementName, message)
    {
    }
}

/// <summary>
/// Exception thrown when concrete design operations fail
/// </summary>
public class EtabsConcreteDesignException : EtabsDesignException
{
    public EtabsConcreteDesignException(string designCode, string message)
        : base("Concrete", designCode, message)
    {
    }

    public EtabsConcreteDesignException(string designCode, string elementName, string message)
        : base("Concrete", designCode, elementName, message)
    {
    }

    public EtabsConcreteDesignException(int returnCode, string operation, string designCode, string elementName, string message)
        : base(returnCode, operation, "Concrete", designCode, elementName, message)
    {
    }
}

/// <summary>
/// Exception thrown when shear wall design operations fail
/// </summary>
public class EtabsShearWallDesignException : EtabsDesignException
{
    public EtabsShearWallDesignException(string designCode, string message)
        : base("Shear Wall", designCode, message)
    {
    }

    public EtabsShearWallDesignException(string designCode, string elementName, string message)
        : base("Shear Wall", designCode, elementName, message)
    {
    }

    public EtabsShearWallDesignException(int returnCode, string operation, string designCode, string elementName, string message)
        : base(returnCode, operation, "Shear Wall", designCode, elementName, message)
    {
    }
}

/// <summary>
/// Exception thrown when design code is not supported or not available
/// </summary>
public class EtabsDesignCodeException : EtabsDesignException
{
    /// <summary>
    /// Gets the list of available design codes
    /// </summary>
    public List<string>? AvailableCodes { get; }

    public EtabsDesignCodeException(string designType, string requestedCode, List<string>? availableCodes = null)
        : base(designType, $"Design code '{requestedCode}' is not supported or not available. " +
               (availableCodes?.Any() == true ? $"Available codes: {string.Join(", ", availableCodes)}" : ""))
    {
        // DesignCode is inherited from EtabsDesignException and is read-only
        // We cannot set it directly here, but the message already includes the requested code
        AvailableCodes = availableCodes;
    }
}

/// <summary>
/// Exception thrown when design preferences or parameters are invalid
/// </summary>
public class EtabsDesignPreferencesException : EtabsDesignException
{
    /// <summary>
    /// Gets the name of the invalid preference parameter
    /// </summary>
    public string? ParameterName { get; }

    /// <summary>
    /// Gets the invalid value that was provided
    /// </summary>
    public object? InvalidValue { get; }

    public EtabsDesignPreferencesException(string designType, string designCode, string parameterName, object? invalidValue, string message)
        : base(designType, designCode, $"Invalid preference '{parameterName}': {message}")
    {
        ParameterName = parameterName;
        InvalidValue = invalidValue;
    }
}
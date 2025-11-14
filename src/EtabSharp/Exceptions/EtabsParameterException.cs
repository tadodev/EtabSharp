namespace EtabSharp.Exceptions;

/// <summary>
/// Exception thrown when a required parameter is null or empty
/// </summary>
public class EtabsParameterNullException : EtabsInvalidParameterException
{
    public EtabsParameterNullException(string parameterName)
        : base(parameterName, null, "Parameter cannot be null or empty")
    {
    }

    public EtabsParameterNullException(string parameterName, string customMessage)
        : base(parameterName, null, customMessage)
    {
    }
}

/// <summary>
/// Exception thrown when a parameter value is out of the valid range
/// </summary>
public class EtabsParameterOutOfRangeException : EtabsInvalidParameterException
{
    /// <summary>
    /// Gets the minimum valid value
    /// </summary>
    public object? MinValue { get; }

    /// <summary>
    /// Gets the maximum valid value
    /// </summary>
    public object? MaxValue { get; }

    public EtabsParameterOutOfRangeException(string parameterName, object? invalidValue, object? minValue, object? maxValue)
        : base(parameterName, invalidValue, $"Value must be between {minValue} and {maxValue}")
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public EtabsParameterOutOfRangeException(string parameterName, object? invalidValue, string customMessage)
        : base(parameterName, invalidValue, customMessage)
    {
    }
}

/// <summary>
/// Exception thrown when a parameter value is not in the list of valid options
/// </summary>
public class EtabsParameterInvalidOptionException : EtabsInvalidParameterException
{
    /// <summary>
    /// Gets the list of valid options
    /// </summary>
    public List<object> ValidOptions { get; }

    public EtabsParameterInvalidOptionException(string parameterName, object? invalidValue, List<object> validOptions)
        : base(parameterName, invalidValue, $"Valid options are: {string.Join(", ", validOptions)}")
    {
        ValidOptions = validOptions;
    }

    public EtabsParameterInvalidOptionException(string parameterName, object? invalidValue, params object[] validOptions)
        : base(parameterName, invalidValue, $"Valid options are: {string.Join(", ", validOptions)}")
    {
        ValidOptions = validOptions.ToList();
    }
}

/// <summary>
/// Exception thrown when multiple parameters have validation errors
/// </summary>
public class EtabsMultipleParameterException : EtabsException
{
    /// <summary>
    /// Gets the dictionary of parameter names and their validation errors
    /// </summary>
    public Dictionary<string, string> ParameterErrors { get; }

    public EtabsMultipleParameterException(Dictionary<string, string> parameterErrors)
        : base($"Multiple parameter validation errors: {string.Join("; ", parameterErrors.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}")
    {
        ParameterErrors = parameterErrors;
    }

    public EtabsMultipleParameterException(params (string parameterName, string error)[] errors)
        : base($"Multiple parameter validation errors: {string.Join("; ", errors.Select(e => $"{e.parameterName}: {e.error}"))}")
    {
        ParameterErrors = errors.ToDictionary(e => e.parameterName, e => e.error);
    }
}

/// <summary>
/// Exception thrown when coordinate system parameters are invalid
/// </summary>
public class EtabsCoordinateSystemException : EtabsInvalidParameterException
{
    /// <summary>
    /// Gets the coordinate system name that was invalid
    /// </summary>
    public string? CoordinateSystemName { get; }

    public EtabsCoordinateSystemException(string coordinateSystemName, string message)
        : base("CoordinateSystem", coordinateSystemName, message)
    {
        CoordinateSystemName = coordinateSystemName;
    }

    public EtabsCoordinateSystemException(string coordinateSystemName)
        : base("CoordinateSystem", coordinateSystemName, $"Coordinate system '{coordinateSystemName}' does not exist or is not valid")
    {
        CoordinateSystemName = coordinateSystemName;
    }
}

/// <summary>
/// Exception thrown when unit system parameters are invalid
/// </summary>
public class EtabsUnitSystemException : EtabsInvalidParameterException
{
    /// <summary>
    /// Gets the unit system that was invalid
    /// </summary>
    public string? UnitSystem { get; }

    public EtabsUnitSystemException(string unitSystem, string message)
        : base("UnitSystem", unitSystem, message)
    {
        UnitSystem = unitSystem;
    }

    public EtabsUnitSystemException(string unitSystem)
        : base("UnitSystem", unitSystem, $"Unit system '{unitSystem}' is not supported")
    {
        UnitSystem = unitSystem;
    }
}

/// <summary>
/// Exception thrown when load pattern parameters are invalid
/// </summary>
public class EtabsLoadPatternException : EtabsInvalidParameterException
{
    /// <summary>
    /// Gets the load pattern name that was invalid
    /// </summary>
    public string? LoadPatternName { get; }

    public EtabsLoadPatternException(string loadPatternName, string message)
        : base("LoadPattern", loadPatternName, message)
    {
        LoadPatternName = loadPatternName;
    }

    public EtabsLoadPatternException(string loadPatternName)
        : base("LoadPattern", loadPatternName, $"Load pattern '{loadPatternName}' does not exist")
    {
        LoadPatternName = loadPatternName;
    }
}
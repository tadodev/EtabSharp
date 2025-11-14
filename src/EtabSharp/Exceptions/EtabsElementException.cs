namespace EtabSharp.Exceptions;

/// <summary>
/// Exception thrown when an element operation fails
/// </summary>
public class EtabsElementException : EtabsException
{
    /// <summary>
    /// Gets the name of the element that caused the error
    /// </summary>
    public string? ElementName { get; }

    /// <summary>
    /// Gets the type of element (Point, Frame, Area, etc.)
    /// </summary>
    public string? ElementType { get; }

    /// <summary>
    /// Initializes a new instance of the EtabsElementException class
    /// </summary>
    public EtabsElementException(string elementType, string elementName, string message)
        : base($"{elementType} '{elementName}': {message}")
    {
        ElementType = elementType;
        ElementName = elementName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsElementException class with return code
    /// </summary>
    public EtabsElementException(int returnCode, string operation, string elementType, string elementName, string message)
        : base(returnCode, operation, $"{elementType} '{elementName}': {message}")
    {
        ElementType = elementType;
        ElementName = elementName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsElementException class with inner exception
    /// </summary>
    public EtabsElementException(string elementType, string elementName, string message, Exception innerException)
        : base($"{elementType} '{elementName}': {message}", innerException)
    {
        ElementType = elementType;
        ElementName = elementName;
    }

    /// <summary>
    /// Initializes a new instance of the EtabsElementException class with full details and inner exception
    /// </summary>
    public EtabsElementException(int returnCode, string operation, string elementType, string elementName, string message, Exception innerException)
        : base(returnCode, operation, $"{elementType} '{elementName}': {message}", innerException)
    {
        ElementType = elementType;
        ElementName = elementName;
    }
}

/// <summary>
/// Exception thrown when a point object operation fails
/// </summary>
public class EtabsPointException : EtabsElementException
{
    public EtabsPointException(string pointName, string message)
        : base("Point", pointName, message)
    {
    }

    public EtabsPointException(int returnCode, string operation, string pointName, string message)
        : base(returnCode, operation, "Point", pointName, message)
    {
    }

    public EtabsPointException(string pointName, string message, Exception innerException)
        : base("Point", pointName, message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a frame object operation fails
/// </summary>
public class EtabsFrameException : EtabsElementException
{
    public EtabsFrameException(string frameName, string message)
        : base("Frame", frameName, message)
    {
    }

    public EtabsFrameException(int returnCode, string operation, string frameName, string message)
        : base(returnCode, operation, "Frame", frameName, message)
    {
    }

    public EtabsFrameException(string frameName, string message, Exception innerException)
        : base("Frame", frameName, message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when an area object operation fails
/// </summary>
public class EtabsAreaException : EtabsElementException
{
    public EtabsAreaException(string areaName, string message)
        : base("Area", areaName, message)
    {
    }

    public EtabsAreaException(int returnCode, string operation, string areaName, string message)
        : base(returnCode, operation, "Area", areaName, message)
    {
    }

    public EtabsAreaException(string areaName, string message, Exception innerException)
        : base("Area", areaName, message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when an element connectivity operation fails
/// </summary>
public class EtabsConnectivityException : EtabsElementException
{
    /// <summary>
    /// Gets the list of elements involved in the connectivity issue
    /// </summary>
    public List<string> InvolvedElements { get; }

    public EtabsConnectivityException(string elementType, List<string> involvedElements, string message)
        : base(elementType, string.Join(", ", involvedElements), message)
    {
        InvolvedElements = involvedElements;
    }

    public EtabsConnectivityException(int returnCode, string operation, string elementType, List<string> involvedElements, string message)
        : base(returnCode, operation, elementType, string.Join(", ", involvedElements), message)
    {
        InvolvedElements = involvedElements;
    }
}
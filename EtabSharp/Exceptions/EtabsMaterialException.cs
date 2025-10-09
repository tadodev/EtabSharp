namespace EtabSharp.Exceptions;

/// <summary>
/// Exception thrown when a material operation fails
/// </summary>
public class EtabsMaterialException : EtabsException
{
    /// <summary>
    /// Gets the name of the material that caused the error
    /// </summary>
    public string? MaterialName { get; }

    public EtabsMaterialException(string materialName, string message)
        : base(message)
    {
        MaterialName = materialName;
    }

    public EtabsMaterialException(int returnCode, string operation, string materialName, string message)
        : base(returnCode, operation, $"Material '{materialName}': {message}")
    {
        MaterialName = materialName;
    }
}

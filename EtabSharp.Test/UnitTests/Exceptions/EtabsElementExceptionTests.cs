using EtabSharp.Exceptions;

namespace EtabSharp.Test.UnitTests.Exceptions;

public class EtabsElementExceptionTests
{
    [Fact]
    public void EtabsElementException_WithElementTypeAndName_ShouldFormatMessageCorrectly()
    {
        // Arrange
        const string elementType = "Point";
        const string elementName = "P1";
        const string message = "Failed to create point";

        // Act
        var exception = new EtabsElementException(elementType, elementName, message);

        // Assert
        Assert.Equal($"{elementType} '{elementName}': {message}", exception.Message);
        Assert.Equal(elementType, exception.ElementType);
        Assert.Equal(elementName, exception.ElementName);
        Assert.Equal(-1, exception.ReturnCode);
    }

    [Fact]
    public void EtabsElementException_WithReturnCode_ShouldIncludeReturnCodeInMessage()
    {
        // Arrange
        const int returnCode = 5;
        const string operation = "AddPoint";
        const string elementType = "Point";
        const string elementName = "P1";
        const string message = "Invalid coordinates";

        // Act
        var exception = new EtabsElementException(returnCode, operation, elementType, elementName, message);

        // Assert
        Assert.Contains($"[{operation}]", exception.Message);
        Assert.Contains($"{elementType} '{elementName}': {message}", exception.Message);
        Assert.Contains($"(Return code: {returnCode})", exception.Message);
        Assert.Equal(returnCode, exception.ReturnCode);
        Assert.Equal(operation, exception.Operation);
        Assert.Equal(elementType, exception.ElementType);
        Assert.Equal(elementName, exception.ElementName);
    }

    [Fact]
    public void EtabsElementException_WithInnerException_ShouldPreserveInnerException()
    {
        // Arrange
        const string elementType = "Frame";
        const string elementName = "F1";
        const string message = "Connection failed";
        var innerException = new InvalidOperationException("Inner error");

        // Act
        var exception = new EtabsElementException(elementType, elementName, message, innerException);

        // Assert
        Assert.Equal(innerException, exception.InnerException);
        Assert.Contains($"{elementType} '{elementName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsPointException_ShouldSetElementTypeToPoint()
    {
        // Arrange
        const string pointName = "P1";
        const string message = "Point creation failed";

        // Act
        var exception = new EtabsPointException(pointName, message);

        // Assert
        Assert.Equal("Point", exception.ElementType);
        Assert.Equal(pointName, exception.ElementName);
        Assert.Contains($"Point '{pointName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsFrameException_ShouldSetElementTypeToFrame()
    {
        // Arrange
        const string frameName = "F1";
        const string message = "Frame creation failed";

        // Act
        var exception = new EtabsFrameException(frameName, message);

        // Assert
        Assert.Equal("Frame", exception.ElementType);
        Assert.Equal(frameName, exception.ElementName);
        Assert.Contains($"Frame '{frameName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsAreaException_ShouldSetElementTypeToArea()
    {
        // Arrange
        const string areaName = "A1";
        const string message = "Area creation failed";

        // Act
        var exception = new EtabsAreaException(areaName, message);

        // Assert
        Assert.Equal("Area", exception.ElementType);
        Assert.Equal(areaName, exception.ElementName);
        Assert.Contains($"Area '{areaName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsConnectivityException_WithMultipleElements_ShouldJoinElementNames()
    {
        // Arrange
        const string elementType = "Frame";
        var involvedElements = new List<string> { "P1", "P2", "F1" };
        const string message = "Connectivity validation failed";

        // Act
        var exception = new EtabsConnectivityException(elementType, involvedElements, message);

        // Assert
        Assert.Equal(elementType, exception.ElementType);
        Assert.Equal(involvedElements, exception.InvolvedElements);
        Assert.Contains("P1, P2, F1", exception.ElementName);
        Assert.Contains(message, exception.Message);
    }

    [Theory]
    [InlineData("Point", "P1", "Test message")]
    [InlineData("Frame", "F1", "Another test")]
    [InlineData("Area", "A1", "Area test")]
    public void EtabsElementException_WithDifferentElementTypes_ShouldFormatCorrectly(
        string elementType, string elementName, string message)
    {
        // Act
        var exception = new EtabsElementException(elementType, elementName, message);

        // Assert
        Assert.Equal(elementType, exception.ElementType);
        Assert.Equal(elementName, exception.ElementName);
        Assert.Equal($"{elementType} '{elementName}': {message}", exception.Message);
    }
}
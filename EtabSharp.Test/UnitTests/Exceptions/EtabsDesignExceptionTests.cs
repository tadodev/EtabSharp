using EtabSharp.Exceptions;

namespace EtabSharp.Test.UnitTests.Exceptions;

public class EtabsDesignExceptionTests
{
    [Fact]
    public void EtabsDesignException_WithBasicMessage_ShouldSetMessage()
    {
        // Arrange
        const string message = "Design failed";

        // Act
        var exception = new EtabsDesignException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Null(exception.DesignType);
        Assert.Null(exception.DesignCode);
        Assert.Null(exception.ElementName);
    }

    [Fact]
    public void EtabsDesignException_WithDesignType_ShouldFormatMessage()
    {
        // Arrange
        const string designType = "Steel";
        const string message = "Design check failed";

        // Act
        var exception = new EtabsDesignException(designType, message);

        // Assert
        Assert.Equal($"Design [{designType}]: {message}", exception.Message);
        Assert.Equal(designType, exception.DesignType);
        Assert.Null(exception.DesignCode);
        Assert.Null(exception.ElementName);
    }

    [Fact]
    public void EtabsDesignException_WithDesignTypeAndCode_ShouldFormatMessage()
    {
        // Arrange
        const string designType = "Steel";
        const string designCode = "AISC 360-22";
        const string message = "Code check failed";

        // Act
        var exception = new EtabsDesignException(designType, designCode, message);

        // Assert
        Assert.Equal($"Design [{designType} - {designCode}]: {message}", exception.Message);
        Assert.Equal(designType, exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Null(exception.ElementName);
    }

    [Fact]
    public void EtabsDesignException_WithFullDetails_ShouldFormatCompleteMessage()
    {
        // Arrange
        const string designType = "Concrete";
        const string designCode = "ACI 318-19";
        const string elementName = "C1";
        const string message = "Reinforcement design failed";

        // Act
        var exception = new EtabsDesignException(designType, designCode, elementName, message);

        // Assert
        Assert.Equal($"Design [{designType} - {designCode}] Element '{elementName}': {message}", exception.Message);
        Assert.Equal(designType, exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(elementName, exception.ElementName);
    }

    [Fact]
    public void EtabsDesignException_WithReturnCodeAndFullDetails_ShouldIncludeAllInformation()
    {
        // Arrange
        const int returnCode = 5;
        const string operation = "DesignSteel";
        const string designType = "Steel";
        const string designCode = "AISC 360-22";
        const string elementName = "B1";
        const string message = "Section inadequate";

        // Act
        var exception = new EtabsDesignException(returnCode, operation, designType, designCode, elementName, message);

        // Assert
        Assert.Contains($"[{operation}]", exception.Message);
        Assert.Contains($"Design [{designType} - {designCode}] Element '{elementName}': {message}", exception.Message);
        Assert.Contains($"(Return code: {returnCode})", exception.Message);
        Assert.Equal(returnCode, exception.ReturnCode);
        Assert.Equal(operation, exception.Operation);
        Assert.Equal(designType, exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(elementName, exception.ElementName);
    }

    [Fact]
    public void EtabsSteelDesignException_ShouldSetDesignTypeToSteel()
    {
        // Arrange
        const string designCode = "AISC 360-22";
        const string message = "Steel design failed";

        // Act
        var exception = new EtabsSteelDesignException(designCode, message);

        // Assert
        Assert.Equal("Steel", exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Contains($"Design [Steel - {designCode}]: {message}", exception.Message);
    }

    [Fact]
    public void EtabsSteelDesignException_WithElementName_ShouldIncludeElementName()
    {
        // Arrange
        const string designCode = "AISC 360-22";
        const string elementName = "B1";
        const string message = "Beam design failed";

        // Act
        var exception = new EtabsSteelDesignException(designCode, elementName, message);

        // Assert
        Assert.Equal("Steel", exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(elementName, exception.ElementName);
        Assert.Contains($"Design [Steel - {designCode}] Element '{elementName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsConcreteDesignException_ShouldSetDesignTypeToConcrete()
    {
        // Arrange
        const string designCode = "ACI 318-19";
        const string message = "Concrete design failed";

        // Act
        var exception = new EtabsConcreteDesignException(designCode, message);

        // Assert
        Assert.Equal("Concrete", exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Contains($"Design [Concrete - {designCode}]: {message}", exception.Message);
    }

    [Fact]
    public void EtabsShearWallDesignException_ShouldSetDesignTypeToShearWall()
    {
        // Arrange
        const string designCode = "ACI 318-19";
        const string elementName = "SW1";
        const string message = "Shear wall design failed";

        // Act
        var exception = new EtabsShearWallDesignException(designCode, elementName, message);

        // Assert
        Assert.Equal("Shear Wall", exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(elementName, exception.ElementName);
        Assert.Contains($"Design [Shear Wall - {designCode}] Element '{elementName}': {message}", exception.Message);
    }

    [Fact]
    public void EtabsDesignCodeException_WithAvailableCodes_ShouldListAvailableCodes()
    {
        // Arrange
        const string designType = "Steel";
        const string requestedCode = "INVALID_CODE";
        var availableCodes = new List<string> { "AISC 360-22", "Canadian S16-24", "Eurocode 3" };

        // Act
        var exception = new EtabsDesignCodeException(designType, requestedCode, availableCodes);

        // Assert
        Assert.Equal(designType, exception.DesignType);
        // DesignCode is inherited from EtabsDesignException and cannot be set in EtabsDesignCodeException
        // But the message should include the requested code
        Assert.Equal(availableCodes, exception.AvailableCodes);
        Assert.Contains("not supported or not available", exception.Message);
        Assert.Contains("Available codes: AISC 360-22, Canadian S16-24, Eurocode 3", exception.Message);
        Assert.Contains(requestedCode, exception.Message);
    }

    [Fact]
    public void EtabsDesignCodeException_WithoutAvailableCodes_ShouldNotListCodes()
    {
        // Arrange
        const string designType = "Steel";
        const string requestedCode = "INVALID_CODE";

        // Act
        var exception = new EtabsDesignCodeException(designType, requestedCode);

        // Assert
        Assert.Equal(designType, exception.DesignType);
        // DesignCode is inherited from EtabsDesignException and cannot be set in EtabsDesignCodeException
        // But the message should include the requested code
        Assert.Null(exception.AvailableCodes);
        Assert.Contains("not supported or not available", exception.Message);
        Assert.DoesNotContain("Available codes:", exception.Message);
        Assert.Contains(requestedCode, exception.Message);
    }

    [Fact]
    public void EtabsDesignPreferencesException_ShouldIncludeParameterDetails()
    {
        // Arrange
        const string designType = "Steel";
        const string designCode = "AISC 360-22";
        const string parameterName = "Cb";
        const double invalidValue = -1.5;
        const string message = "Value must be positive";

        // Act
        var exception = new EtabsDesignPreferencesException(designType, designCode, parameterName, invalidValue, message);

        // Assert
        Assert.Equal(designType, exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Contains($"Invalid preference '{parameterName}': {message}", exception.Message);
    }

    [Theory]
    [InlineData("Steel", "AISC 360-22", "B1", "Beam inadequate")]
    [InlineData("Concrete", "ACI 318-19", "C1", "Column reinforcement insufficient")]
    [InlineData("Shear Wall", "ACI 318-19", "SW1", "Wall capacity exceeded")]
    public void EtabsDesignException_WithVariousDesignTypes_ShouldFormatCorrectly(
        string designType, string designCode, string elementName, string message)
    {
        // Act
        var exception = new EtabsDesignException(designType, designCode, elementName, message);

        // Assert
        Assert.Equal(designType, exception.DesignType);
        Assert.Equal(designCode, exception.DesignCode);
        Assert.Equal(elementName, exception.ElementName);
        Assert.Contains(designType, exception.Message);
        Assert.Contains(designCode, exception.Message);
        Assert.Contains(elementName, exception.Message);
        Assert.Contains(message, exception.Message);
    }
}
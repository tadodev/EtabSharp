using EtabSharp.Exceptions;

namespace EtabSharp.Test.UnitTests.Exceptions;

public class EtabsParameterExceptionTests
{
    [Fact]
    public void EtabsParameterNullException_WithParameterName_ShouldFormatMessage()
    {
        // Arrange
        const string parameterName = "pointName";

        // Act
        var exception = new EtabsParameterNullException(parameterName);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Null(exception.InvalidValue);
        Assert.Contains("Parameter cannot be null or empty", exception.Message);
        Assert.Contains($"Invalid parameter '{parameterName}'", exception.Message);
    }

    [Fact]
    public void EtabsParameterNullException_WithCustomMessage_ShouldUseCustomMessage()
    {
        // Arrange
        const string parameterName = "coordinates";
        const string customMessage = "Coordinate array cannot be null";

        // Act
        var exception = new EtabsParameterNullException(parameterName, customMessage);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Contains(customMessage, exception.Message);
    }

    [Fact]
    public void EtabsParameterOutOfRangeException_WithMinMaxValues_ShouldIncludeRange()
    {
        // Arrange
        const string parameterName = "angle";
        const double invalidValue = 450.0;
        const double minValue = 0.0;
        const double maxValue = 360.0;

        // Act
        var exception = new EtabsParameterOutOfRangeException(parameterName, invalidValue, minValue, maxValue);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Equal(minValue, exception.MinValue);
        Assert.Equal(maxValue, exception.MaxValue);
        Assert.Contains($"Value must be between {minValue} and {maxValue}", exception.Message);
    }

    [Fact]
    public void EtabsParameterOutOfRangeException_WithCustomMessage_ShouldUseCustomMessage()
    {
        // Arrange
        const string parameterName = "loadFactor";
        const double invalidValue = -2.0;
        const string customMessage = "Load factor must be positive";

        // Act
        var exception = new EtabsParameterOutOfRangeException(parameterName, invalidValue, customMessage);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Contains(customMessage, exception.Message);
    }

    [Fact]
    public void EtabsParameterInvalidOptionException_WithValidOptionsList_ShouldListOptions()
    {
        // Arrange
        const string parameterName = "unitSystem";
        const string invalidValue = "INVALID";
        var validOptions = new List<object> { "kip_in_F", "kN_m_C", "lb_ft_F" };

        // Act
        var exception = new EtabsParameterInvalidOptionException(parameterName, invalidValue, validOptions);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Equal(validOptions, exception.ValidOptions);
        Assert.Contains("Valid options are: kip_in_F, kN_m_C, lb_ft_F", exception.Message);
    }

    [Fact]
    public void EtabsParameterInvalidOptionException_WithParamsArray_ShouldConvertToList()
    {
        // Arrange
        const string parameterName = "analysisType";
        const string invalidValue = "INVALID";

        // Act
        var exception = new EtabsParameterInvalidOptionException(parameterName, invalidValue, "Static", "Modal", "ResponseSpectrum");

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Equal(3, exception.ValidOptions.Count);
        Assert.Contains("Static", exception.ValidOptions);
        Assert.Contains("Modal", exception.ValidOptions);
        Assert.Contains("ResponseSpectrum", exception.ValidOptions);
        Assert.Contains("Valid options are: Static, Modal, ResponseSpectrum", exception.Message);
    }

    [Fact]
    public void EtabsMultipleParameterException_WithDictionary_ShouldJoinErrors()
    {
        // Arrange
        var parameterErrors = new Dictionary<string, string>
        {
            { "pointName", "Cannot be null or empty" },
            { "coordinates", "Must have 3 values" },
            { "unitSystem", "Invalid unit system" }
        };

        // Act
        var exception = new EtabsMultipleParameterException(parameterErrors);

        // Assert
        Assert.Equal(parameterErrors, exception.ParameterErrors);
        Assert.Contains("Multiple parameter validation errors", exception.Message);
        Assert.Contains("pointName: Cannot be null or empty", exception.Message);
        Assert.Contains("coordinates: Must have 3 values", exception.Message);
        Assert.Contains("unitSystem: Invalid unit system", exception.Message);
    }

    [Fact]
    public void EtabsMultipleParameterException_WithTupleArray_ShouldCreateDictionary()
    {
        // Arrange & Act
        var exception = new EtabsMultipleParameterException(
            ("x", "X coordinate is required"),
            ("y", "Y coordinate is required"),
            ("z", "Z coordinate is required")
        );

        // Assert
        Assert.Equal(3, exception.ParameterErrors.Count);
        Assert.Equal("X coordinate is required", exception.ParameterErrors["x"]);
        Assert.Equal("Y coordinate is required", exception.ParameterErrors["y"]);
        Assert.Equal("Z coordinate is required", exception.ParameterErrors["z"]);
        Assert.Contains("Multiple parameter validation errors", exception.Message);
    }

    [Fact]
    public void EtabsCoordinateSystemException_WithCoordinateSystemName_ShouldFormatMessage()
    {
        // Arrange
        const string coordinateSystemName = "INVALID_CS";
        const string message = "Coordinate system not found";

        // Act
        var exception = new EtabsCoordinateSystemException(coordinateSystemName, message);

        // Assert
        Assert.Equal("CoordinateSystem", exception.ParameterName);
        Assert.Equal(coordinateSystemName, exception.InvalidValue);
        Assert.Equal(coordinateSystemName, exception.CoordinateSystemName);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void EtabsCoordinateSystemException_WithDefaultMessage_ShouldUseDefaultMessage()
    {
        // Arrange
        const string coordinateSystemName = "INVALID_CS";

        // Act
        var exception = new EtabsCoordinateSystemException(coordinateSystemName);

        // Assert
        Assert.Equal(coordinateSystemName, exception.CoordinateSystemName);
        Assert.Contains("does not exist or is not valid", exception.Message);
        Assert.Contains(coordinateSystemName, exception.Message);
    }

    [Fact]
    public void EtabsUnitSystemException_WithUnitSystem_ShouldFormatMessage()
    {
        // Arrange
        const string unitSystem = "INVALID_UNITS";
        const string message = "Unit system not supported";

        // Act
        var exception = new EtabsUnitSystemException(unitSystem, message);

        // Assert
        Assert.Equal("UnitSystem", exception.ParameterName);
        Assert.Equal(unitSystem, exception.InvalidValue);
        Assert.Equal(unitSystem, exception.UnitSystem);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void EtabsUnitSystemException_WithDefaultMessage_ShouldUseDefaultMessage()
    {
        // Arrange
        const string unitSystem = "INVALID_UNITS";

        // Act
        var exception = new EtabsUnitSystemException(unitSystem);

        // Assert
        Assert.Equal(unitSystem, exception.UnitSystem);
        Assert.Contains("is not supported", exception.Message);
        Assert.Contains(unitSystem, exception.Message);
    }

    [Fact]
    public void EtabsLoadPatternException_WithLoadPatternName_ShouldFormatMessage()
    {
        // Arrange
        const string loadPatternName = "INVALID_PATTERN";
        const string message = "Load pattern not found";

        // Act
        var exception = new EtabsLoadPatternException(loadPatternName, message);

        // Assert
        Assert.Equal("LoadPattern", exception.ParameterName);
        Assert.Equal(loadPatternName, exception.InvalidValue);
        Assert.Equal(loadPatternName, exception.LoadPatternName);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void EtabsLoadPatternException_WithDefaultMessage_ShouldUseDefaultMessage()
    {
        // Arrange
        const string loadPatternName = "INVALID_PATTERN";

        // Act
        var exception = new EtabsLoadPatternException(loadPatternName);

        // Assert
        Assert.Equal(loadPatternName, exception.LoadPatternName);
        Assert.Contains("does not exist", exception.Message);
        Assert.Contains(loadPatternName, exception.Message);
    }

    [Theory]
    [InlineData("param1", 5, 1, 10)]
    [InlineData("param2", -5, 0, 100)]
    [InlineData("param3", 150, 0, 100)]
    public void EtabsParameterOutOfRangeException_WithVariousValues_ShouldFormatCorrectly(
        string parameterName, object invalidValue, object minValue, object maxValue)
    {
        // Act
        var exception = new EtabsParameterOutOfRangeException(parameterName, invalidValue, minValue, maxValue);

        // Assert
        Assert.Equal(parameterName, exception.ParameterName);
        Assert.Equal(invalidValue, exception.InvalidValue);
        Assert.Equal(minValue, exception.MinValue);
        Assert.Equal(maxValue, exception.MaxValue);
        Assert.Contains($"Value must be between {minValue} and {maxValue}", exception.Message);
    }
}
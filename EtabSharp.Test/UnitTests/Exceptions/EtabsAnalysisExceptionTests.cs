using EtabSharp.Exceptions;

namespace EtabSharp.Test.UnitTests.Exceptions;

public class EtabsAnalysisExceptionTests
{
    [Fact]
    public void EtabsAnalysisException_WithBasicMessage_ShouldSetMessage()
    {
        // Arrange
        const string message = "Analysis failed";

        // Act
        var exception = new EtabsAnalysisException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Null(exception.AnalysisType);
        Assert.Null(exception.CaseName);
    }

    [Fact]
    public void EtabsAnalysisException_WithAnalysisType_ShouldFormatMessage()
    {
        // Arrange
        const string analysisType = "Modal";
        const string message = "Eigenvalue analysis failed";

        // Act
        var exception = new EtabsAnalysisException(analysisType, message);

        // Assert
        Assert.Equal($"Analysis [{analysisType}]: {message}", exception.Message);
        Assert.Equal(analysisType, exception.AnalysisType);
        Assert.Null(exception.CaseName);
    }

    [Fact]
    public void EtabsAnalysisException_WithAnalysisTypeAndCase_ShouldFormatMessage()
    {
        // Arrange
        const string analysisType = "Static";
        const string caseName = "DEAD";
        const string message = "Load case analysis failed";

        // Act
        var exception = new EtabsAnalysisException(analysisType, caseName, message);

        // Assert
        Assert.Equal($"Analysis [{analysisType}] Case '{caseName}': {message}", exception.Message);
        Assert.Equal(analysisType, exception.AnalysisType);
        Assert.Equal(caseName, exception.CaseName);
    }

    [Fact]
    public void EtabsAnalysisException_WithReturnCode_ShouldIncludeReturnCode()
    {
        // Arrange
        const int returnCode = 3;
        const string operation = "RunAnalysis";
        const string message = "Analysis execution failed";

        // Act
        var exception = new EtabsAnalysisException(returnCode, operation, message);

        // Assert
        Assert.Contains($"[{operation}]", exception.Message);
        Assert.Contains($"(Return code: {returnCode})", exception.Message);
        Assert.Equal(returnCode, exception.ReturnCode);
        Assert.Equal(operation, exception.Operation);
    }

    [Fact]
    public void EtabsAnalysisException_WithFullDetails_ShouldFormatCompleteMessage()
    {
        // Arrange
        const int returnCode = 7;
        const string operation = "RunAnalysis";
        const string analysisType = "Response Spectrum";
        const string caseName = "RSX";
        const string message = "Spectrum analysis failed";

        // Act
        var exception = new EtabsAnalysisException(returnCode, operation, analysisType, caseName, message);

        // Assert
        Assert.Contains($"[{operation}]", exception.Message);
        Assert.Contains($"Analysis [{analysisType}] Case '{caseName}': {message}", exception.Message);
        Assert.Contains($"(Return code: {returnCode})", exception.Message);
        Assert.Equal(returnCode, exception.ReturnCode);
        Assert.Equal(operation, exception.Operation);
        Assert.Equal(analysisType, exception.AnalysisType);
        Assert.Equal(caseName, exception.CaseName);
    }

    [Fact]
    public void EtabsModelValidationException_WithMultipleErrors_ShouldJoinErrors()
    {
        // Arrange
        var validationErrors = new List<string>
        {
            "Missing restraints on point P1",
            "Invalid material assignment on frame F1",
            "Duplicate load pattern name"
        };

        // Act
        var exception = new EtabsModelValidationException(validationErrors);

        // Assert
        Assert.Equal(validationErrors, exception.ValidationErrors);
        Assert.Contains("3 error(s)", exception.Message);
        Assert.Contains("Missing restraints on point P1", exception.Message);
        Assert.Contains("Invalid material assignment on frame F1", exception.Message);
        Assert.Contains("Duplicate load pattern name", exception.Message);
    }

    [Fact]
    public void EtabsModelValidationException_WithSingleError_ShouldFormatSingleError()
    {
        // Arrange
        const string singleError = "Model has no elements";

        // Act
        var exception = new EtabsModelValidationException(singleError);

        // Assert
        Assert.Single(exception.ValidationErrors);
        Assert.Equal(singleError, exception.ValidationErrors[0]);
        Assert.Contains("Model validation failed", exception.Message);
        Assert.Contains(singleError, exception.Message);
    }

    [Fact]
    public void EtabsAnalysisExecutionException_WithProgress_ShouldIncludeProgress()
    {
        // Arrange
        const string analysisType = "Static";
        const string message = "Analysis stopped";
        const double progress = 0.75;

        // Act
        var exception = new EtabsAnalysisExecutionException(analysisType, message, progress);

        // Assert
        Assert.Equal(analysisType, exception.AnalysisType);
        Assert.Equal(progress, exception.Progress);
        Assert.Contains("75%", exception.Message);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void EtabsAnalysisExecutionException_WithoutProgress_ShouldDefaultToZero()
    {
        // Arrange
        const string analysisType = "Modal";
        const string message = "Analysis failed to start";

        // Act
        var exception = new EtabsAnalysisExecutionException(analysisType, message);

        // Assert
        Assert.Equal(0.0, exception.Progress);
        Assert.Contains("0%", exception.Message);
    }

    [Fact]
    public void EtabsResultsException_WithResultType_ShouldFormatMessage()
    {
        // Arrange
        const string resultType = "Joint Forces";
        const string message = "Results not available";

        // Act
        var exception = new EtabsResultsException(resultType, message);

        // Assert
        Assert.Equal(resultType, exception.ResultType);
        Assert.Equal($"Results [{resultType}]: {message}", exception.Message);
    }

    [Fact]
    public void EtabsResultsException_WithResultTypeAndCase_ShouldIncludeCaseName()
    {
        // Arrange
        const string resultType = "Frame Forces";
        const string caseName = "DEAD";
        const string message = "No results found";

        // Act
        var exception = new EtabsResultsException(resultType, caseName, message);

        // Assert
        Assert.Equal(resultType, exception.ResultType);
        // CaseName is inherited from EtabsAnalysisException and cannot be set in EtabsResultsException
        // But the message should include the case name
        Assert.Equal($"Results [{resultType}] for case '{caseName}': {message}", exception.Message);
        Assert.Contains(caseName, exception.Message);
    }

    [Theory]
    [InlineData("Static", "DEAD", "Load case failed")]
    [InlineData("Modal", "MODAL", "Eigenvalue extraction failed")]
    [InlineData("Response Spectrum", "RSX", "Spectrum analysis error")]
    public void EtabsAnalysisException_WithVariousInputs_ShouldFormatCorrectly(
        string analysisType, string caseName, string message)
    {
        // Act
        var exception = new EtabsAnalysisException(analysisType, caseName, message);

        // Assert
        Assert.Equal(analysisType, exception.AnalysisType);
        Assert.Equal(caseName, exception.CaseName);
        Assert.Contains(analysisType, exception.Message);
        Assert.Contains(caseName, exception.Message);
        Assert.Contains(message, exception.Message);
    }
}
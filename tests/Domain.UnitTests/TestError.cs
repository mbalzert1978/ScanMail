using Domain.Primitives;
using FluentAssertions;

namespace Domain.UnitTests;

public class ErrorTests
{
    [Theory]
    [InlineData("", "Test Description", "Test Description")]
    [InlineData("Test Code", "Test Description", "Test Description")]
    [InlineData("Test Code", null, null)]
    [InlineData("", null, null)]
    public void Error_WhenCreatedWithVariousInputs_ShouldHaveCorrectCodeAndDescription(string code, string? description, string? expectedDescription)
    {
        // Act
        var error = new Error(code, description);

        // Assert
        error.Code.Should().Be(code);
        error.Description.Should().Be(expectedDescription);
    }

    [Fact]
    public void Error_WhenCreatedAsNone_ShouldHaveEmptyCodeAndDescription()
    {
        // Act
        var error = Error.None;

        // Assert
        error.Code.Should().BeEmpty();
        error.Description.Should().BeEmpty();
    }
}

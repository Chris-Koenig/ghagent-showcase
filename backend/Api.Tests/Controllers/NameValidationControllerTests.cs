using System.Text;
using Api.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Api.Tests.Controllers;

public class NameValidationControllerTests
{
    private readonly NameValidationController _controller;

    public NameValidationControllerTests()
    {
        _controller = new NameValidationController();
    }

    [Theory]
    [InlineData("Hans Müller")] // German umlaut ü
    [InlineData("Jürgen Schmidt")] // German umlaut ü
    [InlineData("Björn Andersson")] // German umlaut ö
    [InlineData("Günther Weber")] // German umlaut ü
    [InlineData("Häns König")] // German umlaut ä
    [InlineData("Müller Götz")] // German umlauts ü, ö
    [InlineData("Schönefeld")] // German umlaut ö
    [InlineData("Weiß")] // German sharp s (ß)
    [InlineData("Franz Größer")] // German ß and ö
    public void ValidateName_GermanCharacters_ShouldReturnValid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("François Dupont")] // French ç
    [InlineData("Amélie Laurent")] // French é
    [InlineData("André Dubois")] // French é
    [InlineData("Céline Martin")] // French é
    [InlineData("Hélène Rousseau")] // French è
    [InlineData("Gérard Moreau")] // French é
    [InlineData("Agnès Simon")] // French è
    [InlineData("Léon Bernard")] // French é
    [InlineData("Anaïs Petit")] // French ï
    [InlineData("Noël Robert")] // French ë
    [InlineData("Zoë Roux")] // French ë
    [InlineData("Chloé Garnier")] // French é
    [InlineData("Raphaël Vincent")] // French ë
    [InlineData("Gaëlle Chevalier")] // French ë
    public void ValidateName_FrenchCharacters_ShouldReturnValid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("John123")] // Numbers
    [InlineData("Mary456")] // Numbers
    [InlineData("Test789")] // Numbers
    [InlineData("Name1")] // Number
    [InlineData("User2024")] // Numbers
    public void ValidateName_WithNumbers_ShouldReturnInvalid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name can only contain letters and spaces", validationResult.Message);
    }

    [Theory]
    [InlineData("John.Doe")] // Period
    [InlineData("Mary,Smith")] // Comma
    [InlineData("Test;Name")] // Semicolon
    [InlineData("User@Domain")] // At symbol
    [InlineData("Name.Test")] // Period
    [InlineData("First,Last")] // Comma
    [InlineData("A;B")] // Semicolon
    [InlineData("Email@Test")] // At symbol
    public void ValidateName_WithSpecialCharacters_ShouldReturnInvalid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name can only contain letters and spaces", validationResult.Message);
    }

    [Theory]
    [InlineData("John Doe")] // Basic English name
    [InlineData("Mary Jane Smith")] // Three words
    [InlineData("Jean Claude Van Damme")] // Four words
    [InlineData("A B")] // Single letters with space
    [InlineData("Anna")] // Single name
    public void ValidateName_ValidEnglishNames_ShouldReturnValid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("")] // Empty string
    [InlineData("   ")] // Only spaces
    [InlineData("\t")] // Tab character
    [InlineData("\n")] // Newline character
    public void ValidateName_EmptyOrWhitespace_ShouldReturnInvalid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name cannot be empty", validationResult.Message);
    }

    [Theory]
    [InlineData("A")] // Single character
    [InlineData("X")] // Single character
    public void ValidateName_TooShort_ShouldReturnInvalid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name must be at least 2 characters long", validationResult.Message);
    }

    [Fact]
    public void ValidateName_TooLong_ShouldReturnInvalid()
    {
        // Arrange - Create a name longer than 100 characters
        var longName = new string('A', 101);
        var request = new NameValidationDto(longName);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name cannot exceed 100 characters", validationResult.Message);
    }

    [Theory]
    [InlineData("  John Doe  ")] // Leading and trailing spaces
    [InlineData(" Mary Smith ")] // Leading and trailing spaces
    [InlineData("\tTest Name\t")] // Leading and trailing tabs
    public void ValidateName_WithLeadingTrailingSpaces_ShouldTrimAndValidate(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("José María")] // Spanish names with accents
    [InlineData("Piña Colada")] // Spanish ñ
    [InlineData("Niño Santos")] // Spanish ñ
    [InlineData("Montaña Verde")] // Spanish ñ
    public void ValidateName_SpanishCharacters_ShouldReturnValid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("Αλέξανδρος")] // Greek letters
    [InlineData("Δημήτρης")] // Greek letters
    [InlineData("Κατερίνα")] // Greek letters
    public void ValidateName_GreekCharacters_ShouldReturnValid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.True(validationResult.IsValid);
        Assert.Equal("Name is valid", validationResult.Message);
    }

    [Theory]
    [InlineData("Name!")] // Exclamation mark
    [InlineData("Name?")] // Question mark
    [InlineData("Name#")] // Hash
    [InlineData("Name$")] // Dollar sign
    [InlineData("Name%")] // Percent
    [InlineData("Name&")] // Ampersand
    [InlineData("Name*")] // Asterisk
    [InlineData("Name+")] // Plus
    [InlineData("Name=")] // Equals
    [InlineData("Name-")] // Hyphen
    [InlineData("Name_")] // Underscore
    [InlineData("Name/")] // Forward slash
    [InlineData("Name\\")] // Backslash
    [InlineData("Name|")] // Pipe
    [InlineData("Name<")] // Less than
    [InlineData("Name>")] // Greater than
    [InlineData("Name[")] // Opening bracket
    [InlineData("Name]")] // Closing bracket
    [InlineData("Name{")] // Opening brace
    [InlineData("Name}")] // Closing brace
    [InlineData("Name(")] // Opening parenthesis
    [InlineData("Name)")] // Closing parenthesis
    [InlineData("Name'")] // Single quote
    [InlineData("Name\"")] // Double quote
    [InlineData("Name`")] // Backtick
    [InlineData("Name~")] // Tilde
    [InlineData("Name^")] // Caret
    public void ValidateName_WithVariousSpecialCharacters_ShouldReturnInvalid(string name)
    {
        // Arrange
        var request = new NameValidationDto(name);

        // Act
        var result = _controller.ValidateName(request);

        // Assert
        var okResult = Assert.IsType<ActionResult<NameValidationResultDto>>(result);
        var returnValue = Assert.IsType<OkObjectResult>(okResult.Result);
        var validationResult = Assert.IsType<NameValidationResultDto>(returnValue.Value);

        Assert.False(validationResult.IsValid);
        Assert.Equal("Name can only contain letters and spaces", validationResult.Message);
    }
}
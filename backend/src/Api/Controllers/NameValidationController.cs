using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class NameValidationController : ControllerBase
{
    private static readonly Regex NameValidationRegex = new("^[a-zA-Z\\s]+$", RegexOptions.Compiled);

    [HttpPost("validate")]
    public ActionResult<NameValidationResultDto> ValidateName([FromBody] NameValidationDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Ok(new NameValidationResultDto(false, "Name cannot be empty"));
        }

        var trimmedName = request.Name.Trim();

        if (trimmedName.Length < 2)
        {
            return Ok(new NameValidationResultDto(false, "Name must be at least 2 characters long"));
        }

        if (trimmedName.Length > 100)
        {
            return Ok(new NameValidationResultDto(false, "Name cannot exceed 100 characters"));
        }

        bool isValid = NameValidationRegex.IsMatch(trimmedName);

        if (isValid)
        {
            return Ok(new NameValidationResultDto(true, "Name is valid"));
        }
        else
        {
            return Ok(new NameValidationResultDto(false, "Name can only contain letters and spaces"));
        }
    }
}
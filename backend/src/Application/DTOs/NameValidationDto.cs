namespace Application.DTOs;

public sealed record NameValidationDto(string Name);

public sealed record NameValidationResultDto(bool IsValid, string Message);
namespace Application.DTOs;

public sealed record HealthTimeDto(DateTime UtcNow, string Status);

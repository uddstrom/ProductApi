namespace productapi.model;

public record InsuranceProductRequest
{
    public int? Ram { get; init; } = null;
    public bool? SurvivorProtection { get; init; }
}


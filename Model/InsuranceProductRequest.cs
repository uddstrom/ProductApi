namespace productapi.model;

public record InsuranceProductRequest
{
    public int? PremiumAmount { get; init; } = null;
    public TaxCategory TaxCategory { get; init; }
    public bool? SurvivorProtection { get; init; }
}


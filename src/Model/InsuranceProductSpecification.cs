namespace productapi.model;

public record InsuranceProductSpecification
{
    public DateOnly BirthDate { get; init; }
    public DateOnly Z { get; init; }
    public int? PremiumAmount { get; init; } = null;
    public TaxCategory TaxCategory { get; init; }
    public bool? SurvivorProtection { get; init; }
}


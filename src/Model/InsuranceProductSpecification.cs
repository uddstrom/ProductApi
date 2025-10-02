namespace productapi.model;

public record InsuranceProductSpecification
{
    public bool? SurvivorProtection { get; init; }
    
    // Get from JWT
    public DateOnly BirthDate { get; init; }

    // Get from Lumera
    public DateOnly Z { get; init; }
    public TaxCategory TaxCategory { get; init; }
    public string BenefitProductId { get; init; } = string.Empty;
    public int Maturity { get; init; } = 0;
}


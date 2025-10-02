using productapi.model;

public record LumeraSettings
{
    public string InsuranceProductName { get; init; } = string.Empty;
    public string InsuranceProductDescription { get; init; } = string.Empty;
    public string BenefitProductId { get; init; } = string.Empty;
    public int Maturity { get; init; } = 0;
    public DateOnly Z { get; init; }
    public TaxCategory TaxCategory { get; init; }
}
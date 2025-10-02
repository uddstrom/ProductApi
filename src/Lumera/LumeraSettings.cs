using productapi.model;

public record LumeraSettings
{
    public string InsuranceProductName { get; init; } = string.Empty;
    public string InsuranceProductDescription { get; init; } = string.Empty;
    public DateOnly Z { get; init; }
    public TaxCategory TaxCategory { get; init; }

}
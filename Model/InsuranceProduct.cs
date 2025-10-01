namespace productapi.model;

public record InsuranceProduct : SchemaBase
{
    public required string InsuranceProductId { get; init; }
    public required string InsuranceProductName { get; init; }
    public required string InsuranceProductDescription { get; init; }
    public ICollection<Parameter> Parameters { get; init; } = [];
}




namespace productapi.model;

public record InsuranceProduct : SchemaBase
{
    public required string InsuranceProductId { get; set; }
    public required string InsuranceProductName { get; set; }
    public required string InsuranceProductDescription { get; set; }
    public Parameter[] Parameters { get; set; } = [];
}




using System.Text.Json.Serialization;

namespace productapi.model;

public record InsuranceProduct : SchemaBase
{
    public required string InsuranceProductId { get; set; }
    public required string InsuranceProductName { get; set; }
    public required string InsuranceProductDescription { get; set; }
    public Parameter[] Parameters { get; set; } = [];
    public string[] Required
    {
        get
        {
            return ["InsuranceProductId", "InsuranceProductName", "InsuranceProductDescription"];
        }
    }
}

public record SurvivorProtection : Parameter
{
    public new string Name { get => "SurvivorProtection"; }
    public new ParameterType Type { get => ParameterType.BooleanType; }
    public new string Message { get => "SurvivorProtection does not have a valid value. Must be true/false."; }
    public bool Value { get; set; } = true;
}

public record PayoutStartAge : Parameter
{
    public new string Name { get => "PayoutStartAge"; }
    public new ParameterType Type { get => ParameterType.ObjectType; }
    public int Minimum { get; set; }
    public int Maximum { get; set; }
}

[JsonDerivedType(typeof(SurvivorProtection))]
[JsonDerivedType(typeof(PayoutStartAge))]
public abstract record Parameter
{
    public virtual string Name { get; } = string.Empty;
    public ParameterType Type { get; set; }
    public string Message { get; } = string.Empty;
}

public enum ParameterType
{
    [JsonPropertyName("object")]
    ObjectType,
    [JsonPropertyName("string")]
    StringType,
    [JsonPropertyName("boolean")]
    BooleanType,
    [JsonPropertyName("integer")]
    IntegerType,
}
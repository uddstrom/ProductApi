using System.Text.Json.Serialization;

namespace productapi.model;


[JsonDerivedType(typeof(SurvivorProtection))]
[JsonDerivedType(typeof(PayoutStartAge))]
[JsonDerivedType(typeof(PremiumAmount))]
public abstract record Parameter
{
    public virtual string Name { get; } = string.Empty;
    public virtual string Label { get; } = string.Empty;
    public ParameterType Type { get; }
    public bool Required { get; } = false;
}


namespace productapi.model;

public record SurvivorProtection : Parameter
{
    public new string Name { get => "SurvivorProtection"; }
    public new string Label { get => "Återbetalningsskydd"; }
    public new ParameterType Type { get => ParameterType.BooleanType; }
    public bool Value { get; set; } = true;
}

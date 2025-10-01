namespace productapi.model;

public record SurvivorProtection : Parameter
{
    public new string Name { get => "SurvivorProtection"; }
    public new string Label { get => "Efterlevandeskydd"; }
    public new ParameterType Type { get => ParameterType.BooleanType; }
    public bool IsEditable { get; init; } = true;
    public bool Value { get; init; } = true;
    public bool Default { get; init; } = true;
}

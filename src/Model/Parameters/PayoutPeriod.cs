namespace productapi.model;

public record PayoutPeriod: Parameter
{
    public new string Name { get => "PayoutPeriod"; }
    public new string Label { get => "Utbetalningstid"; }
    public new ParameterType Type { get => ParameterType.ObjectType; }
    public new bool Required { get => true; }
    public int Minimum { get; init; }
    public int Maximum { get; init; }
}

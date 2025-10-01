namespace productapi.model;

public record PayoutStartDate : Parameter
{
    public new string Name { get => "PayoutStartDate"; }
    public new string Label { get => "Utbetalningsstart"; }
    public new ParameterType Type { get => ParameterType.ObjectType; }
    public new bool Required { get => true; }
    public DateOnly Minimum { get; init; }
    public DateOnly Maximum { get; init; }
    public DateOnly Default { get; init; } // Z?
}

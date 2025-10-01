namespace productapi.model;

public record PayoutStartAge : Parameter
{
    public new string Name { get => "PayoutStartAge"; }
    public new string Label { get => "Utbetalningsålder"; }
    public new ParameterType Type { get => ParameterType.ObjectType; }
    public new bool Required { get => true; }
    public int Minimum { get; init; }
    public int Maximum { get; init; }
    public int Default { get; init; }
}

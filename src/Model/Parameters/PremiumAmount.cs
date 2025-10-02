namespace productapi.model;

public record PremiumAmount : Parameter
{
    public new string Name { get => "PremiumAmount"; }
    public new string Label { get => "Avtalad premie"; }
    public new ParameterType Type { get => ParameterType.IntegerType; }
    public new bool Required { get => true; }
    public int Minimum { get; init; }
    public int Maximum { get; init; }
    public int Default { get; init; }
}

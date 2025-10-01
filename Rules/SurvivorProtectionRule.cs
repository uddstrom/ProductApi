using productapi.model;

namespace productapi.rules;

public class SurvivorProtectionRule : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        return new SurvivorProtection { IsEditable = true, Value = spec.SurvivorProtection ?? true };
    }
}
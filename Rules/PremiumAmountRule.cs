using productapi.model;

namespace productapi.rules;

public class PremiumAmountRule : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        return new PremiumAmount { Minimum = 100, Maximum = 100000, Default = 1000 };
    }
}
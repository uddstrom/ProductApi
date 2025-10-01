using productapi.model;

namespace productapi.rules;

public class RulesEngine
{
    private readonly string _productId;
    private readonly ICollection<IProductRule> _rules = [];

    public RulesEngine(string productId)
    {
        _productId = productId;

        // Populate the _rules collection based on productId
        _rules.Add(new PayoutStartDateRule());
        // _rules.Add(new SurvivorProtectionRule());
        // _rules.Add(new PremiumAmountRule());
    }

    public InsuranceProduct GetProdcutConfigurationSchema(InsuranceProductSpecification parameters)
    {
        var schema = new InsuranceProduct
        {
            InsuranceProductId = _productId,
            InsuranceProductName = "Pensionsförsäkring",
            InsuranceProductDescription = "Some product description",
        };

        foreach (var rule in _rules)
        {
            schema.Parameters.Add(rule.Evaluate(parameters));
        }

        return schema;
    }
}
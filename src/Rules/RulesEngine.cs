using productapi.model;

namespace productapi.rules;

public class RulesEngine
{
    private readonly string _productId;
    private readonly LumeraSettings _lumeraSettings;
    private readonly ICollection<IProductRule> _rules = [];

    public RulesEngine(string productId, DateOnly birthDate, LumeraSettings lumeraSettings)
    {
        _productId = productId;
        _lumeraSettings = lumeraSettings;

        // Populate the _rules collection based on productId
        _rules.Add(new PayoutStartDateRule(birthDate, lumeraSettings.Z, lumeraSettings.TaxCategory));
        _rules.Add(new PayoutPeriodRule(birthDate, lumeraSettings.Z, lumeraSettings.BenefitProductId, lumeraSettings.Maturity));
    }

    public InsuranceProductSchema GetProductConfigurationSchema(InsuranceProductSpecification parameters)
    {
        var schema = new InsuranceProductSchema
        {
            InsuranceProductId = _productId,
            InsuranceProductName = _lumeraSettings.InsuranceProductName,
            InsuranceProductDescription = _lumeraSettings.InsuranceProductDescription,
        };

        foreach (var rule in _rules)
        {
            schema.Parameters.Add(rule.Evaluate(parameters));
        }

        return schema;
    }
}
using productapi.model;

namespace productapi.rules;

public interface IProductRule
{
    Parameter Evaluate(InsuranceProductSpecification parameters);
}

using productapi.model;
using productapi.rules;

namespace productapi.unittests;

public class PayoutPeriodRuleTests
{
    [Theory]
    [InlineData("1959-06-01", "2024-06-01", 360, 2, 480)]
    [InlineData("1959-06-01", "2024-06-01", 96, 24, 480)]
    [InlineData("1959-06-01", "2024-07-01", 240, 2, 480)]
    [InlineData("1925-08-01", "2024-06-01", 240, 2, 254)]
    [InlineData("1925-01-01", "2024-06-01", 240, 2, 247)]
    public void GivenzBenefitProduct_KST1K_ReturnsCorrectPayoutPeriodValues(string birthdate, string z, int maturity, int expectedMin, int expectedMax)
    {
        var benefitProductId = "653984"; // KST1-K
        var _sut = new PayoutPeriodRule(DateOnly.Parse(birthdate), DateOnly.Parse(z), benefitProductId, maturity);
        var spec = new InsuranceProductSpecification { SurvivorProtection = true };

        var result = _sut.Evaluate(spec) as PayoutPeriod;

        Assert.NotNull(result);
        Assert.IsType<PayoutPeriod>(result);
        Assert.Equal(expectedMin, result.Minimum);
        Assert.Equal(expectedMax, result.Maximum);
    }
}

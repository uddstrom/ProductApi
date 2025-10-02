using productapi.model;
using productapi.rules;

namespace productapi.unittests;

public class PayoutStartDateRuleTests
{

    public PayoutStartDateRuleTests()
    {
    }

    [Theory]
    //[InlineData("1959-06-01", "2024-05-01", "2024-09-01", "2026-05-01")] // 4-24
    //[InlineData("1959-06-01", "2024-06-01", "2024-10-01", "2026-06-01")] // 4-24
    [InlineData("1959-06-01", "2024-07-01", "2025-07-01", "2025-07-01")] // 12-1 -> beräkna exakt ålder, räcker inte med hela år!!
    //[InlineData("1925-08-01", "2024-06-01", "2025-06-01", "2025-06-01")] // 12-12
    //[InlineData("1925-01-01", "2024-06-01", "2050-01-01", "2050-01-01")] // N/A
    public void GivenTaxCategoryP_WithSurvivorProtection_ReturnsCorrectPayoutStartDateValues(string birthdate, string z, string expectedMin, string expectedMax)
    {
        var _sut = new PayoutStartDateRule(DateOnly.Parse(birthdate), DateOnly.Parse(z), TaxCategory.P);
        var spec = new InsuranceProductSpecification { SurvivorProtection = true };

        var result = _sut.Evaluate(spec) as PayoutStartDate;

        Assert.NotNull(result);
        Assert.IsType<PayoutStartDate>(result);
        Assert.Equal(expectedMin, result.Minimum.ToString());
        Assert.Equal(expectedMax, result.Maximum.ToString());
    }
}

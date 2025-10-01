using productapi.model;

namespace productapi.rules;

public class PayoutStartDateRule(int _age, DateOnly _Z, TaxCategory _taxCategory) : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        var maxAge = 100;
        var ageAtZ = GetAgeAtFutureDate(_age, _Z);


        if (_taxCategory == TaxCategory.P || _taxCategory == TaxCategory.T)
        {
            if (spec.Age < 65)
            {
                // Z-4 mån - Z+24 mån
                return new PayoutStartDate { Minimum = _Z.AddMonths(-4), Maximum = _Z.AddMonths(24), Default = _Z };
            }

            if (_taxCategory == TaxCategory.P && spec.SurvivorProtection.HasValue && spec.SurvivorProtection.Value == false)
            {
                maxAge = 80;
            }
        }

        if (ageAtZ + 1 < maxAge)
        {
            // Z +- 12 mån
            return new PayoutStartDate { Minimum = _Z.AddMonths(-12), Maximum = _Z.AddMonths(12), Default = _Z };
        }

        // Rullning ej tillåten
        return new PayoutStartDate { Minimum = _Z, Maximum = _Z, Default = _Z };
    }

    private static int GetAgeAtFutureDate(int currentAge, DateOnly futureDate)
    {
        DateTime now = DateTime.Today;
        DateOnly today = new(now.Year, now.Month, now.Day);
        DateOnly birthDate = today.AddYears(-currentAge);

        int ageAtFuture = futureDate.Year - birthDate.Year;

        // If birthday hasn't occurred yet in the future year, subtract 1
        if (futureDate < birthDate.AddYears(ageAtFuture))
        {
            ageAtFuture--;
        }

        return ageAtFuture;
    }
}
using productapi.model;

namespace productapi.rules;

public class PayoutStartDateRule : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        var Z = spec.Z; // do we get this from Lumera?
        var maxAge = 100;
        var ageAtZ = GetAgeAtFutureDate(spec.Age, Z);


        if (spec.TaxCategory == TaxCategory.P || spec.TaxCategory == TaxCategory.T)
        {
            if (spec.Age < 65)
            {
                // Z-4 mån - Z+24 mån
                return new PayoutStartDate { Minimum = Z.AddMonths(-4), Maximum = Z.AddMonths(24), Default = Z };
            }

            if (spec.TaxCategory == TaxCategory.P && spec.SurvivorProtection.HasValue && spec.SurvivorProtection.Value == false)
            {
                maxAge = 80;
            }
        }

        if (ageAtZ + 1 < maxAge)
        {
            // Z +- 12 mån
            return new PayoutStartDate { Minimum = Z.AddMonths(-12), Maximum = Z.AddMonths(12), Default = Z };
        }

        // Rullning ej tillåten
        return new PayoutStartDate { Minimum = Z, Maximum = Z, Default = Z };
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
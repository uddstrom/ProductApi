using productapi.model;

namespace productapi.rules;

public class PayoutStartDateRule(DateOnly _birthdate, DateOnly _Z, TaxCategory _taxCategory) : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        var age = GetAgeFromBirthDate(_birthdate);
        var maxAge = 100;
        var ageAtZ = GetAgeAtFutureDate(age, _Z);

        if (_taxCategory == TaxCategory.P || _taxCategory == TaxCategory.T)
        {
            if (ageAtZ <= 65)
            {
                // Okej att rulla 4 - 24 månader
                return new PayoutStartDate { Minimum = _Z.AddMonths(4), Maximum = _Z.AddMonths(24), Default = _Z };
            }

            if (_taxCategory == TaxCategory.P && spec.SurvivorProtection.HasValue && spec.SurvivorProtection.Value == false)
            {
                maxAge = 80;
            }
        }

        if (ageAtZ + 1 < maxAge)
        {
            // Okej att rulla 12 månader.
            return new PayoutStartDate { Minimum = _Z.AddMonths(12), Maximum = _Z.AddMonths(12), Default = _Z };
        }

        // Rullning ej tillåten.
        return new PayoutStartDate { Minimum = _Z, Maximum = _Z, Default = _Z };
    }

    private static int GetAgeFromBirthDate(DateOnly birthDate)
    {
        DateOnly today = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

        int age = today.Year - birthDate.Year;

        // If the birthday hasn't occurred yet this year, subtract one
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
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
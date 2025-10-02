using productapi.model;

namespace productapi.rules;

public class PayoutStartDateRule(DateOnly _birthdate, DateOnly _Z, TaxCategory _taxCategory) : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        var maxAge = 100;
        var ageAtZ = CalculateAge(_birthdate, _Z);

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

    private static double CalculateAge(DateOnly birthdate, DateOnly targetDate)
    {
        var daysLived = targetDate.DayNumber - birthdate.DayNumber;
        return Double.Round(daysLived / 365.2425, 2); // average year length including leap years
    }
}
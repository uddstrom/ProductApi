using productapi.model;

namespace productapi.rules;

public class PayoutPeriodRule(DateOnly _birthdate, DateOnly _Z, string _benefitProductId, int _maturity) : IProductRule
{
    public Parameter Evaluate(InsuranceProductSpecification spec)
    {
        int MIN(int a, int b) => a < b ? a : b;
        int MAX(int a, int b) => a > b ? a : b;

        var ageAtZ = CalculateAge(_birthdate, _Z); // age at z in months
        var utbMin = 0; // minsta möjliga utbetalningstid vid temporär utbetalning
        var utbMax = 0; // maximal utbetalningstid vid temporär utbetalning
        var minMaturity = 0; // försäkringens minsta tillåtna ålder
        var senastUtb = 0;

        switch (_benefitProductId)
        {
            case "653984": // KST1-K
                utbMin = 2;
                utbMax = 480;
                minMaturity = 120;
                senastUtb = 1440;
                break;

            //case "?": // KSF1-K
            //    utbMin = 0;
            //    utbMax = 0;
            //    minMaturity = 0;
            //    senastUtb = 1440;
            //    break;

            //case "??": // ÅpTA1-T
            //case "???": // ÅpTA2-T
            //case "????": // ÅpFA1-T
            //    utbMin = 0;
            //    utbMax = 0;
            //    minMaturity = 0;
            //    senastUtb = 1440;
            //    break;

            default:
                throw new NotImplementedException();
        };

        var min = MAX(utbMin, minMaturity - _maturity);
        var max = MIN(utbMax, senastUtb - ageAtZ);

        return new PayoutPeriod { Minimum = min, Maximum = max };
    }

    private static int CalculateAge(DateOnly birthdate, DateOnly targetDate)
    {
        var daysLived = targetDate.DayNumber - birthdate.DayNumber;
        return (int)Math.Round(daysLived / 365.2425 * 12); // average year length including leap years
    }
}
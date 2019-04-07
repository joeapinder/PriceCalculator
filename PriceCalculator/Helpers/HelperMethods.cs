using System;

namespace PriceCalculator.Helpers
{
    public class HelperMethods
    {
        public static string FormatAmount(decimal amount)
        {
            if (amount >= 1)
            {
                return string.Format("-{0:c}", amount);
            }
            return $"-{Convert.ToInt32(amount * 100)}p";
        }
    }
}

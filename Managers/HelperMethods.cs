using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiyatMandli
{
    public static class HelperMethods
    {
        public static decimal CalculateLocalFundAmount(decimal rate, decimal landArea)
        {
            decimal localfundPercent = Convert.ToDecimal(AppManager.GetFlag("LocalFundPercent").ToEnglish());
            return ((rate * landArea) * localfundPercent) / 100;
        }
        public static decimal CalculateGrossAmount(decimal rate, decimal landArea)
        {
            return (rate * landArea);
        }
    }
}

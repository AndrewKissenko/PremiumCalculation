using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{
   public class ProrateMonthPremium : IProratePremium
    {
        public ProrateMonthPremium() { }
        //Retrive  prorate amount by months
        public double CalcProratePremium(double fullPremiumAmount, DateTime policyEndDate)
        {
            if (policyEndDate <= DateTime.Now) return 0;
            if (fullPremiumAmount > 0)
            {
                var monthsLeft = (policyEndDate.Month - DateTime.Now.Month) + 1;
                return
                   Math.Round(fullPremiumAmount / 12 * monthsLeft,2);
            }
            return 0;
        }
    }
}

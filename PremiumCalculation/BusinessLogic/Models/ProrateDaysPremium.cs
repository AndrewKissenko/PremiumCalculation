using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{
    public class ProrateDaysPremium : IProratePremium
    {
        public ProrateDaysPremium() { }
        //Retrive  prorate amount by days
        public double CalcProratePremium(double fullPremiumAmount, DateTime policyEndDate)
        {
            if (policyEndDate <= DateTime.Now) return 0;
            if (fullPremiumAmount > 0)
            {
                var daysLeft = (policyEndDate.Date - DateTime.Now.Date).TotalDays + 1;
                return
                    IsLeapYear(policyEndDate.Year) ? Math.Round(fullPremiumAmount / 366 * daysLeft,2) : Math.Round(fullPremiumAmount / 365 * daysLeft,2);
            }
            return 0;
        }
        //Check days num in a year
        private bool IsLeapYear(int year)
        {
            if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0)) return false;
            return true;
        }

    }
}

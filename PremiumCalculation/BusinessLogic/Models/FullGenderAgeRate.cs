using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{
    public class FullGenderAgeRate : FullAgeRate,IFullRate
    {
        public FullGenderAgeRate() { }
        public double CalcFullPremium(char gender, int age)
        {
            if (gender != 'm' && gender != 'f') return 0;
            if (age > 0 && age <= 120)
            {
                if (gender == 'f' && age > 18) return 1.5 * CalcFullPremium(age);
                return CalcFullPremium(age);
            }
            return 0;
        }
    }
}

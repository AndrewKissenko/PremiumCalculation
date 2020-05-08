using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{
    public class FullAgeRate:IFullRate
    {
        public FullAgeRate() { }
        //Retrive  Anual employee's rate by age
        public  double CalcFullPremium(int age)
        {
            var formulala = (age / 10 + 1) * age * 100;
            if (age > 0 && age <= 120) return formulala;
            return 0;
        }
    }
}

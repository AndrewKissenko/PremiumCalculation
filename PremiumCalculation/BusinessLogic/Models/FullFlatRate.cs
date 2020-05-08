using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{
    public class FullFlatRate:IFullRate
    {
        public FullFlatRate() { }
        public double CalcFullPremium()
        {
            return 1000;
        }
    }
}

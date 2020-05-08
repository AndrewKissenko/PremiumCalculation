using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models.Interface
{
   public interface IProratePremium
    {
        double CalcProratePremium(double fullPremiumAmount, DateTime policyEndDate);
    }
}

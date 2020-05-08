using PremiumCalculation.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation.Models
{

   public enum PricingModels
    {
        FullFlatRate,
        FullAgeRate,
        FullGenderAgeRate
    }
    public enum ProrateModels
    {
        ProrateDaysPremium,
        ProrateMonthPremium
    }
   public class EmployeePaymentCalculator
    {
        public EmployeePaymentCalculator() { }
        //Implementation #1
        public (int,double,double) CalculatePremium(Employee employee,IFullRate fullRateModel, IProratePremium proratePremiumModel, DateTime policyEndDate)
        {
            double fullPremium = 0;
            double proratePremium = 0;
            //policy end date validation
            if (policyEndDate <= DateTime.Now) throw new Exception($"Inner exception occured.POLICY END DATE CANNOT BE LESS THAN {DateTime.Now.Date}");

            //cast types to retrieve the model 
            if (fullRateModel is FullFlatRate flatRateModel) fullPremium = flatRateModel.CalcFullPremium();
            else if (fullRateModel is FullGenderAgeRate genderAgeRateModel) fullPremium = genderAgeRateModel.CalcFullPremium(employee.Gender, employee.Age);
            else if (fullRateModel is FullAgeRate ageRateModel) fullPremium = ageRateModel.CalcFullPremium(employee.Age);
           
            //user sex validation
            if (fullPremium == 0) throw new Exception("Inner exception occured. Employee's age or sex invalid");

            proratePremium = proratePremiumModel.CalcProratePremium(fullPremium, policyEndDate);

            return (employee.Id, fullPremium, proratePremium);
        }
        //Implementation #2
        //This implementation is more suitable for run time ...https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/ms182128(v=vs.100)?redirectedfrom=MSDN
        public (int, double, double) CalculatePremium(Employee employee, PricingModels pricingModels, ProrateModels prorateModels, DateTime policyEndDate)
        {
            //policy end date validation
            if (policyEndDate <= DateTime.Now) throw new Exception($"Inner exception occured.POLICY END DATE CANNOT BE LESS THAN {DateTime.Now.Date.Add(TimeSpan.FromDays(1))}");

            double fullPremium = CalculateFullRate(employee, pricingModels);
            double proratePremium = 0;
            ProrateDaysPremium _prorateDaysPremium = null;
            ProrateMonthPremium _prorateMonthPremium = null;

            //user sex validation
            if (fullPremium == 0) throw new Exception("Inner exception occured. Employee's age or sex invalid");

            if (prorateModels == ProrateModels.ProrateDaysPremium)
            {
                if (_prorateDaysPremium == null) _prorateDaysPremium = new ProrateDaysPremium();
                proratePremium = _prorateDaysPremium.CalcProratePremium(fullPremium, policyEndDate);
            }
            else 
            {
                if (_prorateMonthPremium == null) _prorateMonthPremium = new ProrateMonthPremium();
                proratePremium = new ProrateMonthPremium().CalcProratePremium(fullPremium, policyEndDate);
            } 

            return (employee.Id, fullPremium, proratePremium);
        }

        private double CalculateFullRate(Employee employee, PricingModels pricingModels)
        {
            FullAgeRate _fullAgeRate = null;
            FullFlatRate _fullFlatRate = null;
            FullGenderAgeRate _fullGenderAgeRate = null;
            if (pricingModels == PricingModels.FullAgeRate)
            {
                if (_fullAgeRate == null) _fullAgeRate = new FullAgeRate();
                return _fullAgeRate.CalcFullPremium(employee.Age);
            }
            else if (pricingModels == PricingModels.FullFlatRate)
            {
                if (_fullFlatRate == null) _fullFlatRate = new FullFlatRate();
                return _fullFlatRate.CalcFullPremium();
            }
            else
            {
                if (_fullGenderAgeRate == null) _fullGenderAgeRate = new FullGenderAgeRate();
                return _fullGenderAgeRate.CalcFullPremium(employee.Gender, employee.Age);
            }
        }

    }
}

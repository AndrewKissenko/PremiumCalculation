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
        public (int,double,double) CalculatePremium(Employee employee,IFullRate fullRateModel, IProratePremium proratePremiumModel, DateTime policyEndDate)
        {
            double fullPremium = 0;
            double proratePremium = 0;

            if (policyEndDate <= DateTime.Now) throw new Exception($"Inner exception occured.POLICY END DATE CANNOT BE LESS THAN {DateTime.Now.Date}");

            if (fullRateModel is FullFlatRate flatRateModel) fullPremium = flatRateModel.CalcFullPremium();
            else if (fullRateModel is FullGenderAgeRate genderAgeRateModel) fullPremium = genderAgeRateModel.CalcFullPremium(employee.Gender, employee.Age);
            else if (fullRateModel is FullAgeRate ageRateModel) fullPremium = ageRateModel.CalcFullPremium(employee.Age);
           

            if (fullPremium == 0) throw new Exception("Inner exception occured. Employee's age or sex invalid");

            proratePremium = proratePremiumModel.CalcProratePremium(fullPremium, policyEndDate);

            return (employee.Id, fullPremium, proratePremium);
        }
        //if we want to use Enum then we need no interfaces
        //public (int, double, double) CalculatePremium(Employee employee,PricingModels pricingModels, ProrateModels prorateModels, DateTime policyEndDate)
        //{
        //    double fullPremium = 0;
        //    double proratePremium = 0;

        //    if (policyEndDate <= DateTime.Now) throw new Exception($"Inner exception occured.POLICY END DATE CANNOT BE LESS THAN {DateTime.Now.Date.Add(TimeSpan.FromDays(1))}");

        //    if (pricingModels == PricingModels.FullAgeRate) fullPremium = new FullAgeRate().CalcFullPremium(employee.Age);
        //    else if (pricingModels == PricingModels.FullFlatRate) fullPremium = new FullFlatRate().CalcFullPremium();
        //    else fullPremium = new FullGenderAgeRate().CalcFullPremium(employee.Gender,employee.Age);

        //    if (fullPremium == 0) throw new Exception("Inner exception occured. Employee's age or sex invalid");

        //    if (prorateModels == ProrateModels.ProrateDaysPremium) proratePremium = new ProrateDaysPremium().CalcProratePremium(fullPremium, policyEndDate);
        //    else proratePremium = new ProrateMonthPremium().CalcProratePremium(fullPremium, policyEndDate);

        //    return (employee.Id, fullPremium, proratePremium);
        //}
        public (int, double, double) CalculatePremium(Employee employee, PricingModels pricingModels, ProrateModels prorateModels, DateTime policyEndDate)
        {
            if (policyEndDate <= DateTime.Now) throw new Exception($"Inner exception occured.POLICY END DATE CANNOT BE LESS THAN {DateTime.Now.Date.Add(TimeSpan.FromDays(1))}");

            double fullPremium = CalculateFullRate(employee, pricingModels);
            double proratePremium = 0;
            ProrateDaysPremium _prorateDaysPremium = null;
            ProrateMonthPremium _prorateMonthPremium = null;
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

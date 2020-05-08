using PremiumCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee andrew = new Employee() { Id = 1, Age = 23, Gender = 'm' };
            Employee victoria = new Employee() { Id = 2, Age = 23, Gender = 'f' };
            DateTime policyEndDate= new DateTime(2020, 6, 1);
            EmployeePaymentCalculator paymentCalculator = new EmployeePaymentCalculator();
            //try
            //{
            //    var res = paymentCalculator.CalculatePremium(
            //        andrew,
            //        new FullAgeRate(),
            //        new ProrateDaysPremium(),
            //        policyEndDate);
            //    Console.WriteLine(res.Item1);
            //    Console.WriteLine(res.Item2);
            //    Console.WriteLine(res.Item3);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            try
            {
                var res = paymentCalculator.CalculatePremium(andrew, PricingModels.FullAgeRate, ProrateModels.ProrateDaysPremium, policyEndDate);
                Console.WriteLine("Emp1 Id--> {0}",res.Item1);
                Console.WriteLine("Emp1 Annual Rate--> ${0}", res.Item2);
                Console.WriteLine("Emp1 Prorate By Days Premium -- ${0} -- till {1}", res.Item3, policyEndDate.ToShortDateString());
                Console.WriteLine();

                var res2= paymentCalculator.CalculatePremium(victoria, PricingModels.FullGenderAgeRate, ProrateModels.ProrateMonthPremium, policyEndDate);
                Console.WriteLine("Emp2 Id--> {0}", res2.Item1);
                Console.WriteLine("Emp2 Annual Rate--> ${0}", res2.Item2);
                Console.WriteLine("Emp2 Prorate By Months Premium -- ${0} -- till {1}", res2.Item3, policyEndDate.ToShortDateString());
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}

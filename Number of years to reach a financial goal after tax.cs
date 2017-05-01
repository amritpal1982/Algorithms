using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            Console.WriteLine(CalculateYears(1000.0, 0.05, 0.18, 1100.0));
        }
        
        //This function tells the number of years to reach a financial goal if the investment is giving a compounded interest annually
		//principal = Initial Investment amount
		//interest = % of annual compound interest
		//tax = % of annual tax rate (the tax is calculated only on interest earned)
		//desiredPrincipal = Final amount needed (Financial goal)
		//E.g.
		// 1100 = 1000(1 + netinterest)^n
		// netinterest = interestPercent - (interestPercent * taxPercent)
		// netinterest = .05 - (.05 * .18) = .05 - .009 = .041
		// 1100 = 1000(1.041)^n
		// 1100/1000 = (1.041)^n
		// log(1100/1000) = log((1.041)^n)
		// log(1100/1000) = nlog(1.041)
		// n = log(1100/1000)/log(1.041)
		//This will return the decimal number of years. We will have to use Ceil to find the whole number of years needed
		public static int CalculateYears(double principal, double interest,  double tax, double desiredPrincipal)
        {
          
           
            int years = Convert.ToInt32(Math.Ceiling(Math.Log(desiredPrincipal/principal) /  Math.Log((1 + (interest - (interest * tax))))));
            
            
            return years;
        }
    }
}
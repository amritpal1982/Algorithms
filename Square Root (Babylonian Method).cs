using System;
using System.Collections.Generic;


namespace Algorithms
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//Your code goes here
			double sqrt = SquareRoot(128.8976);//128.8976
			Console.WriteLine(sqrt);
		}
		
		public static double SquareRoot(double number)
		{
			//start with a random number. We can find a method to find this initial number to be close to actual square root.
			//But for the sake of simplicity, I am starting it with 1, here
			double result = 1.0;
			int iteration = 0;
			
			//In the decimal numbers, instead of checking for equality, we need to check for the gap of the result, upto a certain decimal places.
			double approximation = 0.0000000000001;
			double iterationResult;
			do
			{
				//Check if the number when divided by current result yields the same result
				//For example 15/3.8762 = 3.8762 
				//In other words 15/3.8762 - 3.8762 = 0.0 or 0.000000000001
				//If so, we have found the square root, so break; 
				if(Math.Abs((number/result) - result) <= approximation)
				{
					break;
				}
				
				//if not, then find the next approximation, by averaging the last yield with its result.
				result = ((number/result) + result)/2;
				iteration++;
			}while(true);
			
			Console.WriteLine("Iterations: " + iteration);
			return result;
		}
	}
}
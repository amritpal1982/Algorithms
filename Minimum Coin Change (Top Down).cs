using System;
using System.Collections.Generic;

namespace Algorithms
{
    
    //Quick and Dirty Class that depicts the Top Down approach
    //for finding minimum coins required for making change for a specific amount.
	
	//Find more details at https://www.linkedin.com/pulse/dynamic-programming-part-2-amritpal-singh
    public class Program
    {
        public static Dictionary<int, int> sumCoins = new Dictionary<int, int>();
        
        //This variable is not needed for the algorithm. I took it to measure the time complexity
        public static int TotalCounter = 0;
        
        public static void Main(string[] args)
        {
            //Array of coin denominations
            int[] coins = {1,5,6,9};
            
            //Total amount we are trying to make 
            int balance = 11;
            
            //Call the funtion
            minCoinsTopDown(coins, balance);
                           
            Console.WriteLine("Min Coins required:" + minChange[balance] + "\nComplexity:" + TotalCounter);
        }
        
        
        public static void minCoinsTopDown(int[] coins, int balance)
        {
            //if we have reached 0 balance, return;
            if(sum == 0) return 0;

			//If the dictionary contains the minimum coins for the current Sum, return the number of coins
			if(sumCoins.ContainsKey(sum))
			{
				return sumCoins[sum];
			}
			else
			{
				
				int numberOfCoins = 0;
				int min = 999999;

				for(int counter = 0 ; counter < coins.Length ; counter++)
				{
					if(sum >= coins[counter])
					{
						numberOfCoins = 1 + Math.Min(min, MinCoins(coins, sum - coins[counter]));

						if(numberOfCoins < min)
						{
							min = numberOfCoins ;

						}
					}

				}

				sumCoins.Add(sum, min);
				return min;
			}
		}
    
    
}
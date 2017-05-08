using System;
using System.Collections.Generic;


namespace Algorithms
{
    public class Program
    {
        static List<int> minCoins = new List<int>();
        static List<int> combinations = new List<int>();
        
        public static void Main(string[] args)
        {
            //Your code goes here
            List<int> coins = new List<int>(new int[] {7,2,3,6});
            
            
            MinimumCoinsNeeded(coins, 12);
            PrintCoins(coins);
            
        }
        
        public static void MinimumCoinsNeeded(List<int> coins, int Total)
        {
            //minCoins List contains the minimum number of coins needed to reach a total
			minCoins = new List<int>();
			//combinations List is used to find the actual coins needed to make a total. Each total will contain the index of only first coin needed. Rest of the coins can be found by reducing this amount from the total
            combinations = new List<int>();
        
			//Initialize the two lists. To find Minimum coins, the first index 0 is initialized as 0, as it requires 0 coins to reach a total of 0
			//All other values in minCoins are set to a high value so that we can then find the Minimum of this value and the coins needed.
			
            minCoins.Add(0);
            combinations.Add(-1);
            for(int counter = 1; counter <= Total; counter++)
            {
                minCoins.Add(Int16.MaxValue);
                combinations.Add(-1);
            }
            
            
            int temp = -1;
            int coinIndex = 0;
            
            foreach(int coin in coins)
            {
                for(int counter = 0 ; counter <= Total ; counter++)
                {
                    if(counter < coin)
                        continue;
                    
                    else
                    {
                        temp = Math.Min(minCoins[counter], (1 + minCoins[counter - coin]));
                        
                        if(temp != minCoins[counter])
                        {
                            minCoins[counter] = temp;
                            combinations[counter] = coinIndex;
                        }
                                          
                    }
                }
                
                coinIndex++;
            }
            
        }
        
        public static void PrintCoins(List<int> coins)
        {
            int coinIndex = 0;
            int tempSum = 0;
            
            foreach(int coin in minCoins)
            {
                if(combinations[coinIndex] >= 0)
                {
                    Console.Write("A Sum of " + coinIndex + " needs a minimum of " + coin + " coins.");
                    
                    
                    tempSum = coinIndex;
                    
                    Console.Write(" Coins Needed ----->");
                    while(tempSum > 0)
                    {
                       Console.Write("  " + coins[combinations[tempSum]]);
                       tempSum -= coins[combinations[tempSum]];
                           
                    }
                    
                    coinIndex++;
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(coinIndex + " can not be changed with the combination of current coins");
                    coinIndex++;
                }
                
            }
        }
        
        
    }
}
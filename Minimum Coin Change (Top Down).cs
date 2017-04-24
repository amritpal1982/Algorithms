using System;
using System.Collections.Generic;

namespace Algorithms
{
    
    //Quick and Dirty Class that depicts the Top Down approach
    //for finding minimum coins required for making change for a specific amount.
    public class Program
    {
        //Dictionary that will contain the minimum number of coins for change. Note that 
        //the dictionary will not only contain the change for the final balance but for all the amounts
        //from 1 to balance.
        //Although, I have taken this Dictionary as a static object of the class, ideally it should be passed 
        //in the Recursive function.
        public static Dictionary<int, int> minChange = new Dictionary<int, int>();
        
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
            if(balance == 0)
                return;
        
            int min = 999999;
            int temp = 1;
            int tempBalance = balance;
            
            //Call the recursive function to generate the recursion tree
            //In each recursive call, we are reducing the amount by the denomination of 
            //first coin in the array.
            //Please note that we are assuming that the first coin will be 1 as that will ensure 
            //that all the sub balance amounts are covered. For example, in case of 11, it will cover 10,9,8....1
            minCoinsTopDown(coins, tempBalance - coins[0]);
            
            //Once the recursion returns, go through each coin in the array of denominations to check
            //which coin will make the correct balance
            for (int counter = 0 ; counter <= coins.Length - 1 ; counter++)
            {
                
                //if the coin is larger than balance, no need to traverse
                if(coins[counter] <= balance)
                {
                    tempBalance = balance;
                    
                    //reduce the balance with current coin amount
                    tempBalance -= coins[counter];
                    
                    //If after using the current coin, the balance is 0, then the balance is equal to coin,
                    //so this coin will make the balance with only single coin.
                    if(tempBalance == 0)
                    {
                        temp = 1;
                    }
                    //Otherwise , check if we already have the balance in the Dictionary, if so, take the minimum 
                    //number f coins that we have noted for this amount of balance, and increase it by 1 becaue we are using the 
                    //current coin as well
                    else if(minChange.ContainsKey(tempBalance))
                    {
                        temp = 1 + minChange[tempBalance];
                    }
                    
                    //check the minimum out of two
                    min = min<=temp?min:temp;
                    
                    //Increase the static counter to check how many times we have reached here, to calculate the total complexity
                    TotalCounter++;
                }
                
            }
            
            //After going through all the coins, add the minimum amount to dictionary for the current balance
            minChange.Add(balance, min);
        }
            
                        
    }
    
    
}
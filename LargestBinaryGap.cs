using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> bitsArray = new List<int> {0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1,0,1,0,0,1,1,1,1,0,0,1,0,0,0,1,1,0,1,0,0,0,1,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0};
            int gap = FindLargestBinaryGap(bitsArray);
            Console.WriteLine(gap);
            gap = FindLargestBinaryGap(3390383893760);
            Console.WriteLine(gap);
            
        }
        
		//Find Binary Gap in a list of binary values
        private static int FindLargestBinaryGap(List<int> bits)
        {
            int count = 0;
            int largestGap = 0;
            int foundOne = 0;
            
            foreach(int bit in bits)
            {
                if(bit == 0 && foundOne == 1)
                {
                    count++;
                }
                else if(bit == 1)
                {
                    foundOne = 1;
                    largestGap = largestGap > count ? largestGap : count;
                    count = 0;
                }
            }
            
            return largestGap;
        }
        
		//Overloaded Method to find the Binary Gap of a decimal number
		//First Converts the decimal to Binary List
        private static int FindLargestBinaryGap(long number)
        {
            List<int> bitsArray = ConvertToBinary(number);
            return FindLargestBinaryGap(bitsArray);
        }
        
        
		//Converts the decimal number to a List of Binary Values
        private static List<int> ConvertToBinary(long number)
        {
            List<int> bits = new List<int>();
            int index = 0;
            while(number > 0)
            {
                bits.Add(Convert.ToInt32(number % 2));
                index++;
                number = number / 2;
            }
            
            bits.Reverse();
            return bits;
        }
        
    }
}
using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string mergedString = MergeSortUnique("xyaabbbccccdefww", "xxxxyyyyabklmopq");
            Console.WriteLine(mergedString);
        }
        
        public static string MergeSortUnique(string first, string second)
        {
            string firstandsecond = first + second;
            int[] alphabets = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            
            foreach(char c in firstandsecond)
            {
                alphabets[((int)c) - 97] = 1; 
            }
            
                        
            string final = "";
            for(int counter = 0; counter < 26 ; counter++)
            {
                if(alphabets[counter] == 1)
                {
                    final += (char)(counter + 97);
                }
            }
            
            return final;
        }
    }
}
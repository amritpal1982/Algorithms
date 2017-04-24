using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Algorithms
{
    public class Program
    {
        static List<float> oldMeanPoints = new List<float>();
        static List<float> newMeanPoints = new List<float>();
        static Dictionary<float, List<int>> groups = new Dictionary<float, List<int>>();
        
        public static void Main(string[] args)
        {
            
            //int[] points = {2,5,6,8,12,15,18,28,30};
            //Test Points
            int[] points = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,22,25,27,30,31,32,34,37,38,39,40,42,43,46,48,49,51,53,54,56,58,59,60,61};
            //Call the main function of the K Means algorithm. For now we would like to divide the points to three groups.
            CalculateKMeans(points, 3);
            
            
        }
        
        //Main function of K Means Algorithm. Just calling the relevant steps
        //Step 1 : Initialize random mean points to create the first group
        //Loop
        //Step 2: Divide the Data points according to the mean points generated 
        //Step 3: Again calculate the mean points taking the average of new groups
        //Step 4: Calculate new mean points according to new groups, check if the means have changed since last iteration, if so , loops through Step 2 to Step 4. If not, exit 
        public static void CalculateKMeans(int[] points, int kGroups)
        {
            InitializeMeanPoints(points, kGroups, oldMeanPoints);
            int iteration = 1;
            do
            {
                GroupPoints(points);
                ReCalculateMeanPoints(points);
                Print(iteration);
                iteration++;
            }while(AreMeanPointsShifted());
             
        }
        
        //Initialize the first mean points. The first mean points can be random, however, here I have used first and last point of array and then divided the rest of the points
        //with equal gaps in indexes. For example, if kGropus = 3, first mean point is points[0], third mean point is points[length-1], second mean point is the middle point 
        //that has equal gap from start and end. 
        public static void InitializeMeanPoints(int[] points, int kGroups, List<float> meanPoints)
        {
            int length = points.Length;
            int gap = length/(kGroups - 1);
            
            meanPoints.Add(points[0]);
            for(int counter = 1 ; counter <= kGroups - 2 ; counter++)
            {
                meanPoints.Add(points[counter * gap]);
            }
            
            meanPoints.Add(points[length - 1]);
              
        }
        
        //Group all the points using the mean points generated so far. 
        // Takes the help of another method to find the distance of each point from the means and then add the points to the Dictionary with "mean value" as the Key
        //Ex. dictionary[3.5, {2,3,4,5}]
        public static void GroupPoints(int[] points)
        {
            float nearestMean;
            List<int> list;
            foreach(int num in points)
            {
                nearestMean = CalculateNearestDistance(num);
                
                if (!groups.TryGetValue(nearestMean, out list))
                {
                    list = new List<int>();
                    groups.Add(nearestMean, list);
                }
                
                list.Add(num);
            }
          
        }
        
        //Calculate the nearest point of "num" in mean points List. So for example, num is 2 and meanPoints  list is 3.25, 8.25 and 28, this will return 3.25
        public static float CalculateNearestDistance(int num)
        {
            float smallest = 99999.0F;
            float diff;
            float selected = 0.0F;
            foreach(float mean in oldMeanPoints)
            {
                diff = Math.Abs(mean - num);
                if(diff < smallest)
                {
                    smallest = diff;
                    selected = mean;
                    
                }
            }
            
            return selected;
        }
        
        
        //Check if the oldMeanpoints in previous iteration are different from the newMeanpoints generated in this iteration
        //returns true if the mean points are different.
        public static bool AreMeanPointsShifted()
        {
                      
            var firstNotSecond = oldMeanPoints.Except(newMeanPoints).ToList();
            var secondNotFirst = newMeanPoints.Except(oldMeanPoints).ToList();

            
            bool shifted = firstNotSecond.Any() && secondNotFirst.Any();
            
            if(shifted)
            {
                oldMeanPoints = newMeanPoints;
                newMeanPoints = new List<float>();
                groups = new Dictionary<float, List<int>>();
                    
            }
            
                  
            return shifted;
        }
        
        //Calculates the Mean Points according to new groups. Takes the help of method CalculateAverageOfgroup to return the new mean of each group
        public static void ReCalculateMeanPoints(int[] points)
        {
            float meanPoint = 0.0F;
            List<int> list;
            float average = 0.0F;
            foreach(KeyValuePair<float,List<int>> kvp in groups)
            {
                meanPoint = kvp.Key;
                list = (List<int>)kvp.Value;
                average = CalculateAverageOfgroup(list);
                newMeanPoints.Add(average);
            }
        }
        
        //Just calculates average of a group/list. Adds the items and divide by the total number of items
        public static float CalculateAverageOfgroup(List<int> list)
        {
            float sum = 0.0F;
            int count = 0;
            foreach(int item in list)
            {
                sum += item;
                count++;
            }
            
            return (sum/count);
        }
        
        //Prints the results of each iteration
        public static void Print (int iteration)
        {
            Console.WriteLine("\n\n........................Iteration" + iteration + "................................");            
            List<int> list;
            float meanPoint;
            foreach(KeyValuePair<float,List<int>> kvp in groups)
            {
                meanPoint = kvp.Key;
                Console.WriteLine("\n\nMean Point:"+ meanPoint + ".......");
                Console.Write("Group:");
                list = (List<int>)kvp.Value;
                foreach(float item in list)
                {
                    Console.Write(item + "   ");
                }
            }
        }
    }
}
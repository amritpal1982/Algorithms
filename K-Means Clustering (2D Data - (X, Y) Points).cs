using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Algorithms
{
    public class Program
    {
        static List<PointF> oldMeanPoints = new List<PointF>();
        static List<PointF> newMeanPoints = new List<PointF>();
        static Dictionary<PointF, List<PointF>> groups = new Dictionary<PointF, List<PointF>>();
        
        public static void Main(string[] args)
        {
            
            List<PointF> points = new List<PointF>();
            
            points.Add(new PointF(65.0F, 220.0F ));
            points.Add(new PointF(73.0F, 160.0F ));
            points.Add(new PointF(59.0F, 110.0F ));
            points.Add(new PointF(61.0F, 120.0F ));
            points.Add(new PointF(75.0F, 150.0F ));
            points.Add(new PointF(67.0F, 240.0F ));
            points.Add(new PointF(68.0F, 230.0F ));
            points.Add(new PointF(70.0F, 220.0F ));
            points.Add(new PointF(62.0F, 130.0F ));
            points.Add(new PointF(66.0F, 210.0F ));
            points.Add(new PointF(77.0F, 190.0F ));
            points.Add(new PointF(75.0F, 180.0F ));
            points.Add(new PointF(74.0F, 170.0F ));
            points.Add(new PointF(70.0F, 210.0F ));
            points.Add(new PointF(61.0F, 110.0F ));
            points.Add(new PointF(58.0F, 100.0F ));
            points.Add(new PointF(66.0F, 230.0F ));
            points.Add(new PointF(59.0F, 120.0F ));
            points.Add(new PointF(68.0F, 210.0F ));
            points.Add(new PointF(61.0F, 130.0F ));


            //Call the main function of the K Means algorithm. For now we would like to divide the points to three groups.
            CalculateKMeans(points, 3);
            
            
        }
        
        //Main function of K Means Algorithm. Just calling the relevant steps
        //Step 1 : Initialize random mean points to create the first group
        //Loop
        //Step 2: Divide the Data points according to the mean points generated 
        //Step 3: Again calculate the mean points taking the average of new groups
        //Step 4: Calculate new mean points according to new groups, check if the means have changed since last iteration, if so , loops through Step 2 to Step 4. If not, exit 
        public static void CalculateKMeans(List<PointF> points, int kGroups)
        {
            InitializeMeanPoints(points, kGroups, oldMeanPoints);
            int iteration = 1;
            do
            {
                GroupPoints(points);
                ReCalculateMeanPoints(points);
                Print(iteration);
                iteration++;
                if(iteration >25)
                    break;
            }while(AreMeanPointsShifted());
             
        }
        
        //Initialize the first mean points. The first mean points can be random, however, here I have used first and last point of array and then divided the rest of the points
        //with equal gaps in indexes. For example, if kGropus = 3, first mean point is points[0], third mean point is points[length-1], second mean point is the middle point 
        //that has equal gap from start and end. 
        public static void InitializeMeanPoints(List<PointF> points, int kGroups, List<PointF> meanPoints)
        {
            int length = points.Count();
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
        public static void GroupPoints(List<PointF> points)
        {
            PointF nearestMean;
            List<PointF> list;
            foreach(PointF point in points)
            {
                nearestMean = CalculateNearestDistance(point);
                
                if (!groups.TryGetValue(nearestMean, out list))
                {
                    list = new List<PointF>();
                    groups.Add(nearestMean, list);
                }
                
                list.Add(point);
            }
          
        }
        
        //Calculate the nearest point of "num" in mean points List. So for example, num is 2 and meanPoints  list is 3.25, 8.25 and 28, this will return 3.25
        public static PointF CalculateNearestDistance(PointF point)
        {
            double smallest = 99999.0;
            double diff;
            PointF selected = new PointF(0,0);
            
            double x,y;
            double pointX = Convert.ToDouble(point.X);
            double pointY = Convert.ToDouble(point.Y);
            
            foreach(PointF mean in oldMeanPoints)
            {
                x = Convert.ToDouble(mean.X);
                y = Convert.ToDouble(mean.Y);
                diff = Math.Sqrt(Math.Abs(Math.Pow(x, 2) - Math.Pow(pointX, 2)) + Math.Abs(Math.Pow(y, 2) - Math.Pow(pointY, 2)));
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
                newMeanPoints = new List<PointF>();
                groups = new Dictionary<PointF, List<PointF>>();
                    
            }
            
                  
            return shifted;
        }
        
        //Calculates the Mean Points according to new groups. Takes the help of method CalculateAverageOfgroup to return the new mean of each group
        public static void ReCalculateMeanPoints(List<PointF> points)
        {
            PointF meanPoint = new PointF(0,0);
            List<PointF> list;
            PointF average = new PointF(0,0);
            foreach(KeyValuePair<PointF,List<PointF>> kvp in groups)
            {
                meanPoint = kvp.Key;
                list = (List<PointF>)kvp.Value;
                average = CalculateAverageOfgroup(list);
                newMeanPoints.Add(average);
            }
        }
        
        //Just calculates average of a group/list. Adds the items and divide by the total number of items
        public static PointF CalculateAverageOfgroup(List<PointF> list)
        {
            float sumX = 0.0F;
            float sumY = 0.0F;
            int count = 0;
            foreach(PointF item in list)
            {
                sumX += item.X;
                sumY += item.Y;
                count++;
            }
            
            return (new PointF(sumX/count, sumY/count));
        }
        
        //Prints the results of each iteration
        public static void Print (int iteration)
        {
            Console.WriteLine("\n\n........................Iteration" + iteration + "................................");            
            List<PointF> list;
            PointF meanPoint;
            foreach(KeyValuePair<PointF,List<PointF>> kvp in groups)
            {
                meanPoint = kvp.Key;
                Console.WriteLine("\n\nMean Point: ( "+ meanPoint.X + ", " + meanPoint.Y + ").......");
                Console.Write("Group:");
                list = (List<PointF>)kvp.Value;
                foreach(PointF item in list)
                {
                    Console.Write("(" + item.X + ", " + item.Y + ")   ");
                }
            }
        }
    }
}
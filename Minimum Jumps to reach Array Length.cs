using System;

namespace Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Test Values
            //int[] a = {4,1,11,1,1,1,6,5,7,2,9};
            //int[] a = {1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9};
            //int[] a = {11, 3, 6, 3, 2, 3, 6, 8, 9, 5};
            //int[] a = {1, 3, 6, 3, 2, 3, 6, 8, 9, 5};
            
            //int[] a = {1,1,1,1,1,1,1,1,1,1,1,1};
            //int[] a = {1, 3, 6, 1, 0, 9};
            int[] a = {2,0,3,5};
            int jumps = MinJump(a);
            
			Console.WriteLine(jumps);
            
        }
        
        public static int MinJump(int[] a)
        {
            //if the first value in array is equal to or greater than array Length, we know we can cross the array in one jump
			if(a[0] >= a.Length)
            {
                return 1;
            }
            
			//We can work on the logic where we see X steps ahead and see of any of the next X values reach the end of the array
			//We start at first position, and the value at index 0 is, say, 4. Now we traverse the array and see for the next 4 values 
			// and see if ANY of the value can get us to the end of the array by checking the sum of its index to the value.
			//E.g. if we have the array {4,1,11,2,3,1,5,4,7,8,9}, the first value is 4, which means we can jump to either a[1], a[2], a[3] or a[4]
			//Now if we see a[2], we can check 11 (Value) add it to its index (2) = 13 and see if have a number greater or equal to array length
			//This means by using 11 at index 2, we can jump to positions 3(minimum) - 13(maximum) so if our array length is within this range,
			// we know we can reach the end of the array using this value.
            
			//initialize jump to 1
			int count = 1;
			
			//store the value of first index. Since we are targeting for O(n) (single linear traversal of array), we need a variable to check, when
			//have we traversed next X values. For example in this case next 4 values. So, to check it, we will reduce the step in the loop. 	
            int step = a[0];
            
            for(int i = 1; i< a.Length ; i++)
            {
                //Check if by using current value we can reach the end of the array, if so, we have our solution. If we are still inside our jump step
				//we need to increment the count of jump by 1. 
				//E.g. {4,1,11,2,3,1,5,4,7,8,9}, we have initialized count to 1, now next time we will increment is when we have traversed all 4 values
				//at a[1], a[2], a[3], a[4], however, if we have found our soluiton before our steps end, we need to count for that jump from 
				//that value to the end 
                if(a.Length <= (i+a[i]+1))
                {
                    if(step > 0)
                        count++;
                    
                    return count;
                }
                
				//reduce the steps as discussed above 
                if(--step == 0)
                {
                    //if we have completed the steps in a jump (i.e. traveresed all the values in the jump value (4 for example), then, increment the jump
					count++;
                    
					//reset the step to the current value so that we can calculate the next jump. Lets say if we have traversed a[1], a[2], a[3], a[4] 
					//and we reach at a[5], which is 3, initialize the step to 3 so that we can now find out when we have traversed the next 3 values.
					step = a[i];
					
					//check if we have reached the end of a jump and we find a zero, that means we can not move further; in this case return -1
                    if(a[i] == 0)
                    {
                       return -1; 
                    }
                }
                
            }
			
			
            return -1;
        }
    }
}
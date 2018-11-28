using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Merge
    {
        // Merges two subarrays of arr[]. 
        // First subarray is arr[l..m] 
        // Second subarray is arr[m+1..r] 
        public void merge(int[] array, int firstArrIndex, int secondArrIndex, int endArrIndex)
        {        
            int leftArraySize = secondArrIndex - firstArrIndex + 1;
            int rightArraySize = endArrIndex - secondArrIndex;

            // create temp arrays
            int[] leftArray = new int[leftArraySize];
            int[] rightArray = new int[rightArraySize];

            // Copy data to temp arrays
            for (int copy = 0; copy < leftArraySize; copy++)
                leftArray[copy] = array[firstArrIndex + copy];
            for (int copy = 0; copy < rightArraySize; copy++)
                rightArray[copy] = array[secondArrIndex + 1 + copy];

            /* Merge the temp arrays back into arr[l..r]*/
            int i = 0; // Initial index of first subarray 
            int j = 0; // Initial index of second subarray 
            int k = firstArrIndex; // Initial index of merged subarray 
            while (i < leftArraySize && j < rightArraySize)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            /* Copy the remaining elements of L[], if there 
               are any */
            while (i < leftArraySize)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            /* Copy the remaining elements of R[], if there 
               are any */
            while (j < rightArraySize)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }

        /* l is for left index and r is right index of the 
           sub-array of arr to be sorted */
        public void mergesort(int[] array)
        {
            if (array.Length == 1)
                return;

            int[] leftArray;
            for (int i = 0; i < array.Length / 2; i++)
            {
                leftArray[i] = array[i]; 
            }
            var l2 as array = a[n / 2 + 1]...a[n];

            l1 = mergesort(l1);
            l2 = mergesort(l2);

            return merge(l1, l2)
        }
    }
}

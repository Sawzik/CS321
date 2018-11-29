using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MergeSort
{
    class Merger
    {
        protected bool isSorted;
        protected List<int> mergeList;

        public bool IsSorted { get { return isSorted; } }

        public Merger(List<int> list)
        {
            isSorted = false;
            mergeList = list;
        }

        public virtual List<int> Sort()
        {
            return Sort(ref mergeList);
        }

        protected virtual List<int> Sort(ref List<int> notSorted)
        {
            if (notSorted.Count <= 1) //cannot sort something with less than 2 elements.
                return notSorted;

            int split = notSorted.Count / 2; //integer math to find the middle point.

            List<int> left = new List<int>(); // creating left and right SubLists
            List<int> right = new List<int>();

            for (int i = 0; i < split; i++)  //Dividing the unsorted lists         
                left.Add(notSorted[i]);

            for (int i = split; i < notSorted.Count; i++)  //Dividing the unsorted lists         
                right.Add(notSorted[i]);

            left = Sort(ref left);      //recursively call merge on left and right arrays.
            right = Sort(ref right);

            return Merge(ref left, ref right);
        }

        protected virtual List<int> Merge(ref List<int> left, ref List<int> right)
        {
            List<int> merged = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0) //when both lists still have data in them.
                {
                    if (left.First() <= right.First())
                    {
                        merged.Add(left.First()); // adds the first element on the left array to the merged list.
                        left.Remove(left.First()); // removes the first element from the list.
                    }
                    else // right list haas larger value
                    {
                        merged.Add(right.First()); 
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0) // when only the left array still has data in it.
                {
                    merged.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0) // when only the right array has data in it.
                {
                    merged.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return merged;
        }
    }

    class ThreadedMerger : Merger
    {
        public ThreadedMerger(List<int> list) : base(list) { } // uses the constructor for base class.

        protected override List<int> Sort(ref List<int> notSorted)
        {
            if (notSorted.Count <= 1) //cannot sort something with less than 2 elements.
                return notSorted;

            int split = notSorted.Count / 2; //integer math to find the middle point.

            List<int> left = new List<int>(); // creating left and right SubLists
            List<int> right = new List<int>();

            for (int i = 0; i < split; i++)  //Dividing the unsorted lists         
                left.Add(notSorted[i]);

            for (int i = split; i < notSorted.Count; i++)  //Dividing the unsorted lists         
                right.Add(notSorted[i]);

            //left = Sort(left);      //recursively call merge on left and right arrays.
            //right = Sort(right);

            return Merge(ref left, ref right);
        }
    }

    //testing to see if using arrays is more efficient since arrays arent managed objects.
    // Structs arent managed objects. So this _SHOULD_ be faster at least a little bit.
    class ArrayMerger
    {
        protected bool isSorted;
        protected int[] mergeList;

        public bool IsSorted { get { return isSorted; } }

        public ArrayMerger(int[] list)
        {
            isSorted = false;
            mergeList = list.ToArray(); //does a deep copy of the array
        }

        public virtual int[] Sort()
        {
            return Sort(0, mergeList.Length);
        }

        protected virtual int[] Sort(int left, int right)
        {
            if (left < right)
            {
                // Calculating value to split into separate parts with integer math
                int split = (left + right) / 2;

                // Recursive call on left and right sides
                Sort(left, split);
                Sort(split, right - 1);

                // Merge the two sides 
                Merge(left, split, right);
            }
            return mergeList;
        }

        protected virtual void Merge(int left, int split, int right)
        {
            int leftSize = split - left + 1;
            int rightSize = right - split;

            // creating temporary copies of mergeList to prevent overwrite of data
            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];
            Array.Copy(mergeList, left, leftArray, 0, leftSize); // deep copies mergeList into two smaller arrays.
            Array.Copy(mergeList, split, rightArray, 0, rightSize);

            int leftIndex = 0, rightIndex = 0; // Initial index of sub-arrays
            int mergeListIndex = left; // Initial index of the merge
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftArray[leftIndex] <= mergeList[rightIndex]) // if element in the left array is less than or equal to the one on the right
                {
                    mergeList[mergeListIndex] = leftArray[leftIndex]; //
                    leftIndex++;
                }
                else
                {
                    mergeList[mergeListIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergeListIndex++;
            }

            while (leftIndex < leftSize) // copies the remaining elements in the left array to source array.
            {
                mergeList[mergeListIndex] = leftArray[leftIndex];
                leftIndex++;
                mergeListIndex++;
            }

            while (rightIndex < rightSize) // copies the remaining elements in the right array to source array.
            {
                mergeList[mergeListIndex] = rightArray[rightIndex];
                rightIndex++;
                mergeListIndex++;
            }            
        }
    }
}

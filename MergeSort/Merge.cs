using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// A whole bunch of different mergesorts with different algorithms and thread handling
namespace MergeSort
{
    class ListMerger
    {
        protected bool isSorted;
        protected List<int> mergeList;

        public bool IsSorted { get { return isSorted; } }

        public ListMerger(List<int> list)
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

    //testing to see if using arrays is more efficient since arrays arent managed objects.
    // Structs arent managed objects. So this _SHOULD_ be faster at least a little bit.
    class Merger
    {
        protected bool isSorted;
        protected int[] mergeList;

        public bool IsSorted { get { return isSorted; } }

        public Merger(int[] list)
        {
            isSorted = false;
            mergeList = list.ToArray(); //does a deep copy of the array
        }

        public virtual int[] Sort()
        {
            Sort(0, mergeList.Length - 1);
            return mergeList;
        }

        protected virtual void Sort(int left, int right)
        {
            if (left < right)
            {
                // Calculating value to split into separate parts with integer math
                // can deal with larger numbers than Int32.MaxLength
                int split = left + (right - left) / 2; ;

                // Recursive call on left and right sides
                Sort(left, split);
                Sort(split + 1, right);

                // Merge the two sides 
                Merge(left, split, right);
            }
        }

        protected virtual void Merge(int left, int split, int right)
        {
            int leftSize = split - left + 1;
            int rightSize = right - split;

            // creating temporary copies of mergeList to prevent overwrite of data
            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];
            Array.Copy(mergeList, left, leftArray, 0, leftSize); // deep copies mergeList into two smaller arrays.
            Array.Copy(mergeList, split + 1, rightArray, 0, rightSize);

            int leftIndex = 0, rightIndex = 0; // Initial index of sub-arrays
            int mergeListIndex = left; // Initial index of the merge
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftArray[leftIndex] <= rightArray[rightIndex]) // if element in the left array is less than or equal to the one on the right
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

    class ThreadedMerger : Merger
    {
        int maxThreads;
        //public ThreadedMerger(int[] list) : base(list) { } // uses the constructor for base class.

        public ThreadedMerger(int[] list, int threads) : base(list)
        {
            //mergeList = list.ToArray();
            maxThreads = threads;
        }

        protected override void Sort(int left, int right)
        {
            if (left < right)
            {            
                // Calculating value to split into separate parts with integer math
                // can deal with larger numbers than Int32.MaxLength
                int split = left + (right - left) / 2;

                // Recursive call on left and right sides                

                Thread leftThread = new Thread(() => Sort(left, split));
                leftThread.Start();

                Thread rightThread = new Thread(() => Sort(split + 1, right));
                rightThread.Start();

                leftThread.Join(); //waits for both threads to be done before merging
                rightThread.Join();

                // Merge the two sides 
                Merge(left, split, right);

                Thread.Sleep(0);
            }
        }
    }

    class StaticThreadedMerger : Merger
    {
        int maxThreads;

        public StaticThreadedMerger(int[] list, int threads) : base(list)
        {
            maxThreads = threads;
        }

        public override int[] Sort()
        {
            Thread[] threads = new Thread[maxThreads];
            int split = mergeList.Length / maxThreads; //determining indexes to split up into threads
            int currIndex = 0;


            for (int i = 0; i < maxThreads - 1; i++)
            {
                threads[i] = new Thread(() => Sort(currIndex, currIndex + split - 1)); // making a thread on about 1/maxThreads of the array
                threads[i].Start();
                Thread.Sleep(100); // 10ms should be more than enough to wait for all threads to be completed.
                currIndex += split;
            }

            threads[maxThreads - 1] = new Thread(() => Sort(currIndex, mergeList.Length - 1)); // making a thread on the last section until the end of the array
            threads[maxThreads - 1].Start();

            foreach (Thread thread in threads)
            {                
                thread.Join(); //tell all the threads to wait until they are all finished to continue.
            }


            // middle > end
            if (mergeList.Length / (maxThreads / 2) > maxThreads / 2)
            {
                int chunks = maxThreads / 2; //breaks up the rest of the work into half as many units for merging.            

                while (chunks > 1)
                {
                    //threads = new Thread[chunks]; // deletes all the old threads and makes a new array with half as many.
                    split = mergeList.Length / chunks; // determining where to split up the work.
                    currIndex = 0;

                    for (int i = 0; i < chunks - 1; i++)
                    {
                        int end = currIndex + split;
                        int middle = currIndex + ((end - currIndex) / 2);
                        threads[i] = new Thread(() => Merge(currIndex, middle -1, end - 1)); // making a thread on about 1/chunks of the array
                        threads[i].Start();
                        Thread.Sleep(100); // 10ms should be more than enough to wait for all threads to be completed.
                        currIndex += split;
                    }
                    
                    int e = mergeList.Length;
                    int m = currIndex + ((e - currIndex)  / 2);
                    threads[chunks - 1] = new Thread(() => Merge(currIndex + 1, m - 1, e - 1)); // making a thread on the last section until the end of the array
                    threads[chunks - 1].Start();

                    foreach (Thread thread in threads)
                    {
                        thread.Join(); //tell all the threads to wait until they are all finished to continue.
                    }

                    chunks /= 2;
                }

                Merge(0, (mergeList.Length / 2) - 1, mergeList.Length - 1);
            }

            return mergeList;
        }

        protected override void Merge(int left, int split, int right)
        {
            int leftSize = split - left + 1;
            int rightSize = right - split;

            // creating temporary copies of mergeList to prevent overwrite of data
            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];
            Array.Copy(mergeList, left, leftArray, 0, leftSize); // deep copies mergeList into two smaller arrays.
            Array.Copy(mergeList, split + 1, rightArray, 0, rightSize);

            int leftIndex = 0, rightIndex = 0; // Initial index of sub-arrays
            int mergeListIndex = left; // Initial index of the merge
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftArray[leftIndex] <= rightArray[rightIndex]) // if element in the left array is less than or equal to the one on the right
                {
                    lock (mergeList)
                        mergeList[mergeListIndex] = leftArray[leftIndex]; 
                    leftIndex++;
                }
                else
                {
                    lock (mergeList)
                        mergeList[mergeListIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergeListIndex++;
            }

            while (leftIndex < leftSize) // copies the remaining elements in the left array to source array.
            {
                lock (mergeList)
                    mergeList[mergeListIndex] = leftArray[leftIndex];
                leftIndex++;
                mergeListIndex++;
            }

            while (rightIndex < rightSize) // copies the remaining elements in the right array to source array.
            {
                lock (mergeList)
                    mergeList[mergeListIndex] = rightArray[rightIndex];
                rightIndex++;
                mergeListIndex++;
            }
        }
    }
}

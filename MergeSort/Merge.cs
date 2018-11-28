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
            return Sort(ref mergeList);
        }

        protected virtual int[] Sort(ref int[] notSorted)
        {
            if (notSorted.Length <= 1) //cannot sort something with less than 2 elements.
                return notSorted;

            int split = notSorted.Length / 2; //integer math to find the middle point.

            int[] left = new int[split]; // creating left and right SubLists
            int[] right = new int[split - notSorted.Length];

            for (int i = 0; i < split; i++)  //Dividing the unsorted lists         
                left.Add(notSorted[i]);

            for (int i = split; i < notSorted.Length; i++)  //Dividing the unsorted lists         
                right.Add(notSorted[i]);

            left = Sort(ref left);      //recursively call merge on left and right arrays.
            right = Sort(ref right);

            return Merge(ref left, ref right);
        }

        protected virtual int[] Merge(ref int[] left, ref int[] right)
        {
            int[] merged = new int[]();

            while (left.Length > 0 || right.Length > 0)
            {
                if (left.Length > 0 && right.Length > 0) //when both lists still have data in them.
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
                else if (left.Length > 0) // when only the left array still has data in it.
                {
                    merged.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Length > 0) // when only the right array has data in it.
                {
                    merged.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return merged;
        }
    }
}

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
            return Sort(mergeList);
        }

        protected virtual List<int> Sort(List<int> notSorted)
        {
            if (notSorted.Count <= 1) //cannot sort something with less than 2 elements.
                return notSorted;

            int split = notSorted.Count / 2; //integer math to find the middle point.

            List<int> left = new List<int>(); // creating left and right sub-arrays with the correct size.
            List<int> right = new List<int>();

            for (int i = 0; i < split; i++)  //Dividing the unsorted lists         
                left.Add(notSorted[i]);

            for (int i = split; i < notSorted.Count; i++)  //Dividing the unsorted lists         
                right.Add(notSorted[i]);

            left = Sort(left);      //recursively call merge on left and right arrays.
            right = Sort(right);

            return Merge(left, right);
        }

        protected virtual List<int> Merge(List<int> left, List<int> right)
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
    }
}

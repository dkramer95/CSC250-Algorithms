using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class BinaryHeap<T> where T : IComparable 
    {
        private const int DEFAULT_SIZE = 8;

        // Backing array
        public T[] Values { get; private set; }
        public int Count { get; private set; }


        public BinaryHeap()
        {
            Init(DEFAULT_SIZE);
        }

        /// <summary>
        /// Constructs a new BinaryHeap with the specified initial size
        /// </summary>
        /// <param name="initialSize">Initial capacity for the backing array</param>
        public BinaryHeap(int initialSize)
        {
            Init(initialSize);
        }

        /// <summary>
        /// Initializes this heap with the specified initial capacity
        /// for the backing array
        /// </summary>
        /// <param name="initialSize">Starting size of the backing array</param>
        private void Init(int initialSize)
        {
            Values = new T[initialSize];
            Count = 0;
        }

        /// <summary>
        /// Inserts the specified value to this BinaryHeap
        /// </summary>
        /// <param name="valueToAdd">value we're adding</param>
        public void Insert(T valueToAdd)
        {
            ResizeArrayIfFull();

            int index = GetInsertionIndex();
            Insert(valueToAdd, index);
            ++Count;
        }

        /// <summary>
        /// Helper method to insert the value into the BinaryHeap ensuring
        /// that the heap property is maintained, which is that all children
        /// nodes should be <= to their root nodes. This method will recurse
        /// until this condition is met.
        /// </summary>
        /// <param name="valueToAdd">value we're adding</param>
        /// <param name="index">index to add at</param>
        private void Insert(T valueToAdd, int index)
        {
            InsertValueAtIndexIfNull(valueToAdd, index);

            int rootIndex = GetRootIndex(index);
            T rootValue = Values[rootIndex];

            if (valueToAdd.CompareTo(rootValue) <= 0) { return; }

            Swap(rootIndex, index);
            Insert(valueToAdd, rootIndex);
        }

        /// <summary>
        /// Checks to see if this BinaryHeap is empty
        /// </summary>
        /// <returns>true if empty</returns>
        public bool IsEmpty()
        {
            bool isEmpty = (Count == 0);
            return isEmpty;
        }

        /// <summary>
        /// Removes the maximum value from this BinaryHeap
        /// </summary>
        /// <returns>Value of what we removed</returns>
        public T RemoveMax()
        {
            if (IsEmpty()) { return default(T); }

            T valueToRemove = Values[0];
            int lastIndex = (GetInsertionIndex() - 1);

            if (lastIndex > 0)
            {
                T lastValue = Values[lastIndex];
                Values[0] = lastValue;
                Values[lastIndex] = default(T);
                Heapify(lastValue, 0);
            }
            --Count;
            return valueToRemove;
        }

        /// <summary>
        /// Constructs a new Binary Heap from a list of values
        /// </summary>
        /// <param name="values">Values to add to this BinaryHeap</param>
        /// <returns>new BinaryHeap</returns>
        public static BinaryHeap<T> BuildHeap(List<T> values)
        {
            BinaryHeap<T> heap = new BinaryHeap<T>();
            values.ForEach(v => heap.Insert(v));
            return heap;
        }

        /// <summary>
        /// Swaps 2 values from the specified indexes
        /// </summary>
        /// <param name="indexA">First index</param>
        /// <param name="indexB">Second index</param>
        private void Swap(int indexA, int indexB)
        {
            T temp = Values[indexA];
            Values[indexA] = Values[indexB];
            Values[indexB] = temp;
        }

        /// <summary>
        /// Finds the root index from the specified starting index
        /// </summary>
        /// <param name="startIndex">Index that we want the root index of</param>
        /// <returns>int value of the index</returns>
        private int GetRootIndex(int startIndex)
        {
            if (startIndex <= 0) { return 0; }

            int rootIndex = ((startIndex - 1) / 2);
            return rootIndex;
        }

        /// <summary>
        /// Finds the next available index for inserting new values in
        /// this Binary Heap
        /// </summary>
        /// <returns>int value of the next available index</returns>
        private int GetInsertionIndex()
        {
            int[] indexes = GetInsertionIndexCandidates();

            foreach(int index in indexes)
            {
                if (IsNullOrDefault(Values[index])) { return index; }
            }
            // shouldn't ever reach here
            return -1;
        }

        /// <summary>
        /// Helper method that creates an array of possible insertion 
        /// index candidates that could be used to insert a new value
        /// in this heap.
        /// </summary>
        /// <returns>array of indexes</returns>
        private int[] GetInsertionIndexCandidates()
        {
            int rootIndex = GetRootIndex(Count);
            int[] indexes =
            {
                rootIndex, GetLeftChildIndex(rootIndex), GetRightChildIndex(rootIndex),
            };
            return indexes;
        }

        /// <summary>
        /// Checks to see if the specified value is NULL or the DEFAULT (in the
        /// case of non-nullable generic types).
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>true if value is NULL or the DEFAULT</returns>
        private bool IsNullOrDefault(T value)
        {
            bool result = (value == null) || (value.Equals(default(T)));
            return result;
        }

        /// <summary>
        /// Finds the correct left child index from the specified starting index
        /// </summary>
        /// <param name="startIndex">Index we want the left child of</param>
        /// <returns>int value of the left child index</returns>
        private int GetLeftChildIndex(int startIndex)
        {
            int leftChildIndex = ((2 * startIndex) + 1);
            return leftChildIndex;
        }

        /// <summary>
        /// Finds the correct right child index from the specified starting index
        /// </summary>
        /// <param name="startIndex">index we want the right child of</param>
        /// <returns>int value of the right child index</returns>
        private int GetRightChildIndex(int startIndex)
        {
            int leftChildIndex = ((2 * startIndex) + 2);
            return leftChildIndex;
        }

        /// <summary>
        /// Resizes the backing array if by adding another value, we would
        /// exceed the capacity. 
        /// </summary>
        /// <returns>true if a resize operation was performed</returns>
        private bool ResizeArrayIfFull()
        {
            bool didResize = false;

            if (Count + 1 >= Values.Length)
            {
                Values = ResizeArray(Values);
                didResize = true;
            }
            return didResize;
        }

        /// <summary>
        /// Creates a new array containing all the values in the
        /// specified array, that is double the capacity.
        /// </summary>
        /// <param name="arr">array of values</param>
        /// <returns>copy of array that is twice as big</returns>
        private T[] ResizeArray(T[] arr)
        {
            int newSize = (arr.Length * 2);
            T[] resizedArray = new T[newSize];

            for (int j = 0; j < Count; ++j)
            {
                resizedArray[j] = arr[j];
            }
            return resizedArray;
        }

        /// <summary>
        /// Rebuilds the heap by ensuring that the values are in their correct
        /// position with respect to their child nodes. (i.e. the root node's
        /// children are <= root node's value)
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="index">index of value</param>
        private void Heapify(T value, int index)
        {
            if (IsNullOrDefault(Values[index])) { return; }

            int leftIndex  = GetLeftChildIndex(index);
            int rightIndex = GetRightChildIndex(index);

            // stop if we're attempting to access non-existant children beyond
            // the size of our backing array
            if (leftIndex >= Count) { return; }

            int swapIndex = GetSwapIndex(value, leftIndex, rightIndex);

            if (swapIndex != -1)
            {
                Swap(index, swapIndex);
                T newValue = Values[swapIndex];
                Heapify(newValue, swapIndex);
            }
        }

        /// <summary>
        /// Helper method for determining the proper index to swap the value
        /// against
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="leftIndex">left child index</param>
        /// <param name="rightIndex">right child index</param>
        /// <returns>swap index</returns>
        private int GetSwapIndex(T value, int leftIndex, int rightIndex)
        {
            int swapIndex = -1;

            T leftChild  = Values[leftIndex];
            T rightChild = Values[rightIndex];

            // check to see if children have a greater value
            if (Compare(leftChild, value))
            {
                swapIndex = Compare(rightChild, leftChild) ? rightIndex : leftIndex;
            }
            else if (Compare(rightChild, value))
            {
                swapIndex = rightIndex;
            }
            return swapIndex;
        }

        /// <summary>
        /// Convenience helper method for checking if the first value
        /// is not the DEFAULT or NULL value and if it's comparison
        /// to the second value is > 0
        /// </summary>
        /// <param name="value1">Value to check if DEFAULT or NULL and 
        /// and compare against second value</param>
        /// <param name="value2">Value to compare against </param>
        /// <returns>true if comparison val1 > val2 </returns>
        private bool Compare(T value1, T value2)
        {
            bool result = false;

            if (!IsNullOrDefault(value1) && value1.CompareTo(value2) > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Convenience helper method to insert a value in the backing
        /// array if the value at the specified index is either the
        /// DEFAULT generic value or NULL
        /// </summary>
        /// <param name="value">value to insert</param>
        /// <param name="index">index to check</param>
        /// <returns>true if value was inserted</returns>
        private bool InsertValueAtIndexIfNull(T value, int index)
        {
            bool didInsert = false;
            if (IsNullOrDefault(Values[index]))
            {
                Values[index] = value;
                didInsert = true;
            }
            return didInsert;
        }
    }
}

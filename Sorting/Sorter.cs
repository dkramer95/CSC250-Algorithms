using System;

namespace SortingLibrary
{
    public class Sorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts an array using the BubbleSort algorithm
        /// </summary>
        /// <param name="arr">Array of data to be sorted</param>
        public static void BubbleSort(T[] arr)
        {
            int count = arr.Length;

            for (int j = 0; j < count - 1; ++j)
            {
                for (int k = j + 1; k < count; ++k)
                {
                    if (arr[k].CompareTo(arr[j]) < 0)
                    {
                        Swap(ref arr[k], ref arr[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Sorts an array using the SelectionSort algorithm
        /// </summary>
        /// <param name="arr">Array of data to be sorted</param>
        public static void SelectionSort(T[] arr)
        {
            int count = arr.Length;

            for (int j = 0; j < count - 1; ++j)
            {
                int minIndex = j;

                for (int k = j + 1; k < count; ++k)
                {
                    if (arr[k].CompareTo(arr[minIndex]) < 0)
                    {
                        minIndex = k;
                    }
                }
                Swap(ref arr[j], ref arr[minIndex]);
            }
        }

        /// <summary>
        /// Sorts an array using the InsertionSort algorithm
        /// </summary>
        /// <param name="arr">Array of data to be sorted</param>
        public static void InsertionSort(T[] arr)
        {
            int count = arr.Length;

            for (int k = 1; k < count; ++k)
            {
                int pos = k - 1;
                T val = arr[k];

                while (pos >= 0 && (arr[pos].CompareTo(val) > 0))
                {
                    // move
                    arr[pos + 1] = arr[pos];
                    --pos;
                }
                arr[pos + 1] = val;
            }
        }

        /// <summary>
        /// Sorts an array using the QuickSort algorithm
        /// </summary>
        /// <param name="arr">Array of data to be sorted</param>
        public static void QuickSort(T[] arr)
        {
            QuickSort(arr, 0, arr.Length);
        }

        private static void QuickSort(T[] arr, int low, int high)
        {
            if (low < high)
            {
                int partition = Partition(arr, low, high);
                QuickSort(arr, low, partition);
                QuickSort(arr, partition + 1, high);
            }
        }

        /// <summary>
        /// Sorts an array using the MergeSort algorithm
        /// </summary>
        /// <param name="arr">Array of data to be sorted</param>
        public static void MergeSort(T[] arr)
        {
            MergeSort(arr, arr.Length);
        }

        private static void MergeSort(T[] arr, int n)
        {
            if (n > 1)
            {
                T[] left  = CopyArray(arr, 0, (n / 2));
                T[] right = CopyArray(arr, (n / 2), n);

                // Divide
                MergeSort(left, left.Length);
                MergeSort(right, right.Length);

                // Conquer
                Merge(left, right, arr);
            }
        }

        /// <summary>
        /// Merges the left and right arrays back into the source array, in their
        /// sorted order.
        /// </summary>
        /// <param name="left">left side array</param>
        /// <param name="right">right side array</param>
        /// <param name="source">original array</param>
        private static void Merge(T[] left, T[] right, T[] source)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int sortedIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    source[sortedIndex++] = left[leftIndex++];
                }
                else
                {
                    source[sortedIndex++] = right[rightIndex++];
                }
            }
            if (leftIndex == left.Length)
            {
                // copy the rest of the right array
                CopyIntoArray(right, source, rightIndex, sortedIndex);
            }
            else
            {
                // copy the rest of the left array
                CopyIntoArray(left, source, leftIndex, sortedIndex);
            }
        }

        /// <summary>
        /// Copys data from the source array, into the destination array.
        /// </summary>
        /// <param name="source">Array containing data to be copied</param>
        /// <param name="dest">Array that will be filled with data</param>
        /// <param name="sourceStartIndex">starting index of the source array to begin copying from</param>
        /// <param name="destIndex">starting index of the destination array to begin inserting into</param>
        private static void CopyIntoArray(T[] source, T[] dest, int sourceStartIndex, int destIndex)
        {
            for (int j = sourceStartIndex; j < source.Length; ++j)
            {
                dest[destIndex++] = source[j];
            }
        }

        /// <summary>
        /// Makes a copy of all the values in the specified array
        /// </summary>
        /// <param name="arr">Array containing data to be copied</param>
        /// <param name="start">starting index to begin copying from</param>
        /// <param name="end">ending index to end copying</param>
        /// <returns>new array containing copied data</returns>
        private static T[] CopyArray(T[] arr, int start, int end)
        {
            int size = (end - start);
            int k = start;

            T[] arrCopy = new T[size];

            for (int j = 0; j < size; ++j)
            {
                arrCopy[j] = arr[k++];
            }
            return arrCopy;
        }

        /// <summary>
        /// Swaps 2 elements by adjusting the values that they reference
        /// </summary>
        /// <param name="a">Value to become 'b'</param>
        /// <param name="b">Value to become 'a'</param>
        private static void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Partitions an array and creates a pivot point where all
        /// values that are less than pivot are on the left, and all
        /// values that are greater than pivot are on the right
        /// </summary>
        /// <param name="arr">Array to partition</param>
        /// <param name="low">Starting index</param>
        /// <param name="high">Ending index</param>
        /// <returns>int value containining index that represents position in array where
        /// any values less than the index are in sorted order</returns>
        private static int Partition(T[] arr, int low, int high)
        {
            T pivot = arr[low];
            int j = low;

            for (int k = low; k < high; ++k)
            {
                if (arr[k].CompareTo(pivot) < 0)
                {
                    Swap(ref arr[k], ref arr[j]);
                    ++j;
                }
            }
            Swap(ref pivot, ref arr[j]);

            return j;
        }
    }
}

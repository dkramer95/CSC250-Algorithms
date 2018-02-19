using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class ArrayPriorityQueue<T> where T : IComparable
    {
        public BinaryHeap<PQNode<T>> Queue { get; set; }
        public int Count { get { return Queue.Count; } }

        public ArrayPriorityQueue()
        {
            Init();
        }

        private void Init()
        {
            Queue = new BinaryHeap<PQNode<T>>();
        }

        /// <summary>
        /// Creates a PQNode with the specified priority and value
        /// and adds it to this Queue. PQNodes with the highest
        /// priority will be added to the front of this Queue.
        /// </summary>
        /// <param name="priority">priority level</param>
        /// <param name="value">value</param>
        public void Enqueue(int priority, T value)
        {
            PQNode<T> nodeToAdd = new PQNode<T>(priority, value);
            Queue.Insert(nodeToAdd);
        }

        /// <summary>
        /// Removes the first PQNode in this queue, which has the
        /// highest priority.
        /// </summary>
        /// <returns>PQNode that we removed</returns>
        public PQNode<T> Dequeue()
        {
            PQNode<T> nodeToRemove = Queue.RemoveMax();
            return nodeToRemove;
        }

        /// <summary>
        /// Returns but doesn't remove the first PQNode in this queue,
        /// which has the highest priority.
        /// </summary>
        /// <returns>PQNode at the start of this Queue</returns>
        public PQNode<T> Peek()
        {
            PQNode<T> node = Queue.Values[0];
            return node;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < Count; ++j)
            {
                PQNode<T> node = Queue.Values[j];
                sb.Append(node + ", ");
            }
            string str = sb.ToString().Trim().TrimEnd(',');
            return str;
        }
    }
}

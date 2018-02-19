using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class SingleLinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public int Count { get; protected set; }


        /// <summary>
        /// Adds a new node with the specified value at the end
        /// of this LinkedList
        /// </summary>
        /// <param name="val"></param>
        public void Add(T val)
        {
            Node<T> nodeToAdd = new Node<T>(val);

            if (Head != null)
            {
                //Tail = GetLastNode().Next = nodeToAdd;
                Tail = Tail.Next = nodeToAdd;
            } else
            {
                // first time adding a node
                Head = nodeToAdd;
                Tail = Head;
            }
            ++Count;
        }

        [Obsolete]  // I don't know why I had this, because all I have to do is reference the tail!!
        /// <summary>
        /// Returns the last node in this LinkedList
        /// </summary>
        /// <returns></returns>
        protected Node<T> GetLastNode()
        {
            Node<T> node = Head;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node;
        }

        /// <summary>
        /// Creates a node from the specified value and inserts it at the
        /// specified index
        /// </summary>
        /// <param name="val">value for node</param>
        /// <param name="index">index location to insert at</param>
        public void Insert(T val, int index)
        {
            if (Count == 0)
            {
                Add(val);
            }
            else
            {
                Node<T> nodeToInsert = new Node<T>(val);

                if (index == 0)
                {
                    nodeToInsert.Next = Head;
                    Head = nodeToInsert;
                }
                else
                {
                    Node<T> existingNode = NodeAt(index - 1);
                    nodeToInsert.Next = existingNode.Next;
                    existingNode.Next = nodeToInsert;
                }
                ++Count;
            }
        }

        /// <summary>
        /// Helper method that preserves the last node, and last index
        /// we looked at, to avoid unnecessary duplicate traversing in
        /// this LinkedList in some cases.
        /// </summary>
        /// <param name="lastNode">Last node looked at</param>
        /// <param name="lastIndex">Index last looked at</param>
        /// <param name="index">Target index to reach</param>
        /// <returns>Node at the target index</returns>
        protected Node<T> NodeAt(Node<T> lastNode, int lastIndex, int index)
        {
            if (!RangeCheck(index))
            {
                throw new IndexOutOfRangeException();
            }
            Node<T> node = lastNode;

            for (int j = lastIndex; j < index; ++j)
            {
                node = node.Next;
            }
            return node;
        }

        /// <summary>
        /// Returns node at specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected Node<T> NodeAt(int index)
        {
            Node<T> node = NodeAt(Head, 0, index);
            return node;
        }

        /// <summary>
        /// Returns value of node at specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T Get(int index)
        {
            Node<T> node = NodeAt(index);
            return node.Value;
        }

        /// <summary>
        /// Removes the first node of this LinkedList and
        /// returns its value
        /// </summary>
        /// <returns></returns>
        public T Remove()
        {
            T value = RemoveAt(0);
            return value;
        }

        /// <summary>
        /// Removes a node at the specified index and returns
        /// its value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T RemoveAt(int index)
        {
            T value = default(T);

            if (Count > 0)
            {
                if (index == 0)
                {
                    value = Head.Value;
                    Head = Head.Next;
                } else
                {
                    Node<T> node = NodeAt(index - 1);
                    value = node.Next.Value;
                    node.Next = node.Next.Next;
                }
                --Count;
            }
            return value;
        }

        /// <summary>
        /// Removes the last node in this LinkedList and
        /// returns its value
        /// </summary>
        /// <returns></returns>
        public T RemoveLast()
        {
            T value = RemoveAt(Count - 1);
            Tail = (Count >= 1) ? NodeAt(Count - 1) : null;
            return value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node<T> node = Head;

            while (node != null)
            {
                sb.Append(" " + node.Value + ",");
                node = node.Next;
            }
            return sb.ToString().Trim().TrimEnd(',');
        }

        /// <summary>
        /// Clears out this LinkedList
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Helper method to ensure the index is within the acceptable
        /// range of this LinkedList
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if index is within range</returns>
        protected bool RangeCheck(int index)
        {
            bool isValid = (index >= 0 && index < Count);
            return isValid;
        }

        /// <summary>
        /// Searches for the specified value in this LinkedList
        /// </summary>
        /// <param name="val"></param>
        /// <returns>index of first node match, otherwise -1</returns>
        public int Search(T val)
        {
            int result = -1;
            Node<T> node = Head;

            for (int j = 0; j < Count && node != null; ++j)
            {
                if (node.Value.Equals(val))
                {
                    result = j;
                    break;
                }
                node = node.Next;
            }
            return result;
        }

        /// <summary>
        /// Creates an array of all the values in this LinkedList
        /// </summary>
        /// <returns>Array of all values in this LinkedList</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            Node<T> node = Head;

            for (int j = 0; j < Count; ++j)
            {
                array[j] = node.Value;
                node = node.Next;
            }
            return array;
        }
    }
}

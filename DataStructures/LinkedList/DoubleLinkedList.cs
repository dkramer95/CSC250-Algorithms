using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class DoubleLinkedList<T> : SingleLinkedList<T>
    {
        public new DoubleNode<T> Head { get; set; }
        public new DoubleNode<T> Tail { get; set; }

        /// <summary>
        /// Adds a new node with the specified value at the end of
        /// this DoubleLinkedList
        /// </summary>
        /// <param name="val"></param>
        public new void Add(T val)
        {
            DoubleNode<T> nodeToAdd = new DoubleNode<T>(val);

            if (Head != null)
            {
                nodeToAdd.Next = Tail.Next;
                Tail.Next = nodeToAdd;
                nodeToAdd.Previous = Tail;
                Tail = nodeToAdd;
            } else
            {
                // first time adding a node
                Head = nodeToAdd;
                Tail = Head;
            }
            ++Count;
        }

        /// <summary>
        /// Creates a node from the specified value and inserts it at the
        /// specified index
        /// </summary>
        /// <param name="val">value for node</param>
        /// <param name="index">index location to insert at</param>
        public new void Insert(T val, int index)
        {
            if (IsEmpty())
            {
                Add(val);
            } else
            {
                DoubleNode<T> existingNode = NodeAt(index);
                DoubleNode<T> nodeToInsert = new DoubleNode<T>(val);

                if (index == 0)
                {
                    // insert at the head
                    nodeToInsert.Next = Head;
                    nodeToInsert.Previous = Head.Previous;
                    Head.Previous = nodeToInsert;
                    Head = nodeToInsert;
                }
                else
                {
                    existingNode.Previous.Next = nodeToInsert;
                    nodeToInsert.Previous = existingNode.Previous;
                    nodeToInsert.Next = existingNode;
                    existingNode.Previous = nodeToInsert;
                }
                ++Count;
            }
        }

        /// <summary>
        /// Helper method to update the values of the pointers of nodes
        /// </summary>
        /// <param name="existingNode"></param>
        /// <param name="nodeToInsert"></param>
        protected void UpdateNodes(DoubleNode<T> existingNode, DoubleNode<T> nodeToInsert)
        {
            existingNode.Previous.Next = nodeToInsert;
            nodeToInsert.Previous = existingNode.Previous;
            nodeToInsert.Next = existingNode;
            existingNode.Previous = nodeToInsert;
        }

        /// <summary>
        /// Removes a node at the head of this DoubleLinkedList
        /// </summary>
        /// <returns>value of node that was removed</returns>
        public new T Remove()
        {
            T value = RemoveAt(0);
            return value;
        }

        /// <summary>
        /// Removes a node at specified index and returns its value
        /// </summary>
        /// <param name="index">Index of node to remove</param>
        /// <returns>value of node that was removed</returns>
        public new T RemoveAt(int index)
        {
            T value = default(T);

            if (Count > 0)
            {
                DoubleNode<T> nodeToRemove = NodeAt(index);
                value = nodeToRemove.Value;

                if (Count == 1)
                {
                    Clear();
                }
                else
                {
                    if (nodeToRemove == Head)
                    {
                        Head.Next.Previous = Head.Previous;
                        Head = Head.Next;
                    }
                    else if (nodeToRemove == Tail)
                    {
                        Tail.Previous.Next = Tail.Next;
                        Tail = Tail.Previous;
                    }
                    else
                    {
                        nodeToRemove.Previous.Next = nodeToRemove.Next;
                        nodeToRemove.Next.Previous = nodeToRemove.Previous; 
                    }
                    --Count;
                }
            }
            return value;
        }

        /// <summary>
        /// Removes the last node in this DoubleLinkedList
        /// </summary>
        /// <returns></returns>
        public new T RemoveLast()
        {
            T value = RemoveAt((Count - 1));
            return value;
        }

        /// <summary>
        /// Gets a node at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new T Get(int index)
        {
            DoubleNode<T> node = NodeAt(index);
            return node.Value;
        }

        /// <summary>
        /// Returns the node at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected new DoubleNode<T> NodeAt(int index)
        {
            DoubleNode<T> start = (index <= Count / 2) ? Head : Tail;
            DoubleNode<T> node = NodeAt(start, 0, index);
            return node;
        }

        /// <summary>
        /// Helper method that preserves the last node, and the last index
        /// we looked at, to avoid unnecessary duplicate traversing in
        /// this LinkedList in some cases. There's also an optional boolean
        /// flag to force the traversal direction, which may be useful
        /// in some cases.
        /// </summary>
        /// <param name="lastNode">Last Node looked at</param>
        /// <param name="lastIndex">Index last looked at</param>
        /// <param name="index">Target index to reach</param>
        /// <param name="forceForward">optional forced forward traversal flag</param>
        /// <returns></returns>
        protected DoubleNode<T> NodeAt(DoubleNode<T> lastNode, int lastIndex, int index, bool forceForward = false)
        {
            if (!RangeCheck(index))
            {
                throw new IndexOutOfRangeException();
            }
            DoubleNode<T> node = lastNode;

            if (index <= (Count / 2) || forceForward)
            {
                // traverse forwards
                for (int j = lastIndex; j < index; ++j)
                {
                    node = node.Next;
                }
            }
            else
            {
                // traverse backwards
                for (int j = (Count - lastIndex - 1); j > index; --j)
                {
                    node = node.Previous;
                }
            }

            return node;
        }

        /// <summary>
        /// Searches for the specified value starting at both the head
        /// and tail of this DoubleLinkedList
        /// </summary>
        /// <param name="val">Value to search for</param>
        /// <returns>index of first node match, otherwise -1</returns>
        public new int Search(T val)
        {
            int result = -1;

            // nothing in list, can't search!
            if (IsEmpty()) { return result; }

            DoubleNode<T> forwardNode = Head;
            DoubleNode<T> backwardNode = Tail;

            int targetIndex = (Count / 2);

            for (int j = 0; j <= targetIndex; ++j)
            {
                int forwardIndex = j;
                int backwardIndex = (Count - 1 - j);

                if (forwardNode.Value.Equals(val))
                {
                    result = forwardIndex;
                    break;
                }
                else if (backwardNode.Value.Equals(val))
                {
                    result = backwardIndex;
                    break;
                }
                forwardNode = forwardNode.Next;
                backwardNode = backwardNode.Previous;
            }

            return result;
        }

        public new void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool IsEmpty()
        {
            bool isEmpty = (Head == null) && (Tail == null) && (Count == 0);
            return isEmpty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            DoubleNode<T> node = Head;

            while (node != null)
            {
                sb.Append(" " + node.Value + ",");
                node = node.Next;
            }
            return sb.ToString().Trim().TrimEnd(',');
        }
    }
}

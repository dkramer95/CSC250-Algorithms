using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class PQNode<T> : IComparable where T : IComparable
    {
        public int Priority { get; set; }
        public T Value { get; set; }

        public PQNode(int priority, T value)
        {
            Priority = priority;
            Value = value;
        }

        public override string ToString()
        {
            string str = string.Format("{0}:{1}", Priority, Value);
            return str;
        }

        public int CompareTo(object obj)
        {
            PQNode<T> other = obj as PQNode<T>;
            int compareValue = Priority.CompareTo(other.Priority);

            if (compareValue == 0)
            {
                // priority is the same, compare by their value
                compareValue = Value.CompareTo(other.Value);
            }
            return compareValue;
        }
    }
}

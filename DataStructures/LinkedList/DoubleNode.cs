using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class DoubleNode<T> : Node<T>
    {
        public DoubleNode<T> Previous { get; set; }

        public new DoubleNode<T> Next { get; set; }

        public DoubleNode(T val) : base(val)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class TreeNode<T>
    {
        public TreeNode<T> Parent { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }


        public T Value { get; set; }

        public TreeNode(T val)
        {
            Value = val;
        }

        public bool HasLeft()
        {
            bool hasLeft = (Left != null);
            return hasLeft;
        }

        public bool HasRight()
        {
            bool hasRight = (Right != null);
            return hasRight;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

using System;

namespace AlgoDataStructures
{
    public class BinarySearchTree<T> where T : IComparable
    {
        public TreeNode<T> RootNode { get; private set; }
        public int Count { get; private set; }


        /// <summary>
        /// Adds the specified value to this BinarySearchTree
        /// </summary>
        /// <param name="value">value to add</param>
        public void Add(T value)
        {
            TreeNode<T> nodeToAdd = new TreeNode<T>(value);

            if (IsEmpty())
            {
                RootNode = nodeToAdd;
            }
            else
            {
                TreeNode<T> currentNode = RootNode;
                TreeNode<T> nextNode = null;

                bool didInsert = false;

                while (!didInsert)
                {
                    nextNode = currentNode;
                    nodeToAdd.Parent = nextNode;

                    // traverse left
                    if (nodeToAdd.Value.CompareTo(nextNode.Value) < 0)
                    {
                        currentNode = currentNode.Left;

                        if (currentNode == null)
                        {
                            nextNode.Left = nodeToAdd;
                            didInsert = true;
                        }
                    }
                    // traverse right
                    else
                    {
                        currentNode = currentNode.Right;

                        if (currentNode == null)
                        {
                            nextNode.Right = nodeToAdd;
                            didInsert = true;
                        }
                    }
                }
            }
            ++Count;
        }

        /// <summary>
        /// Checks to see if this BinarySearchTree is empty
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            bool isEmpty = (Count == 0) && (RootNode == null);
            return isEmpty;
        }

        /// <summary>
        /// Checks to see if this BinarySearchTree contains the specified
        /// value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true if tree contains value</returns>
        public bool Contains(T value)
        {
            bool didFind = FindNode(value) != null;
            return didFind;
        }

        /// <summary>
        /// Finds the node with the specified value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Node with value or null</returns>
        private TreeNode<T> FindNode(T value)
        {
            TreeNode<T> node = RootNode;

            while (node != null && !node.Value.Equals(value))
            {
                node = (value.CompareTo(node.Value) < 0) ? node.Left : node.Right;
            }
            return node;
        }

        /// <summary>
        /// Removes the specified value by finding the first node with that
        /// value (if it exists).
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            TreeNode<T> nodeToRemove = FindNode(value);

            if (nodeToRemove == null) { return; }

            bool hasLeftChild = nodeToRemove.HasLeft();
            bool hasRightChild = nodeToRemove.HasRight();

            if (hasLeftChild && hasRightChild)
            {
                RemoveNodeWith2Children(nodeToRemove);
            }
            else if (hasRightChild)
            {
                RemoveNodeWithRightChild(nodeToRemove);
            }
            else if (hasLeftChild)
            {
                RemoveNodeWithLeftChild(nodeToRemove);
            }
            else
            {
                RemoveLeafNode(nodeToRemove);
            }
            --Count;
        }

        /// <summary>
        /// Helper method for removing a node that has no children
        /// </summary>
        /// <param name="nodeToRemove"></param>
        private void RemoveLeafNode(TreeNode<T> nodeToRemove)
        {
            if (nodeToRemove == nodeToRemove.Parent.Right)
            {
                nodeToRemove.Parent.Right = null;
            }
            else
            {
                nodeToRemove.Parent.Left = null;
            }
            nodeToRemove.Parent = null;
        }

        /// <summary>
        /// Helper method for removing a node that has both left and
        /// right children
        /// </summary>
        /// <param name="nodeToRemove"></param>
        private void RemoveNodeWith2Children(TreeNode<T> nodeToRemove)
        {
            TreeNode<T> replacementNode = nodeToRemove.Right;

            while (replacementNode.HasLeft())
            {
                replacementNode = replacementNode.Left;
            }
            nodeToRemove.Value = replacementNode.Value;

            // delete the replacement node
            if (replacementNode.Parent != null)
            {
                if (replacementNode == replacementNode.Parent.Right)
                {
                    replacementNode.Parent.Right = null;
                } else
                {
                    replacementNode.Parent.Left = null;
                }
            }

            // remove replacement node
            replacementNode.Parent = nodeToRemove.Parent;
        }

        /// <summary>
        /// Helper method for removing a node that has only a right child
        /// </summary>
        /// <param name="nodeToRemove"></param>
        private void RemoveNodeWithRightChild(TreeNode<T> nodeToRemove)
        {
            if (nodeToRemove == nodeToRemove.Parent.Left)
            {
                nodeToRemove.Parent.Left = nodeToRemove.Right;
                nodeToRemove.Right.Parent = nodeToRemove.Parent;
            }
            else
            {
                nodeToRemove.Parent.Right = nodeToRemove.Right;
                nodeToRemove.Right.Parent = nodeToRemove.Parent;
            }
        }

        /// <summary>
        /// Helper method for removing a node that has only a left child
        /// </summary>
        /// <param name="nodeToRemove"></param>
        private void RemoveNodeWithLeftChild(TreeNode<T> nodeToRemove)
        {
            nodeToRemove.Parent.Left = nodeToRemove.Left;
            nodeToRemove.Left.Parent = nodeToRemove.Parent;
        }

        /// <summary>
        /// Clears out this BinarySearchTree
        /// </summary>
        public void Clear()
        {
            RootNode = null;
            Count = 0;
        }

        /// <summary>
        /// </summary>
        /// <returns>string of values in order</returns>
        public string Inorder()
        {
            // Left, Root, Right 
            string str = Inorder(RootNode);
            return str.Trim().TrimEnd(',');
        }

        // Helper recursive method for inorder traversal
        private string Inorder(TreeNode<T> node)
        {
            if (node == null) { return ""; }

            string str = Inorder(node.Left);
            str += node.Value + ", ";
            str += Inorder(node.Right);

            return str;
        }

        /// <summary>
        /// </summary>
        /// <returns>string of values in pre order</returns>
        public string Preorder()
        {
            // Root, Left, Right
            string str = Preorder(RootNode);
            return str.Trim().TrimEnd(',');
        }

        // Helper recursive method for preorder traversal
        private string Preorder(TreeNode<T> node)
        {
            if (node == null) { return ""; }

            string str = node.Value + ", ";
            str += Preorder(node.Left);
            str += Preorder(node.Right);

            return str;
        } 

        /// <summary>
        /// </summary>
        /// <returns>string of values in post order</returns>
        public string Postorder()
        {
            // Left, Right, Root
            string str = Postorder(RootNode);
            return str.Trim().TrimEnd(',');
        }

        // Helper recursive method for postorder traversal
        private string Postorder(TreeNode<T> node)
        {
            if (node == null) { return ""; }

            string str = Postorder(node.Left);
            str += Postorder(node.Right);
            str += node.Value + ", ";

            return str;
        }

        /// <summary>
        /// Finds the height of this binary tree, which is defined
        /// as the number of edges in longest path from root
        /// to a leaf node
        /// </summary>
        /// <returns>height int value</returns>
        public int Height()
        {
            int height = Height(RootNode);
            return height;
        }

        /// <summary>
        /// Recursively determines height of this tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns>height of this tree</returns>
        private int Height(TreeNode<T> node)
        {
            if (node == null) { return 0; }

            int leftHeight = Height(node.Left);
            int rightHeight = Height(node.Right);

            return (1 + Math.Max(leftHeight, rightHeight));
        }

        /// <summary>
        /// Creates an array of all the values (in order) that are contained within
        /// this BinarySearchTree
        /// </summary>
        /// <returns>Array of all values in this BinarySearchTree</returns>
        public T[] ToArray()
        {
            T[] arr = new T[Count];
            int index = 0;
            BuildArray(arr, ref index, RootNode);
            return arr;
        }

        // Helper method to recursively add all node values to an array
        private void BuildArray(T[] arr, ref int index, TreeNode<T> node)
        {
            if (node == null || index >= Count) { return; }

            BuildArray(arr, ref index, node.Left);
            arr[index++] = node.Value;
            BuildArray(arr, ref index, node.Right);
        }
    }
}

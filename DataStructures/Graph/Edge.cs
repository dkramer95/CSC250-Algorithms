using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class Edge<T> : IComparable
    {
        public T Vertex1 { get; set; }
        public T Vertex2 { get; set; }
        public int Weight { get; set; }

        public Edge(T vertex1, T vertex2, int weight)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
        }

        public override string ToString()
        {
            string str = string.Format("{0}-{1}:{2}", Vertex1, Vertex2, Weight);
            return str;
        }

        public int CompareTo(object obj)
        {
            Edge<T> other = obj as Edge<T>;
            int result = Weight.CompareTo(other.Weight);
            return result;
        }
    }
}

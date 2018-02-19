using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDataStructures
{
    public class Graph<T>
    {
        public List<T> Vertices { get; private set; }
        public List<Edge<T>> Edges { get; private set; }

        public Graph()
        {
            Init();
        }

        private void Init()
        {
            Vertices = new List<T>();
            Edges = new List<Edge<T>>();
        }

        public void AddVertex(T vertex)
        {
            Vertices.Add(vertex);
        }

        public void AddEdge(Edge<T> edge)
        {
            Edges.Add(edge);
        }

        public List<Graph<T>> GetMinimumSpanningTrees()
        {
            List<Graph<T>> trees = GetMinimumSpanningTrees(Vertices, Edges);
            return trees;
        }

        /// <summary>
        /// Creates a list of all minimum spanning trees in this Graph.
        /// </summary>
        /// <param name="vertices">Source of vertices</param>
        /// <param name="edges">Source of edges</param>
        /// <returns>List of minimum spanning trees</returns>
        private List<Graph<T>> GetMinimumSpanningTrees(List<T> vertices, List<Edge<T>> edges)
        {
            // all the potential MST's (in the case of a disconnected graph)
            List<Graph<T>> minimumSpanningTrees = new List<Graph<T>>();

            // current MST we're building
            Graph<T> mst = new Graph<T>();
            List<Edge<T>> connectedEdges = new List<Edge<T>>();

            // flag to determine if a new vertex has been found, that is not yet part of the MST
            bool newVertexFound = true;
            T lastVertex = vertices[0];

            while (mst.Vertices.Count != vertices.Count)
            {
                if (newVertexFound)
                {
                    newVertexFound = AddVertexToMST(edges, mst, connectedEdges, ref lastVertex);
                }
                else
                {
                    // graph is disconnected... make a new MST
                    mst = CreateNewMST(vertices, minimumSpanningTrees, mst, connectedEdges, out newVertexFound, out lastVertex);
                }
            }
            minimumSpanningTrees.Add(mst);
            return minimumSpanningTrees;
        }

        /// <summary>
        /// Helper method to add the current MST to the list of MST's and begin creating a new one
        /// in the case of a disconnected graph
        /// </summary>
        /// <param name="vertices">Source of all vertices</param>
        /// <param name="minimumSpanningTrees">List of MST's</param>
        /// <param name="mst">Current MST that is being built</param>
        /// <param name="connectedEdges">All the connected eges of the current MST</param>
        /// <param name="newVertexFound">flag to show if a new undiscovered vertex has been
        /// found for the current MST</param>
        /// <param name="lastVertex">Last visited vertex</param>
        /// <returns>a new empty Graph</returns>
        private Graph<T> CreateNewMST(List<T> vertices, List<Graph<T>> minimumSpanningTrees, Graph<T> mst, 
            List<Edge<T>> connectedEdges, out bool newVertexFound, out T lastVertex)
        {
            minimumSpanningTrees.Add(mst);
            mst.Vertices.ForEach(v => vertices.Remove(v));
            mst = new Graph<T>();
            lastVertex = vertices[0];
            newVertexFound = true;
            connectedEdges.Clear();
            return mst;
        }

        /// <summary>
        /// Adds a vertex to the current MST and adds the next lowest weight edge
        /// </summary>
        /// <param name="edges">All the edges of this Graph</param>
        /// <param name="mst">Current MST that is being built</param>
        /// <param name="connectedEdges">All the connected edges of the current MST</param>
        /// <param name="lastVertex"></param>
        /// <returns>true if a new vertex has been found that is not a part of 
        /// the current MST</returns>
        private bool AddVertexToMST(List<Edge<T>> edges, Graph<T> mst, List<Edge<T>> connectedEdges, ref T lastVertex)
        {
            bool newVertexFound = false;
            // add new vertex and find all the new connected edges
            mst.Vertices.Add(lastVertex);
            connectedEdges.AddRange(GetConnectedEdges(lastVertex, edges));
            connectedEdges.Sort();

            // find the next lowest weight edge
            for (int j = 0; j < connectedEdges.Count; ++j)
            {
                Edge<T> edge = connectedEdges[j];

                if (!mst.Vertices.Contains(edge.Vertex2))
                {
                    lastVertex = edge.Vertex2;
                    mst.AddEdge(edge);
                    newVertexFound = true;
                    break;
                }
            }
            return newVertexFound;
        }

        /// <summary>
        /// Finds all the edges that are connected to the specified vertex,
        /// from the specified list of existing edges
        /// </summary>
        /// <param name="vertex">Vertex to find connected edges to</param>
        /// <param name="edges">List of existing edges</param>
        /// <returns>List of edges that are connected to the vertex</returns>
        private List<Edge<T>> GetConnectedEdges(T vertex, List<Edge<T>> edges)
        {
            List<Edge<T>> connectedEdges = new List<Edge<T>>();

            for (int j = 0; j < edges.Count; ++j)
            {
                Edge<T> edge = edges[j];
                if (edge.Vertex1.Equals(vertex))
                {
                    connectedEdges.Add(edge);
                }
            }
            return connectedEdges;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int totalWeight = 0;

            for (int j = 0; j < Edges.Count; ++j)
            {
                Edge<T> edge = Edges[j];
                totalWeight += edge.Weight;
                sb.Append(edge.ToString() + " ");
            }
            sb.Append("--> " + totalWeight);
            return sb.ToString().TrimEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>string of all the vertices within this graph</returns>
        public string VerticesString()
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 0; j < Vertices.Count; ++j)
            {
                T vertex = Vertices[j];
                sb.Append(vertex + ", ");
            }
            return sb.ToString().TrimEnd().Trim(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The combined total weight of all the edges in this graph</returns>
        public int Weight()
        {
            int weight = 0;

            for (int j = 0; j < Edges.Count; ++j)
            {
                Edge<T> edge = Edges[j];
                weight += edge.Weight;
            }
            return weight;
        }
    }
}

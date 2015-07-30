using System;
using System.Collections.Generic;
using AE.Graphs.Core.Exceptions;

namespace AE.Graphs.Core
{
    /// <summary>
    ///     Directed Graph
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public abstract class AbstractDiGraph<TNode>
    {
        public abstract List<Tuple<TNode, TNode, int>> AllEdges { get; }

        public abstract List<TNode> AllNodes { get; }

        public abstract void AddNode(TNode node);

        public abstract List<TNode> GetNeighbourNodes(TNode node);


        public abstract void AddEdge(TNode fromNode, TNode toNode, int weight);

        /// <summary>
        ///     Returns the weight of the edge between the source and the destination node
        /// </summary>
        /// <param name="fromNode">source node</param>
        /// <param name="toNode">destination node</param>
        /// <returns>the weight of the edge connection the two nodes </returns>
        /// <exception cref="EdgeNotFoundException">Throws EdgeNotFoundException if there is no node connecting the two nodes</exception>
        public abstract int GetEdgeWeight(TNode fromNode, TNode toNode);

        /// <summary>
        ///     Finds the weight of the path represented by the list of nodes by adding the weight of the edges.
        /// </summary>
        /// <param name="nodes">The list of nodes representing a valid path in a graph.</param>
        /// <returns>Returns the weight of the edges connecting the nodes</returns>
        /// <exception cref="System.ArgumentNullException">Throws ArgumentNullException if the nodes list or the graph is null. </exception>
        /// <exception cref="System.ArgumentException">Throws ArgumentException if the list of nodes does not constitute a valid path.</exception>
        public virtual int FindPathWeight( List<TNode> nodes)
        {
             int totalWeight = 0;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                totalWeight += GetEdgeWeight(nodes[i], nodes[i + 1]);
            }

            return totalWeight;
        }
    }
}
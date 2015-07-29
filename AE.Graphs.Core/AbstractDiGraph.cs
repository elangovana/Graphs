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
    }
}
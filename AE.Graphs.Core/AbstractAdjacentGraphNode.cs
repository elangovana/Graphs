using System.Collections.Generic;

namespace AE.Graphs.Core
{
    /// <summary>
    /// Represents a node within a graph by an adjaceny list.
    /// Each node is represented by the name of the node and list of its neighbours along with edge weights
    /// </summary>
    /// <typeparam name="TNode">The type used to represent a node</typeparam>
    public abstract class AbstractAdjacentGraphNode<TNode>
    {
        public abstract List<AdjacentNodeEdge<TNode>> Neighbours { get; }

        public abstract TNode Node { get; }

        public abstract void AddEdgeNeighbour(AdjacentNodeEdge<TNode> adjacentNodeEdge);

        public abstract int GetEdgeWeight(TNode neighbourNode);
    }
}
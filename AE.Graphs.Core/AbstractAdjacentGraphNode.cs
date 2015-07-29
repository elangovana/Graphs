using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public abstract class AbstractAdjacentGraphNode<TNode>
    {
        public abstract List<AbstractAdjacentNodeEdge<TNode>> Neighbours { get; }

        public abstract TNode Node { get; }
        public abstract void AddEdgeNeighbour(AbstractAdjacentNodeEdge<TNode> adjacentNodeEdge);

        public abstract int GetEdgeWeight(TNode neighbourNode);
    }
}
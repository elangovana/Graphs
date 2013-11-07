using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    internal class AdjacencyGraphNode<TNode> : AbstractAdjacenyGraphNode<TNode>
    {
        private readonly TNode _node;
        private List<AbstractAdjacentNodeEdge<TNode>> _adjacentNodeEdges;

        public AdjacencyGraphNode(TNode node)
        {
            _node = node;
        }

        private List<AbstractAdjacentNodeEdge<TNode>> AdjacentNodeEdges
        {
            get { return _adjacentNodeEdges ?? (_adjacentNodeEdges = new List<AbstractAdjacentNodeEdge<TNode>>()); }
        }

        public override List<AbstractAdjacentNodeEdge<TNode>> Neighbours
        {
            get { return AdjacentNodeEdges; }
        }

        public override TNode Node
        {
            get { return _node; }
        }

        public override void AddEdgeNeighbour(AbstractAdjacentNodeEdge<TNode> adjacentNodeEdge)
        {
            AdjacentNodeEdges.Add(adjacentNodeEdge);
        }

        public override int GetEdgeWeight(TNode node)
        {
            var neighbourNode = AdjacentNodeEdges.SingleOrDefault(x => x.Node.Equals(node));
            if (neighbourNode == null) throw new ArgumentException(string.Format("The Node {0} does not exist", node));

            return neighbourNode.Weight;
        }

        public override string ToString()
        {
            return _node.ToString();
        }
    }
}
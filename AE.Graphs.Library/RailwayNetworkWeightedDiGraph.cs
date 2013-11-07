using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;
using AE.Graphs.Core.Exceptions;
using AE.Graphs.Library.Exceptions;

namespace AE.Graphs.Library
{
    /// <summary>
    ///     An implementation of the abstract weighted graph with additional restrictions
    ///     - Edge weight be must be greater than Zero.
    ///     - Atmost one edge connects a source to destination.
    ///     - There cannot be any self edges
    /// </summary>
    /// <exception cref="DuplicateEdgeException">Throw DuplicateEdgeException when an edge is added between a source and destination that is already connected through an edge </exception>
    /// <typeparam name="TNode">The type of Node</typeparam>
    public class RailwayNetworkWeightedDigraph<TNode> : AbstractDiGraph<TNode>
    {
        #region Abstract Implementation

        public override List<TNode> AllNodes
        {
            get { return AdjacencyList.Select(x => x.Node).ToList(); }
        }

        public override List<Tuple<TNode, TNode, int>> AllEdges
        {
            get
            {
                var result = new List<Tuple<TNode, TNode, int>>();
                foreach (var graphNode in AdjacencyList)
                {
                    graphNode.Neighbours
                             .ForEach(n => result.Add(new Tuple<TNode, TNode, int>(graphNode.Node, n.Node, n.Weight)));
                }
                return result;
            }
        }

        public override void AddNode(TNode node)
        {
            AddNode(new AdjacencyGraphNode<TNode>(node));
        }

        public override List<TNode> GetNeighbourNodes(TNode node)
        {
            return GetGraphNode(node).Neighbours.Select(x => x.Node).ToList();
        }

        public override void AddEdge(TNode fromNode, TNode toNode, int weight)
        {
            AddEdge(new AdjacencyGraphNode<TNode>(fromNode), new AdjacentNodeEdge<TNode>(toNode, weight));
        }


        public override int GetEdgeWeight(TNode fromNode, TNode toNode)
        {
            AbstractAdjacentNodeEdge<TNode> edgeNode;

            var source = GetGraphNode(fromNode);
            var dest = GetGraphNode(toNode);

            if (!TryGetEdge(source.Node, dest.Node, out edgeNode))
                throw new EdgeNotFoundException(string.Format("The is no edge from {0} to {1}", fromNode, toNode));
            return edgeNode.Weight;
        }

        #endregion

        public void AddNode(AbstractAdjacenyGraphNode<TNode> graphNode)
        {
            if (graphNode == null) throw new ArgumentNullException("graphNode");
            if (DoesNodeExist(graphNode.Node))
                throw new DuplicateNodeException(string.Format("The node {0} already exists and cannot be added!",
                                                               graphNode.Node));
            AdjacencyList.Add(graphNode);
        }

        public void AddEdge(AbstractAdjacenyGraphNode<TNode> graphNode, AbstractAdjacentNodeEdge<TNode> adjacentNodeEdge)
        {
            //Validate Arguments
            if (graphNode == null) throw new ArgumentNullException("graphNode");
            if (adjacentNodeEdge == null) throw new ArgumentNullException("adjacentNodeEdge");
            if (graphNode.Node.Equals(adjacentNodeEdge.Node))
                throw new ArgumentException(
                    string.Format("The source {0} node and the destinate node cannot be the same", graphNode.Node));

            if (adjacentNodeEdge.Weight <= 0)
                throw new InvalidEdgeWeightException(
                    string.Format("The edge weight {0} is invalid. It must be greater than zero",
                                  adjacentNodeEdge.Weight));

            AbstractAdjacentNodeEdge<TNode> existingNodeEdge;
            if (TryGetEdge(graphNode.Node, adjacentNodeEdge.Node, out existingNodeEdge))
            {
                throw new DuplicateEdgeException(
                    string.Format(
                        "An edge with weight {0} from source node {1} to destination node {2} already exists! ",
                        existingNodeEdge.Weight, graphNode.Node, adjacentNodeEdge.Node));
            }

            //Create Nodes and Edge


            AddNodeIfDoesntExist(graphNode.Node).AddEdgeNeighbour(adjacentNodeEdge);
            AddNodeIfDoesntExist(adjacentNodeEdge.Node);
        }

        #region

        private List<AbstractAdjacenyGraphNode<TNode>> _adjacenylist;

        private List<AbstractAdjacenyGraphNode<TNode>> AdjacencyList
        {
            get { return _adjacenylist ?? (_adjacenylist = new List<AbstractAdjacenyGraphNode<TNode>>()); }
        }

        private AbstractAdjacenyGraphNode<TNode> AddNodeIfDoesntExist(TNode node)
        {
            if (!DoesNodeExist(node)) AddNode(node);
            return GetGraphNode(node);
        }

        private bool DoesNodeExist(TNode node)
        {
            return (TryGetGraphNode(node) != null);
        }

        private AbstractAdjacenyGraphNode<TNode> TryGetGraphNode(TNode node)
        {
            return AdjacencyList.SingleOrDefault(x => x.Node.Equals(node));
        }

        private AbstractAdjacenyGraphNode<TNode> GetGraphNode(TNode node)
        {
            var result = TryGetGraphNode(node);

            if (result == null)
            {
                throw new NodeNotFoundException(string.Format("The node {0} does not exist", node));
            }

            return result;
        }

        private bool TryGetEdge(TNode source, TNode destination, out AbstractAdjacentNodeEdge<TNode> nodeedge)
        {
            nodeedge = null;
            if (DoesNodeExist(source))
            {
                nodeedge = GetGraphNode(source).Neighbours.SingleOrDefault(x => x.Node.Equals(destination));
            }

            return (nodeedge != null);
        }

        #endregion
    }
}
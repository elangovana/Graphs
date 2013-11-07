using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;
using AE.Graphs.Library.Exceptions;

namespace AE.Graphs.Library
{
    /// <summary>
    ///     Implements Dijkastra Shortest Path algorithm for graphs with non negative edge weights.
    /// </summary>
    /// <typeparam name="TNode">The node type</typeparam>
    public class DijkstrasShortestPathAlgorithm<TNode> : IAlgorithmShortestPath<TNode>
    {
        private const int Infinity = GraphPath<TNode>.InfinityValue;
        private readonly Dictionary<TNode, GraphPath<TNode>> _estimatedDistances;
        private readonly Dictionary<TNode, TNode> _predessorNodes;
        private readonly List<TNode> _shortestPathKnownNodes;
        private readonly List<GraphPath<TNode>> _shortestPathUnKnownNodesQueue;

        public DijkstrasShortestPathAlgorithm()
        {
            _shortestPathKnownNodes = new List<TNode>();
            _estimatedDistances = new Dictionary<TNode, GraphPath<TNode>>();
            _shortestPathUnKnownNodesQueue = new List<GraphPath<TNode>>();
            _predessorNodes = new Dictionary<TNode, TNode>();
        }

        #region IAlgorithmShortestPath<TNode> Members

        public AbstractGraphPath<TNode> GetShortestPath(AbstractDiGraph<TNode> graph, TNode sourceNode,
                                                        TNode destinationNode)
        {
            //Validate Input
            if (graph == null) throw new ArgumentNullException("graph");

            if (graph.AllEdges.Any(x => x.Item3 <= 0))
                throw new InvalidEdgeWeightException(
                    string.Format("The graph contains one or more edges with edge weight less than or equal to zero"));

            //Initialise data structures
            Initialise(graph, sourceNode);

            //Compute Shortest Path
            while (_shortestPathUnKnownNodesQueue.Any() && !_shortestPathKnownNodes.Contains(destinationNode))
            {
                foreach (var shortestPathKnownNode in _shortestPathKnownNodes)
                {
                    foreach (
                        var neighbour in
                            graph.GetNeighbourNodes(shortestPathKnownNode)
                                 .Where(x => !_shortestPathKnownNodes.Contains(x)))
                    {
                        int edgeWeight = graph.GetEdgeWeight(shortestPathKnownNode, neighbour);
                        int newestimatedDistance = _estimatedDistances[shortestPathKnownNode].PathWeight +
                                                   edgeWeight;
                        if (IsEstimatedDistanceBetter(neighbour, newestimatedDistance))
                        {
                            _estimatedDistances[neighbour].PathWeight = newestimatedDistance;

                            _predessorNodes[neighbour] = shortestPathKnownNode;
                        }
                    }
                }
                DequeueShortestPathKnownNode();
            }
            CompletePaths();
            var result = _estimatedDistances.SingleOrDefault(x => x.Value.DestinationNode.Equals(destinationNode)).Value;

            if (result.PathWeight == Infinity) return null;
            return result;
        }

        #endregion

        private void Initialise(AbstractDiGraph<TNode> graph, TNode sourceNode)
        {
            foreach (var node in graph.AllNodes.Where(x => !x.Equals(sourceNode)))
            {
                var shortestPathNode = new GraphPath<TNode>();
                shortestPathNode.DestinationNode = node;
                shortestPathNode.PathWeight = Infinity;
                shortestPathNode.SourceNode = sourceNode;
                _estimatedDistances.Add(node, shortestPathNode);
                _shortestPathUnKnownNodesQueue.Add(shortestPathNode);
            }
            //Source Node Initialise
            _estimatedDistances.Add(sourceNode,
                                    new GraphPath<TNode>() {DestinationNode = sourceNode, SourceNode = sourceNode});
            _estimatedDistances[sourceNode].PathWeight = 0;
            _shortestPathKnownNodes.Add(sourceNode);
        }


        private bool IsEstimatedDistanceBetter(TNode node, int newEstimatedDistance)
        {
            if (_estimatedDistances[node].PathWeight == Infinity) return true;
            return newEstimatedDistance < _estimatedDistances[node].PathWeight;
        }

        private void DequeueShortestPathKnownNode()
        {
            _shortestPathUnKnownNodesQueue.Sort();
            var shortestPathKnownNode = _shortestPathUnKnownNodesQueue.ElementAt(0);

            _shortestPathKnownNodes.Add(shortestPathKnownNode.DestinationNode);
            _shortestPathUnKnownNodesQueue.RemoveAt(0);
        }

        private void CompletePaths()
        {
            foreach (var entry in _estimatedDistances)
            {
                FillPredecessor(entry.Value, entry.Value.DestinationNode);
            }
        }

        private void FillPredecessor(AbstractGraphPath<TNode> graphPath, TNode node)
        {
            TNode predecessor;
            if (_predessorNodes.TryGetValue(node, out predecessor))
            {
                graphPath.Path.Insert(0, predecessor);
                FillPredecessor(graphPath, predecessor);
            }
        }
    }
}
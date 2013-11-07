using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    /// <summary>
    ///     An Implementation Of LEROY JOHNSON DCycle to find all Simple Cycles
    /// </summary>
    /// <typeparam name="TNode">Graph Node</typeparam>
    public class DCycleLeroyJohnson<TNode> : IAlgorithmCycleFinder<TNode>


    {
        public DCycleLeroyJohnson()
        {
            _allcycles = new List<AbstractGraphPath<TNode>>();
            _vistedEdges = new List<Tuple<TNode, TNode, int>>();

            _vStack = new Stack<TNode>();
            _oStack = new Stack<TNode>();
        }

        #region IAlgorithmCycleFinder<TNode> Members

        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph)
        {
            var cycleFinder = InitialiseAndGetInstance(graph);
            cycleFinder.FindSimpleCycles(graph);

            return cycleFinder._allcycles;
        }

        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode,
                                                                  int maxStops)
        {
            var cycleFinder = InitialiseAndGetInstance(graph);

            cycleFinder.FindSimpleCycle(graph, startNode);

            return cycleFinder._allcycles.Where(x => x.Path.Count <= maxStops + 1).ToList();
        }


        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            var cycleFinder = InitialiseAndGetInstance(graph);
            cycleFinder.FindSimpleCycle(graph, startNode);

            return cycleFinder._allcycles;
        }

        public List<AbstractGraphPath<TNode>> FindShortestCycle(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            var cycleFinder = InitialiseAndGetInstance(graph);
            cycleFinder.FindSimpleCycle(graph, startNode);

            return
                cycleFinder._allcycles.Where(x => x.PathWeight == cycleFinder._allcycles.Min(y => y.PathWeight))
                           .ToList();
        }

        #endregion

        #region Private

        private readonly List<AbstractGraphPath<TNode>> _allcycles;
        private readonly Stack<TNode> _oStack;
        private readonly Stack<TNode> _vStack;

        private readonly List<Tuple<TNode, TNode, int>> _vistedEdges;
        private List<Tuple<TNode, TNode, int>> _unvistedEdges;

        private static DCycleLeroyJohnson<TNode> InitialiseAndGetInstance(AbstractDiGraph<TNode> graph)
        {
            var cycleFinder = new DCycleLeroyJohnson<TNode>();
            cycleFinder.Initialise(graph);
            return cycleFinder;
        }

        private void Initialise(AbstractDiGraph<TNode> graph)
        {
            _unvistedEdges = graph.AllEdges;
        }

        private void FindSimpleCycles(AbstractDiGraph<TNode> graph)
        {
            foreach (var node in graph.AllNodes)
            {
                FindSimpleCycle(graph, node);
            }
        }

        private void FindSimpleCycle(AbstractDiGraph<TNode> graph, TNode node)
        {
            if (!_oStack.Contains(node))
            {
                _vStack.Push(node);
                var keyNode = node;
                Advance(graph, keyNode);
            }
        }


        private void Advance(AbstractDiGraph<TNode> graph, TNode node)
        {
            TNode neighbourNode;
            if (GetUnvistedEdge(graph, node, out neighbourNode))
            {
                MarkEdgeAsVisted(node, neighbourNode);
                if (!_vStack.Contains(neighbourNode))
                {
                    node = neighbourNode;

                    _vStack.Push(neighbourNode);

                    Advance(graph, node);
                }
                else if (_vStack.Contains(neighbourNode) && !_oStack.Contains(neighbourNode))
                {
                    ReportCycle(neighbourNode, graph);

                    node = _vStack.Peek();

                    Advance(graph, node);
                    ;
                }
                else
                {
                    Advance(graph, neighbourNode);
                }
            }
            else if (_vStack.Any())
            {
                ReTreat(graph);
            }
        }

        private bool GetUnvistedEdge(AbstractDiGraph<TNode> graph, TNode sourceNode, out TNode destinationNode)
        {
            destinationNode = default(TNode);
            var hasUnvistitedNodes = graph.GetNeighbourNodes(sourceNode)
                                          .Any(
                                              y =>
                                              !_vistedEdges.Any(x => x.Item1.Equals(sourceNode) && x.Item2.Equals(y)));

            if (hasUnvistitedNodes)
            {
                destinationNode = graph.GetNeighbourNodes(sourceNode)
                                       .First(
                                           y => !_vistedEdges.Any(x => x.Item1.Equals(sourceNode) && x.Item2.Equals(y)));
            }
            return hasUnvistitedNodes;
        }


        private void ReportCycle(TNode node, AbstractDiGraph<TNode> graph)
        {
            var cycle = new GraphPath<TNode>();
            _allcycles.Add(cycle);
            cycle.SourceNode = node;
            cycle.DestinationNode = node;
            cycle.Path = new List<TNode>();

            var stacklist = _vStack.ToList();

            int i = 0;
            while (!stacklist[i].Equals(node))
            {
                i++;
            }

            for (; i >= 0; i--)
            {
                cycle.Path.Add(stacklist[i]);
            }
            cycle.Path.Add(node);

            cycle.PathWeight = 0;
            for (int j = 1; j < cycle.Path.Count; j++)
            {
                cycle.PathWeight += graph.GetEdgeWeight(cycle.Path[j - 1], cycle.Path[j]);
            }
        }

        private void ReTreat(AbstractDiGraph<TNode> graph)
        {
            var node = _vStack.Pop();
            if (!_oStack.Contains(node)) _oStack.Push(node);
            MarkOriginatingEdgesAsUnVisted(node);
            if (_vStack.Any())
            {
                Advance(graph, _vStack.Peek());
            }
        }


        private void MarkEdgeAsVisted(TNode sourceNode, TNode destNode)
        {
            var edgeTuple = _unvistedEdges.Single(x => x.Item1.Equals(sourceNode) && x.Item2.Equals(destNode));
            _unvistedEdges.Remove(edgeTuple);
            _vistedEdges.Add(edgeTuple);
        }

        private void MarkOriginatingEdgesAsUnVisted(TNode sourceNode)
        {
            var edgeTuples = _vistedEdges.Where(x => x.Item1.Equals(sourceNode)).ToList();
            foreach (var edgeTuple in edgeTuples)
            {
                _unvistedEdges.Add(edgeTuple);
                _vistedEdges.Remove(edgeTuple);
            }
        }

        #endregion Private
    }
}
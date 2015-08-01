using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class AlgorithmSimplePathSearch<TNode> : IAlgorithmSimplePathSearch<TNode>
    {
        public AlgorithmSimplePathSearch()
        {
            _allpaths = new List<AbstractGraphPath<TNode>>();
            _vistedEdges = new List<Tuple<TNode, TNode, int>>();

            _vStack = new Stack<TNode>();
            _oStack = new Stack<TNode>();
            _cycleFreeGraph = new RailwayNetworkWeightedDigraph<TNode>();
        }

        #region Private

        private readonly List<AbstractGraphPath<TNode>> _allpaths;
        private readonly AbstractDiGraph<TNode> _cycleFreeGraph;
        private readonly Stack<TNode> _oStack;
        private readonly Stack<TNode> _vStack;

        private readonly List<Tuple<TNode, TNode, int>> _vistedEdges;
        private IAlgorithmDepthFirstSearch<TNode> _DFSAlgorithm;
        private ICycleOperations<TNode> _cycleFnder;
        private List<Tuple<TNode, TNode, int>> _unvistedEdges;


        public IAlgorithmDepthFirstSearch<TNode> DFSAlgorithm
        {
            get { return (_DFSAlgorithm = _DFSAlgorithm ?? new DepthFirstSearch<TNode>()); }
            set { _DFSAlgorithm = value; }
        }


        public ICycleOperations<TNode> CycleOperations
        {
            get { return (_cycleFnder = _cycleFnder ?? new CycleOperations<TNode>()); }
            set { _cycleFnder = value; }
        }


        private AlgorithmSimplePathSearch<TNode> InitialiseAndGetInstance(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            var pathFinder = new AlgorithmSimplePathSearch<TNode>();
            pathFinder.RemoveCycles(graph, startNode);
            pathFinder.Initialise();
            return pathFinder;
        }


        private void RemoveCycles(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            var dfsEdges = DFSAlgorithm.TraverseGraph(graph, startNode);
            var _backedges =
                dfsEdges.Where(
                    x => x.EdgeType == DepthFirstSearchEdgeType.BackEdge && x.DestinationNode.Equals(startNode))
                        .ToList();

            _backedges.ForEach(x => dfsEdges.Remove(x));

            dfsEdges.ForEach(x => _cycleFreeGraph.AddEdge(x.SourceNode, x.DestinationNode, x.EdgeWeight));
        }

        private void Initialise()
        {
            _unvistedEdges = _cycleFreeGraph.AllEdges;
        }


        private void FindSimplepath(TNode sourceNode, TNode dNode)
        {
            _vStack.Push(sourceNode);
            var keyNode = sourceNode;
            Advance(keyNode, dNode, keyNode);
        }


        private void Advance(TNode sourceNode, TNode dNode, TNode currentNode)
        {
            TNode neighbourNode;
            if (GetUnvistedEdge(_cycleFreeGraph, currentNode, out neighbourNode))
            {
                MarkEdgeAsVisted(currentNode, neighbourNode);
                if (! dNode.Equals(neighbourNode))
                {
                    currentNode = neighbourNode;

                    _vStack.Push(neighbourNode);

                    Advance(sourceNode, dNode, currentNode);
                }
                    //    else if (dNode.Equals(neighbourNode) && !_oStack.Contains(neighbourNode))
                else if (dNode.Equals(neighbourNode))
                {
                    Reportpath(sourceNode, dNode, _cycleFreeGraph);

                    currentNode = _vStack.Peek();

                    Advance(sourceNode, dNode, currentNode);
                    ;
                }
                else
                {
                    Advance(currentNode, dNode, neighbourNode);
                }
            }
            else if (_vStack.Any())
            {
                ReTreat(sourceNode, dNode);
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


        private void Reportpath(TNode node, TNode dnode, AbstractDiGraph<TNode> graph)
        {
            var path = new GraphPath<TNode>();
            _allpaths.Add(path);
            path.SourceNode = node;
            path.DestinationNode = dnode;
            path.Path = new List<TNode>();

            var stacklist = _vStack.ToList();

            int i = 0;
            while (!stacklist[i].Equals(node))
            {
                i++;
            }

            for (; i >= 0; i--)
            {
                path.Path.Add(stacklist[i]);
            }
            path.Path.Add(dnode);

            path.PathWeight = 0;
            for (int j = 1; j < path.Path.Count; j++)
            {
                path.PathWeight += graph.GetEdgeWeight(path.Path[j - 1], path.Path[j]);
            }
        }

        private void ReTreat(TNode sourceNode, TNode dNode)
        {
            var node = _vStack.Pop();
            if (!_oStack.Contains(node)) _oStack.Push(node);
            MarkOriginatingEdgesAsUnVisted(node);
            if (_vStack.Any())
            {
                Advance(sourceNode, dNode, _vStack.Peek());
            }
        }


        private void MarkEdgeAsVisted(TNode sourceNode, TNode destNode)
        {
            var edgeTuple = _unvistedEdges.Single(x => x.Item1.Equals(sourceNode) && x.Item2.Equals(destNode));
            _unvistedEdges.Remove(edgeTuple);
            _vistedEdges.Add(edgeTuple);
        }

        private void MarkOriginatingEdgesAsUnVisted(TNode node)
        {
            var edgeTuples = _vistedEdges.Where(x => x.Item1.Equals(node)).ToList();
            foreach (var edgeTuple in edgeTuples)
            {
                _unvistedEdges.Add(edgeTuple);
                _vistedEdges.Remove(edgeTuple);
            }
        }

        #endregion Private

        #region IAlgorithmPathFinder<TNode> Members

        public List<AbstractGraphPath<TNode>> FindAllSimplePaths(AbstractDiGraph<TNode> graph, TNode source,
                                                                 TNode destination)
        {
            var pathFinder = InitialiseAndGetInstance(graph, source);

            pathFinder.FindSimplepath(source, destination);

            return pathFinder._allpaths;
        }

        #endregion
    }
}
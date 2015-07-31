using System;
using System.Collections.Generic;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class DepthFirstSearch<TNode> : IAlgorithmDepthFirstSearch<TNode>
    {
        private readonly List<DepthFirstSearchEdge<TNode>> _dfsEdges;
        private readonly Dictionary<TNode, int> _endTime;
        private readonly Dictionary<TNode, int> _order;
        private readonly Dictionary<TNode, TNode> _parent;
        private readonly Dictionary<TNode, int> _startTime;
        private bool _isTraversed;

        private int _time;

        public DepthFirstSearch()
        {
            _dfsEdges = new List<DepthFirstSearchEdge<TNode>>();
            _parent = new Dictionary<TNode, TNode>();
            _startTime = new Dictionary<TNode, int>();
            _order = new Dictionary<TNode, int>();
            _endTime = new Dictionary<TNode, int>();
        }

        #region IAlgorithmDepthFirstSearch<TNode> Members

        public List<DepthFirstSearchEdge<TNode>> TraverseGraph(AbstractDiGraph<TNode> graph)
        {
            foreach (TNode vertex in graph.AllNodes)
            {
                if (!IsNodeinParentOrChild(vertex))
                {
                    DFSVisit(graph, vertex);
                }
            }
            _isTraversed = true;

            return _dfsEdges;
        }

        public List<DepthFirstSearchEdge<TNode>> TraverseGraph(AbstractDiGraph<TNode> graph, TNode sourceNode)
        {
            var search = new DepthFirstSearch<TNode>();
            search.DFSVisit(graph, sourceNode);
            return search._dfsEdges;
        }

        public Dictionary<TNode, int> OrderTraversalOrder
        {
            get { return _order; }
        }

        #endregion

        private void DFSVisit(AbstractDiGraph<TNode> graph, TNode node)
        {
            _startTime[node] = ++_time;

            foreach (TNode neighbour in graph.GetNeighbourNodes(node))
            {
                var dfsEdge = new DepthFirstSearchEdge<TNode> {SourceNode = node, DestinationNode = neighbour};
                dfsEdge.EdgeWeight = graph.GetEdgeWeight(dfsEdge.SourceNode, dfsEdge.DestinationNode);
                _dfsEdges.Add(dfsEdge);
                bool isNeighbourVisited = IsNodeinParentOrChild(neighbour);

                if (!isNeighbourVisited)
                {
                    _parent[neighbour] = node;
                    dfsEdge.EdgeType = DepthFirstSearchEdgeType.TreeEdge;

                    DFSVisit(graph, neighbour);
                }
                else if (! _endTime.ContainsKey(neighbour))
                {
                    dfsEdge.EdgeType = DepthFirstSearchEdgeType.BackEdge;
                }
                else if (_startTime[node] < _startTime[neighbour])
                {
                    dfsEdge.EdgeType = DepthFirstSearchEdgeType.ForwardEdge;
                }
                else
                {
                    dfsEdge.EdgeType = DepthFirstSearchEdgeType.CrossEdge;
                }
            }
            _endTime[node] = ++_time;
            _order.Add(node, _order.Count + 1);
        }


        private bool IsNodeinParentOrChild(TNode node)
        {
            return _parent.ContainsKey(node) || _parent.ContainsValue(node);
        }

        private void ValidateStateForEdgeRetrieval()
        {
            if (!_isTraversed)
                throw new InvalidOperationException(
                    string.Format("The TraverseGraph fuction must be invoked before the DFS edges can be retrieved."));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    internal class GraphCycleFinder<TNode>
    {
        public List<AbstractGraphPath<TNode>> FindCycles(List<DepthFirstSearchEdge<TNode>> depthFirstSearchEdges,
                                                         TNode sourceVertex, int maxStops)
        {
            var cycles = new List<AbstractGraphPath<TNode>>();
            foreach (
                var allDFSBackEdges in depthFirstSearchEdges.Where(x => x.EdgeType == DepthFirstSearchEdgeType.BackEdge)
                )
            {
                var sourceNode = allDFSBackEdges.DestinationNode;

                foreach (
                    var backEdge in
                        depthFirstSearchEdges.Where(
                            x => x.EdgeType == DepthFirstSearchEdgeType.BackEdge && x.DestinationNode.Equals(sourceNode))
                    )
                {
                    var cycle = new GraphPath<TNode>() {SourceNode = sourceNode, DestinationNode = sourceNode};
                    cycle.Path = new List<TNode>() {sourceNode};
                    cycle.PathWeight = 0;
                    var treeEdges = depthFirstSearchEdges.Where(x => x.EdgeType == DepthFirstSearchEdgeType.TreeEdge);
                    var treeEdge = treeEdges.Single(x => x.SourceNode.Equals(sourceNode));
                    int nodeCounter = 2;
                    while (!treeEdge.DestinationNode.Equals(backEdge.SourceNode) &&
                           !IsMaxNodeLimitReached(maxStops, nodeCounter))
                    {
                        cycle.PathWeight += treeEdge.EdgeWeight;
                        cycle.Path.Add(treeEdge.DestinationNode)
                            ;
                        treeEdge = treeEdges.Single(x => x.SourceNode.Equals(treeEdge.DestinationNode));
                    }
                    cycle.Path.Add(backEdge.SourceNode);
                    cycle.Path.Add(backEdge.DestinationNode);
                    cycle.PathWeight += backEdge.EdgeWeight;
                    if (!IsMaxNodeLimitReached(maxStops, nodeCounter))
                    {
                        cycles.Add(cycle);
                    }
                }
            }

            return cycles;
        }

        private bool IsMaxNodeLimitReached(int maxNodes, int countsofar)
        {
            if (maxNodes == 0) return false;

            return countsofar > maxNodes;
        }


        //private  List<List<TNode>> GetSimpleCycles(TNode sourceNode)
        //{
        //    List<List<TNode>> simpleCycles = new List<List<TNode>>();
        //    foreach (var backEdge in _backEdges.Where(x=>x.Item2.Equals(sourceNode)))
        //    {
        //        var simpleCycle = new List<TNode>() {sourceNode};
        //        simpleCycles.Add(simpleCycle);
        //        var treeEdge = _treeEdges.Single(x=>x.Item1.Equals( sourceNode ));
        //        while (!treeEdge.Item2 .Equals(backEdge.Item1))
        //        {
        //             simpleCycle.Add(treeEdge.Item2    )
        //            ;
        //            treeEdge = _treeEdges.Single(x => x.Item1.Equals(treeEdge.Item2));
        //        }
        //        simpleCycle.Add(backEdge.Item1);
        //        simpleCycle.Add(backEdge.Item2);
        //    }

        //    return simpleCycles;
        //}
    }
}
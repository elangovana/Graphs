using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IAlgorithmDepthFirstSearch<TNode>
    {
        Dictionary<TNode, int> OrderTraversalOrder { get; }

        List<DepthFirstSearchEdge<TNode>> TraverseGraph(AbstractDiGraph<TNode> graph);

        List<DepthFirstSearchEdge<TNode>> TraverseGraph(AbstractDiGraph<TNode> graph, TNode sourceNode);
    }
}
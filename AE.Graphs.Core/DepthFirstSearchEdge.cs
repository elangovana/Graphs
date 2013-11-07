namespace AE.Graphs.Core
{
    public class DepthFirstSearchEdge<TNode>
    {
        public TNode SourceNode { get; set; }

        public TNode DestinationNode { get; set; }

        public DepthFirstSearchEdgeType EdgeType { get; set; }

        public int EdgeWeight { get; set; }
    }
}
namespace AE.Graphs.Core
{
    /// <summary>
    ///     Represents an edge as part of the depth first search.
    /// </summary>
    /// <para>
    ///     This is similar to regular edge (source, destination & edge weight) but also includes the type of edge specific to
    ///     DFS
    ///     If vertex v is visited for the first time as we traverse the edge (u; v), then the edge is a tree edge.
    ///     2. Else, v has already been visited:
    ///     (a) If v is an ancestor of u, then edge (u; v) is a back edge.
    ///     (b) Else, if v is a descendant of u, then edge (u; v) is a forward edge.
    ///     (c) Else, if v is neither an ancestor or descendant of u, then edge (u; v) is a cross edge.
    /// </para>
    /// <typeparam name="TNode"></typeparam>
    /// <seealso cref="http://courses.csail.mit.edu/6.006/spring11/rec/rec13.pdf" />
    public class DepthFirstSearchEdge<TNode>
    {
        public TNode SourceNode { get; set; }
        public TNode DestinationNode { get; set; }
        public DepthFirstSearchEdgeType EdgeType { get; set; }
        public int EdgeWeight { get; set; }
    }
}
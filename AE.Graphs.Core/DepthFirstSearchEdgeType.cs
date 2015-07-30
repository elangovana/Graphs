namespace AE.Graphs.Core
{
    /// <summary>
    /// Represents the type of edge for depth first search traversal.
    /// </summary>
    /// <seealso cref="http://courses.csail.mit.edu/6.006/spring11/rec/rec13.pdf" />
    public enum DepthFirstSearchEdgeType
    {
        ForwardEdge,
        BackEdge,
        CrossEdge,
        TreeEdge
    }
}
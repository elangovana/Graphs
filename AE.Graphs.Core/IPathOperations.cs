using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IPathOperations<TNode>
    {
        int CountAllPaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination,
                                                    int numberOfStops);
    }
}
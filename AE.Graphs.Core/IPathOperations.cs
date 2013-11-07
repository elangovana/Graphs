using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IPathOperations<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllPaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination,
                                                    int numberOfStops);
    }
}
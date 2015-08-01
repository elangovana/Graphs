using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IPathOperations<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimplePaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination);

        int CountAllPaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination,
                        int numberOfStops);
    }
}
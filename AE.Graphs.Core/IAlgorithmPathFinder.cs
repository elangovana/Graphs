using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IAlgorithmPathFinder<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimplePaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination);
    }
}
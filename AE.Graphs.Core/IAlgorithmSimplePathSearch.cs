using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IAlgorithmSimplePathSearch<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimplePaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination);
    }
}
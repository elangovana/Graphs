using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface
        ICycleOperations<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxWeight);
    }
}
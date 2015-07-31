using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface ICycleOperations<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph);

        List<AbstractGraphPath<TNode>> FindShortestCycle(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxStops);

        List<AbstractGraphPath<TNode>> FindAllCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxWeight);
    }
}
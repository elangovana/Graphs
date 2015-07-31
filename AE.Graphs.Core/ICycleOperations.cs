using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface ICycleOperations<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph);

        List<AbstractGraphPath<TNode>> FindShortestCycle(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxStops);

        /// <summary>
        /// This counts all cycles, not just simple cycles restricted only by the max path weight
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startNode"></param>
        /// <param name="maxWeight"></param>
        /// <returns></returns>
        int CountAllCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxWeight);
    }
}
namespace AE.Graphs.Core
{
    public interface IAlgorithmShortestPath<TNode>
    {
        /// <summary>
        ///     Finds one shortest Path. In the case of more than one shortest path, returns any one path.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="sourceNode"></param>
        /// <param name="destinationNode"></param>
        /// <returns>the shortest path, or null if there is no path from source to destination</returns>
        AbstractGraphPath<TNode> GetShortestPath(AbstractDiGraph<TNode> graph, TNode sourceNode,
                                                 TNode destinationNode);
    }
}
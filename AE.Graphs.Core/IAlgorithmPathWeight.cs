using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IAlgorithmPathWeight<TNode>
    {
        /// <summary>
        ///     Finds the weight of the path represented by the list of nodes by adding the weight of the edges.
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="nodes">The list of nodes representing a valid path in a graph.</param>
        /// <returns>Returns the weight of the edges connecting the nodes</returns>
        /// <exception cref="System.ArgumentNullException">Throws ArgumentNullException if the nodes list or the graph is null. </exception>
        /// <exception cref="System.ArgumentException">Throws ArgumentException if the list of nodes does not constitute a valid path.</exception>
        int FindPathWeight(AbstractDiGraph<TNode> graph, List<TNode> nodes);
    }
}
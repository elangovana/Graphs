using System.Collections.Generic;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class AlgorithmPathWeight<TNode> : IAlgorithmPathWeight<TNode>
    {
        #region IAlgorithmPathWeight<TNode> Members

        public int FindPathWeight(AbstractDiGraph<TNode> graph, List<TNode> nodes)
        {
            int totalWeight = 0;

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                totalWeight += graph.GetEdgeWeight(nodes[i], nodes[i + 1]);
            }

            return totalWeight;
        }

        #endregion
    }
}
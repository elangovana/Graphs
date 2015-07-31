using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE.Graphs.Core
{
    /// <summary>
    /// Algorithm to find all elementary circuits
    /// </summary>
    public interface IAlgorithmElementaryCircuitSearch<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllElementaryCircuits(AbstractDiGraph<TNode> graph);

        List<AbstractGraphPath<TNode>> FindAllElementaryCircuits(AbstractDiGraph<TNode> graph, TNode startNode);
    }
}

using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    /// <summary>
    ///     An Implementation Of LEROY JOHNSON DCycle to find all Simple Cycles
    /// </summary>
    /// <typeparam name="TNode">Graph Node</typeparam>
    /// <see cref="http://www.cs.tufts.edu/comp/150GA/homeworks/hw1/Johnson%2075.PDF" />
    public class AlgorithmCycleFinder<TNode> : IAlgorithmCycleFinder<TNode>
    {
        private IAlgorithmElementaryCircuitSearch<TNode> _algorithmElementaryCircuitSearch;

        public IAlgorithmElementaryCircuitSearch<TNode> AlgorithmElementaryCircuitSearch
        {
            get
            {
                return _algorithmElementaryCircuitSearch ??
                       (_algorithmElementaryCircuitSearch = new JohnsonCycle<TNode>());
            }
            set { _algorithmElementaryCircuitSearch = value; }
        }


        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph)
        {
            return AlgorithmElementaryCircuitSearch.FindAllElementaryCircuits(graph);
        }

        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode,
                                                                  int maxStops)
        {
            IEnumerable<AbstractGraphPath<TNode>> cyclesWithStartNode = AlgorithmElementaryCircuitSearch
                .FindAllElementaryCircuits(graph, startNode);


            return cyclesWithStartNode.Where(x => x.Path.Count <= maxStops + 1).ToList();
        }


        public List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            IEnumerable<AbstractGraphPath<TNode>> cyclesWithStartNode = AlgorithmElementaryCircuitSearch
                .FindAllElementaryCircuits(graph, startNode);

            return cyclesWithStartNode.ToList();
        }

        public List<AbstractGraphPath<TNode>> FindShortestCycle(AbstractDiGraph<TNode> graph, TNode startNode)
        {
            var cyclesWithStartNode = AlgorithmElementaryCircuitSearch
                 .FindAllElementaryCircuits(graph, startNode);

            return
                cyclesWithStartNode.Where(x => x.PathWeight == cyclesWithStartNode.Min(y => y.PathWeight))
                                   .ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class CycleOperations<TNode> : ICycleOperations<TNode>
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
            List<AbstractGraphPath<TNode>> cyclesWithStartNode = AlgorithmElementaryCircuitSearch
                .FindAllElementaryCircuits(graph, startNode);

            return
                cyclesWithStartNode.Where(x => x.PathWeight == cyclesWithStartNode.Min(y => y.PathWeight))
                                   .ToList();
        }

        public int CountAllCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxWeight)
        {
            List<AbstractGraphPath<TNode>> simpleCycles = FindAllSimpleCycles(graph, startNode);


            // Permutation problem to find keep adding combinations of  cycles until max weight.
            // 1. permutation & combination by how many ways the cycles can be selected and then ordered
            int result = 0;

            //how many to select
            for (int i = 1; i <= simpleCycles.Count; i++)
            {               
                //which cycles to add starting point
                for (int j = 0; j < simpleCycles.Count; j++)
                {
                    var currentSetWeight = 0; 
                    //add cycle at starting point until count is reached
                    for (int k = j; k < j + i; k++)
                    {
                        currentSetWeight += simpleCycles[k % simpleCycles.Count].PathWeight;
                    }

                    result += Factorial(i) * (maxWeight / currentSetWeight);
                }
               
            }

            return result;
        }

        private int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result = result*i;
            }

            return result;
        }

       
    }
}
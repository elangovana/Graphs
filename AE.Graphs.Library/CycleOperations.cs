using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class CycleOperations<TNode> : ICycleOperations<TNode>
    {
        private IAlgorithmCycleFinder<TNode> _cycleFnder;

        public IAlgorithmCycleFinder<TNode> CycleFinder
        {
            get { return (_cycleFnder = _cycleFnder ?? new AlgorithmCycleFinder<TNode>()); }
            set { _cycleFnder = value; }
        }

        #region ICycleOperations<TNode> Members

        public List<AbstractGraphPath<TNode>> FindAllCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxWeight)
        {
            var simpleCycles = CycleFinder.FindAllSimpleCycles(graph, startNode);

            var result = new List<AbstractGraphPath<TNode>>();

            ComputePath(graph, simpleCycles, maxWeight, result, CycleFinder);

            return result;
        }

        #endregion

        private static void ComputePath(AbstractDiGraph<TNode> graph, List<AbstractGraphPath<TNode>> simpleCycles,
                                        int maxWeight, List<AbstractGraphPath<TNode>> result,
                                        IAlgorithmCycleFinder<TNode> cycleFinder)
        {
            var allsimpleMainCycles = simpleCycles.Where(x => x.PathWeight <= maxWeight).ToList();

            var allsimpleCycles =
                cycleFinder.FindAllSimpleCycles(graph).Where(x => x.PathWeight <= maxWeight).ToList();

            foreach (var simpleMainCycle in allsimpleMainCycles.Where(x => x.PathWeight <= maxWeight))
            {
                foreach (var simpleCycle in allsimpleCycles.Where(x => simpleMainCycle.Path.Contains(x.SourceNode)))
                {
                    AddCycles(maxWeight, simpleMainCycle, simpleCycle, result);
                }
            }
            foreach (
                var simplePath in allsimpleMainCycles.Where(x => x.PathWeight <= maxWeight))
            {
                CycleHelper<TNode>.AddGrapthPath(simplePath, result);
            }
        }

        public static void AddCycles(int maxWeight, AbstractGraphPath<TNode> mainCycle,
                                     AbstractGraphPath<TNode> simpleCycle,
                                     List<AbstractGraphPath<TNode>> result)
        {
            bool hasExceededMax = false;
            for (int i = 1; !hasExceededMax; i++)
            {
                if (mainCycle.PathWeight + simpleCycle.PathWeight*i <= maxWeight)
                {
                    var pathWithCycles = new List<TNode>(mainCycle.Path);
                    int totalIncreasedPathWeight = simpleCycle.PathWeight*i;
                    int occuranceIndex = 0;
                    foreach (var occurance in mainCycle.Path.Where(x => x.Equals(simpleCycle.SourceNode)))
                    {
                        occuranceIndex = mainCycle.Path.IndexOf(simpleCycle.SourceNode, occuranceIndex);

                        for (int j = 1; j <= i; j++)
                        {
                            pathWithCycles.InsertRange(occuranceIndex, simpleCycle.Path);

                            pathWithCycles.RemoveAt(occuranceIndex + simpleCycle.Path.Count - 1);
                        }
                        CycleHelper<TNode>.AddGrapthPath(mainCycle, result, totalIncreasedPathWeight, pathWithCycles);
                        occuranceIndex += 1;
                    }
                }
                else hasExceededMax = true;
            }
        }
    }
}
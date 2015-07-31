using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class PathOperations<TNode> : IPathOperations<TNode>
    {
        #region Private

        private IAlgorithmCycleFinder<TNode> _cycleFnder;
        private IAlgorithmPathFinder<TNode> _pathFinder;

        #endregion

        public IAlgorithmCycleFinder<TNode> CycleFinder
        {
            get { return (_cycleFnder = _cycleFnder ?? new AlgorithmCycleFinder<TNode>()); }
            set { _cycleFnder = value; }
        }


        public IAlgorithmPathFinder<TNode> PathFinder
        {
            get { return (_pathFinder = _pathFinder ?? new PathFinder<TNode>()); }
            set { _pathFinder = value; }
        }

        #region IPathOperations<TNode> Members

        public List<AbstractGraphPath<TNode>> FindAllPaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination,
                                                           int numberOfStops)
        {
            var result = new List<AbstractGraphPath<TNode>>();
            var simplePaths = PathFinder.FindAllSimplePaths(graph, source, destination);
            ComputePath(graph, numberOfStops, simplePaths, result, CycleFinder);
            return result;
        }

        #endregion

        private static void ComputePath(AbstractDiGraph<TNode> graph, int numberOfStops,
                                        List<AbstractGraphPath<TNode>> simplePaths,
                                        List<AbstractGraphPath<TNode>> result, IAlgorithmCycleFinder<TNode> cycleFinder)
        {
            var allsimplePaths = simplePaths.Where(x => CycleHelper<TNode>.GetPathCount(x) <= numberOfStops).ToList();

            var allsimpleCycles =
                cycleFinder.FindAllSimpleCycles(graph)
                           .Where(x => CycleHelper<TNode>.GetPathCount(x) < numberOfStops)
                           .ToList();

            foreach (var simplePath in allsimplePaths.Where(x => CycleHelper<TNode>.GetPathCount(x) < numberOfStops))
            {
                foreach (var simpleCycle in allsimpleCycles.Where(x => simplePath.Path.Contains(x.SourceNode)))
                {
                    CycleHelper<TNode>.AddCycles(numberOfStops, simplePath, simpleCycle, result);
                }
            }
            foreach (var simplePath in allsimplePaths.Where(x => CycleHelper<TNode>.GetPathCount(x) == numberOfStops))
            {
                CycleHelper<TNode>.AddGrapthPath(simplePath, result);
            }
        }
    }
}
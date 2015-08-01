﻿using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class PathOperations<TNode> : IPathOperations<TNode>
    {
      

        private ICycleOperations<TNode> _cycleFnder;
        private IAlgorithmSimplePathSearch<TNode> _pathFinder;

       

        public ICycleOperations<TNode> CycleOperations
        {
            get { return (_cycleFnder = _cycleFnder ?? new CycleOperations<TNode>()); }
            set { _cycleFnder = value; }
        }


        public IAlgorithmSimplePathSearch<TNode> PathFinder
        {
            get { return (_pathFinder = _pathFinder ?? new AlgorithmSimplePathSearch<TNode>()); }
            set { _pathFinder = value; }
        }


        public List<AbstractGraphPath<TNode>> FindAllSimplePaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination)
        {
            return _pathFinder.FindAllSimplePaths(graph, source, destination);
        }

        public int CountAllPaths(AbstractDiGraph<TNode> graph, TNode source, TNode destination,
                                                           int numberOfStops)
        {
            var result = 0;
            var simplePaths = PathFinder.FindAllSimplePaths(graph, source, destination);

            result = simplePaths.Count(x=>x.Path.Count -1 == numberOfStops);
           
            //Find cycles that could intersect with paths with smaller stops and make  the path longer
            var potentialPaths = simplePaths.Where(x => x.Path.Count < numberOfStops);
            var cycles = CycleOperations.FindAllSimpleCycles(graph);

            foreach (var potentialPath in potentialPaths)
            {
                var cyclesThatIntersection = cycles.Where(c => c.Path.Any(p => potentialPath.Path.Contains(p)) );

                result += cyclesThatIntersection.Count(c => c.Path.Count - 1 + potentialPath.Path.Count - 1 == numberOfStops);
            }

            return result;
        }

       

      
    }
}
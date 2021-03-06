﻿using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core;
using AE.Graphs.Core.Exceptions;
using AE.Graphs.Library;

namespace AE.Graphs.Application
{
    internal class Calculate
    {
        private const string Separator = ", ";
        private const int EdgeLength = 3;


        private readonly AbstractDiGraph<Char> _graph;
        private ICycleOperations<char> _cycleCountCalculator;     
        private IPathOperations<char> _pathOperations;
        private IAlgorithmShortestPath<char> _shortestPathCalculator;

        /// <summary>
        ///     Creates a Graph Operations Calculator. Throws format exception if the format is incorrect.
        /// </summary>
        /// <param name="graph"></param>
        public Calculate(string graph) : this(new RailwayNetworkWeightedDigraph<char>())
        {
            ConstructGraph(graph);
        }


        public Calculate(AbstractDiGraph<char> graph)
        {
            _graph = graph;
        }


        public ICycleOperations<char> CycleCalculator
        {
            get { return _cycleCountCalculator ?? (_cycleCountCalculator = new CycleOperations<char>()); }
            set { _cycleCountCalculator = value; }
        }


        public IAlgorithmShortestPath<char> ShortestPathCalulator
        {
            get { return _shortestPathCalculator ?? (_shortestPathCalculator = new DijkstrasShortestPathAlgorithm<char>()); }
            set { _shortestPathCalculator = value; }
        }

     


        public IPathOperations<char> PathOperationsCalculator
        {
            get { return _pathOperations ?? (_pathOperations = new PathOperations<char>()); }
            set { _pathOperations = value; }
        }

        public List<string> BuiltInRun()
        {
            var results = new List<string>();

            ValidateGraphNodes(new List<char> {'A', 'B', 'C', 'D', 'E'});

            results.Add(CalculatePathWeight(new List<char> {'A', 'B', 'C'}));


            results.Add(CalculatePathWeight(new List<char> {'A', 'D'}));


            results.Add(CalculatePathWeight(new List<char> {'A', 'D', 'C'}));


            results.Add(CalculatePathWeight(new List<char> {'A', 'E', 'B', 'C', 'D'}));


            results.Add(CalculatePathWeight(new List<char> {'A', 'E', 'D'}));


            results.Add(CalculateCycleCount('C', 3));


            results.Add(CalculatePathCount('A', 'C', 4));


            results.Add(CalculateShortestPath('A', 'C'));


            results.Add(CalculateShortestCycle('C'));


            results.Add(CalculateCyclesWithMaxWeight('C', 29));

            return results;
        }


        private void ValidateGraphNodes(List<char> nodes)
        {
            if (nodes.Any(x => !_graph.AllNodes.Contains(x)))
            {
                string validNodesList = string.Join(", ", nodes);
                throw new ArgumentException(
                    string.Format("The graph does not contain all the nodes {0} to run the builtin operations",
                                  validNodesList));
            }
        }

        private void ConstructGraph(string graph)
        {
            IEnumerable<string> edgeList = Validate(graph);

            foreach (string edgeString in edgeList)
            {
                _graph.AddEdge(edgeString[0], edgeString[1], Convert.ToInt32(edgeString[2].ToString()));
            }
        }

        private static IEnumerable<string> Validate(string graph)
        {
            string[] edgeList = graph.Split(new[] {Separator}, StringSplitOptions.None);
            int tmpweight;
            if (edgeList.Any(x => x.Length < EdgeLength))
                throw new FormatException(
                    string.Format("The graph {0} is invalid. \nSee sample format sample: AB1, BC2 ", graph));
            if (edgeList.Any(x => !int.TryParse(x.Substring(2), out tmpweight)))
                throw new FormatException(
                    string.Format(
                        "The graph {0} is invalid. Edge weights must be integers. \nSee sample format: AB1, BC2 ",
                        graph));

            return edgeList;
        }

        private string CalculatePathWeight(List<char> nodes)
        {
            string result;
            try
            {
                result = _graph.FindPathWeight(nodes).ToString();
            }
            catch (EdgeNotFoundException)
            {
                result = "NO SUCH ROUTE";
            }

            return result;
        }

        private string CalculateCycleCount(char node, int maxStops)
        {
            string result = CycleCalculator.FindAllSimpleCycles(_graph, node, maxStops).Count.ToString();

            return result;
        }

        private string CalculatePathCount(char snode, char dnode, int numberOfStops)
        {
            string result = PathOperationsCalculator.CountAllPaths(_graph, snode, dnode, numberOfStops).ToString();

            return result;
        }

        private string CalculateShortestPath(char sourceNode, char destinationNode)
        {
            AbstractGraphPath<char> shortestPath = ShortestPathCalulator.GetShortestPath(_graph, sourceNode,
                                                                                         destinationNode);
            string result = shortestPath == null ? "NO SUCH ROUTE" : shortestPath.PathWeight.ToString();


            return result;
        }

        private string CalculateShortestCycle(char sourceNode)
        {
            string result = "NO CYCLE";
            List<AbstractGraphPath<char>> shortestCycles = CycleCalculator.FindShortestCycle(_graph, sourceNode);

            if (shortestCycles.Any())
            {
                result = shortestCycles.First().PathWeight.ToString();
            }

            return result;
        }

        private string CalculateCyclesWithMaxWeight(char node, int maxWeight)
        {
            int count = CycleCalculator.CountAllCycles(_graph, node, maxWeight);

            return count.ToString();
        }
    }
}
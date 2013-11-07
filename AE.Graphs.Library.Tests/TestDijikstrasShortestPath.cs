using System;
using System.Collections.Generic;
using AE.Graphs.Core;
using AE.Graphs.Library.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AE.Graphs.Library.Tests
{
    [TestClass]
    public class TestDijikstrasShortestPath
    {
        private const string Gseparator = ", ";
        private const string GraphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
        private RailwayNetworkWeightedDigraph<char> _graph;


        [TestInitialize]
        public void InitTest()
        {
            _graph = new RailwayNetworkWeightedDigraph<char>();


            var edgeStrings = GraphString.Split(new[] {Gseparator}, StringSplitOptions.None);
            foreach (var edgeString in edgeStrings)
            {
                _graph.AddEdge(edgeString[0], edgeString[1], Convert.ToInt32(edgeString[2].ToString()));
            }
        }

        [TestMethod]
        public void TestShortestPathWeightCase1()
        {
            var result = new DijkstrasShortestPathAlgorithm<char>().GetShortestPath(_graph, 'A', 'C').PathWeight;

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void TestShortestPathWeightNoPathCase2()
        {
            _graph.AddNode('X');
            var result = new DijkstrasShortestPathAlgorithm<char>().GetShortestPath(_graph, 'A', 'X');

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidEdgeWeightException))]
        public void TestShortestPathNegativeWeight()
        {
            var _graphStub = MockRepository.GenerateStub<AbstractDiGraph<char>>();

            _graphStub.Stub(x => x.AllEdges)
                      .Return(new List<Tuple<char, char, int>>() {new Tuple<char, char, int>('A', 'X', -1)});

            new DijkstrasShortestPathAlgorithm<char>().GetShortestPath(_graphStub, 'A', 'X');
        }
    }
}
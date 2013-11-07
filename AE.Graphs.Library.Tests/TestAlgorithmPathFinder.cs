using System;
using System.Collections.Generic;
using System.Linq;
using AE.Graphs.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AE.Graphs.Library.Tests
{
    [TestClass]
    public class TestAlgorithmPathFinder
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
        public void TestPathWeightCase1()
        {
            const string nodesString = "A-B-C";

            var result = new AlgorithmPathWeight<char>().FindPathWeight(_graph, GetNodes(nodesString));

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void TestPathWeightCase2()
        {
            const string nodesString = "A-D";

            var result = new AlgorithmPathWeight<char>().FindPathWeight(_graph, GetNodes(nodesString));

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void TestPathWeightCase3()
        {
            const string nodesString = "A-D-C";

            var result = new AlgorithmPathWeight<char>().FindPathWeight(_graph, GetNodes(nodesString));

            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void TestPathWeightCase4()
        {
            const string nodesString = "A-E-B-C-D";

            var result = new AlgorithmPathWeight<char>().FindPathWeight(_graph, GetNodes(nodesString));

            Assert.AreEqual(22, result);
        }


        [TestMethod]
        [ExpectedException(typeof (EdgeNotFoundException))]
        public void TestPathWeightCase5()
        {
            const string nodesString = "A-E-D";

            var result = new AlgorithmPathWeight<char>().FindPathWeight(_graph, GetNodes(nodesString));
        }

        private static List<char> GetNodes(string nodesString)
        {
            const string nseparator = "-";
            return nodesString.Split(new[] {nseparator}, StringSplitOptions.None).Select(x => x[0]).ToList();
        }
    }
}
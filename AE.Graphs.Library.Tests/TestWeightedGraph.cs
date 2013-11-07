using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AE.Graphs.Library.Tests
{
    [TestClass]
    public class TestWeightedGraph
    {
        private const string Gseparator = ", ";
        private const string GraphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

        private string[] _edgeStrings;
        private RailwayNetworkWeightedDigraph<char> _graph;

        [TestInitialize]
        public void TestInit()
        {
            _graph = new RailwayNetworkWeightedDigraph<char>();
            _edgeStrings = GraphString.Split(new[] {Gseparator}, StringSplitOptions.None);
        }


        [TestMethod]
        public void TestEdgesMatch()
        {
            foreach (var edgeString in _edgeStrings)
            {
                _graph.AddEdge(edgeString[0], edgeString[1], Convert.ToInt32(edgeString[2].ToString()));
            }
            var edgeTuples = _graph.AllEdges;

            var result =
                edgeTuples.Select(x => string.Format("{0}{1}{2}", x.Item1, x.Item2, x.Item3));

            var expectedSorted = _edgeStrings.OrderBy(x => x);
            var actualSorted = result.OrderBy(x => x);


            Assert.IsTrue(expectedSorted.SequenceEqual(actualSorted), "The edges not not match");
        }
    }
}
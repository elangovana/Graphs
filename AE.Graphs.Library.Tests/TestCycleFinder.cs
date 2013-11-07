using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestClass]
    public class TestCycleFinder
    {
        [TestMethod]
        public void TestCyclesCountCase2()
        {
            const string graphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graph = GetGraph(graphString);
            var actual = new DCycleLeroyJohnson<Char>().FindAllSimpleCycles(graph, 'C', 3);

            Assert.AreEqual(2, actual.Count);
            actual = actual.OrderBy(x => x.Path.Count).ToList();
            Assert.IsTrue(actual[0].Path.SequenceEqual(new List<char>() {'C', 'D', 'C'}));
            Assert.IsTrue(actual[1].Path.SequenceEqual(new List<char>() {'C', 'E', 'B', 'C'}));
        }

        [TestMethod]
        public void TestCyclesCountCase3()
        {
            const string graphString = "121, 232, 241, 431, 351, 511";
            var graph = GetGraph(graphString);

            var actual = new DCycleLeroyJohnson<Char>().FindAllSimpleCycles(graph);
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual[0].Path.SequenceEqual(new List<char>() {'1', '2', '3', '5', '1'}));
            Assert.IsTrue(actual[1].Path.SequenceEqual(new List<char>() {'1', '2', '4', '3', '5', '1'}));
        }

        [TestMethod]
        public void TestCyclesCountCase1()
        {
            const string graphString = "AB1, BC1, BE1, BH1, CD1, HI1, DF1, DE1, GF1, EG1, FA1";
            var graph = GetGraph(graphString);
            var actual = new DCycleLeroyJohnson<Char>().FindAllSimpleCycles(graph);
            Assert.AreEqual(3, actual.Count);
        }

        private RailwayNetworkWeightedDigraph<char> GetGraph(string graphString)
        {
            var graph = new RailwayNetworkWeightedDigraph<char>();
            const string Gseparator = ", ";

            var edgeStrings = graphString.Split(new[] {Gseparator}, StringSplitOptions.None);
            foreach (var edgeString in edgeStrings)
            {
                graph.AddEdge(edgeString[0], edgeString[1], Convert.ToInt32(edgeString[2].ToString()));
            }
            return graph;
        }
    }
}
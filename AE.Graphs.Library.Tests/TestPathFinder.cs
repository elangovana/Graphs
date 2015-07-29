using System;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestFixture]
    public class TestPathinder
    {
        [Test]
        public void TestPathCountCase1()
        {
            const string graphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graph = GetGraph(graphString);
            var actual = new PathFinder<Char>().FindAllSimplePaths(graph, 'A', 'C');

            Assert.AreEqual(4, actual.Count);
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
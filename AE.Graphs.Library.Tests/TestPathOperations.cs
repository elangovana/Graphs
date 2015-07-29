using System;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestPathOperations
    {
        [Test]
        public void TestPathCountCase2()
        {
            const string graphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graph = GetGraph(graphString);
            var actual = new PathOperations<Char>().FindAllPaths(graph, 'A', 'C', 4);

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
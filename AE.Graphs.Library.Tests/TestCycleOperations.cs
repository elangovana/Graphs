using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestClass]
    public class TestCycleOperations
    {
        [TestMethod]
        public void TestCyclesOperatationCase1()
        {
            const string graphString = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            var graph = GetGraph(graphString);
            var actual = new CycleOperations<char>().FindAllCycles(graph, 'C', 29).OrderBy(x => x.Path.Count).ToList();
            foreach (var item in actual)
            {
                var output = string.Join("", item.Path);
                System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}", output, item.PathWeight));
            }
            Assert.AreEqual(7, actual.Count);
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
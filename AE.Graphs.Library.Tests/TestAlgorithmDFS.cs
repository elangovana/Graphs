using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AE.Graphs.Core;
using NUnit.Framework;


namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestAlgorithmDFS
    {
        private RailwayNetworkWeightedDigraph<char> _graph;


        [SetUp]
        public void InitTest()
        {
            _graph = new RailwayNetworkWeightedDigraph<char>();
            _graph.AddEdge('1', '2', 1);

            _graph.AddEdge('2', '3', 1);
            _graph.AddEdge('2', '6', 1);

            _graph.AddEdge('3', '4', 1);
            _graph.AddEdge('3', '6', 1);

            _graph.AddEdge('4', '5', 1);
            _graph.AddEdge('4', '6', 1);

            _graph.AddEdge('5', '6', 1);

            _graph.AddEdge('6', '2', 1);
            _graph.AddEdge('6', '3', 1);
            _graph.AddEdge('6', '4', 1);
        }

        [Test]
        public void ShouldTraverseBasicGraph()
        {
            var target = new DepthFirstSearch<char>();

            var result = target.TraverseGraph(_graph);

            IEnumerable<Tuple<char, char>> expected = new BindingList<Tuple<char, char>>()
                {
                    new Tuple<char, char>('1', '2')
                    ,
                    new Tuple<char, char>('2', '3')
                    ,
                    new Tuple<char, char>('3', '4')
                    ,
                    new Tuple<char, char>('4', '5')
                    ,
                    new Tuple<char, char>('5', '6')
                };

            var actual =
                result.Where(x => x.EdgeType == DepthFirstSearchEdgeType.TreeEdge)
                      .Select(x => new Tuple<char, char>(x.SourceNode, x.DestinationNode))
                      .OrderBy(x => x.Item1)
                      .ThenBy(x => x.Item2);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }


        [Test]
        public void ShouldTraverseGraphWithBackEdges()
        {
            var target = new DepthFirstSearch<char>();

            var result = target.TraverseGraph(_graph);

            IEnumerable<Tuple<char, char>> expected = new BindingList<Tuple<char, char>>()
                {
                    new Tuple<char, char>('6', '2')
                    ,
                    new Tuple<char, char>('6', '3')
                    ,
                    new Tuple<char, char>('6', '4')
                };

            var actual =
                result.Where(x => x.EdgeType == DepthFirstSearchEdgeType.BackEdge)
                      .Select(x => new Tuple<char, char>(x.SourceNode, x.DestinationNode))
                      .OrderBy(x => x.Item1)
                      .ThenBy(x => x.Item2);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        public void ShouldTraverseAllEdges()
        {
            var target = new DepthFirstSearch<char>();

            var result = target.TraverseGraph(_graph);

            var expected =
                _graph.AllEdges.Select(x => new Tuple<char, char>(x.Item1, x.Item2))
                      .OrderBy(x => x.Item1)
                      .ThenBy(x => x.Item2);
            var actual =
                result.Select(x => new Tuple<char, char>(x.SourceNode, x.DestinationNode))
                      .OrderBy(x => x.Item1)
                      .ThenBy(x => x.Item2);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
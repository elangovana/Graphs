using System.Linq;
using AE.Graphs.Core;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestDepthFirstSearchAlgorithm
    {
        [TestCase("121-231-261-341-361-451-461-561-621-631-641", "121-231-341-451-561")]
        public void ShouldReturnDfsTreeEdges(string graph, string expected)
        {
            //Arrange
            var sut = new DepthFirstSearchAlgorithm<char>();

            //Act
            var result = sut.TraverseGraph(GraphLoaderHelper.LoadGraphFromString(graph));
            var actual =
                result.Where(x => x.EdgeType == DepthFirstSearchEdgeType.TreeEdge)
                    .OrderBy(x => x.SourceNode)
                    .ThenBy(x => x.DestinationNode).ConvertDfsEdgeToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("121-231-261-341-361-451-461-561-621-631-641", "621-631-641")]
        public void ShouldReturnDfsBackEdges(string graph, string expected)
        {
            var sut = new DepthFirstSearchAlgorithm<char>();

            var result = sut.TraverseGraph(GraphLoaderHelper.LoadGraphFromString(graph));


            var actual =
                result.Where(x => x.EdgeType == DepthFirstSearchEdgeType.BackEdge)
                    .OrderBy(x => x.SourceNode)
                    .ThenBy(x => x.DestinationNode)
                    .ConvertDfsEdgeToString();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("121-231-261-341-361-451-461-561-621-631-641", "121-231-261-341-361-451-461-561-621-631-641")]
        public void ShouldTraverseAllEdges(string graph, string expected)
        {
            //Arrange
            var sut = new DepthFirstSearchAlgorithm<char>();

            //Act
            var result = sut.TraverseGraph(GraphLoaderHelper.LoadGraphFromString(graph));
            var actual =
                result.OrderBy(x => x.SourceNode)
                    .ThenBy(x => x.DestinationNode)
                    .ConvertDfsEdgeToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
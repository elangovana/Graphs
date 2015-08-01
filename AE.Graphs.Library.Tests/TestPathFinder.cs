using System;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestFixture]
    public class TestPathFinder
    {
        [TestCase(" AB5 - BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3 - AE7", 'A', 'C',4)]
        public void ShouldCountSimplePathsGivenSourceAndDestinationNode(string igraph, char isourceNode, char idestinationNode, int expectedNofPaths)
        {
           //Arrange
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);
            var sut = new AlgorithmSimplePathSearch<Char>();

            //Assert
            var actual = sut.FindAllSimplePaths(graph, isourceNode, idestinationNode);

            //Act
            Assert.AreEqual(expectedNofPaths, actual.Count);
        }


       
    }
}
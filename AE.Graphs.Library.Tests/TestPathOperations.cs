using System;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestPathOperations
    {
        [TestCase("AB5 - BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3 - AE7", 'A', 'C', 4, 3)]
        public void ShouldCountPathsWhenGivenSourceDestinationAndNumberOfStops(string igraph, char isourceNode, char idestinationNode, int inoOfStops, int expectedNoOfPaths)
        {
           //Arrange
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);
            var sut = new PathOperations<Char>();

            //Assert
            var actual =sut.FindAllPaths(graph, isourceNode, idestinationNode, inoOfStops);

            //Atc
            Assert.AreEqual(expectedNoOfPaths, actual.Count);
        }

      
    }
}
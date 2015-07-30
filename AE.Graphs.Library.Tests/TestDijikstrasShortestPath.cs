using System;
using System.Collections.Generic;
using AE.Graphs.Core;
using AE.Graphs.Library.Exceptions;

using NUnit.Framework;
using Rhino.Mocks;

namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestDijikstrasShortestPath
    {
        
        [TestCase("AB5 - BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3 - AE7" , 'A', 'C', 9)]
        public void ShouldCalculateShortestPathLength(string igraph, char isourceNode, char idestinationNode, int expectedShortedPathLength)
        {
            //Arrange
            var sut = new DijkstrasShortestPathAlgorithm<char>();
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);

            //Act
            var result = sut.GetShortestPath(graph, isourceNode, idestinationNode ).PathWeight;

            //Assert
            Assert.AreEqual(expectedShortedPathLength, result);
        }

        [TestCase("AB5 - BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3 - AE7 - XW7", 'A', 'X')]
        public void ShouldReturnNullWhenNoPathExistsGivenSourceAndDestinationNode(string igraph, char isourceNode, char idestinationNode)
        {
            //Arrange
            var sut = new DijkstrasShortestPathAlgorithm<char>();
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);

            //Act
            var result = sut.GetShortestPath(graph, isourceNode, idestinationNode);

           //Assert
            Assert.IsNull(result);
        }

        [TestCase('A', 'X', -1)]
        public void ShouldThrowInvalidWeightExceptionGivenNegativeEdgeWeight(char isourceNode, char idestinationNode, int iedgeWeight)
        {
            //Arrange
            var graphStub = MockRepository.GenerateStub<AbstractDiGraph<char>>();
            graphStub.Stub(x => x.AllEdges)
                      .Return(new List<Tuple<char, char, int>>() {new Tuple<char, char, int>(isourceNode, idestinationNode, iedgeWeight)});

            var sut = new DijkstrasShortestPathAlgorithm<char>();
            

           //Act + assert
            Assert.Throws<InvalidEdgeWeightException>(
                () => sut.GetShortestPath(graphStub, isourceNode, idestinationNode));
        }
    }
}
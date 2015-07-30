using System.Linq;
using AE.Graphs.Core;
using AE.Graphs.Core.Exceptions;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    [TestFixture]
    public class TestRailwayNetworkWeightedDigraph
    {
        [TestCase('A', 'B', 5, 1)]
        public void ShouldMatchEdgeCountWithInput(char iSourceNode, char iDestinationNode, int iEdgeWeight, int expectedNoOfEdges)
        {
            //Arrange
            var sut = new RailwayNetworkWeightedDigraph<char>();

            //Act
            sut.AddEdge(iSourceNode, iDestinationNode, iEdgeWeight);
            var actual = sut.AllEdges;

            //Assert
            Assert.AreEqual(expectedNoOfEdges, actual.Count);
        }

        [TestCase('A', 'B', 5, 2)]
        public void ShouldMatchNodeCountWithInput(char iSourceNode, char iDestinationNode, int iEdgeWeight, int expectedNoOfNodes)
        {
            //Arrange
            var sut = new RailwayNetworkWeightedDigraph<char>();

            //Act
            sut.AddEdge(iSourceNode, iDestinationNode, iEdgeWeight);
            var actual = sut.AllNodes;

            //Assert
            Assert.AreEqual(expectedNoOfNodes, actual.Count);
        }

        [TestCase('A', 'B', 5)]
        public void ShouldMatchSourceNodeNameWithInput(char iSourceNode, char iDestinationNode, int iEdgeWeight)
        {
            //Arrange
            var sut = new RailwayNetworkWeightedDigraph<char>();

            //Act
            sut.AddEdge(iSourceNode, iDestinationNode, iEdgeWeight);
            var actual = sut.AllNodes;

            //Assert
            Assert.AreEqual(iSourceNode, actual.Single(n => n == iSourceNode));
        }

        [TestCase('A', 'B', 5)]
        public void ShouldMatchDestinationNodeNameWithInput(char iSourceNode, char iDestinationNode, int iEdgeWeight)
        {
            //Arrange
            var sut = new RailwayNetworkWeightedDigraph<char>();

            //Act
            sut.AddEdge(iSourceNode, iDestinationNode, iEdgeWeight);
            var actual = sut.AllNodes;

            //Assert
            Assert.AreEqual(iDestinationNode, actual.Single(n => n == iDestinationNode));
        }


        [TestCase('A', 'B', 5)]
        public void ShouldMatchEdgeWeightWithInput(char iSourceNode, char iDestinationNode, int iEdgeWeight)
        {
            //Arrange
            var sut = new RailwayNetworkWeightedDigraph<char>();

            //Act
            sut.AddEdge(iSourceNode, iDestinationNode, iEdgeWeight);
            var actual = sut.AllEdges;

            //Assert
            Assert.AreEqual(iEdgeWeight, actual.Single().Item3);
        }

        [TestCase("AB5-BC4-CD8-DC8-DE6-AD5-CE2-EB3-AE7", "A-B-C", 9)]
        [TestCase("AB5-BC4-CD8-DC8-DE6-AD5-CE2-EB3-AE7", "A-D", 5)]
        [TestCase("AB5-BC4-CD8-DC8-DE6-AD5-CE2-EB3-AE7", "A-D-C", 13)]
        [TestCase("AB5-BC4-CD8-DC8-DE6-AD5-CE2-EB3-AE7", "A-E-B-C-D", 22)]
        public void ShouldCalculatePathWeight(string iGraph, string iPath, int expectedPathWeight)
        {
            //Arrange
            var sut = GraphLoaderHelper.LoadGraphFromString(iGraph);
            
            //Act
            var actual = sut.FindPathWeight(GraphLoaderHelper.GetNodes(iPath));

            //Assert
            Assert.AreEqual(expectedPathWeight, actual);
        }

        [TestCase("AB5-BC4-CD8-DC8-DE6-AD5-CE2-EB3-AE7", "A-E-D")]
        public void ShouldThrowExceptionWhenNoPath(string iGraph, string iPath)
        {
            //Arrange
            var sut = GraphLoaderHelper.LoadGraphFromString(iGraph);

            //Act + Assert
           Assert.Throws<EdgeNotFoundException>( () => sut.FindPathWeight(GraphLoaderHelper.GetNodes(iPath) ));
  
        }
     
    }
}
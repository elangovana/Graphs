using System;
using System.Linq;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestFixture]
    public class TestCycleOperations
    {
        [TestCase("AB5 - BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3- AE7", 'C', 29, 7)]
        public void ShouldCountAllCyclesGivenSourceNodeAndMaxPathWeight(string igraph, char isourceNode, int imaxWeight, int expectedCycleCount)
        {
            //Arrange
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);
            var sut = new CycleOperations<char>();

            //Act
            var actual = sut.FindAllCycles(graph, isourceNode, imaxWeight).OrderBy(x => x.Path.Count).ToList();
           
            //Assert
            Assert.AreEqual(expectedCycleCount, actual.Count);
        }


      
    }
}
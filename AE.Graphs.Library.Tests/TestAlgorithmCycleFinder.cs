using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    /// <summary>
    ///     Summary description for TestCycleFinder
    /// </summary>
    [TestFixture]
    public class TestAlgorithmCycleFinder
    {
      

        [TestCase("AB1 - BC1 - BE1 -BH1 - CD1 - HI1 - DF1 - DE1 - GF1 - EG1 - FA1", 3)]
        [TestCase("121 - 232 - 241 - 431 - 351 - 511", 2)]
        public void ShouldCountAllSimpleCycles(string igraph,int expectedCycleCount )
        {
           //Arrange
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);
            var sut = new AlgorithmCycleFinder<char>();

            //Act
            var actual = sut.FindAllSimpleCycles(graph);

            //Assert
            Assert.AreEqual(expectedCycleCount, actual.Count);
        }


        [TestCase("AB5- BC4 - CD8 - DC8 - DE6 - AD5 - CE2 - EB3 - AE7", 'C', 3, 2)]
        public void ShouldCountAllSimpleCyclesWhenGivenSourceNodeAndMaxStops(string igraph, char isourceNode , int imaxStops, int expectedCycleCount)
        {
            //Arrange
            var graph = GraphLoaderHelper.LoadGraphFromString(igraph);
            var sut = new AlgorithmCycleFinder<char>();

            //Act
            var actual = sut.FindAllSimpleCycles(graph, isourceNode, imaxStops);

            //Assert
            Assert.AreEqual(expectedCycleCount, actual.Count);
          
        }

       

       

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AE.Graphs.Core;
using NUnit.Framework;

namespace AE.Graphs.Library.Tests
{
    internal static class GraphLoaderHelper
    {
      public  const char EdgeSeparator = '-';

        /// <summary>
        ///     Creates a graph from string
        /// </summary>
        /// <param name="strGraph">
        ///     Expects graph in format AB5-BC6 where A , B , C are nodes. The weight of the edge between A & B is
        ///     5, B & C is 6. Each node is represented by a single character
        /// </param>
        /// <returns>AbstractDiGraph</returns>
        public static AbstractDiGraph<char> LoadGraphFromString(string strGraph)
        {
            var result = new RailwayNetworkWeightedDigraph<char>();
            strGraph = strGraph.Replace(" ", "");

            var strEdges = strGraph.Split(EdgeSeparator);
            foreach (var strEdge in strEdges)
            {
                //Being lazy and validating edge wise format as opposed to the entire graph, to avoid confusing regex
                if (!Regex.IsMatch(strEdge, "^[a-zA-Z0-9][a-zA-Z0-9][0-9]+$"))
                {
                    throw new Exception(
                        string.Format(
                            "The edge {0} in graph {1} is invalid.Please make sure each edge of the graph is in the format <SingleCharacterSourceNode><SingleCharacterDestNode><EdgeWeightInIntegers>. Use '-'  to seperate the edges. Eg AB5-EF9",
                            strEdge, strGraph));
                }

                var sourceNode = char.Parse(strEdge.Substring(0, 1));
                var destinationNode = char.Parse( strEdge.Substring(1, 1));
                var edgeWeight = int.Parse(strEdge.Substring(2));

                result.AddEdge(sourceNode, destinationNode, edgeWeight);
            }
            return result;
        }

        

        public static string ConvertDfsEdgeToString(this IEnumerable<DepthFirstSearchEdge<char>> dfsTraversedEdges)
        {
            var sb = new StringBuilder();

            foreach (var dfsEdge in dfsTraversedEdges)
            {               
                sb.Append(string.Format("{0}{1}{2}{3}",EdgeSeparator,  dfsEdge.SourceNode, dfsEdge.DestinationNode, dfsEdge.EdgeWeight));
            }

            //Remove "-" at the start
            return sb.ToString().TrimStart(EdgeSeparator);
        }


        public static List<char> GetNodes(string nodesString)
        {           
            return nodesString.Split(new[] { EdgeSeparator }, StringSplitOptions.None).Select(char.Parse).ToList();
        }
    }
}
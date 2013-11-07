using System.Collections.Generic;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public static class CycleHelper<TNode>
    {
        public static void AddCycles(int numberOfStops, AbstractGraphPath<TNode> simplePath,
                                     AbstractGraphPath<TNode> simpleCycle,
                                     List<AbstractGraphPath<TNode>> result)
        {
            bool hasExceededMax = false;
            for (int i = 1; !hasExceededMax; i++)
            {
                if (GetPathCount(simplePath) + GetPathCount(simpleCycle)*i <= numberOfStops)
                {
                    int occuranceIndex = simplePath.Path.IndexOf(simpleCycle.SourceNode);
                    var pathWithCycles = new List<TNode>(simplePath.Path);
                    int totalIncreasedPathWeight = simpleCycle.PathWeight*i;
                    for (int j = 1; j <= i; j++)
                    {
                        pathWithCycles.InsertRange(occuranceIndex, simpleCycle.Path);

                        pathWithCycles.RemoveAt(occuranceIndex + simpleCycle.Path.Count - 1);
                    }

                    AddGrapthPath(simplePath, result, totalIncreasedPathWeight, pathWithCycles);
                }
                else hasExceededMax = true;
            }
        }

        public static void AddGrapthPath(AbstractGraphPath<TNode> simplePath, List<AbstractGraphPath<TNode>> result,
                                         int totalIncreasedPathWeight,
                                         List<TNode> pathWithCycles)
        {
            var resultGraphPath = new GraphPath<TNode>();
            resultGraphPath.PathWeight = simplePath.PathWeight + totalIncreasedPathWeight;
            resultGraphPath.Path = pathWithCycles;
            resultGraphPath.SourceNode = simplePath.SourceNode;
            resultGraphPath.DestinationNode = simplePath.DestinationNode;
            result.Add(resultGraphPath);
        }

        public static void AddGrapthPath(AbstractGraphPath<TNode> simplePath, List<AbstractGraphPath<TNode>> result)
        {
            var resultGraphPath = new GraphPath<TNode>();
            resultGraphPath.PathWeight = simplePath.PathWeight;
            resultGraphPath.Path = new List<TNode>(simplePath.Path);
            resultGraphPath.SourceNode = simplePath.SourceNode;
            resultGraphPath.DestinationNode = simplePath.DestinationNode;
            result.Add(resultGraphPath);
        }

        public static int GetPathCount(AbstractGraphPath<TNode> path)
        {
            return path.Path.Count - 1;
        }
    }
}
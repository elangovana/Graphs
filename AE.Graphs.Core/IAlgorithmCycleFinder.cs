﻿using System.Collections.Generic;

namespace AE.Graphs.Core
{
    public interface IAlgorithmCycleFinder<TNode>
    {
        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph);

        List<AbstractGraphPath<TNode>> FindShortestCycle(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode);

        List<AbstractGraphPath<TNode>> FindAllSimpleCycles(AbstractDiGraph<TNode> graph, TNode startNode, int maxStops);
    }
}
using System;

namespace AE.Graphs.Core
{
    [Obsolete]
    public abstract class AbstractAdjacentNodeEdge<TNode>
    {
        public abstract int Weight { get; }
        public abstract TNode Node { get; }
    }
}
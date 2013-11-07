namespace AE.Graphs.Core
{
    public abstract class AbstractAdjacentNodeEdge<TNode>
    {
        public abstract int Weight { get; }
        public abstract TNode Node { get; }
    }
}
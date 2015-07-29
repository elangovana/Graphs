namespace AE.Graphs.Core
{
    /// <summary>
    /// This is to help represent the adjacent node in a adjaceny list
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public class AdjacentNodeEdge<TNode>
    {
        public AdjacentNodeEdge(TNode node, int weight) 
        {
            Node = node;
            Weight = weight;
        }

        public int Weight { get; private set; }

        public TNode Node { get; private set; }
    }
}
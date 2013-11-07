using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    internal class AdjacentNodeEdge<TNode> : AbstractAdjacentNodeEdge<TNode>
    {
        private TNode _node;
        private int _weight;

        public AdjacentNodeEdge(TNode node, int weight)
        {
            _node = node;
            _weight = weight;
        }

        public override int Weight
        {
            get { return _weight; }
        }

        public override TNode Node
        {
            get { return _node; }
        }
    }
}
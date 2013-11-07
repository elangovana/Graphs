using System.Collections.Generic;
using AE.Graphs.Core;

namespace AE.Graphs.Library
{
    public class GraphPath<TNode> : AbstractGraphPath<TNode>
    {
        public const int InfinityValue = -1;
        private List<TNode> _path;

        public GraphPath()
        {
            _path = new List<TNode>();
        }

        public override List<TNode> Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public override int CompareTo(AbstractGraphPath<TNode> other)
        {
            if (PathWeight == other.PathWeight) return 0;

            if (PathWeight == InfinityValue) return 1;
            if (other.PathWeight == InfinityValue) return -1;

            if (PathWeight > other.PathWeight) return 1;
            return -1;
        }
    }
}
using System;
using System.Collections.Generic;

namespace AE.Graphs.Core
{
    /// <summary>
    /// Represents a graph path. 
    /// Technical definition of a graph path: A path is a trail in which all vertices (except perhaps the first and last ones) are distinct. 
    /// </summary>
    /// <typeparam name="TNode">Represents the type of the node</typeparam>
    public abstract class AbstractGraphPath<TNode> : IComparable<AbstractGraphPath<TNode>>
    {
        public virtual TNode SourceNode { get; set; }

        public virtual TNode DestinationNode { get; set; }

        public virtual int PathWeight { get; set; }


        public virtual List<TNode> Path { get; set; }

        #region IComparable<AbstractGraphPath<TNode>> Members

        public abstract int CompareTo(AbstractGraphPath<TNode> other);

        #endregion

        public override string ToString()
        {
            return string.Format("{0} -> {1} : {2}", SourceNode, DestinationNode, PathWeight);
        }
    }
}
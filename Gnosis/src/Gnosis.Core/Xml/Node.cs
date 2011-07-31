using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public abstract class Node
        : INode
    {
        protected Node()
        {
            this.parent = null;
        }

        protected Node(INode parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            this.parent = parent;
        }

        private readonly INode parent;
        private readonly List<INode> children = new List<INode>();

        #region Protected Methods

        protected int GetDepth()
        {
            var depth = -1;

            var currentParent = Parent;
            while (currentParent != null)
            {
                depth++;
                currentParent = currentParent.Parent;
            }

            return depth;
        }

        protected string GetIndent()
        {
            const int multiplier = 2;
            var depth = GetDepth();
            return (depth > 0) ? string.Empty.PadLeft(depth * multiplier) : string.Empty;
        }

        #endregion

        #region IXmlNode Members

        public INode Parent
        {
            get { return parent; }
        }

        public IEnumerable<INode> Children
        {
            get { return children; }
        }

        public virtual IEnumerable<T> Where<T>(Func<T, bool> predicate)
            where T : class, INode
        {
            var results = new List<T>();

            var self = this as T;
            if (self != null && predicate(self))
                results.Add(self);

            foreach (var child in children)
                results.AddRange(child.Where(predicate));

            return results;
        }

        public void AddChild(INode child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child);
        }

        #endregion
    }
}

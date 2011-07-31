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
            : this(new List<INode>())
        {
        }

        protected Node(IEnumerable<INode> children)
            : this(null, children)
        {
        }

        protected Node(INode parent, IEnumerable<INode> children)
        {
            this.parent = parent;
            this.children = children;

            foreach (var child in children)
                if (child.Parent == null)
                    child.Parent = this;
        }

        private INode parent;
        private readonly IEnumerable<INode> children;

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
            set { parent = value; }
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

        #endregion
    }
}

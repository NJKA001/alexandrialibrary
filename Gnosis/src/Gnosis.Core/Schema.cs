using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Schema
        : ISchema
    {
        public Schema(Uri identifier, string name)
            : this(identifier, name, null)
        {
        }

        public Schema(Uri identifier, string name, ISchema parent)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");
            if (name == null)
                throw new ArgumentNullException("name");

            this.identifier = identifier;
            this.name = name;
            this.parent = parent;
        }

        private readonly Uri identifier;
        private readonly string name;
        private readonly ISchema parent;
        private readonly IDictionary<string, ISchema> children = new Dictionary<string, ISchema>();

        protected void AddChild(ISchema child)
        {
            if (child == null)
                throw new ArgumentNullException("child");

            children.Add(child.Name, child);
        }

        #region ISchema Members

        public Uri Identifier
        {
            get { return identifier; }
        }

        public string Name
        {
            get { return name; }
        }

        public ISchema Parent
        {
            get { return parent; }
        }

        public IEnumerable<ISchema> Children
        {
            get { return children.Values; }
        }

        public ISchema GetChild(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return children.ContainsKey(name) ?
                children[name]
                : null;
        }

        #endregion

        public static readonly ISchema Default = new Schema(new Uri("http://gn0s1s.com/ns/1/schemas/default"), "Default");
    }
}

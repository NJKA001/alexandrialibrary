using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagSchema
        : ITagSchema
    {
        public TagSchema(Uri identifier, string name)
            : this(identifier, name, null)
        {
        }

        public TagSchema(Uri identifier, string name, ITagSchema parent)
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
        private readonly ITagSchema parent;
        private readonly IDictionary<string, ITagSchema> children = new Dictionary<string, ITagSchema>();

        protected void AddChild(ITagSchema child)
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

        public ITagSchema Parent
        {
            get { return parent; }
        }

        public IEnumerable<ITagSchema> Children
        {
            get { return children.Values; }
        }

        public ITagSchema GetChild(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return children.ContainsKey(name) ?
                children[name]
                : null;
        }

        #endregion

        public static readonly ITagSchema Default = new TagSchema(new Uri("http://gn0s1s.com/ns/1/schemas/default"), "Default");
    }
}

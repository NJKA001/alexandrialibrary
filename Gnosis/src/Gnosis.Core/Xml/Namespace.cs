using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public abstract class Namespace
        : INamespace
    {
        protected Namespace(string alias, Uri identifier)
        {
            this.alias = alias;
            this.identifier = identifier;
        }

        private readonly string alias;
        private readonly Uri identifier;
        private readonly IDictionary<string, Func<INode, IQualifiedName, IElement>> elementConstructors = new Dictionary<string, Func<INode, IQualifiedName, IElement>>();

        protected void AddElementConstructor(string name, Func<INode, IQualifiedName, IElement> function)
        {
            elementConstructors[name] = function;
        }

        public string Alias
        {
            get { return alias; }
        }

        public Uri Identifier
        {
            get { return identifier; }
        }

        public IElement GetElement(INode parent, IQualifiedName name)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            if (name == null)
                throw new ArgumentNullException("name");

            return (elementConstructors.ContainsKey(name.LocalPart)) ?
                elementConstructors[name.LocalPart](parent, name)
                : null;
        }
    }
}

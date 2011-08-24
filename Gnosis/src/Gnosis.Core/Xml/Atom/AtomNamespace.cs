using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomNamespace
        : Namespace
    {
        public AtomNamespace()
            : base("atom", new Uri("http://www.w3.org/2005/Atom"))
        {
            AddElementConstructor("author", (parent, name) => new AtomAuthor(parent, name));
            AddElementConstructor("category", (parent, name) => new AtomCategory(parent, name));
            AddElementConstructor("contributor", (parent, name) => new AtomContributor(parent, name));
            AddElementConstructor("link", (parent, name) => new AtomLink(parent, name));
        }
    }
}

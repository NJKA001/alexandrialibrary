using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Atom
{
    public class AtomNamespace
        : Namespace
    {
        public AtomNamespace()
            : base("atom", new Uri("http://www.w3.org/2005/Atom"))
        {
            AddElementConstructor("author", (parent, name) => new AtomAuthor(parent, name));
            AddElementConstructor("category", (parent, name) => new AtomCategory(parent, name));
            AddElementConstructor("content", (parent, name) => new AtomContent(parent, name));
            AddElementConstructor("contributor", (parent, name) => new AtomContributor(parent, name));
            AddElementConstructor("entry", (parent, name) => new AtomEntry(parent, name));
            AddElementConstructor("feed", (parent, name) => new AtomFeed(parent, name));
            AddElementConstructor("generator", (parent, name) => new AtomGenerator(parent, name));
            AddElementConstructor("icon", (parent, name) => new AtomIcon(parent, name));
            AddElementConstructor("id", (parent, name) => new AtomId(parent, name));
            AddElementConstructor("link", (parent, name) => new AtomLink(parent, name));
            AddElementConstructor("logo", (parent, name) => new AtomLogo(parent, name));
            AddElementConstructor("published", (parent, name) => new AtomPublished(parent, name));
            AddElementConstructor("rights", (parent, name) => new AtomRights(parent, name));
            AddElementConstructor("source", (parent, name) => new AtomSource(parent, name));
            AddElementConstructor("subtitle", (parent, name) => new AtomSubtitle(parent, name));
            AddElementConstructor("summary", (parent, name) => new AtomSummary(parent, name));
            AddElementConstructor("title", (parent, name) => new AtomTitle(parent, name));
            AddElementConstructor("updated", (parent, name) => new AtomUpdated(parent, name));
        }
    }
}

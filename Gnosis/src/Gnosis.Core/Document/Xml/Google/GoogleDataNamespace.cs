using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Google
{
    public class GoogleDataNamespace
        : Namespace
    {
        public GoogleDataNamespace()
            : base("gd", new Uri("http://schemas.google.com/g/2005"))
        {
            AddElementConstructor("feedLink", (parent, name) => new GoogleDataFeedLink(parent, name));
        }
    }
}

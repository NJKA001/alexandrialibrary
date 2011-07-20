using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.DublinCore
{
    public class DublinCoreNamespace
        : XmlNamespace
    {
        private DublinCoreNamespace()
            : base(identifier, prefix)
        {
        }

        private static readonly string prefix = "dc";
        private static readonly Uri identifier = new Uri("http://purl.org/dc/elements/1.1/");

        public static readonly IXmlNamespace Singleton = new DublinCoreNamespace();
    }
}

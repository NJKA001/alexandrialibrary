using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Xml.Xhtml
{
    public static class HtmlNodeExtensions
    {
        public static IElement ToElement(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Document.Xml.Xhtml
{
    public interface IHtmlAnchor
        : IHtmlElement
    {
        string AnchorName { get; }
        ICharacterSet CharSet { get; }
        string Class { get; }
        string Content { get; }
        AnchorDirection Dir { get; }
        Uri Href { get; }
        ILanguageTag HrefLang { get; }
        string Id { get; }
        ILanguageTag Lang { get; }
        string Rel { get; }
        string Rev { get; }
        string Style { get; }
        int TabIndex { get; }
        string Title { get; }
        string Target { get; }
    }
}

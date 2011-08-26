using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public interface IAtomLink
        : IAtomCommon
    {
        Uri Href { get; }

        string Rel { get; }
        IMediaType Type { get; }
        ILanguageTag HrefLang { get; }
        string Title { get; }
        int Length { get; }
    }
}

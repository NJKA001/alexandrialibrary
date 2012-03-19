using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomLink
        : IAtomCommon
    {
        Uri Href { get; }
        string MediaType { get; }
        string Rel { get; }
        ILanguageTag HrefLang { get; }
        string Title { get; }
        int Length { get; }
    }
}

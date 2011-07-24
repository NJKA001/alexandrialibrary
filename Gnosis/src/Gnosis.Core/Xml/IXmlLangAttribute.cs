using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Xml
{
    public interface IXmlLangAttribute
        : IXmlAttribute
    {
        new ILanguageTag Value { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Culture;

namespace Gnosis.Core.Document.Xml
{
    public interface ILangAttribute
        : IAttribute
    {
        new ILanguageTag Value { get; }
    }
}

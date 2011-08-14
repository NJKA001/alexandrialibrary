using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public interface ILangAttribute
        : IAttribute
    {
        new ILanguageTag Value { get; }
    }
}

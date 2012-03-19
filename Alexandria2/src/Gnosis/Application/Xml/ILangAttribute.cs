using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml
{
    public interface ILangAttribute
        : IAttribute
    {
        new ILanguageTag Value { get; }
    }
}

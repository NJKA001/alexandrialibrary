using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.OpenSearch
{
    public interface IOpenSearchStartIndex
        : IOpenSearchElement
    {
        int Content { get; }
    }
}

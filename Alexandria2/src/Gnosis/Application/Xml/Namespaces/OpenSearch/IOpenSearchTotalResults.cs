using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.OpenSearch
{
    public interface IOpenSearchTotalResults
        : IOpenSearchElement
    {
        int Content { get; }
    }
}

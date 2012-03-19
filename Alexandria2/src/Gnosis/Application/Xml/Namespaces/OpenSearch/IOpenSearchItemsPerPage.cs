using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.OpenSearch
{
    public interface IOpenSearchItemsPerPage
        : IOpenSearchElement
    {
        int Content { get; }
    }
}

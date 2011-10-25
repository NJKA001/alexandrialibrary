using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.OpenSearch
{
    public interface IOpenSearchItemsPerPage
        : IOpenSearchElement
    {
        int Content { get; }
    }
}

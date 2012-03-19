using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public interface IMetadataPage
    {
        IEnumerable<IMetadata> Items { get; }
        int NumberOfPages { get; }
        int PageIndex { get; }
        int PageSize { get; }
    }
}

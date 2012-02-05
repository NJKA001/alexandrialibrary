using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public interface ISearchTask
        : ITask<IMetadata>
    {
        SearchFilters Filters { get; set; }

        void SetPattern(string pattern);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaSearch
    {
        IEnumerable<IMedia> Execute(IMediaRequest request);
        void ExecuteAsync(IMediaRequest request);
    }
}

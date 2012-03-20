using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;

namespace Gnosis
{
    public interface IImportInfo
    {
        string Path { get; }
        IMediaType MediaType { get; }
        IEntity Entity { get; }
    }
}

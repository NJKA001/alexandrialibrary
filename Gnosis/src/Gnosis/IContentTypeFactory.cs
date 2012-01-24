using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IContentTypeFactory
    {
        IContentType Default { get; }

        IContentType GetByLocation(Uri location);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Yahoo
{
    public interface IMediaGroup
        : IPrimaryMediaRssElement
    {
        IEnumerable<IMediaContent> MediaContents { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.MediaRss
{
    public interface IMediaRating
        : IOptionalMediaRssElement
    {
        Uri Scheme { get; }
        string Content { get; }
    }
}

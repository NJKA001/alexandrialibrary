using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.MediaRss
{
    public interface IMediaRating
        : IOptionalMediaRssElement
    {
        Uri Scheme { get; }
        string Content { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.MediaRss
{
    public interface IMediaContent
        : IPrimaryMediaRssElement
    {
        Uri Url { get; }
        long FileSize { get; }
        IMediaType Type { get; }
        MediaRssMedium Medium { get; }
        bool IsDefault { get; }
        MediaRssExpression Expression { get; }
        int BitRate { get; }
        int SamplingRate { get; }
        int Channels { get; }
        int Duration { get; }
        int Height { get; }
        int Width { get; }
        ILanguageTag Lang { get; }
    }
}

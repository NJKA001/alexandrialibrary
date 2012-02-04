using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaItem
        : IApplication
    {
        string Name { get; }
        string Summary { get; }
        DateTime FromDate { get; }
        DateTime ToDate { get; }
        uint Number { get; }
        TimeSpan Duration { get; }
        uint Height { get; }
        uint Width { get; }
        Uri Creator { get; }
        string CreatorName { get; }
        Uri Catalog { get; }
        string CatalogName { get; }
        Uri Target { get; }
        string TargetType { get; }
        Uri User { get; }
        string UserName { get; }
        Uri Thumbnail { get; }
        byte[] ThumbnailData { get; }
    }
}

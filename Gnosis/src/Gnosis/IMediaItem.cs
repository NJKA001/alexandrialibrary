using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaItem
        : IMedia
    {
        string Name { get; }
        Uri Creator { get; }
        string CreatorName { get; }
        Uri Catalog { get; }
        string CatalogName { get; }
        Uri Target { get; }
        IMediaType TargetType { get; }
        Uri User { get; }
        string UserName { get; }
        Uri Thumbnail { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public interface IMediaItemBuilder<T>
        where T : IMetadata
    {
        IMediaItemBuilder<T> Identity(string name, string summary);
        IMediaItemBuilder<T> Identity(string name, string summary, DateTime fromDate, DateTime toDate, uint number);
        IMediaItemBuilder<T> Identity(string name, string summary, DateTime fromDate, DateTime toDate, uint number, Uri location);

        IMediaItemBuilder<T> Size(TimeSpan duration);
        IMediaItemBuilder<T> Size(TimeSpan duration, uint height, uint width);
        IMediaItemBuilder<T> Size(uint heigth, uint width);

        IMediaItemBuilder<T> Creator(Uri creator, string creatorName);

        IMediaItemBuilder<T> Catalog(Uri catalog, string catalogName);

        IMediaItemBuilder<T> Target(Uri target, string targetType);

        IMediaItemBuilder<T> User(Uri user, string userName);

        IMediaItemBuilder<T> Thumbnail(Uri thumbnail, byte[] thumbnailData);

        T GetDefault();
        T ToMediaItem();
    }
}

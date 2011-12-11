using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisFeedItem
        : GnosisMediaItemBase, IFeedItem
    {
        public GnosisFeedItem(string name, string summary, DateTime publishDate, DateTime updateDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, publishDate, updateDate, number, duration, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisFeedItem(string name, string summary, DateTime publishDate, DateTime updateDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, publishDate, updateDate, number, duration, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisFeedItem, location)
        {
        }

        public static readonly IFeedItem Unknown = new GnosisFeedItem("Unknown Feed Item", string.Empty, DateTime.MinValue, DateTime.MaxValue, 0, TimeSpan.Zero, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

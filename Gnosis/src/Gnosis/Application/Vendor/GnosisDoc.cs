using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisDoc
        : GnosisMediaItemBase, IDoc
    {
        public GnosisDoc(string name, string summary, DateTime publishDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, publishDate, number, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisDoc(string name, string summary, DateTime publishDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, publishDate, DateTime.MaxValue, number, TimeSpan.Zero, 0, 0, creator, creatorName, catalog, creatorName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisClip, location)
        {
        }

        public static readonly IDoc Unknown = new GnosisDoc("Unknown Document", string.Empty, DateTime.MinValue, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

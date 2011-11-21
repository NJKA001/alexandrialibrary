using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisDoc
        : GnosisMediaItemBase, IDoc
    {
        public GnosisDoc(string name, DateTime publishDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, publishDate, number, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisDoc(string name, DateTime publishDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, publishDate, DateTime.MaxValue, number, TimeSpan.Zero, 0, 0, creator, creatorName, catalog, creatorName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisClip, location)
        {
        }

        public static readonly IDoc Unknown = new GnosisDoc("Unknown Document", DateTime.MinValue, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}

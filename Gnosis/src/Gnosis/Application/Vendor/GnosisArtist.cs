using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisArtist
        : GnosisMediaItemBase, IArtist
    {
        public GnosisArtist(string name, DateTime fromDate, DateTime toDate, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, fromDate, toDate, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisArtist(string name, DateTime fromDate, DateTime toDate, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, fromDate, toDate, 0, TimeSpan.Zero, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisArtist, location)
        {
        }

        public static readonly IArtist Unknown = new GnosisArtist("Unknown Artist", DateTime.MinValue, DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}

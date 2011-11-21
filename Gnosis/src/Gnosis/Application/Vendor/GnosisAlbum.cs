using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisAlbum
        : GnosisMediaItemBase, IAlbum
    {
        public GnosisAlbum(string name, DateTime fromDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, fromDate, number, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisAlbum(string name, DateTime fromDate, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, fromDate, DateTime.MaxValue, number, TimeSpan.Zero, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisAlbum, location)
        {
        }

        public static readonly IAlbum Unknown = new GnosisAlbum("Unknown Album", DateTime.MinValue, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}

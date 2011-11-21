using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisTrack
        : GnosisMediaItemBase, ITrack
    {
        public GnosisTrack(string name, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, number, duration, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisTrack(string name, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, DateTime.MinValue, DateTime.MaxValue, number, duration, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisTrack, location)
        {
        }

        public static readonly ITrack Unknown = new GnosisTrack("Unknown Track", 0, TimeSpan.Zero, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}

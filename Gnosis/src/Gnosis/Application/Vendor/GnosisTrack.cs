using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisTrack
        : GnosisMediaItemBase, ITrack
    {
        public GnosisTrack(string name, string summary, DateTime recordDate, DateTime releaseDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, recordDate, releaseDate, number, duration, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisTrack(string name, string summary, DateTime recordDate, DateTime releaseDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, recordDate, releaseDate, number, duration, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisTrack, location)
        {
        }

        public static readonly ITrack Unknown = new GnosisTrack("Unknown Track", string.Empty, DateTime.MinValue, DateTime.MaxValue, 0, TimeSpan.Zero, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

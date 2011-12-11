using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisClip
        : GnosisMediaItemBase, IClip
    {
        public GnosisClip(string name, string summary, DateTime releaseDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, releaseDate, number, duration, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisClip(string name, string summary, DateTime releaseDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, releaseDate, DateTime.MaxValue, number, duration, height, width, creator, creatorName, catalog, creatorName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisClip, location)
        {
        }

        public static readonly IClip Unknown = new GnosisClip("Unknown Clip", string.Empty, DateTime.MinValue, 0, TimeSpan.Zero, 0, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

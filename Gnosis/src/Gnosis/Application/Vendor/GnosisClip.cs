using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisClip
        : GnosisMediaItemBase, IClip
    {
        public GnosisClip(string name, DateTime releaseDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, releaseDate, number, duration, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisClip(string name, DateTime releaseDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, releaseDate, DateTime.MaxValue, number, duration, height, width, creator, creatorName, catalog, creatorName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisClip, location)
        {
        }

        public static readonly IClip Unknown = new GnosisClip("Unknown Clip", DateTime.MinValue, 0, TimeSpan.Zero, 0, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}

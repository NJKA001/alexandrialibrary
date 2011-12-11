using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisPlaylist
        : GnosisMediaItemBase, IPlaylist
    {
        public GnosisPlaylist(string name, string summary, DateTime fromDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, fromDate, number, duration, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisPlaylist(string name, string summary, DateTime fromDate, uint number, TimeSpan duration, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, fromDate, DateTime.MaxValue, number, duration, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisPlaylist, location)
        {
        }

        public static readonly IPlaylist Unknown = new GnosisPlaylist("Unknown Playlist", string.Empty, DateTime.MinValue, 0, TimeSpan.Zero, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisPic
        : GnosisMediaItemBase, IPic
    {
        public GnosisPic(string name, string summary, DateTime fromDate, uint number, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
            : this(name, summary, fromDate, number, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisPic(string name, string summary, DateTime fromDate, uint number, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData, Uri location)
            : base(name, summary, fromDate, DateTime.MaxValue, number, TimeSpan.Zero, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, MediaType.ApplicationGnosisPic, location)
        {
        }

        public static readonly IPic Unknown = new GnosisPic("Unknown Picture", string.Empty, DateTime.MinValue, 0, 0, 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), new byte[0], Guid.Empty.ToUrn());
    }
}

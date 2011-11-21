using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisProgram
        : GnosisMediaItemBase, IProgram
    {
        public GnosisProgram(string name, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, number, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisProgram(string name, uint number, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
            : base(name, DateTime.MinValue, DateTime.MaxValue, 0, TimeSpan.Zero, 0, 0, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, MediaType.ApplicationGnosisProgram, location)
        {
        }

        public static readonly IProgram Unknown = new GnosisProgram("Unknown Program", 0, Guid.Empty.ToUrn(), "Unknown Creator", Guid.Empty.ToUrn(), "Unknown Catalog", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn()); 
    }
}

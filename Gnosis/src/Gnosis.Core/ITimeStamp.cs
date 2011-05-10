using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    [CustomDataType]
    public interface ITimeStamp
    {
        [Column("TimeStamp_CreatedBy", UriExtensions.EmptyUriPath)]
        Uri CreatedBy { get; }

        [Column("TimeStamp_CreatedDate", DateTimeExtensions.EmptyDateString)]
        DateTime CreatedDate { get; }

        [Column("TimeStamp_LastAccessedBy", UriExtensions.EmptyUriPath)]
        Uri LastAccessedBy { get; }

        [Column("TimeStamp_LastAccessedDate", DateTimeExtensions.EmptyDateString)]
        DateTime LastAccessedDate { get; }

        [Column("TimeStamp_LastModifiedBy", UriExtensions.EmptyUriPath)]
        Uri LastModifiedBy { get; }

        [Column("TimeStamp_LastModifiedDate", DateTimeExtensions.EmptyDateString)]
        DateTime LastModifiedDate { get; }
    }
}

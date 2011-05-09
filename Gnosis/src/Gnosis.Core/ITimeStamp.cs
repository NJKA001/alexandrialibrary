using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    [DataType]
    public interface ITimeStamp
    {
        [Column("TimeStamp_CreatedBy", "urn:empty")]
        Uri CreatedBy { get; }

        [Column("TimeStamp_CreatedDate", "0001-01-01T00:00:00")]
        DateTime CreatedDate { get; }

        [Column("TimeStamp_LastAccessedBy", "urn:empty")]
        Uri LastAccessedBy { get; }

        [Column("TimeStamp_LastAccessedDate", "0001-01-01T00:00:00")]
        DateTime LastAccessedDate { get; }

        [Column("TimeStamp_LastModifiedBy", "urn:empty")]
        Uri LastModifiedBy { get; }

        [Column("TimeStamp_LastModifiedDate", "0001-01-01T00:00:00")]
        DateTime LastModifiedDate { get; }
    }
}

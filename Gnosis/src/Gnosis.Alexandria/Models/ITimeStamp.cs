using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface ITimeStamp
    {
        Uri CreatedBy { get; }
        DateTime CreatedDate { get; }
        Uri LastAccessedBy { get; }
        DateTime LastAccessedDate { get; }
        Uri LastModifiedBy { get; }
        DateTime LastModifiedDate { get; }
    }
}

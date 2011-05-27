using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    public interface ITimeStamp : IValue
    {
        Uri CreatedBy { get; }
        DateTime CreatedDate { get; }
        Uri LastAccessedBy { get; }
        DateTime LastAccessedDate { get; }
        Uri LastModifiedBy { get; }
        DateTime LastModifiedDate { get; }
    }
}

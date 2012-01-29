using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public interface IMediaIdentifier
    {
        IMediaInfo Identify(Uri location);
    }
}

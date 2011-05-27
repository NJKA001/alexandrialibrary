using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackIdentifier : IValue
    {
        Uri Scheme { get; }
        string Identifier { get; }
    }
}

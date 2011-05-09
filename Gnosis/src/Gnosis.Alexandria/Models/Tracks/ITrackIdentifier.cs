using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackIdentifier : IValue
    {
        Uri Scheme { get; }
        string Identifier { get; }
    }
}

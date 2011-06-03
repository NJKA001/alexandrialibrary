using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackLink : IValue
    {
        string TextEncoding { get; }
        string Relationship { get; }
        Uri Location { get; }
    }
}

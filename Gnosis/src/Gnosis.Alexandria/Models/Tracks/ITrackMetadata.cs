using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackMetadata : IValue
    {
        string TextEncoding { get; }
        string Description { get; }
        string Content { get; }
    }
}

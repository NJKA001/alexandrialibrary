using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackMetadatum : IValue
    {
        TextEncoding TextEncoding { get; }
        string Description { get; }
        string Content { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackRating : IChild
    {
        byte Rating { get; set; }
        Uri User { get; set; }
        ulong PlayCount { get; set; }
    }
}

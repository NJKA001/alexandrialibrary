using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackRating : IChild
    {
        byte Score { get; set; }
        Uri User { get; set; }
        ulong PlayCount { get; set; }
    }
}

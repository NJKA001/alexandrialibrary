using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackLink : IValue
    {
        TextEncoding TextEncoding { get; }
        string Relationship { get; }
        Uri Location { get; }
    }
}

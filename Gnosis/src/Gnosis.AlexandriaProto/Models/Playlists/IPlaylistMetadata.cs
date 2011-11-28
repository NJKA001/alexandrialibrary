using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistMetadata : IValue
    {
        Uri Relationship { get; }
        string Content { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ITrack : IMutable, IDeletable
    {
        string Title { get; set; }
        string TitleHash { get; }
        IArtist Creator { get; set; }
        IAlbum Album { get; set; }
        int Number { get; set; }
        DateTime ReleaseDate { get; set; }
        string Note { get; set; }
    }
}

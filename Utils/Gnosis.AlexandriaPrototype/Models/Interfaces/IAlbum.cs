using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IAlbum : IMutable, IDeletable
    {
        string Title { get; set; }
        string TitleHash { get; }
        string Abbreviation { get; set; }
        IArtist Creator { get; set; }
        DateTime ReleaseDate { get; set; }
        ICountry ReleaseCountry { get; set; }
        string Note { get; set; }
        IEnumerable<ITrack> Tracks { get; }
        void AddTrack(ITrack track);
        void RemoveTrack(ITrack track);
        IEnumerable<ITrack> GetRemovedTracks();
    }
}

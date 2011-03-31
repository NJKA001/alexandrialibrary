using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITrackController : IRepository<ITrack>
    {
        IEnumerable<ITrack> Tracks { get; }
        int TrackCount { get; }
        ITrack GetTrackAt(int index);

        ITrack ReadFromTag(string path);
        void LoadDirectory(DirectoryInfo directory);
        void Filter(string search);
        int IndexOf(ITrack track);
        void ClearTracks();
        void AddTrack(ITrack track);
        ITrack GetSelectedTrack();
    }
}

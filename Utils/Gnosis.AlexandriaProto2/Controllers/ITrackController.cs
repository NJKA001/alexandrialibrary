using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITrackController : IOldRepository<IOldTrack>
    {
        IEnumerable<IOldTrack> Tracks { get; }
        int TrackCount { get; }
        IOldTrack GetTrackAt(int index);

        IOldTrack ReadFromTag(string path);
        void LoadDirectory(DirectoryInfo directory);
        void Filter(string search);
        IEnumerable<IOldTrack> Search(string search);
        int IndexOf(IOldTrack track);
        void ClearTracks();
        void AddTrack(IOldTrack track);
        IOldTrack GetSelectedTrack();
        void Load(ISource source, DependencyObject handle);

        void CacheTrack(IOldTrack track);
        Uri GetCachedUri(Guid id);

        EventHandler<EventArgs> SourceLoadCompleted { get; set; }
    }
}

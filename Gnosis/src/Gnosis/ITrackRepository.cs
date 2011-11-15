using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITrackRepository
    {
        void Initialize();
        void Save(IEnumerable<ITrack> tracks);
        void Delete(IEnumerable<Uri> tracks);

        ITrack GetByLocation(Uri location);
        ITrack GetByAudioLocation(Uri location);
        IEnumerable<ITrack> GetByAlbum(Uri album);
        IEnumerable<ITrack> GetByTitle(string title);
    }
}

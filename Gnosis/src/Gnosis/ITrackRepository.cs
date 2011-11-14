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
        void Delete(IEnumerable<Guid> tracks);

        ITrack GetById(Guid id);
        ITrack GetByAudioLocation(Uri location);
        IEnumerable<ITrack> GetByAlbum(Guid album);
        IEnumerable<ITrack> GetByTitle(string title);
    }
}

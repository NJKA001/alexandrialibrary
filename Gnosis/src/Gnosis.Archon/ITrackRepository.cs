using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ITrackRepository
    {
        ITrack Get(Guid id);
        void Save(ITrack track);
        void Delete(Guid id);
        IEnumerable<ITrack> Tracks();
        IEnumerable<ITrack> Tracks(IEnumerable<KeyValuePair<string, object>> criteria);
    }
}

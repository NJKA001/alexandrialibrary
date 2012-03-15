using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Cache
    {
        private readonly ObservableCollection<Artist> artists = new ObservableCollection<Artist>();
        private readonly ObservableCollection<Work> works = new ObservableCollection<Work>();
        private readonly IDictionary<uint, Artist> artistsById = new Dictionary<uint, Artist>();
        private readonly IDictionary<uint, Work> worksById = new Dictionary<uint, Work>();

        public IEnumerable<Artist> Artists { get { return artists; } }
        public IEnumerable<Work> Works { get { return works; } }

        public void Add(uint id, Artist artist)
        {
            if (artistsById.ContainsKey(id))
                return;

            artistsById.Add(id, artist);
            artists.Add(artist);
        }

        public void Add(uint id, Work work)
        {
            if (worksById.ContainsKey(id))
                return;

            worksById.Add(id, work);
            works.Add(work);
        }

        public void Remove(Artist artist)
        {
            var id = GetId(artist);
            
            if (id == 0)
                return;

            if (!artistsById.ContainsKey(id))
                return;

            if (artists.Contains(artist))
                artists.Remove(artist);

            artistsById.Remove(id);
        }

        public void Remove(Work work)
        {
            var id = GetId(work);
            if (id == 0)
                return;

            if (!worksById.ContainsKey(id))
                return;

            if (works.Contains(work))
                works.Remove(work);

            worksById.Remove(id);
        }

        public uint GetId(Artist artist)
        {
            if (artist == null)
                return 0;

            return artistsById.Where(x => x.Value == artist).FirstOrDefault().Key;
        }

        public uint GetId(Work work)
        {
            if (work == null)
                return 0;

            return worksById.Where(x => x.Value == work).FirstOrDefault().Key;
        }

        public Artist GetArtist(uint id)
        {
            return artistsById.ContainsKey(id) ? artistsById[id] : null;
        }

        public Work GetWork(uint id)
        {
            return worksById.ContainsKey(id) ? worksById[id] : null;
        }
    }
}

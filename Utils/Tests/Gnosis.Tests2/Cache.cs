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
        private readonly IDictionary<uint, Link> artistLinksById = new Dictionary<uint, Link>();
        private readonly IDictionary<uint, Tag> artistTagsById = new Dictionary<uint, Tag>();
        private readonly IDictionary<uint, Link> workLinksById = new Dictionary<uint, Link>();
        private readonly IDictionary<uint, Tag> workTagsById = new Dictionary<uint, Tag>();
        private readonly IDictionary<uint, IList<Link>> linksByArtistId = new Dictionary<uint, IList<Link>>();
        private readonly IDictionary<uint, IList<Link>> linksByWorkId = new Dictionary<uint, IList<Link>>();
        private readonly IDictionary<uint, IList<Tag>> tagsByArtistId = new Dictionary<uint, IList<Tag>>();
        private readonly IDictionary<uint, IList<Tag>> tagsByWorkId = new Dictionary<uint, IList<Tag>>();

        public IEnumerable<Artist> Artists { get { return artists; } }
        public IEnumerable<Work> Works { get { return works; } }

        public void Add(uint id, Artist artist)
        {
            if (id == 0)
                throw new ArgumentException("id cannot be zero");

            if (artistsById.ContainsKey(id))
                return;

            artistsById.Add(id, artist);
            artists.Add(artist);
        }

        public void Add(uint id, Artist artist, Link link)
        {
            if (artistLinksById.ContainsKey(id))
                return;

            var artistId = GetArtistId(artist);
            if (artistId == 0)
                return;

            artistLinksById.Add(id, link);

            if (linksByArtistId.ContainsKey(artistId))
            {
                if (linksByArtistId[artistId].Contains(link))
                    return;

                linksByArtistId[artistId].Add(link);
            }
            else
                linksByArtistId[artistId] = new List<Link> { link };
        }

        public void Add(uint id, Artist artist, Tag tag)
        {
            if (artistTagsById.ContainsKey(id))
                return;

            var artistId = GetArtistId(artist);
            if (artistId == 0)
                return;

            artistTagsById.Add(id, tag);

            if (tagsByArtistId.ContainsKey(artistId))
            {
                if (tagsByArtistId[artistId].Contains(tag))
                    return;

                tagsByArtistId[artistId].Add(tag);
            }
            else
                tagsByArtistId[artistId] = new List<Tag> { tag };
        }

        public void Add(uint id, Work work)
        {
            if (worksById.ContainsKey(id))
                return;

            worksById.Add(id, work);
            works.Add(work);
        }

        public void Add(uint id, Work work, Link link)
        {
            if (workLinksById.ContainsKey(id))
                return;

            var workId = GetWorkId(work);
            if (workId == 0)
                return;

            workLinksById.Add(id, link);

            if (linksByWorkId.ContainsKey(workId))
            {
                if (linksByWorkId[workId].Contains(link))
                    return;

                linksByWorkId[workId].Add(link);
            }
            else
                linksByWorkId[workId] = new List<Link> { link };
        }

        public void Add(uint id, Work work, Tag tag)
        {
            if (workTagsById.ContainsKey(id))
                return;

            var workId = GetWorkId(work);
            if (workId == 0)
                return;

            workTagsById.Add(id, tag);

            if (tagsByWorkId.ContainsKey(workId))
            {
                if (tagsByWorkId[workId].Contains(tag))
                    return;

                tagsByWorkId[workId].Add(tag);
            }
            else
                tagsByWorkId[workId] = new List<Tag> { tag };
        }

        public void Remove(Artist artist)
        {
            var id = GetArtistId(artist);
            if (id == 0)
                return;

            if (!artistsById.ContainsKey(id))
                return;

            if (artists.Contains(artist))
                artists.Remove(artist);

            artistsById.Remove(id);
        }

        public void Remove(Artist artist, Link link)
        {
            var id = GetArtistLinkId(link);
            if (id == 0)
                return;

            if (!artistLinksById.ContainsKey(id))
                return;

            var artistId = GetArtistId(artist);
            if (linksByArtistId.ContainsKey(artistId))
            {
                if (linksByArtistId[artistId].Contains(link))
                    linksByArtistId[artistId].Remove(link);
            }

            artistLinksById.Remove(id);
        }

        public void Remove(Artist artist, Tag tag)
        {
            var id = GetArtistTagId(tag);
            if (id == 0)
                return;

            if (!artistTagsById.ContainsKey(id))
                return;

            var artistId = GetArtistId(artist);
            if (tagsByArtistId.ContainsKey(artistId))
            {
                if (tagsByArtistId[artistId].Contains(tag))
                    tagsByArtistId[artistId].Remove(tag);
            }

            artistTagsById.Remove(id);
        }

        public void Remove(Work work)
        {
            var id = GetWorkId(work);
            if (id == 0)
                return;

            if (!worksById.ContainsKey(id))
                return;

            if (works.Contains(work))
                works.Remove(work);

            worksById.Remove(id);
        }

        public void Remove(Work work, Link link)
        {
            var id = GetWorkLinkId(link);
            if (id == 0)
                return;

            if (!workLinksById.ContainsKey(id))
                return;

            var workId = GetWorkId(work);
            if (linksByWorkId.ContainsKey(workId))
            {
                if (linksByWorkId[workId].Contains(link))
                    linksByWorkId[workId].Remove(link);
            }

            workLinksById.Remove(id);
        }

        public void Remove(Work work, Tag tag)
        {
            var id = GetWorkTagId(tag);
            if (id == 0)
                return;

            if (!workTagsById.ContainsKey(id))
                return;

            var workId = GetWorkId(work);
            if (tagsByWorkId.ContainsKey(workId))
            {
                if (tagsByWorkId[workId].Contains(tag))
                    tagsByWorkId[workId].Remove(tag);
            }

            workTagsById.Remove(id);
        }

        public uint GetArtistId(Artist artist)
        {
            if (artist == null)
                return 0;

            return artistsById.Where(x => x.Value == artist).FirstOrDefault().Key;
        }

        public uint GetArtistLinkId(Link link)
        {
            if (link == null)
                return 0;

            return artistLinksById.Where(x => x.Value == link).FirstOrDefault().Key;
        }

        public uint GetArtistTagId(Tag tag)
        {
            if (tag == null)
                return 0;

            return artistTagsById.Where(x => x.Value == tag).FirstOrDefault().Key;
        }

        public uint GetWorkId(Work work)
        {
            if (work == null)
                return 0;

            return worksById.Where(x => x.Value == work).FirstOrDefault().Key;
        }

        public uint GetWorkLinkId(Link link)
        {
            if (link == null)
                return 0;

            return workLinksById.Where(x => x.Value == link).FirstOrDefault().Key;
        }

        public uint GetWorkTagId(Tag tag)
        {
            if (tag == null)
                return 0;

            return workTagsById.Where(x => x.Value == tag).FirstOrDefault().Key;
        }

        public Artist GetArtist(uint id)
        {
            return artistsById.ContainsKey(id) ? artistsById[id] : null;
        }

        public Link GetArtistLink(uint id)
        {
            return artistLinksById.ContainsKey(id) ? artistLinksById[id] : null;
        }

        public Tag GetArtistTag(uint id)
        {
            return artistTagsById.ContainsKey(id) ? artistTagsById[id] : null;
        }

        public Work GetWork(uint id)
        {
            return worksById.ContainsKey(id) ? worksById[id] : null;
        }

        public Link GetWorkLink(uint id)
        {
            return workLinksById.ContainsKey(id) ? workLinksById[id] : null;
        }

        public Tag GetWorkTag(uint id)
        {
            return workTagsById.ContainsKey(id) ? workTagsById[id] : null;
        }

        public IEnumerable<Link> GetLinksByArtist(uint id)
        {
            return linksByArtistId.ContainsKey(id) ? linksByArtistId[id].ToList() : Enumerable.Empty<Link>();
        }

        public IEnumerable<Tag> GetTagsByArtist(uint id)
        {
            return tagsByArtistId.ContainsKey(id) ? tagsByArtistId[id].ToList() : Enumerable.Empty<Tag>();
        }

        public IEnumerable<Link> GetLinksByWork(uint id)
        {
            return linksByWorkId.ContainsKey(id) ? linksByWorkId[id].ToList() : Enumerable.Empty<Link>();
        }

        public IEnumerable<Tag> GetTagsByWork(uint id)
        {
            return tagsByWorkId.ContainsKey(id) ? tagsByWorkId[id].ToList() : Enumerable.Empty<Tag>();
        }
    }
}

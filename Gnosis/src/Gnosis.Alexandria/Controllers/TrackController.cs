using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using TagLib;

using Gnosis.Alexandria.Models;
using Gnosis.Core;

namespace Gnosis.Alexandria.Controllers
{
    public class TrackController : ITrackController
    {
        public TrackController(IRepository<ITrack> repository, ITagController tagController)
        {
            this.repository = repository;
            this.tagController = tagController;

            var tracks = repository.All();
            foreach (var track in tracks)
            {
                tagController.LoadPicture(track);
                boundTracks.Add(track);
            }
        }

        private readonly IRepository<ITrack> repository;
        private readonly ITagController tagController;
        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();

        private ITrack GetTrack(string path, Tag tag)
        {
            var track = new Track() { Path = path };

            if (!string.IsNullOrEmpty(tag.Title))
                track.Title = tag.Title;

            if (!string.IsNullOrEmpty(tag.Album))
                track.Album = tag.Album;

            track.TrackNumber = tag.Track;
            track.DiscNumber = tag.Disc;

            if (!string.IsNullOrEmpty(tag.JoinedGenres))
                track.Genre = tag.JoinedGenres;

            if (!string.IsNullOrEmpty(tag.JoinedPerformers))
                track.Artist = tag.JoinedPerformers;

            if (tag.Year > 0 && tag.Year < int.MaxValue)
                track.ReleaseDate = new DateTime((int)tag.Year, 1, 1);

            if (tag.Pictures.Length > 0)
                track.ImageData = tag.Pictures[0].Data;

            track.Comment = tag.Comment;

            return track;
        }

        public ITrack ReadFromTag(string path)
        {
            var tag = tagController.GetTag(path);
            return GetTrack(path, tag);
        }

        public ITrack Get(Guid id)
        {
            return repository.Get(id);
        }

        public void Save(ITrack record)
        {
            repository.Save(record);
        }

        public void Save(IEnumerable<ITrack> records)
        {
            repository.Save(records);
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            repository.Delete(ids);
        }

        public IEnumerable<ITrack> All()
        {
            return repository.All();
        }

        public IEnumerable<ITrack> Search(IEnumerable<KeyValuePair<string, object>> criteria)
        {
            return repository.Search(criteria);
        }

        public void LoadDirectory(System.IO.DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                if (file.FullName.EndsWith(".mp3"))
                {
                    var track = ReadFromTag(file.FullName);
                    Save(track);
                }
            }

            foreach (var child in directory.GetDirectories())
            {
                LoadDirectory(child);
            }
        }

        private IEnumerable<KeyValuePair<string, object>> GetSearchCriteria(string search)
        {
            var criteria = new Dictionary<string, object>();

            var searchLike = string.Format("%{0}%", search);
            var searchHash = search.AsNameHash();
            var searchMetaphone = search.AsDoubleMetaphone();

            criteria.Add("Title", searchLike);
            criteria.Add("TitleHash", searchHash);
            criteria.Add("TitleMetaphone", searchMetaphone);
            criteria.Add("Artist", searchLike);
            criteria.Add("ArtistHash", searchHash);
            criteria.Add("ArtistMetaphone", searchMetaphone);
            criteria.Add("Album", searchLike);
            criteria.Add("AlbumHash", searchHash);
            criteria.Add("AlbumMetaphone", searchMetaphone);

            return criteria;
        }

        public void Filter(string search)
        {
            IEnumerable<ITrack> tracks = null;

            if (!string.IsNullOrEmpty(search))
            {
                var criteria = GetSearchCriteria(search);

                tracks = Search(criteria);
                if (tracks.Count() == 0 && search.Contains(' '))
                {
                    var set = new HashSet<ITrack>();
                    foreach (var word in search.Split(' '))
                    {
                        foreach (var track in Search(GetSearchCriteria(word)))
                            set.Add(track);
                    }
                    tracks = set;
                }
            }
            else
            {
                tracks = All();
            }

            if (tracks != null)
            {
                boundTracks.Clear();
                foreach (var track in tracks)
                {
                    tagController.LoadPicture(track);
                    boundTracks.Add(track);
                }
            }
        }

        public IEnumerable<ITrack> Tracks
        {
            get { return boundTracks; }
        }

        public int TrackCount
        {
            get { return boundTracks.Count; }
        }

        public int IndexOf(ITrack track)
        {
            return boundTracks.IndexOf(track);
        }

        public ITrack GetTrackAt(int index)
        {
            return boundTracks[index];
        }

        public void ClearTracks()
        {
            boundTracks.Clear();
        }

        public void AddTrack(ITrack track)
        {
            boundTracks.Add(track);
        }

        public ITrack GetSelectedTrack()
        {
            return boundTracks.Where(x => x.IsSelected == true).FirstOrDefault();
        }
    }
}

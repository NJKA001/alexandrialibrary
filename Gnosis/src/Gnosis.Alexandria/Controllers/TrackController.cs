using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TagLib;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Controllers
{
    public class TrackController : ITrackController
    {
        public TrackController(IRepository<ITrack> repository, ITagController tagController)
        {
            this.repository = repository;
            this.tagController = tagController;
        }

        private readonly IRepository<ITrack> repository;
        private readonly ITagController tagController;

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
    }
}

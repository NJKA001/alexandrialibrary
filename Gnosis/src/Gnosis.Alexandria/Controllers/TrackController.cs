using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using log4net;
using TagLib;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using Gnosis.Core;

namespace Gnosis.Alexandria.Controllers
{
    public class TrackController : ITrackController
    {
        public TrackController(IRepository<ITrack> repository, ITagController tagController)
        {
            this.repository = repository;
            this.tagController = tagController;

            //var tracks = repository.All();
            //foreach (var track in tracks)
            //{
            //    tagController.LoadPicture(track);
            //    boundTracks.Add(track);
            //}

            imageLoader.WorkerSupportsCancellation = true;
            imageLoader.DoWork += LoadTrackImages;
            imageLoader.RunWorkerCompleted += LoadTrackImagesCompleted;

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }
            else
            {
                LoadCachedFiles();
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(TrackController));
        private readonly IRepository<ITrack> repository;
        private readonly ITagController tagController;
        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();
        private BackgroundWorker imageLoader = new BackgroundWorker();

        private readonly IDictionary<Guid, string> cachedFiles = new Dictionary<Guid, string>();
        private string cachePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Alexandria", "Cache");
        const string extensionMp3 = ".mp3";

        private void LoadCachedFiles()
        {
            foreach (var file in new System.IO.DirectoryInfo(cachePath).GetFiles())
            {
                var index = file.Name.LastIndexOf('.');
                if (index > -1)
                {
                    var prefix = file.Name.Substring(0, index);
                    var id = Guid.Empty;
                    if (Guid.TryParse(prefix, out id))
                    {
                        cachedFiles.Add(id, file.FullName);
                    }
                }
            }
        }

        public Uri GetCachedUri(Guid id)
        {
            return cachedFiles.ContainsKey(id) ? new Uri(cachedFiles[id], UriKind.Absolute) : null;
        }

        public void CacheTrack(ITrack track)
        {
            try
            {
                var fileName = System.IO.Path.Combine(cachePath, track.Id.ToString().Replace("-", string.Empty));

                //TODO: Get mime type from feed
                //      We should not assume the file will always be an MP3 - e.g video podcasts
                if (track.Path.EndsWith(extensionMp3))
                {
                    var index = track.Path.LastIndexOf(extensionMp3);
                    var extension = track.Path.Substring(index, track.Path.Length - index);
                    fileName += extension;
                }
                else
                {
                    fileName += extensionMp3;
                }

                var request = System.Net.HttpWebRequest.Create(track.Path);
                using (var stream = request.GetResponse().GetResponseStream())
                {
                    stream.SaveToFile(fileName);

                    cachedFiles.Add(track.Id, fileName);
                    track.CachePath = fileName;
                }
            }
            catch (Exception ex)
            {
                log.Error("TrackController.CacheTrack", ex);
            }
        }

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

        public IEnumerable<ITrack> Search(string search)
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
                        {
                            set.Add(track);
                        }
                    }
                    tracks = set;
                }
            }
            else
            {
                tracks = All();
            }

            //foreach (var track in tracks)
                //tagController.LoadPicture(track);

            return tracks;
        }

        private void LoadTrackImages(object sender, DoWorkEventArgs args)
        {
            var worker = sender as BackgroundWorker;
            var tracksToLoad = new List<ITrack>();
            lock (boundTracks)
            {
                tracksToLoad.AddRange(boundTracks);
            }

            foreach (var track in tracksToLoad)
            {
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    break;
                }
                tagController.LoadPicture(track);
            }
        }

        private void LoadTrackImagesCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (args.Cancelled && boundTracks.Count > 0)
            {
                imageLoader.RunWorkerAsync();
            }
        }

        public void Filter(string search)
        {
            var tracks = Search(search);

            if (tracks != null)
            {
                boundTracks.Clear();
                foreach (var track in tracks)
                {
                    boundTracks.Add(track);
                }

                if (imageLoader.IsBusy)
                {
                    imageLoader.CancelAsync();
                }
                else
                {
                    imageLoader.RunWorkerAsync();
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

        private ITrack ConvertToTrack(ISource source)
        {
            var album = source.Parent != null ? source.Parent.Name : source.Name;
            return new Track() { Path = source.Path, ImagePath = source.ImagePath, Title = source.Name, Artist = source.Creator, Album = album, Comment = source.Summary };
        }

        private void LoadSource(ISource source)
        {
            var track = Search(new Dictionary<string, object> { { "Path", source.Path } }).FirstOrDefault();
            if (track == null)
            {
                var uri = new Uri(source.Path);
                if (uri.IsFile && System.IO.File.Exists(source.Path))
                {
                    track = ReadFromTag(source.Path);
                    tagController.LoadPicture(track);
                }
                else
                {
                    track = ConvertToTrack(source);
                }
                Save(track);
            }
            AddTrack(track);
        }

        public void Load(ISource source)
        {
            ClearTracks();

            try
            {
                LoadSource(source);
            }
            catch (Exception ex)
            {
                log.Error("TrackController.Load: Could not load source as track", ex);
            }

            foreach (var item in source.Children)
            {
                try
                {
                    var track = Search(new Dictionary<string, object> { { "Path", item.Path } }).FirstOrDefault();
                    if (track == null)
                    {
                        var uri = new Uri(item.Path);
                        if (uri.IsFile && System.IO.File.Exists(item.Path))
                        {
                            track = ReadFromTag(item.Path);
                            Save(track);
                        }
                        else
                        {
                            track = ConvertToTrack(item);
                            CacheTrack(track);
                            Save(track);
                        }
                    }

                    if (track != null && System.IO.File.Exists(track.Path))
                    {
                        tagController.LoadPicture(track);
                        AddTrack(track);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("TrackController.Load: Could not load track path=" + item.Path, ex);
                }
            }
        }
    }
}

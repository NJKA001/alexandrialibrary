using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

using log4net;
using TagLib;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using Gnosis.Alexandria.Views;
using Gnosis.Core;

namespace Gnosis.Alexandria.Controllers
{
    public class TrackController : ITrackController
    {
        public TrackController(IOldRepository<IOldTrack> repository, ITagController tagController)
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
        private readonly IOldRepository<IOldTrack> repository;
        private readonly ITagController tagController;
        private readonly ObservableCollection<IOldTrack> boundTracks = new ObservableCollection<IOldTrack>();
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

        public void CacheTrack(IOldTrack track)
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

        private IOldTrack GetTrack(string path, Tag tag)
        {
            var track = new OldTrack() { Path = path };

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

        public IOldTrack ReadFromTag(string path)
        {
            var tag = tagController.GetTag(path);
            return GetTrack(path, tag);
        }

        public IOldTrack Get(Guid id)
        {
            return repository.Get(id);
        }

        public void Save(IOldTrack record)
        {
            repository.Save(record);
        }

        public void Save(IEnumerable<IOldTrack> records)
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

        public IEnumerable<IOldTrack> All()
        {
            return repository.All();
        }

        public IEnumerable<IOldTrack> Search(IEnumerable<KeyValuePair<string, object>> criteria)
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

        public IEnumerable<IOldTrack> Search(string search)
        {
            IEnumerable<IOldTrack> tracks = null;

            if (!string.IsNullOrEmpty(search))
            {
                var criteria = GetSearchCriteria(search);

                tracks = Search(criteria);
                if (tracks.Count() == 0 && search.Contains(' '))
                {
                    var set = new HashSet<IOldTrack>();
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
            var tracksToLoad = new List<IOldTrack>();
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
                var uri = new Uri(track.Path);
                if (uri.IsFile)
                {
                    tagController.LoadPicture(track);
                }
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

        public IEnumerable<IOldTrack> Tracks
        {
            get { return boundTracks; }
        }

        public int TrackCount
        {
            get { return boundTracks.Count; }
        }

        public int IndexOf(IOldTrack track)
        {
            return boundTracks.IndexOf(track);
        }

        public IOldTrack GetTrackAt(int index)
        {
            return boundTracks[index];
        }

        public void ClearTracks()
        {
            boundTracks.Clear();
        }

        public void AddTrack(IOldTrack track)
        {
            boundTracks.Add(track);
        }

        public IOldTrack GetSelectedTrack()
        {
            return boundTracks.Where(x => x.IsSelected == true).FirstOrDefault();
        }

        private IOldTrack ConvertToTrack(ISource source)
        {
            var album = source.Parent != null ? source.Parent.Name : source.Name;
            return new OldTrack() { Path = source.Path, ImagePath = source.ImagePath, Title = source.Name, Artist = source.Creator, Album = album, Comment = source.Summary };
        }

        private void LoadSource(ISource source, LoadSourceRequest request)
        {
            if (source is DeviceCatalogSource)
                return;

            if (source is HardDiskSource)
                return;

            if (source is OpticalDiscSource)
                return;

            if (source is DirectorySource)
                return;

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
            request.Invoke(() => AddTrack(track));
        }

        private void LoadStarted(object sender, DoWorkEventArgs args)
        {
            try
            {
                log.Info("TrackController.LoadStarted");
                var request = args.Argument as LoadSourceRequest;
                if (request == null)
                {
                    log.Warn("  Request is null");
                    return;
                }

                var source = request.Source;

                request.Invoke(() => ClearTracks());
                LoadSource(source, request);

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
                            request.Invoke(() => AddTrack(track));
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("TrackController.Load: Could not load track path=" + item.Path, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("TrackController.LoadStarted", ex);
            }
        }

        private void LoadCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (args.Result is Exception)
            {
                //Load failed
            }
            else if (args.Cancelled)
            {
                //Load cancelled
            }
            else
            {
                log.Debug("TrackController.LoadCompleted");

                if (SourceLoadCompleted != null)
                    SourceLoadCompleted(this, EventArgs.Empty);
            }
        }

        public void Load(ISource source, DependencyObject handle)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += LoadStarted;
            worker.RunWorkerCompleted += LoadCompleted;
            worker.RunWorkerAsync(new LoadSourceRequest(handle) { Source = source });
        }

        public EventHandler<EventArgs> SourceLoadCompleted { get; set; }
    }
}

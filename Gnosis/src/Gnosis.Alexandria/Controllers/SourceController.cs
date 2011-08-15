using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using log4net;

using Gnosis.Alexandria;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using System.Net;
using System.Windows;
using System.Runtime.InteropServices;
using System.Web;

namespace Gnosis.Alexandria.Controllers
{
    public class SourceController : ISourceController
    {
        public SourceController(IOldRepository<ISource> repository, ITrackController trackController)
        {
            this.repository = repository;
            this.trackController = trackController;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(SourceController));
        private readonly IOldRepository<ISource> repository;
        private readonly ITrackController trackController;

        public ISource Get(Guid id)
        {
            return repository.Get(id);
        }

        public void Save(ISource record)
        {
            repository.Save(record);
        }

        public void Save(IEnumerable<ISource> records)
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

        public IEnumerable<ISource> All()
        {
            return repository.All();
        }

        public IEnumerable<ISource> Search(IEnumerable<KeyValuePair<string, object>> criteria)
        {
            return repository.Search(criteria);
        }

        public ISource GetPlaylistItem(ISource parent, IOldTrack track)
        {
            return new PlaylistItemSource()
            {
                Parent = parent,
                Path = track.Path,
                ImagePath = track.ImagePath,
                ImageData = track.ImageData,
                Name = string.Format("{0} by {1}", track.Title ?? "Untitled", track.Artist ?? "Unknown Artist"),
                Number = parent.Children.Count() + 1
            };
        }

        public void LoadDirectories(ISource source)
        {
            if (source.Children.Count() == 1)
            {
                //var proxy = source.Children.FirstOrDefault() as ProxySource;
                //if (proxy != null)
                //    source.RemoveChild(proxy);

                if (Directory.Exists(source.Path))
                {
                    var directory = new DirectoryInfo(source.Path);
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        var child = source.Children.Where(x => x.Path == subDirectory.FullName).FirstOrDefault();
                        if (child == null)
                        {
                            child = new DirectorySource() { Name = subDirectory.Name, Path = subDirectory.FullName, Parent = source };
                            source.AddChild(child);
                        }
                        //LoadDirectories(child);
                    }
                    foreach (var file in directory.GetFiles("*.mp3"))
                    {
                        try
                        {
                            var track = trackController.Search(new Dictionary<string, object> { { "Path", file.FullName } }).FirstOrDefault();
                            if (track == null)
                            {
                                track = trackController.ReadFromTag(file.FullName);
                                trackController.Save(track);
                            }

                            var item = GetPlaylistItem(source, track);
                            source.AddChild(item);
                        }
                        catch (Exception ex)
                        {
                            log.Error("SourceController.LoadDirectories: Could not load MP3 file. path=" + file.FullName, ex);
                        }
                    }
                    foreach (var file in directory.GetFiles("*.wav"))
                    {
                        try
                        {
                            var track = trackController.Search(new Dictionary<string, object> { { "Path", file.FullName } }).FirstOrDefault();
                            if (track == null)
                            {
                                track = new OldTrack { Path = file.FullName, Title = file.Name };
                                trackController.Save(track);
                            }

                            var item = GetPlaylistItem(source, track);
                            source.AddChild(item);
                        }
                        catch (Exception ex)
                        {
                            log.Error("SourceController.LoadDirectories: Could not load WAV file. path=" + file.FullName, ex);
                        }
                    }
                }
            }
        }

        private string GetResponseBody(string path)
        {
            var request = HttpWebRequest.Create(path);

            using (var stream = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void LoadPodcastStarted(object sender, DoWorkEventArgs args)
        {
            try
            {
                var request = args.Argument as LoadSourceRequest;
                if (request == null)
                {
                    log.Warn("LoadPodcastStarted: request is null");
                    return;
                }

                var source = request.Source;

                if (source.Children.Count() == 1 && source.Children.FirstOrDefault() is ProxySource)
                {
                    var children = Search(new Dictionary<string, object> { { "Parent", source.Id } });
                    if (children != null && children.Count() > 0)
                    {
                        foreach (var child in children)
                            source.AddChild(child);

                        log.Info("SourceController.LoadPodcast: Loaded podcast from repository");
                    }
                }

                var xml = new XmlDocument();
                xml.LoadXml(GetResponseBody(source.Path));
                var nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd");
                nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");

                bool isUpdated = false;
                var imageNode = xml.SelectSingleNode("/rss/channel/image/url", nsmgr);
                if (imageNode != null)
                {
                    if (source.ImagePath != imageNode.InnerText)
                    {
                        source.ImagePath = imageNode.InnerText;
                        isUpdated = true;
                    }
                }
                var lastBuildDateNode = xml.SelectSingleNode("/rss/channel/lastBuildDate", nsmgr);
                if (lastBuildDateNode != null)
                {
                    var lastBuildDate = DateTime.Now;
                    Gnosis.Core.Rfc822DateTime.TryParse(lastBuildDateNode.InnerText, out lastBuildDate);
                    source.Date = lastBuildDate;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    Save(source);
                }

                var itemNodes = xml.SelectNodes("/rss/channel/item", nsmgr);
                if (itemNodes != null && itemNodes.Count > 0)
                {
                    foreach (XmlNode itemNode in itemNodes)
                    {
                        var titleNode = itemNode.SelectSingleNode("title", nsmgr);
                        var linkNode = itemNode.SelectSingleNode("enclosure", nsmgr);
                        var dateNode = itemNode.SelectSingleNode("pubDate", nsmgr);
                        var authorNode = itemNode.SelectSingleNode("itunes:author", nsmgr);
                        var summaryNode = itemNode.SelectSingleNode("itunes:summary", nsmgr);
                        var path = string.Empty;

                        if (linkNode == null)
                        {
                            linkNode = itemNode.SelectSingleNode("link", nsmgr);
                            if (linkNode != null)
                                path = linkNode.InnerText;
                        }
                        else
                        {
                            path = linkNode.Attributes["url"].Value;
                        }

                        if (!string.IsNullOrEmpty(path))
                        {
                            var date = source.Date;
                            if (dateNode != null)
                                Gnosis.Core.Rfc822DateTime.TryParse(dateNode.InnerText, out date);

                            var child = source.Children.Where(x => x.Path == path).FirstOrDefault();

                            if (child != null)
                            {
                                if (child.Date != date && date != DateTime.MinValue)
                                {
                                    child.Date = date;
                                    Save(child);
                                    var track = trackController.Search(new Dictionary<string, object> { { "Path", child.Path } }).FirstOrDefault();
                                    if (track != null)
                                    {
                                        track.ReleaseDate = date;
                                        trackController.Save(track);
                                    }
                                }
                            }
                            else
                            {
                                var playlistItem = new PlaylistItemSource { Path = path, ImagePath = source.ImagePath, Date = date, Parent = source };
                                playlistItem.Name = titleNode != null ? titleNode.InnerText : "Unknown Podcast";
                                playlistItem.Creator = authorNode != null ? authorNode.InnerText : "Unknown Creator";
                                playlistItem.Summary = summaryNode != null ? summaryNode.InnerText : string.Empty;

                                Save(playlistItem);

                                request.Invoke((Action)delegate { source.AddChild(playlistItem); });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceController.LoadPodcastStarted", ex);
                args.Result = ex;
            }
        }

        private void LoadPodcastCompleted(object sender, RunWorkerCompletedEventArgs args)
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
                log.Debug("LoadPodcastCompleted");
            }
        }

        private void LoadSpiderStarted(object sender, DoWorkEventArgs args)
        {
            try
            {
                log.Info("LoadSpiderStarted");
                var request = args.Argument as LoadSourceRequest;
                if (request == null)
                {
                    log.Warn("LoadSpiderStarted: request is null");
                    return;
                }

                var source = request.Source;

                if (source != null && !string.IsNullOrEmpty(source.Path))
                {
                    var body = GetResponseBody(source.Path);

                    if (!string.IsNullOrEmpty(source.ChildPattern))
                    {
                        var regex = new Regex(source.ChildPattern);
                        foreach (Match match in regex.Matches(body))
                        {
                            var childName = match.Groups["NAME"].Value;
                            var childPath = match.Groups["PATH"].Value;
                            var childSummary = match.Groups["SUMMARY"].Value;

                            if (!string.IsNullOrEmpty(childPath))
                            {
                                //We need to turn relative paths into absolute paths
                                if (childPath.StartsWith("~/"))
                                    childPath = childPath.Substring(1, childPath.Length-1);

                                if (childPath.StartsWith("/"))
                                {
                                    var parentUri = new Uri(source.Path);
                                    childPath = string.Format("{0}://{1}{2}", parentUri.Scheme, parentUri.Host, childPath);
                                }

                                var existingChild = source.Children.Where(x => x.Path == childPath).FirstOrDefault();
                                if (existingChild != null)
                                {
                                    //TODO: Decide whether or not we want to ever update existing items
                                    //if (!string.IsNullOrEmpty(childName) && existingChild.Name != childName)
                                    //{
                                        //Save(existingChild);
                                    //}
                                }
                                else
                                {
                                    var childSpider = new SpiderSource { Name = childName, Path = childPath, Summary = childSummary, Parent = source };

                                    log.Info(string.Format("LoadSpiderStarted: Saving new spider child. name={0} path={1}", childName, childPath));
                                    Save(childSpider);
                                    request.Invoke(() => source.AddChild(childSpider));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadSpiderStarted", ex);
            }
        }

        private void LoadSpiderCompleted(object sender, RunWorkerCompletedEventArgs args)
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
                log.Debug("LoadSpiderCompleted");
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetDriveType(string lpRootPathName);

        private enum DriveType
        {
            Unknown = 0,
            NoRoot = 1,
            Removable = 2,
            Localdisk = 3,
            Network = 4,
            CD = 5,
            RAMDrive = 6
        }

        #region LoadDevices

        private void LoadDevicesStarted(object sender, DoWorkEventArgs args)
        {
            try
            {
                log.Info("LoadDevicesStarted");
                var request = args.Argument as LoadSourceRequest;
                if (request == null)
                {
                    log.Warn("LoadDevicesStarted: request is null");
                    return;
                }

                var source = request.Source;

                if (source != null)
                {
                    foreach (var drive in System.Environment.GetLogicalDrives())
                    {
                        switch (GetDriveType(drive))
                        {
                            case (int)DriveType.Localdisk:
                                request.Invoke(() => source.AddChild(new HardDiskSource { Name = drive, Parent = source }));
                                break;
                            case (int)DriveType.CD:
                                request.Invoke(() => source.AddChild(new OpticalDiscSource { Name = drive, Parent = source }));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadDevicesStarted", ex);
            }
        }

        private void LoadDevicesCompleted(object sender, RunWorkerCompletedEventArgs args)
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
                log.Debug("LoadDevicesCompleted");
            }
        }

        #endregion

        #region LoadYouTubeUser

        private void GetYouTubeUserPlaylists(ISource source, LoadSourceRequest request)
        {
            var path = source.Path + "/playlists?v=2";
            var xml = new XmlDocument();
            xml.LoadXml(GetResponseBody(path));
            var nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("openSearch", "http://a9.com/-/spec/opensearchrss/1.0/");
            nsmgr.AddNamespace("gd", "http://schemas.google.com/g/2005");
            nsmgr.AddNamespace("yt", "http://gdata.youtube.com/schemas/2007");
            nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            //nsmgr.AddNamespace("itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd");
            //nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");

            //var userPlaylists = new YouTubePlaylistsSource() { 
            var feedNode = xml.SelectSingleNode("/atom:feed", nsmgr);
            if (feedNode != null)
            {
                var titleNode = feedNode.SelectSingleNode("atom:title", nsmgr);
                var playlistsName = (titleNode != null) ? titleNode.InnerText : "Playlists";

                var userPlaylists = source.Children.Where(x => x.Path == path).FirstOrDefault() as YouTubeUserPlaylistsSource;
                if (userPlaylists == null)
                {
                    userPlaylists = Search(new Dictionary<string, object> { { "Path", path } }).FirstOrDefault() as YouTubeUserPlaylistsSource;
                    if (userPlaylists == null)
                    {
                        userPlaylists = new YouTubeUserPlaylistsSource() { Name = playlistsName, Path = path, Parent = source };
                        Save(userPlaylists);
                    }
                    request.Invoke(() => source.AddChild(userPlaylists));
                }
                
                var playlistNodes = feedNode.SelectNodes("atom:entry", nsmgr);
                if (playlistNodes != null && playlistNodes.Count > 0)
                {
                    foreach (XmlNode node in playlistNodes)
                    {
                        GetYouTubePlaylist(userPlaylists, request, nsmgr, node);
                    }
                }
            }
        }

        private void GetYouTubePlaylist(YouTubeUserPlaylistsSource source, LoadSourceRequest request, XmlNamespaceManager nsmgr, XmlNode node)
        {
            var titleNode = node.SelectSingleNode("atom:title", nsmgr);
            var publishedNode = node.SelectSingleNode("atom:published", nsmgr);
            var contentNode = node.SelectSingleNode("atom:content", nsmgr);
            var authorNode = node.SelectSingleNode("atom:author/atom:name", nsmgr);

            var name = titleNode != null ? titleNode.InnerText : "Untitled Playlist";
            var date = new DateTime(2000, 1, 1);
            if (publishedNode != null)
                DateTime.TryParse(publishedNode.InnerText, out date);
            var path = contentNode != null ? contentNode.Attributes["src"].Value : "unknown";
            var creator = authorNode != null ? authorNode.InnerText : "Unknown Creator";

            var playlist = source.Children.Where(x => x.Path == path).FirstOrDefault() as YouTubePlaylistSource;
            if (playlist == null)
            {
                playlist = Search(new Dictionary<string, object> { { "Path", path } }).FirstOrDefault() as YouTubePlaylistSource;
                if (playlist == null)
                {
                    playlist = new YouTubePlaylistSource() { Name = name, Date = date, Creator = creator, Path = path, Parent = source };
                    Save(playlist);
                }
                request.Invoke(() => source.AddChild(playlist));
            }
            
            GetYouTubeVideos(playlist, request);
        }

        private void AddVideos(ISource source, LoadSourceRequest request, XmlDocument xml, XmlNamespaceManager nsmgr)
        {
            var entries = xml.SelectNodes("/atom:feed/atom:entry", nsmgr);
            if (entries != null && entries.Count > 0)
            {
                foreach (XmlNode entryNode in entries)
                {
                    var titleNode = entryNode.SelectSingleNode("atom:title", nsmgr);
                    var linkNodes = entryNode.SelectNodes("atom:link", nsmgr); //[@rel = \"alternate\"]", nsmgr);
                    XmlNode pathNode = null;
                    foreach (XmlNode linkNode in linkNodes)
                    {
                        var rel = linkNode.Attributes["rel"].Value;
                        if (rel == "alternate")
                        {
                            pathNode = linkNode;
                            break;
                        }
                    }

                    XmlNode imageNode = null;
                    var thumbnailNodes = entryNode.SelectNodes("media:group/media:thumbnail", nsmgr);
                    foreach (XmlNode thumbnailNode in thumbnailNodes)
                    {
                        var thumbNailName = thumbnailNode.Attributes["yt:name"].Value;
                        if (thumbNailName == "hqdefault")
                        {
                            imageNode = thumbnailNode;
                            break;
                        }
                    }
                    //var authorNode = entryNode.SelectSingleNode("author/name", nsmgr);
                    //var thumbnailNode = entryNode.SelectSingleNode("media:thumbnail[@yt:name=\"hqdefault\"]", nsmgr);

                    var name = titleNode != null ? titleNode.InnerText : "Untitled Video";
                    var path = pathNode != null ? pathNode.Attributes["href"].Value : "unknown";
                    if (path != null && path != "unknown")
                        path = System.Web.HttpUtility.UrlDecode(path);

                    var imagePath = imageNode != null ? imageNode.Attributes["url"].Value : null;

                    var video = source.Children.Where(x => x.Path == path).FirstOrDefault() as YouTubeVideoSource;
                    if (video == null)
                    {
                        video = Search(new Dictionary<string, object> { { "Path", path } }).FirstOrDefault() as YouTubeVideoSource;
                        if (video == null)
                        {
                            video = new YouTubeVideoSource() { Name = name, Path = path, ImagePath = imagePath, Parent = source };
                            Save(video);
                        }
                        request.Invoke(() => source.AddChild(video));
                    }
                }
            }
        }

        private void GetYouTubeVideos(ISource source, LoadSourceRequest request)
        {
            if (!string.IsNullOrEmpty(source.Path) && source.Path != "unknown")
            {
                var xml = new XmlDocument();
                xml.LoadXml(GetResponseBody(source.Path));
                var nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("openSearch", "http://a9.com/-/spec/opensearchrss/1.0/");
                nsmgr.AddNamespace("gd", "http://schemas.google.com/g/2005");
                nsmgr.AddNamespace("yt", "http://gdata.youtube.com/schemas/2007");
                nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");
                nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");

                AddVideos(source, request, xml, nsmgr);
            }
        }

        private void GetYouTubeUserFavorites(ISource source, LoadSourceRequest request)
        {
            var path = source.Path + "/favorites?v=2";
            var xml = new XmlDocument();
            xml.LoadXml(GetResponseBody(path));
            var nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("openSearch", "http://a9.com/-/spec/opensearchrss/1.0/");
            nsmgr.AddNamespace("gd", "http://schemas.google.com/g/2005");
            nsmgr.AddNamespace("yt", "http://gdata.youtube.com/schemas/2007");
            nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");
            nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            //nsmgr.AddNamespace("itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd");
            //nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");

            //var userPlaylists = new YouTubePlaylistsSource() { 
            var feedNode = xml.SelectSingleNode("/atom:feed", nsmgr);
            if (feedNode != null)
            {
                var titleNode = feedNode.SelectSingleNode("atom:title", nsmgr);
                var favoritesName = (titleNode != null) ? titleNode.InnerText : "Favorites";

                var favoritesSource = source.Children.Where(x => x.Path == path).FirstOrDefault() as YouTubeUserFavoritesSource;
                if (favoritesSource == null)
                {
                    favoritesSource = Search(new Dictionary<string, object>() { {"Path", path } }).FirstOrDefault() as YouTubeUserFavoritesSource;
                    if (favoritesSource == null)
                    {
                        favoritesSource = new YouTubeUserFavoritesSource() { Name = favoritesName, Path = path, Parent = source };
                        Save(favoritesSource);
                    }
                    request.Invoke(() => source.AddChild(favoritesSource));
                }

                AddVideos(favoritesSource, request, xml, nsmgr);
            }
        }

        private void LoadYouTubeUserStarted(object sender, DoWorkEventArgs args)
        {
            try
            {
                log.Info("LoadYouTubeUserStarted");
                var request = args.Argument as LoadSourceRequest;
                if (request == null)
                {
                    log.Warn("LoadYouTubeUserStarted: request is null");
                    return;
                }

                var source = request.Source;

                if (source != null && !string.IsNullOrEmpty(source.Path))
                {
                    GetYouTubeUserPlaylists(source, request);
                    GetYouTubeUserFavorites(source, request);
                    //Save(source);
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadYouTubeUserStarted", ex);
            }
        }

        private void LoadYouTubeUserCompleted(object sender, RunWorkerCompletedEventArgs args)
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
                log.Debug("LoadYouTubeUserCompleted");
            }
        }

        #endregion

        public void LoadPodcast(ISource source, DependencyObject handle)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += LoadPodcastStarted;
            worker.RunWorkerCompleted += LoadPodcastCompleted;
            worker.RunWorkerAsync(new LoadSourceRequest(handle) { Source = source });
        }

        public void LoadSpider(ISource source, DependencyObject handle)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += LoadSpiderStarted;
            worker.RunWorkerCompleted += LoadSpiderCompleted;
            worker.RunWorkerAsync(new LoadSourceRequest(handle) { Source = source });
        }

        public void LoadDevices(ISource source, DependencyObject handle)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += LoadDevicesStarted;
            worker.RunWorkerCompleted += LoadDevicesCompleted;
            worker.RunWorkerAsync(new LoadSourceRequest(handle) { Source = source });
        }

        public void LoadYouTubeUser(ISource source, DependencyObject handle)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += LoadYouTubeUserStarted;
            worker.RunWorkerCompleted += LoadYouTubeUserCompleted;
            worker.RunWorkerAsync(new LoadSourceRequest(handle) { Source = source });
        }
    }
}

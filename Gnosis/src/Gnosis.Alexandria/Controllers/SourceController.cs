using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using log4net;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria.Controllers
{
    public class SourceController : ISourceController
    {
        public SourceController(IRepository<ISource> repository, ITrackController trackController)
        {
            this.repository = repository;
            this.trackController = trackController;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(SourceController));
        private readonly IRepository<ISource> repository;
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

        public ISource GetPlaylistItem(ISource parent, ITrack track)
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
                var proxy = source.Children.FirstOrDefault() as ProxySource;
                if (proxy != null)
                    source.RemoveChild(proxy);

                if (Directory.Exists(source.Path))
                {
                    var directory = new DirectoryInfo(source.Path);
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        var child = new DirectorySource() { Name = subDirectory.Name, Path = subDirectory.FullName, Parent = source };
                        source.AddChild(child);
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
                            log.Error("SourceController.LoadDirectories: Could not load file path=" + file.FullName, ex);
                        }
                    }
                }
            }
        }
    }
}

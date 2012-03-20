using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Data;
using Gnosis.Logging;

namespace Gnosis
{
    public class MediaImporter
        : IMediaImporter
    {
        public MediaImporter(ILogger logger, IMediaFactory mediaFactory, IEntityRepository repository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.logger = logger;
            this.mediaFactory = mediaFactory;
            this.repository = repository;
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly IEntityRepository repository;

        private Func<string, bool> directoryPathFilter;
        private Func<string, bool> mediaPathFilter;
        private Func<IMedia, bool> mediaFilter;

        private Action<string> directoryCallback;
        private Action<IImportInfo> importCallback;
        private Action completedCallback;

        private string importRoot;

        private IEnumerable<string> GetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path);
            }
            catch (Exception ex)
            {
                logger.Error("  GetFiles failed for path: " + path, ex);
                return Enumerable.Empty<string>();
            }
        }

        private IEnumerable<string> GetDirectories(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch (Exception ex)
            {
                logger.Error("  GetDirectories failed for path: " + path, ex);
                return Enumerable.Empty<string>();
            }
        }

        private IEntity GetEntity(IMedia media)
        {
            var name = media.Path.RemoveExtension();
            try
            {
                var url = new Uri(media.Path);
                if (url.IsFile)
                {
                    var fileInfo = new FileInfo(new Uri(media.Path).LocalPath);
                    name = fileInfo.Name.RemoveExtension();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error getting entity name for path: " + media.Path, ex);
            }

            switch (media.Type.Supertype)
            {
                case MediaSupertype.Application:
                case MediaSupertype.Message:
                case MediaSupertype.Model:
                case MediaSupertype.Multipart:
                    return new Work(WorkType.Document, null, null, name, 0, 0);
                case MediaSupertype.Audio:
                    return new Work(WorkType.Track, null, null, name, 0, 0);
                case MediaSupertype.Image:
                    return new Work(WorkType.Image, null, null, name, 0, 0);
                case MediaSupertype.Text:
                    return new Work(WorkType.Text, null, null, name, 0, 0);
                case MediaSupertype.Video:
                    return new Work(WorkType.Video, null, null, name, 0, 0);
                default:
                    return null;
            }
        }

        private IImportInfo GetImportInfo(IMedia media)
        {
            var entity = GetEntity(media);
            return new ImportInfo(media, entity);
        }

        private void ProcessDirectory(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            logger.Debug("Directory: " + path);

            try
            {
                if (directoryCallback != null)
                    directoryCallback(path);
            }
            catch (Exception ex)
            {
                logger.Error("  directoryCallback failed", ex);
            }

            foreach (var file in GetFiles(path))
            {
                if (mediaPathFilter != null && !mediaPathFilter(file))
                    continue;

                var media = mediaFactory.GetMedia(new Uri(file));

                if (mediaFilter != null && !mediaFilter(media))
                    continue;

                logger.Debug(string.Format("  {0,-20} {1}", media.Type.Name, file));

                var importInfo = GetImportInfo(media);

                if (importCallback != null)
                    importCallback(importInfo);
            }

            foreach (var child in GetDirectories(path))
            {
                if (directoryPathFilter != null && !directoryPathFilter(child))
                    continue;

                ProcessDirectory(child);
            }
        }

        public void SetDirectoryPathFilter(Func<string, bool> directoryPathFilter)
        {
            if (directoryPathFilter == null)
                throw new ArgumentNullException("directoryPathFilter");

            this.directoryPathFilter = directoryPathFilter;
        }

        public void SetMediaPathFilter(Func<string, bool> mediaPathFilter)
        {
            if (mediaPathFilter == null)
                throw new ArgumentNullException("mediaPathFilter");

            this.mediaPathFilter = mediaPathFilter;
        }

        public void SetMediaFilter(Func<IMedia, bool> mediaFilter)
        {
            if (mediaFilter == null)
                throw new ArgumentNullException("mediaFilter");

            this.mediaFilter = mediaFilter;
        }

        public void SetDirectoryCallback(Action<string> directoryCallback)
        {
            if (directoryCallback == null)
                throw new ArgumentNullException("directoryCallback");

            this.directoryCallback = directoryCallback;
        }

        public void SetImportCallback(Action<IImportInfo> importCallback)
        {
            if (importCallback == null)
                throw new ArgumentNullException("importCallback");

            this.importCallback = importCallback;
        }

        public void SetCompletedCallback(Action completedCallback)
        {
            if (completedCallback == null)
                throw new ArgumentNullException("completedCallback");

            this.completedCallback = completedCallback;
        }

        public void Import(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);

            logger.Debug("Import Started: " + path);

            importRoot = path;

            ProcessDirectory(path);

            if (completedCallback != null)
                completedCallback();

            logger.Debug("Import Completed: " + path);
        }
    }
}

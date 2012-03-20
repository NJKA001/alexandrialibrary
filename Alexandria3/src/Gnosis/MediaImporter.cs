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
        private Action<IMedia> mediaCallback;
        private Action completedCallback;

        private void ProcessDirectory(string path)
        {
            logger.Debug("Directory: " + path);

            if (directoryCallback != null)
                directoryCallback(path);

            foreach (var file in Directory.GetFiles(path))
            {
                if (mediaPathFilter != null && !mediaPathFilter(file))
                    continue;

                var media = mediaFactory.GetMedia(new Uri(file));

                if (mediaFilter != null && !mediaFilter(media))
                    continue;

                logger.Debug(string.Format("  {0,-20} {1}", media.Type.Name, file));

                if (mediaCallback != null)
                    mediaCallback(media);
            }

            foreach (var child in Directory.GetDirectories(path))
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

        public void SetMediaCallback(Action<IMedia> mediaCallback)
        {
            if (mediaCallback == null)
                throw new ArgumentNullException("mediaCallback");

            this.mediaCallback = mediaCallback;
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

            ProcessDirectory(path);

            if (completedCallback != null)
                completedCallback();

            logger.Debug("Import Completed: " + path);
        }
    }
}

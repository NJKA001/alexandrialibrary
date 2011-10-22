using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Image;

namespace Gnosis.Data.SQLite
{
    public class MediaDetailSearchTask
        : TaskBase<IEnumerable<IMediaDetail>>
    {
        public MediaDetailSearchTask(ILogger logger, ITagRepository tagRepository, ILinkRepository linkRepository, IAlgorithm algorithm, string pattern)
            : base(logger)
        {
            this.tagRepository = tagRepository;
            this.linkRepository = linkRepository;
            this.algorithm = algorithm;
            this.pattern = pattern;
        }

        private readonly ITagRepository tagRepository;
        private readonly ILinkRepository linkRepository;
        private readonly IAlgorithm algorithm;
        private readonly string pattern;

        private void TagsCallback(IEnumerable<ITag> tags)
        {
            try
            {
                var results = new List<IMediaDetail>();

                foreach (var tag in tags)
                {
                    var thumbnail = GetThumbnail(tag.Target);
                    results.Add(new MediaDetail(tag, thumbnail));
                }

                UpdateResults(results);
            }
            catch (Exception ex)
            {
                logger.Error("  TagsCallback", ex);
            }
        }

        private IImage GetThumbnail(Uri location)
        {
            var link = linkRepository.GetBySource(location, LinkType.ThumbnailImage).FirstOrDefault();

            return link != null ?
                new BitmapImage(link.Target)
                : null;
        }

        protected override void DoWork()
        {
            var task = tagRepository.Search(algorithm, pattern);
            task.AddResultsCallback(x => TagsCallback(x));
            task.StartSynchronously();
        }
    }
}

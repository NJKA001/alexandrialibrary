using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class MediaSummaryRepository
        : IMediaSummaryRepository
    {
        public MediaSummaryRepository(ILogger logger, IMediaRepository mediaRepository, ITagRepository tagRepository, ILinkRepository linkRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");

            this.logger = logger;
            this.mediaRepository = mediaRepository;
            this.tagRepository = tagRepository;
            this.linkRepository = linkRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaRepository mediaRepository;
        private readonly ITagRepository tagRepository;
        private readonly ILinkRepository linkRepository;

        #region IMediaSummaryRepository Members

        public Action Search(IMediaSummaryRequest request)
        {
            try
            {
                var isRunning = true;
                var tagsByTarget = new Dictionary<string, IList<ITag>>();
                var itemsByLocation = new Dictionary<string, IMediaSummary>();

                Action<IEnumerable<ITag>> tagCallback = tags =>
                    {
                        foreach (var tag in tags)
                        {
                            var key = tag.Target.ToString();
                            if (!tagsByTarget.ContainsKey(key))
                                tagsByTarget[key] = new List<ITag> { tag };
                            else
                                tagsByTarget[key].Add(tag);
                        }
                    };

                var cancelTagSearch = tagRepository.Search(Algorithm.Default, request.Pattern, tagCallback, () => { });

                if (isRunning)
                {
                }

                return () => { isRunning = false; cancelTagSearch(); };
            }
            catch (Exception ex)
            {
                logger.Error("  MediaSummaryRepository.Search", ex);
                throw;
            }
        }

        #endregion
    }
}

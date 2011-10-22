using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Image;

namespace Gnosis.Core
{
    public class MediaDetailRepository
        : IMediaDetailRepository
    {
        public MediaDetailRepository(ILogger logger, ITagRepository tagRepository, ILinkRepository linkRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");

            this.logger = logger;
            this.tagRepository = tagRepository;
            this.linkRepository = linkRepository;
        }

        private readonly ILogger logger;
        private readonly ITagRepository tagRepository;
        private readonly ILinkRepository linkRepository;

        private IImage GetThumbnail(Uri location)
        {
            var link = linkRepository.GetBySource(location, LinkType.ThumbnailImage).FirstOrDefault();

            return link != null ?
                new BitmapImage(link.Target)
                : null;
        }

        #region IMediaSummaryRepository Members

        public ITask<IEnumerable<IMediaDetail>> Search(IMediaDetailRequest request)
        {
            try
            {
                return null;

                //var isRunning = true;

                //Action<IEnumerable<ITag>> tagCallback = tags =>
                //    {
                //        try
                //        {
                //            logger.Debug("In tagCallback");
                //            if (isRunning)
                //            {
                //                foreach (var tag in tags)
                //                {
                //                    var thumbnail = GetThumbnail(tag.Target);
                //                    request.ItemCallback(new MediaDetail(tag, thumbnail));
                //                }
                //            }
                //        }
                //        catch (Exception callbackEx)
                //        {
                //            logger.Error("  Error in tagCallback", callbackEx);
                //        }
                //    };

                //logger.Debug("Before Search");
                //var task = tagRepository.Search(Algorithm.Default, request.Pattern); //, //tagCallback, () => { request.CompletedCallback(); });
                //task.AddResultsCallback(tagCallback);
                //task.AddCompletedCallback(() => request.CompletedCallback());

                //return () => { isRunning = false; task.Cancel(); };
            }
            catch (Exception ex)
            {
                logger.Error("  MediaDetailRepository.Search", ex);
                throw;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Gnosis.Links;

namespace Gnosis.Tasks
{
    public class CatalogMediaTask
        : TaskBase<IEnumerable<IMedia>>
    {
        public CatalogMediaTask(ILogger logger, IMediaFactory mediaFactory, ISpider spider, Uri target, TimeSpan delay, int maxErrors)
            : base(logger)
        {
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (spider == null)
                throw new ArgumentNullException("spider");
            if (target == null)
                throw new ArgumentNullException("target");

            this.mediaFactory = mediaFactory;
            this.spider = spider;
            this.target = target;
            this.delayMilliseconds = Convert.ToInt32(delay.TotalMilliseconds);
            this.maxErrors = maxErrors;
        }

        private readonly IMediaFactory mediaFactory;
        private readonly ISpider spider;
        private readonly Uri target;
        private readonly int delayMilliseconds;
        private readonly int maxErrors;
        private const int maxProgress = 100;

        private int progressCount = 0;
        private int errorCount = 0;

        private void AddProgress(string description)
        {
            logger.Debug(description);

            progressCount = progressCount < maxProgress ?
                progressCount + 1
                : 0;

            UpdateProgress(progressCount, maxProgress, description);
        }

        private void AddError(string description, Exception exception)
        {
            logger.Error(description, exception);

            errorCount++;

            UpdateError(errorCount, maxErrors, description, exception);

            if (maxErrors > 0 && errorCount > maxErrors)
                Fail();
        }

        private IMedia GetMedia(Uri location)
        {
            AddProgress("Media At: " + location.ToString());
            var item = new TaskItem(location, (uint)progressCount, location.ToString().ElideString(10), TimeSpan.Zero, Guid.Empty.ToUrn(), mediaFactory.Default.Type, false, false, null);
            UpdateItem(item);
            try
            {
                var medium = spider.GetMedia(location);
                if (medium != null)
                    return medium;
                else
                    logger.Warn("Media undefined for location: " + location.ToString());
            }
            catch (Exception ex)
            {
                var description = "Could not get media at: " + location.ToString();
                AddError(description, ex);
            }
            
            return null;
        }

        private IEnumerable<ILink> GetLinks(IMedia medium)
        {
            AddProgress("Links For: " + medium.Location.ToString());

            var links = Enumerable.Empty<ILink>();
            try
            {
                links = medium.GetLinks();
            }
            catch (Exception ex)
            {
                var description = "Could not get links for: " + medium.Location.ToString();
                AddError(description, ex);
            }
            return links;
        }

        private IEnumerable<ITag> GetTags(IMedia medium)
        {
            AddProgress("Tags For: " + medium.Location.ToString());
            
            var tags = Enumerable.Empty<ITag>();
            try
            {
                tags = medium.GetTags();
            }
            catch (Exception ex)
            {
                var description = "Could not get tags for: " + medium.Location.ToString();
                AddError(description, ex);
            }
            return tags;
        }

        private void SaveMedia(IMedia medium)
        {
            try
            {
                AddProgress("Saving Media At: " + medium.Location.ToString());
                spider.HandleMedia(medium);
            }
            catch (Exception ex)
            {
                var description = "Could not save media at: " + medium.Location.ToString();
                AddError(description, ex);
            }
        }

        private void SaveLinks(Uri location, IEnumerable<ILink> links)
        {
            AddProgress("Saving Links For: " + location.ToString());

            try
            {
                spider.HandleLinks(links);
            }
            catch (Exception ex)
            {
                var description = "Could not save links for: " + location.ToString();
                AddError(description, ex);
            }
        }

        private void SaveTags(Uri location, IEnumerable<ITag> tags)
        {
            AddProgress("Saving Tags For: " + location.ToString());

            try
            {
                spider.HandleTags(tags);
            }
            catch (Exception ex)
            {
                var description = "Could not saved tags for: " + location.ToString();
                AddError(description, ex);
            }
        }

        private void Process(Uri location)
        {
            if (!IsActive())
                return;

            var medium = GetMedia(location);
            if (medium == null)
            {
                logger.Warn("Media undefined or invalid at: " + location.ToString());
                return;
            }

            if (!IsActive())
                return;

            try
            {
                medium.Load();
            }
            catch (Exception ex)
            {
                var description = "Could not load media at: " + location.ToString();
                AddError(description, ex);
            }

            if (!IsActive())
                return;

            var links = GetLinks(medium);

            if (!IsActive())
                return;

            var tags = GetTags(medium);

            if (!IsActive())
                return;

            SaveMedia(medium);

            if (!IsActive())
                return;

            SaveLinks(location, links);

            if (!IsActive())
                return;

            SaveTags(location, tags);

            if (!IsActive())
                return;

            UpdateResults(new List<IMedia> { medium });

            foreach (var childLocation in links.Select(x => x.Target).DistinctBy(x => x.ToString()))
            {
                //System.Diagnostics.Debug.WriteLine(string.Format("*** Link of {0} is {1}", location, childLocation));
                if (!IsActive())
                    return;

                if (delayMilliseconds > 0)
                    Thread.Sleep(delayMilliseconds);

                Process(childLocation);
            }
        }

        protected override void DoWork()
        {
            Process(target);

            UpdateProgress(maxProgress, maxProgress, "Completed");
        }

        public Uri Target
        {
            get { return target; }
        }
    }
}

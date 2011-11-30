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
        public CatalogMediaTask(ILogger logger, ISpider spider, Uri target, TimeSpan delay, int maxErrors)
            : base(logger)
        {
            if (spider == null)
                throw new ArgumentNullException("spider");
            if (target == null)
                throw new ArgumentNullException("target");

            this.spider = spider;
            this.target = target;
            this.delayMilliseconds = Convert.ToInt32(delay.TotalMilliseconds);
            this.maxErrors = maxErrors;
        }

        private readonly ISpider spider;
        private readonly Uri target;
        private readonly int delayMilliseconds;
        private readonly int maxErrors;
        private const int maxProgress = 100;

        private int progressCount = 0;
        private int errorCount = 0;

        private void AddProgress(string description)
        {
            progressCount = progressCount < maxProgress ?
                progressCount + 1
                : 0;

            UpdateProgress(progressCount, maxProgress, description);
        }

        private void AddError(Exception ex)
        {
            errorCount++;
            if (maxErrors > 0 && errorCount > maxErrors)
                Fail(ex, "CatalogMediaTask Failed: Exceeded maximum number of allowable errors");
            else
                Error(ex);
        }

        private IMedia GetMedia(Uri location)
        {
            AddProgress("Media At: " + location.ToString());
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
                logger.Error("Could not get media at: " + location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not get links for: " + medium.Location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not get tags for: " + medium.Location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not save media at: " + medium.Location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not save links for: " + location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not saved tags for: " + location.ToString(), ex);
                AddError(ex);
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
                logger.Error("Could not load media at: " + location.ToString(), ex);
                AddError(ex);
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

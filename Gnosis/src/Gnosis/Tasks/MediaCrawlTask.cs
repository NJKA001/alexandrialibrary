using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Links;

namespace Gnosis.Tasks
{
    public class MediaCrawlTask
        : TaskBase<IEnumerable<IMedia>>
    {
        public MediaCrawlTask(ILogger logger, ISpider spider, Uri target)
            : base(logger)
        {
            if (spider == null)
                throw new ArgumentNullException("spider");
            if (target == null)
                throw new ArgumentNullException("target");

            this.spider = spider;
            this.target = target;
        }

        private readonly ISpider spider;
        private readonly Uri target;
        private int progressNumber = 0;
        private int errorCount = 0;
        private const int maxErrors = 10;

        private void AddProgress(string description)
        {
            progressNumber++;
            UpdateProgress(progressNumber, description);
        }

        private void AddError(Exception ex)
        {
            errorCount++;
            if (errorCount > maxErrors)
                Fail(ex, "Exceeded maximum number of allowable errors");
        }

        private IMedia ReadMedia(Uri location)
        {
            //var media = new List<IMedia>();

            //var distinctTargets = locations.DistinctBy(x => x.ToString());
            //foreach (var target in distinctTargets)
            //{

            AddProgress("Reading Media At: " + location.ToString());
            try
            {
                var medium = spider.ReadMedia(location);
                if (medium != null)
                    return medium;
                else
                    logger.Warn("Media undefined for location: " + location.ToString());
            }
            catch (Exception ex)
            {
                logger.Error("Could not read media at: " + location.ToString(), ex);
                AddError(ex);
            }
            
            return null;
        }

        private IEnumerable<ILink> ReadLinks(IMedia medium)
        {
            AddProgress("Reading Links For: " + medium.Location.ToString());

            var links = Enumerable.Empty<ILink>();
            try
            {
                links = medium.GetLinks();
            }
            catch (Exception ex)
            {
                logger.Error("Could not read links for: " + medium.Location.ToString(), ex);
                AddError(ex);
            }
            return links;
        }

        private IEnumerable<ITag> ReadTags(IMedia medium)
        {
            AddProgress("Reading Tags For: " + medium.Location.ToString());
            
            var tags = Enumerable.Empty<ITag>();
            try
            {
                tags = medium.GetTags();
            }
            catch (Exception ex)
            {
                logger.Error("Could not read tags for: " + medium.Location.ToString(), ex);
                AddError(ex);
            }
            return tags;
        }

        private void SaveMedia(IMedia medium)
        {
            try
            {
                AddProgress("Saving Media At: " + medium.Location.ToString());
                spider.SaveMedia(medium);
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
                spider.SaveLinks(links);
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
                spider.SaveTags(tags);
            }
            catch (Exception ex)
            {
                logger.Error("Could not saved tags for: " + location.ToString(), ex);
                AddError(ex);
            }
        }

        private void Process(Uri location)
        {
            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            //logger.Info("  Getting media for: " + location.ToString());
            var medium = ReadMedia(location);
            if (medium == null)
            {
                logger.Warn("Media undefined for location: " + location.ToString());
                return;
            }

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            var links = ReadLinks(medium);

            //logger.Debug("  link count: " + links.Count());

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            var tags = ReadTags(medium);

            //logger.Debug("  tag count: " + tags.Count());

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            SaveMedia(medium);

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            SaveLinks(location, links);

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            SaveTags(location, tags);

            BlockIfPaused();
            if (Status == TaskStatus.Cancelled)
                return;

            //logger.Debug("*** UPDATING RESULTS: " + medium.Location.ToString());
            UpdateResults(new List<IMedia> { medium });

            //Recursively call Process for each distinct child URL
            foreach (var childLocation in links.Select(x => x.Target).DistinctBy(x => x.ToString()))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("*** Link of {0} is {1}", location, childLocation));
                BlockIfPaused();
                if (Status == TaskStatus.Cancelled)
                    return;

                Process(childLocation);
            }
        }

        protected override void DoWork()
        {
            Process(target);
        }
    }
}

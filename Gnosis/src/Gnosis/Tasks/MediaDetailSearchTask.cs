using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Image;
using Gnosis.Links;
using Gnosis.Tags;

namespace Gnosis.Tasks
{
    public class MediaDetailSearchTask
        : TaskBase<IEnumerable<IMediaDetail>>
    {
        public MediaDetailSearchTask(ILogger logger, ITagRepository tagRepository, ILinkRepository linkRepository, string pattern)
            : base(logger)
        {
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            this.tagRepository = tagRepository;
            this.linkRepository = linkRepository;
            this.pattern = pattern;
        }

        private readonly ITagRepository tagRepository;
        private readonly ILinkRepository linkRepository;
        private readonly string pattern;

        private IEnumerable<IMediaDetail> GetResults(IEnumerable<ITag> tags)
        {
            var results = new List<IMediaDetail>();

            foreach (var tag in tags)
            {
                var thumbnail = GetThumbnail(tag.Target);
                results.Add(new MediaDetail(tag, thumbnail));
            }

            return results;
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
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.String, pattern)));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.StringArray, pattern)));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.Id3v1SimpleGenre, pattern)));

            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Americanized, TagDomain.String, pattern.ToAmericanizedString() + "%")));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Americanized, TagDomain.StringArray, pattern.ToAmericanizedString() + "%")));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Americanized, TagDomain.Id3v1SimpleGenre, pattern.ToAmericanizedString() + "%")));
        }
    }
}

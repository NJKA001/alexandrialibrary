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
                var artistThumbnail = GetArtistThumbnail(tag.Target);
                var albumThumbnail = GetAlbumThumbnail(tag.Target);
                results.Add(new MediaDetail(tag, artistThumbnail, albumThumbnail));
            }

            return results;
        }

        private IImage GetArtistThumbnail(Uri location)
        {
            var link = linkRepository.GetBySource(location, LinkType.ArtistThumbnail).FirstOrDefault();

            return link != null ?
                new JpegImage(link.Target)
                : null;
        }

        private IImage GetAlbumThumbnail(Uri location)
        {
            var link = linkRepository.GetBySource(location, LinkType.AlbumThumbnail).FirstOrDefault();

            return link != null ?
                new JpegImage(link.Target)
                : null;
        }

        protected override void DoWork()
        {
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.String, pattern)));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.Id3v1SimpleGenre, pattern)));

            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Americanized, TagDomain.String, pattern.ToAmericanizedString() + "%")));
            UpdateResults(GetResults(tagRepository.GetByAlgorithm(Algorithm.Americanized, TagDomain.Id3v1SimpleGenre, pattern.ToAmericanizedString() + "%")));
        }
    }
}

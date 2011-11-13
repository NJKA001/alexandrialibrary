using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public class ArtistSearchTask
        : TaskBase<IEnumerable<IArtistSummary>>
    {
        public ArtistSearchTask(ILogger logger, ITagRepository tagRepository, string pattern)
            : base(logger)
        {
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            this.tagRepository = tagRepository;
            this.pattern = pattern;
        }

        private readonly ITagRepository tagRepository;
        private readonly string pattern;

        protected override void DoWork()
        {
            UpdateResults(tagRepository.GetArtists(pattern));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public class MediaController
        : IMediaController
    {
        public MediaController(ILogger logger, IMediaDetailRepository mediaDetailRepository)
        {
            this.logger = logger;
            this.mediaDetailRepository = mediaDetailRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaDetailRepository mediaDetailRepository;

        public ITask<IEnumerable<IMediaDetail>> Search(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            var formatted = pattern.EndsWith("%") ? pattern : pattern + "%";

            return mediaDetailRepository.Search(formatted);
        }

    }
}

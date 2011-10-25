using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Tasks;
using Gnosis.Core.Utilities;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMediaDetailRepository
        : IMediaDetailRepository
    {
        public SQLiteMediaDetailRepository(ILogger logger, ITagRepository tagRepository, ILinkRepository linkRepository)
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

        public ITask<IEnumerable<IMediaDetail>> Search(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            return new MediaDetailSearchTask(logger, tagRepository, linkRepository, pattern);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tags;

namespace Gnosis.Tasks
{
    public class TagSearchTask
        : TaskBase<IEnumerable<ITag>>
    {
        public TagSearchTask(ILogger logger, ITagRepository repository, IAlgorithm algorithm, string pattern)
            : base(logger)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            this.repository = repository;
            this.algorithm = algorithm;
            this.pattern = pattern;
        }

        private readonly ITagRepository repository;
        private readonly IAlgorithm algorithm;
        private readonly string pattern;

        protected override void DoWork()
        {
            UpdateResults(repository.GetByAlgorithm(algorithm, TagDomain.String, pattern));
            UpdateResults(repository.GetByAlgorithm(algorithm, TagDomain.Id3v1SimpleGenre, pattern));
        }
    }
}

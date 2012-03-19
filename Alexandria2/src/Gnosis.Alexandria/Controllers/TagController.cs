using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public class TagController
        : ITagController
    {
        public TagController(ILogger logger, ITagRepository tagRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");

            this.logger = logger;
            this.tagRepository = tagRepository;
        }

        private readonly ILogger logger;
        private readonly ITagRepository tagRepository;

        public IEnumerable<ITagViewModel> GetTags(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            var tagViewModels = new List<ITagViewModel>();

            foreach (var tag in tagRepository.GetByTarget(target))
            {
                tagViewModels.Add(new TagViewModel(tag));
            }

            return tagViewModels;
        }
    }
}

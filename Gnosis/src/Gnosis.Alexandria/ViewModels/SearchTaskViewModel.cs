using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class SearchTaskViewModel
        : TaskViewModel<IMediaItem>
    {
        public SearchTaskViewModel(ILogger logger, MediaItemSearchTask task, string search)
            : base(logger, task, "Search", search, "pack://application:,,,/Images/Sphinx.png")
        {
            searchTask = task;
        }

        private readonly MediaItemSearchTask searchTask;

        public void SetPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            searchTask.SetPattern(pattern);
        }
    }
}

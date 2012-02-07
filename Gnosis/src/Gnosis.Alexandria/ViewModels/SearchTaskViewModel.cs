using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class SearchTaskViewModel
        : TaskViewModel<IMetadata>
    {
        public SearchTaskViewModel(ILogger logger, ISearchTask task, string search)
            : base(logger, task, search, GetDescription(search), "pack://application:,,,/Images/sphinx_circle.png")
        {
            searchTask = task;
        }

        private readonly ISearchTask searchTask;

        private static string GetDescription(string search)
        {
            return string.Format("Search: " + search);
        }

        public SearchFilters Filters
        {
            get { return searchTask.Filters; }
            set { searchTask.Filters = value; }
        }

        public void SetPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            searchTask.SetPattern(pattern);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class SearchTaskViewModel
        : TaskViewModel
    {
        public SearchTaskViewModel(ILogger logger, ITask task, string search)
            : base(logger, task, "Search: " + search, "pack://application:,,,/Images/Eye_Of_Horus.png")
        {
        }
    }
}

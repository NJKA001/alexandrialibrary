using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels.Commands
{
    public class ArtistCommandViewModel
        : CommandViewModel
    {
        public ArtistCommandViewModel()
            : base("Artists", "The individuals and groups who create and contribute to media", "pack://application:,,,/Images/crown.png")
        {
        }

        protected override void DoExecute(ITaskController taskController, TaskResultView taskResultView)
        {
            taskResultView.MarqueeBrowse(MetadataCategory.Artist, Name, Icon);
            //var viewModel = taskController.GetSearchViewModel(string.Empty);
            //viewModel.Filters = SearchFilters.Artists;
            //taskResultView.Search(viewModel);
        }
    }
}

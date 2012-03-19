using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.ViewModels.Commands
{
    public class AlbumCommandViewModel
        : CommandViewModel
    {
        public AlbumCommandViewModel()
            : base("Albums", "Collections of media that artists have named and released", "pack://application:,,,/Images/scarab.gif")
        {
        }

        protected override void DoExecute(ITaskController taskController, TaskResultView taskResultView)
        {
            taskResultView.MarqueeBrowse(MetadataCategory.Album, Name, Icon);
        }
    }
}

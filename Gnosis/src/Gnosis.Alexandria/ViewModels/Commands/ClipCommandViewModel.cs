using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.ViewModels.Commands
{
    public class ClipCommandViewModel
        : CommandViewModel
    {
        public ClipCommandViewModel()
            : base("Clips", "Movies, TV shows, music videos and other video media", "pack://application:,,,/Images/eye_of_horus.jpg")
        {
        }

        protected override void DoExecute(ITaskController taskController, TaskResultView taskResultView)
        {
            taskResultView.MarqueeBrowse(MetadataCategory.Clip, Name, Icon);
        }
    }
}

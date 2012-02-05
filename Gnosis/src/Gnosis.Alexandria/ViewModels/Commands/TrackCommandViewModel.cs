using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.ViewModels.Commands
{
    public class TrackCommandViewModel
        : CommandViewModel
    {
        public TrackCommandViewModel()
            : base("Tracks", "Music, spoken word, sounds and other audio media", "pack://application:,,,/Images/lyre.jpg")
        {
        }

        protected override void DoExecute(ITaskController taskController, TaskResultView taskResultView)
        {
            taskResultView.MarqueeBrowse(MetadataCategory.Track, Name, Icon);
        }
    }
}

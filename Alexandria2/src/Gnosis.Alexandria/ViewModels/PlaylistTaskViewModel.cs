using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistTaskViewModel
        : TaskViewModel<TaskItem>
    {
        public PlaylistTaskViewModel(ILogger logger, PlaylistTask task, string playlistName, object icon)
            : base(logger, task, playlistName, "Playlist", GetIcon(icon))
        {
            playlistTask = task;
        }

        private readonly PlaylistTask playlistTask;

        private static object GetIcon(object icon)
        {
            return icon != null ? icon : "pack://application:,,,/Images/play-simple.png";
        }
    }
}

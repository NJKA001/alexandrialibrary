using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistItemViewModel
        : MediaItemViewModel, IPlaylistItemViewModel
    {
        public PlaylistItemViewModel(IPlaylistItem playlistItem)
            : base(playlistItem, "PLAYLIST ITEM", "pack://application:,,,/Images/play-simple.png")
        {
        }

        private bool isPlaying;

        public object PlaybackIcon
        {
            get
            {
                if (isPlaying)
                    return "pack://application:,,,/Images/play-simple.png";

                var type = item.TargetType.ToString();

                if (type == MediaType.AudioMpeg.ToString())
                    return "pack://application:,,,/Images/File Audio MP3-01.png";

                return "pack://application:,,,/Images/File Audio-01.png";
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
                OnPropertyChanged("PlaybackIcon");
            }
        }

        public IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext)
        {
            return this;
        }

        public TaskItem ToTaskItem()
        {
            return new TaskItem(item.Location, item.Number, item.Name, item.Duration, item.Target, item.TargetType, true, true, Image);
        }
    }
}

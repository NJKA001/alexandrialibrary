using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

using Gnosis.Application.Vendor;
using Gnosis.Alexandria.Controllers;
using Gnosis.Metadata;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistItemViewModel
        : MediaItemViewModel, IPlaylistItemViewModel
    {
        public PlaylistItemViewModel(IMediaItemController controller, IPlaylistItem playlistItem)
            : base(controller, playlistItem, "PLAYLIST ITEM", "pack://application:,,,/Images/play-simple.png")
        {
        }

        private bool isPaused;
        private bool isStopped;
        private bool isPlaying;

        public object PlaybackIcon
        {
            get
            {
                if (isStopped)
                    return "pack://application:,,,/Images/stop-simple.png";

                if (isPaused)
                    return "pack://application:,,,/Images/pause-simple.png";

                if (isPlaying)
                    return "pack://application:,,,/Images/play-simple.png";

                var type = item.TargetType.ToString();

                //if (type == MediaType.AudioMpeg.ToString())
                //    return "pack://application:,,,/Images/File Audio MP3-01.png";
                //else if (type == MediaType.VideoAvi.ToString())
                //    return "pack://application:,,,/Images/File Video AVI-01.png";
                if (item.TargetType.Name.StartsWith("audio/"))
                    return "pack://application:,,,/Images/File Audio-01.png";
                else if (item.TargetType.Name.StartsWith("video/"))
                    return "pack://application:,,,/Images/File Video-01.png";

                return "pack://application:,,,/Images/File Audio-01.png";
            }
        }

        public bool IsPaused
        {
            get { return isPaused; }
            set
            {
                isPaused = value;
                if (isPaused)
                {
                    isPlaying = false;
                    OnPropertyChanged("IsPlaying");
                    isStopped = false;
                    OnPropertyChanged("IsStopped");
                }

                OnPropertyChanged("IsPaused");
                OnPropertyChanged("PlaybackIcon");
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                if (isPlaying)
                {
                    isPaused = false;
                    OnPropertyChanged("IsPaused");
                    isStopped = false;
                    OnPropertyChanged("IsStopped");
                }

                OnPropertyChanged("IsPlaying");
                OnPropertyChanged("PlaybackIcon");
            }
        }

        public bool IsStopped
        {
            get { return isStopped; }
            set
            {
                isStopped = value;
                if (isStopped)
                {
                    isPaused = false;
                    OnPropertyChanged("IsPaused");
                    isPlaying = false;
                    OnPropertyChanged("IsPlaying");
                }

                OnPropertyChanged("IsStopped");
                OnPropertyChanged("PlaybackIcon");
            }
        }

        public void ClearStatus()
        {
            isPaused = false;
            isPlaying = false;
            isStopped = false;

            OnPropertyChanged("IsPaused");
            OnPropertyChanged("IsPlaying");
            OnPropertyChanged("IsStopped");
            OnPropertyChanged("PlaybackIcon");
        }

        public IPlaylistViewModel ToPlaylist(ISecurityContext securityContext, IMediaFactory mediaFactory)
        {
            var date = DateTime.Now.ToUniversalTime();

            var builder = new MediaItemBuilder<IPlaylist>(securityContext, mediaFactory)
                .Identity(Name, Summary, date, date, 0)
                .Thumbnail(item.Thumbnail, item.ThumbnailData);

            var playlist = builder.ToMediaItem();
            var playlistItems = new List<IPlaylistItemViewModel> { ToPlaylistItem(securityContext, mediaFactory, 1) };
            return new PlaylistViewModel(controller, playlist, playlistItems);
        }

        public IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext, IMediaFactory mediaFactory, uint number)
        {
            return this;
        }

        public TaskItem ToTaskItem()
        {
            return new TaskItem(item.Location, item.Number, item.Name, item.Duration, item.Target, item.TargetType, true, true, Image);
        }
    }
}

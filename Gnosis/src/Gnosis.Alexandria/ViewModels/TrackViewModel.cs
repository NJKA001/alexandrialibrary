using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.ViewModels
{
    public class TrackViewModel
        : MediaItemViewModel, ITrackViewModel
    {
        public TrackViewModel(IMediaItemController controller, ITrack track)
            : base(controller, track, "TRACK", GetIcon(track))
        {
        }

        private static object GetIcon(ITrack track)
        {
            if (track.TargetType == MediaType.AudioMpeg)
            {
                return "pack://application:,,,/Images/File Audio MP3-01.png";
            }

            return "pack://application:,,,/Images/File Audio-01.png";
        }

        private bool isPaused;
        private bool isPlaying;
        private bool isStopped;

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

                var type = TargetType.ToString();

                if (type == MediaType.AudioMpeg.ToString())
                    return "pack://application:,,,/Images/File Audio MP3-01.png";

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

        public IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext)
        {
            var playlistItem = new GnosisPlaylistItem(Name, Summary, item.FromDate, Number, Duration, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, item.Thumbnail, item.ThumbnailData);
            return new PlaylistItemViewModel(controller, playlistItem);
        }
    }
}

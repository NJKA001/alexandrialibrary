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

        private bool isPlaying;

        public object PlaybackIcon
        {
            get
            {
                if (isPlaying)
                    return "pack://application:,,,/Images/play-simple.png";

                var type = TargetType.ToString();

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
            var playlistItem = new GnosisPlaylistItem(Name, Summary, item.FromDate, Number, Duration, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, item.Thumbnail, item.ThumbnailData);
            return new PlaylistItemViewModel(controller, playlistItem);
        }
    }
}

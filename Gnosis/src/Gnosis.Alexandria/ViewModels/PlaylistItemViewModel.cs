using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistItemViewModel
        : IPlaylistItemViewModel
    {
        public PlaylistItemViewModel(IPlaylistItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.item = item;
        }

        private readonly IPlaylistItem item;

        private bool isPlaying;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Id
        {
            get { return item.Location; }
        }

        public uint Number
        {
            get { return item.Number; }
        }

        public string Name
        {
            get { return item.Name; }
        }

        public string Duration
        {
            get { return item.Duration.ToFormattedString(); }
        }

        public uint Height
        {
            get { return item.Height; }
        }

        public uint Width
        {
            get { return item.Width; }
        }

        public string CreatorName
        {
            get { return item.CreatorName; }
        }

        public string CatalogName
        {
            get { return item.CatalogName; }
        }

        public object Image
        {
            get
            {
                return item.ThumbnailData != null && item.ThumbnailData.Length > 0 ?
                    (object)item.ThumbnailData
                    : item.Thumbnail;
            }
        }

        public Visibility DurationVisibility
        {
            get { return item.Duration > TimeSpan.Zero ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility SizeVisibility
        {
            get { return Height > 0 && Width > 0 ? Visibility.Visible : Visibility.Collapsed; }
        }

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

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskItem ToTaskItem()
        {
            return new TaskItem(item.Location, item.Number, item.Name, item.Duration, item.Target, item.TargetType, true, true, Image);
        }
    }
}

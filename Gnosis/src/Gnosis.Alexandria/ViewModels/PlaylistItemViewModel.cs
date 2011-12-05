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

        public string Title
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
    }
}

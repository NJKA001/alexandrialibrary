using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public class Track : ITrack
    {
        public Track()
        {
        }

        private string path;
        private string imagePath = string.Empty;
        private ICollection<byte> imageData;
        private string title;
        private string artist;
        private string album;
        private uint number;
        private DateTime releaseDate;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ITrack Members

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ICollection<byte> ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;
                    OnPropertyChanged("ImageData");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public object ImageSource
        {
            get
            {
                return !string.IsNullOrEmpty(imagePath) ? (object)imagePath : imageData;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Artist
        {
            get
            {
                return artist;
            }
            set
            {
                if (artist != value)
                {
                    artist = value;
                    OnPropertyChanged("Artist");
                }
            }
        }

        public string Album
        {
            get
            {
                return album;
            }
            set
            {
                if (album != value)
                {
                    album = value;
                    OnPropertyChanged("Album");
                }
            }
        }

        public uint Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if (releaseDate != value)
                {
                    releaseDate = value;
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

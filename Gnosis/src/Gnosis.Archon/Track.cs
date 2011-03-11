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
            id = Guid.NewGuid();
        }

        public Track(Guid id)
        {
            this.id = id;
        }

        private readonly Guid id;
        private string path;
        private string imagePath = string.Empty;
        private ICollection<byte> imageData;
        private string title = "Untitled";
        private string artist = "Unknown Artist";
        private string album = "Unknown Album";
        private uint trackNumber = 0;
        private uint discNumber = 0;
        private string genre = "Unknown Genre";
        private DateTime releaseDate = new DateTime(2000, 1, 1);
        private bool isSelected;
        private string playbackStatus;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ITrack Members

        public Guid Id
        {
            get { return id; }
        }

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

        public uint TrackNumber
        {
            get
            {
                return trackNumber;
            }
            set
            {
                if (trackNumber != value)
                {
                    trackNumber = value;
                    OnPropertyChanged("TrackNumber");
                }
            }
        }

        public uint DiscNumber
        {
            get
            {
                return discNumber;
            }
            set
            {
                if (discNumber != value)
                {
                    discNumber = value;
                    OnPropertyChanged("DiscNumber");
                }
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged("Genre");
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

        public string ReleaseYear
        {
            get { return releaseDate.Year.ToString(); }
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

        public string PlaybackStatus
        {
            get { return playbackStatus; }
            set
            {
                if (playbackStatus != value)
                {
                    playbackStatus = value;
                    OnPropertyChanged("PlaybackStatus");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

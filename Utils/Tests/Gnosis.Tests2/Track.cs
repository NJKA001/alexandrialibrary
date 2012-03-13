using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Track
        : Entity
    {
        private Artist artist;
        private Album album;
        private ushort number;
        private ushort duration;

        public Artist Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                NotifyPropertyChanged("Artist");
            }
        }

        public Album Album
        {
            get { return album; }
            set
            {
                album = value;
                NotifyPropertyChanged("Album");
            }
        }

        public ushort Number
        {
            get { return number; }
            set
            {
                number = value;
                NotifyPropertyChanged("Number");
            }
        }

        public ushort Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }
    }
}

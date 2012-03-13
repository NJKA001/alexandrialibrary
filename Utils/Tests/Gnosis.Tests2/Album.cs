using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Album
        : Entity
    {
        private Artist artist;
        private int year;
        private readonly ObservableCollection<Track> tracks = new ObservableCollection<Track>();

        public Artist Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                NotifyPropertyChanged("Artist");
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                NotifyPropertyChanged("Year");
            }
        }

        public IEnumerable<Track> Tracks { get { return tracks; } }

        public void AddTrack(Track track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (tracks.Contains(track))
                return;

            tracks.Add(track);
        }

        public void RemoveTrack(Track track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (!tracks.Contains(track))
                return;

            tracks.Remove(track);
        }
    }
}

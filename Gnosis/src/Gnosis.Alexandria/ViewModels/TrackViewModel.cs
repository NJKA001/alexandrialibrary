using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class TrackViewModel
        : ITrackViewModel
    {
        public TrackViewModel(Uri track, string title, uint number, TimeSpan duration, DateTime date, Uri artist, string artistName, Uri album, string albumTitle, Uri thumbnail, byte[] thumbnailData, string bio)
        {
            if (track == null)
                throw new ArgumentNullException("");
            if (title == null)
                throw new ArgumentNullException("");
            if (artist == null)
                throw new ArgumentNullException("");
            if (artistName == null)
                throw new ArgumentNullException("");
            if (album == null)
                throw new ArgumentNullException("");
            if (albumTitle == null)
                throw new ArgumentNullException("");

            this.track = track;
            this.title = title;
            this.number = number;
            this.duration = duration;
            this.date = date;
            this.artist = artist;
            this.artistName = artistName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
            this.bio = bio;
        }

        private readonly Uri track;
        private readonly string title;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly DateTime date;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly string bio;

        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Track
        {
            get { return track; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Number
        {
            get { return number > 0 ? number.ToString() : "unknown"; }
        }

        public string Duration
        {
            get { return duration.ToFormattedString(); }
        }

        public string Year
        {
            get { return date.ToString("yyyy"); }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public Uri Album
        {
            get { return album; }
        }

        public string AlbumTitle
        {
            get { return albumTitle; }
        }

        public string Bio
        {
            get { return bio; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
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

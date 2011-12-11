using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class ArtistViewModel
        : IArtistViewModel
    {
        public ArtistViewModel(Uri artist, string name, string summary, DateTime startDate, DateTime endDate, Uri thumbnail, byte[] thumbnailData)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (name == null)
                throw new ArgumentNullException("name");

            this.artist = artist;
            this.name = name;
            this.summary = summary;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;

            var years = new StringBuilder();
            if (startDate != DateTime.MinValue)
                years.AppendFormat("{0} - ", startDate.Year);
            else
                years.Append("Unknown - ");
            if (endDate != DateTime.MaxValue)
                years.Append(endDate.Year);
            else
                years.Append("Present");

            this.years = years.ToString();
        }

        private readonly Uri artist;
        private readonly string name;
        private readonly string summary;
        private readonly string years;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly ObservableCollection<ILink> links = new ObservableCollection<ILink>();
        private readonly ObservableCollection<ITag> tags = new ObservableCollection<ITag>();
        private readonly ObservableCollection<IAlbumViewModel> albums = new ObservableCollection<IAlbumViewModel>();

        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Summary
        {
            get { return summary; }
        }

        public string Years
        {
            get { return years; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
        }

        public IEnumerable<ILink> Links
        {
            get { return links; }
        }

        public IEnumerable<ITag> Tags
        {
            get { return tags; }
        }

        public IEnumerable<IAlbumViewModel> Albums
        {
            get { return albums; }
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
        
        public void AddLink(ILink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            links.Add(link);
        }

        public void RemoveLink(ILink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            links.Remove(link);
        }

        public void AddTag(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            tags.Add(tag);
        }

        public void RemoveTag(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            tags.Remove(tag);
        }

        public void AddAlbum(IAlbumViewModel album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            albums.Add(album);
        }

        public void RemoveAlbum(IAlbumViewModel album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            albums.Remove(album);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

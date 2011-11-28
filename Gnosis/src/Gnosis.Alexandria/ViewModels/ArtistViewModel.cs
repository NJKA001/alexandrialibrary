using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class ArtistViewModel
        : IArtistViewModel
    {
        public ArtistViewModel(Uri artist, string name, DateTime startDate, DateTime endDate, Uri thumbnail, byte[] thumbnailData, string bio)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (name == null)
                throw new ArgumentNullException("name");

            this.artist = artist;
            this.name = name;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
            this.bio = bio ?? string.Empty;

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
        private readonly string years;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly string bio;
        private readonly ObservableCollection<ILink> links = new ObservableCollection<ILink>();
        private readonly ObservableCollection<ITag> tags = new ObservableCollection<ITag>();
        private readonly ObservableCollection<IAlbumViewModel> albums = new ObservableCollection<IAlbumViewModel>();

        public Uri Artist
        {
            get { return artist; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Years
        {
            get { return years; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
        }

        public string Bio
        {
            get { return bio; }
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
    }
}

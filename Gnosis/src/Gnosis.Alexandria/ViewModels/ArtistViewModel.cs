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
        public ArtistViewModel(string name, DateTime startDate, DateTime endDate, IImage image, string bio)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (image == null)
                throw new ArgumentNullException("image");

            this.name = name;
            this.image = image;
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

        private readonly string name;
        //private readonly DateTime startDate;
        //private readonly DateTime endDate;
        private readonly string years;
        private readonly IImage image;
        private readonly string bio;
        private readonly ObservableCollection<ILink> links = new ObservableCollection<ILink>();
        private readonly ObservableCollection<ITag> tags = new ObservableCollection<ITag>();
        private readonly ObservableCollection<IAlbumViewModel> albums = new ObservableCollection<IAlbumViewModel>();

        public string Name
        {
            get { return name; }
        }

        public string Years
        {
            get { return years; }
        }

        public IImage Image
        {
            get { return image; }
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

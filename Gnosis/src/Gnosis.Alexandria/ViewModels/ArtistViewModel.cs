using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public class ArtistViewModel
        : MediaItemViewModel, IArtistViewModel
    {
        public ArtistViewModel(IMediaItemController controller, IArtist artist)
            : base(controller, artist, "ARTIST", "pack://application:,,,/Images/artist.png")
        {
            var years = new StringBuilder();
            if (item.FromDate != DateTime.MinValue)
                years.AppendFormat("{0} - ", item.FromDate.Year);
            else
                years.Append("Unknown - ");
            if (item.ToDate != DateTime.MaxValue)
                years.Append(item.ToDate.Year);
            else
                years.Append("Present");

            this.years = years.ToString();
        }

        private readonly string years;
        private readonly ObservableCollection<IAlbumViewModel> albums = new ObservableCollection<IAlbumViewModel>();

        public override string Years
        {
            get { return years; }
        }

        public IEnumerable<IAlbumViewModel> Albums
        {
            get { return albums; }
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

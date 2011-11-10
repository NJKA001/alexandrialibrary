using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class AlbumViewModel
        : IAlbumViewModel
    {
        public AlbumViewModel(string title, DateTime date, IImage image)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (image == null)
                throw new ArgumentNullException("image");

            this.title = title;
            this.date = date;
            this.image = image;
        }

        private readonly string title;
        private readonly DateTime date;
        private readonly IImage image;

        public string Title
        {
            get { return title; }
        }

        public string Year
        {
            get { return date.Year.ToString(); }
        }

        public IImage Image
        {
            get { return image; }
        }
    }
}

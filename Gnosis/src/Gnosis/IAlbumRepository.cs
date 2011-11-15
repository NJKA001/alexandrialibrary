using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAlbumRepository
    {
        void Initialize();
        void Save(IEnumerable<IAlbum> albums);
        void Delete(IEnumerable<Uri> albums);

        IAlbum GetByLocation(Uri location);
        IEnumerable<IAlbum> GetByArtist(Uri artist);
        IEnumerable<IAlbum> GetByTitle(string title);
    }
}

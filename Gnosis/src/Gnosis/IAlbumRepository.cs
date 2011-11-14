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
        void Delete(IEnumerable<Guid> albums);

        IAlbum GetById(Guid id);
        IEnumerable<IAlbum> GetByArtist(Guid artist);
        IEnumerable<IAlbum> GetByTitle(string title);
    }
}

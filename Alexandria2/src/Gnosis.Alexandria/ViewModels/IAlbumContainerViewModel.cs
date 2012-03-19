using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumContainerViewModel
    {
        IEnumerable<IAlbumViewModel> Albums { get; }

        void AddAlbum(IAlbumViewModel album);
        void RemoveAlbum(IAlbumViewModel album);
    }
}

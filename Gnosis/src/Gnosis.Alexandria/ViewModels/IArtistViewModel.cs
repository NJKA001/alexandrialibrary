using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IArtistViewModel
        : IMediaItemViewModel
    {
        Uri Artist { get; }
        string Name { get; }
        string Years { get; }
        object Image { get; }
        string Bio { get; }
        IEnumerable<ILink> Links { get; }
        IEnumerable<ITag> Tags { get; }
        IEnumerable<IAlbumViewModel> Albums { get; }

        void AddLink(ILink link);
        void RemoveLink(ILink link);
        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
        void AddAlbum(IAlbumViewModel album);
        void RemoveAlbum(IAlbumViewModel album);
    }
}

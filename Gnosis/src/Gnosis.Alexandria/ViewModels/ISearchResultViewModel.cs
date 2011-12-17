using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ISearchResultViewModel
        : INotifyPropertyChanged
    {
        Uri Id { get; }
        string Name { get; }
        string Years { get; }
        string Type { get; }

        Visibility CreatorVisibility { get; }
        string CreatorName { get; }
        Visibility CatalogVisibility { get; }
        string CatalogName { get; }

        object Icon { get; }
        object Image { get; }

        string Summary { get; }
        
        Visibility TracksVisibility { get; }
        IEnumerable<ITrackViewModel> Tracks { get; }

        Visibility ClipsVisibility { get; }
        IEnumerable<IClipViewModel> Clips { get; }

        Visibility AlbumsVisibility { get; }
        IEnumerable<IAlbumViewModel> Albums { get; }

        IEnumerable<ILinkViewModel> Links { get; }
        void AddLink(ILinkViewModel link);
        void RemoveLink(ILinkViewModel link);

        IEnumerable<ITagViewModel> Tags { get; }
        void AddTag(ITagViewModel tag);
        void RemoveTag(ITagViewModel tag);
        IEnumerable<ITag> GetSystemTags();

        bool IsClosed { get; set; }
        bool IsSelected { get; set; }

        void AddCloseCallback(Action<ISearchResultViewModel> callback);
        void AddAlbum(IAlbumViewModel album);
        void AddTrack(ITrackViewModel track);
        void AddClip(IClipViewModel clip);

        void UpdateThumbnail(IMediaItemController controller, Uri thumbnail, byte[] thumbnailData);
        void UpdateSummary(IMediaItemController controller, string summary);
    }
}

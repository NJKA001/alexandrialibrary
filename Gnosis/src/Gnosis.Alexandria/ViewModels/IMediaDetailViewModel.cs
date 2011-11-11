using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IMediaDetailViewModel
        : INotifyPropertyChanged
    {
        IMediaDetail Detail { get; }
        string Type { get; }
        string Value { get; }
        object ArtistThumbnail { get; }
        object CollectionThumbnail { get; }

        bool IsSelected { get; set; }
    }
}

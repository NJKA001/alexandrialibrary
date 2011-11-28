using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITrackViewModel
        : IMediaItemViewModel
    {
        Uri Track { get; }
        string Title { get; }
        string Number { get; }
        string Duration { get; }
        string Year { get; }
        Uri Artist { get; }
        string ArtistName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }
        string Bio { get; }
        object Image { get; }
    }
}

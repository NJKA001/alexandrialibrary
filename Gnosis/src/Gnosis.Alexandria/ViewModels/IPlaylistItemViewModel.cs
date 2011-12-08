using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlaylistItemViewModel
        : INotifyPropertyChanged
    {
        Uri Id { get; }
        uint Number { get; }
        string Name { get; }
        string Duration { get; }
        uint Height { get; }
        uint Width { get; }
        string CreatorName { get; }
        string CatalogName { get; }
        object Image { get; }
        object PlaybackIcon { get; }
        Visibility DurationVisibility { get; }
        Visibility SizeVisibility { get; }

        bool IsPlaying { get; set; }
        bool IsSelected { get; set; }

        TaskItem ToTaskItem();
    }
}

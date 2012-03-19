using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlaylistItemContainerViewModel
    {
        IPlaylistItemViewModel CurrentPlaylistItem { get; }
        IEnumerable<IPlaylistItemViewModel> PlaylistItems { get; }

        void AddPlaylistItem(IPlaylistItemViewModel playlistItem);
        void InsertPlaylistItem(int index, IPlaylistItemViewModel item);
        void RemovePlaylistItem(IPlaylistItemViewModel playlistItem);
        void SelectPlaylistItem(IPlaylistItemViewModel item);
        void SelectPreviousPlaylistItem();
        void SelectNextPlaylistItem();

        TaskItem GetPreviousTaskItem();
        TaskItem GetCurrentTaskItem();
        TaskItem GetNextTaskItem();
    }
}

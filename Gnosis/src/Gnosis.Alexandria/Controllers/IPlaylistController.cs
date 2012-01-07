using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public interface IPlaylistController
    {
        IPlaylistViewModel Playlist { get; }
        IPlaylistItemViewModel CurrentItem { get; }
        IEnumerable<IPlaylistItemViewModel> Items { get; }

        void AddStartedCallback(Action<ITaskViewModel> callback);
        void AddPausedCallback(Action<ITaskViewModel> callback);
        void AddResumedCallback(Action<ITaskViewModel> callback);
        void AddStoppedCallback(Action<ITaskViewModel> callback);
        void AddItemChangedCallback(Action<TaskItem> callback);
        void AddResultsCallback(Action<TaskItem> callback);

        void SelectItem(IPlaylistItemViewModel item);
        void SelectPreviousItem();
        void SelectNextItem();
    }
}

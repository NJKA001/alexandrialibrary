using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlaylistViewModel
        : INotifyPropertyChanged
    {
        Uri Id { get; }
        string CreatorName { get; }
        string Name { get; }
        string Number { get; }
        string Year { get; }
        object Image { get; }

        IEnumerable<IPlaylistItemViewModel> Items { get; }
        IPlaylistItemViewModel CurrentItem { get; }

        void AddItem(IPlaylistItemViewModel item);
        void InsertItem(int index, IPlaylistItemViewModel item);
        void RemoveItem(IPlaylistItemViewModel item);
        void PreviousItem();
        void NextItem();

        TaskItem GetPreviousTaskItem();
        TaskItem GetCurrentTaskItem();
        TaskItem GetNextTaskItem();
    }
}

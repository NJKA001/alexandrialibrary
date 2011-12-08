using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistViewModel
        : IPlaylistViewModel
    {
        public PlaylistViewModel(IPlaylist playlist, IEnumerable<IPlaylistItemViewModel> items)
        {
            if (playlist == null)
                throw new ArgumentNullException("playlist");
            if (items == null)
                throw new ArgumentNullException("items");

            this.playlist = playlist;
            this.items = new ObservableCollection<IPlaylistItemViewModel>(items);
            currentItem = this.items.FirstOrDefault();
        }

        private readonly IPlaylist playlist;
        private readonly ObservableCollection<IPlaylistItemViewModel> items;
        private IPlaylistItemViewModel currentItem;
        private int currentIndex = 0;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Id
        {
            get { return playlist.Location; }
        }

        public string CreatorName
        {
            get { return playlist.CreatorName; }
        }

        public string Name
        {
            get { return playlist.Name; }
        }

        public string Number
        {
            get { return playlist.Number > 0 ? playlist.Number.ToString() : string.Empty; }
        }

        public string Year
        {
            get { return playlist.FromDate.Year.ToString(); }
        }

        public object Image
        {
            get { return playlist.ThumbnailData != null && playlist.ThumbnailData.Length > 0 ?
                (object)playlist.ThumbnailData
                : playlist.Thumbnail;
            }
        }

        public IEnumerable<IPlaylistItemViewModel> Items
        {
            get { return items; }
        }

        public IPlaylistItemViewModel CurrentItem
        {
            get { return currentItem; }
            private set
            {
                currentItem = value;
                currentIndex = items.IndexOf(currentItem);
                OnPropertyChanged("CurrentItem");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            items.Add(item);
        }

        public void InsertItem(int index, IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            items.Insert(index, item);
        }

        public void RemoveItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (items.Contains(item))
                items.Remove(item);
        }

        public void PreviousItem()
        {
            if (items.Count > 0)
            {
                if (items.Count == 1)
                {
                    CurrentItem = currentItem;
                }
                else
                {
                    var index = items.IndexOf(currentItem);
                    switch (index)
                    {
                        case -1:
                            CurrentItem = items.FirstOrDefault();
                            return;
                        case 0:
                            CurrentItem = items.LastOrDefault();
                            return;
                        default:
                            CurrentItem = items.ElementAtOrDefault(index -1);
                            return;
                    }
                }
            }
        }

        public void NextItem()
        {
            if (items.Count > 0)
            {
                if (items.Count == 1)
                {
                    CurrentItem = currentItem;
                }
                else
                {
                    var index = items.IndexOf(currentItem);
                    if (index == -1)
                    {
                        CurrentItem = items.FirstOrDefault();
                        return;
                    }
                    else
                    {
                        var lastIndex = items.Count - 1;
                        if (index < lastIndex)
                        {
                            CurrentItem = items.ElementAtOrDefault(index + 1);
                        }
                        else
                        {
                            CurrentItem = items.FirstOrDefault();
                        }
                    }
                }
            }
        }

        public TaskItem GetPreviousTaskItem()
        {
            PreviousItem();

            return GetCurrentTaskItem();
        }

        public TaskItem GetCurrentTaskItem()
        {
            return currentItem != null ? currentItem.ToTaskItem() : default(TaskItem);
        }

        public TaskItem GetNextTaskItem()
        {
            NextItem();

            return GetCurrentTaskItem();
        }
    }
}

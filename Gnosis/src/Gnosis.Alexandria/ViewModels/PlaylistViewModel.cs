using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistViewModel
        : MediaItemViewModel, IPlaylistViewModel
    {
        //public PlaylistViewModel(IMediaItemController controller, IPlaylist playlist, IEnumerable<IPlaylistItemViewModel> playlistItems)
        //    : this(controller, playlist, playlistItems, "pack://application:,,,/Images/play-simple.png")
        //{
        //}

        public PlaylistViewModel(IMetadataController controller, IPlaylist playlist, IEnumerable<IPlaylistItemViewModel> playlistItems)
            : base(controller, playlist, "PLAYLIST", GetIcon(playlist))
        {
            if (playlistItems == null)
                throw new ArgumentNullException("playlistItems");

            this.playlistItems = new ObservableCollection<IPlaylistItemViewModel>(playlistItems);
            currentPlaylistItem = this.playlistItems.FirstOrDefault();
        }

        private readonly ObservableCollection<IPlaylistItemViewModel> playlistItems;
        private IPlaylistItemViewModel currentPlaylistItem;
        private int currentIndex = 0;

        private static object GetIcon(IPlaylist playlist)
        {
            return playlist.ThumbnailData != null && playlist.ThumbnailData.Length > 0 ? playlist.ThumbnailData : (object)playlist.Thumbnail;
        }

        public IEnumerable<IPlaylistItemViewModel> PlaylistItems
        {
            get { return playlistItems; }
        }

        public IPlaylistItemViewModel CurrentPlaylistItem
        {
            get { return currentPlaylistItem; }
            private set
            {
                currentPlaylistItem = value;
                currentIndex = playlistItems.IndexOf(currentPlaylistItem);
                OnPropertyChanged("CurrentPlaylistItem");
            }
        }

        public void AddPlaylistItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            playlistItems.Add(item);
        }

        public void InsertPlaylistItem(int index, IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            playlistItems.Insert(index, item);
        }

        public void RemovePlaylistItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (playlistItems.Contains(item))
                playlistItems.Remove(item);
        }

        public void SelectPlaylistItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!playlistItems.Contains(item))
                throw new ArgumentException("Cannot select playlist item, it is not contained in this playlist");

            CurrentPlaylistItem = item;
        }

        public void SelectPreviousPlaylistItem()
        {
            if (playlistItems.Count > 0)
            {
                if (playlistItems.Count == 1)
                {
                    CurrentPlaylistItem = currentPlaylistItem;
                }
                else
                {
                    var index = playlistItems.IndexOf(currentPlaylistItem);
                    switch (index)
                    {
                        case -1:
                            CurrentPlaylistItem = playlistItems.FirstOrDefault();
                            return;
                        case 0:
                            CurrentPlaylistItem = playlistItems.LastOrDefault();
                            return;
                        default:
                            CurrentPlaylistItem = playlistItems.ElementAtOrDefault(index -1);
                            return;
                    }
                }
            }
        }

        public void SelectNextPlaylistItem()
        {
            if (playlistItems.Count > 0)
            {
                if (playlistItems.Count == 1)
                {
                    CurrentPlaylistItem = currentPlaylistItem;
                }
                else
                {
                    var index = playlistItems.IndexOf(currentPlaylistItem);
                    if (index == -1)
                    {
                        CurrentPlaylistItem = playlistItems.FirstOrDefault();
                        return;
                    }
                    else
                    {
                        var lastIndex = playlistItems.Count - 1;
                        if (index < lastIndex)
                        {
                            CurrentPlaylistItem = playlistItems.ElementAtOrDefault(index + 1);
                        }
                        else
                        {
                            CurrentPlaylistItem = playlistItems.FirstOrDefault();
                        }
                    }
                }
            }
        }

        public TaskItem GetPreviousTaskItem()
        {
            SelectPreviousPlaylistItem();

            return GetCurrentTaskItem();
        }

        public TaskItem GetCurrentTaskItem()
        {
            return currentPlaylistItem != null ? currentPlaylistItem.ToTaskItem() : default(TaskItem);
        }

        public TaskItem GetNextTaskItem()
        {
            SelectNextPlaylistItem();

            return GetCurrentTaskItem();
        }
    }
}

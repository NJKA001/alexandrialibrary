using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public class PlaylistController
        : IPlaylistController
    {
        public PlaylistController(ILogger logger, IPlaylistViewModel playlist, PlaylistTaskViewModel task, IVideoPlayer videoPlayer)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (playlist == null)
                throw new ArgumentNullException("playlist");
            if (task == null)
                throw new ArgumentNullException("task");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");

            this.logger = logger;
            this.playlist = playlist;
            this.task = task;
            this.videoPlayer = videoPlayer;
        }

        private readonly ILogger logger;
        private readonly IPlaylistViewModel playlist;
        private readonly PlaylistTaskViewModel task;
        private readonly IVideoPlayer videoPlayer;

        public IPlaylistViewModel Playlist
        {
            get { return playlist; }
        }

        public IPlaylistItemViewModel CurrentItem
        {
            get { return playlist.CurrentPlaylistItem; }
        }

        public IEnumerable<IPlaylistItemViewModel> Items
        {
            get { return playlist.PlaylistItems; }
        }

        public void AddStartedCallback(Action<ITaskViewModel> callback)
        {
            task.AddStartedCallback(callback);
        }

        public void AddPausedCallback(Action<ITaskViewModel> callback)
        {
            task.AddPausedCallback(callback);
        }

        public void AddResumedCallback(Action<ITaskViewModel> callback)
        {
            task.AddResumedCallback(callback);
        }

        public void AddStoppedCallback(Action<ITaskViewModel> callback)
        {
            task.AddStoppedCallback(callback);
        }

        public void AddItemChangedCallback(Action<TaskItem> callback)
        {
            task.AddItemChangedCallback(callback);
        }

        public void AddResultsCallback(Action<TaskItem> callback)
        {
            task.AddResultsCallback(callback);
        }

        public void SelectItem(IPlaylistItemViewModel item)
        {
            try
            {
                task.Stop();
                playlist.SelectPlaylistItem(item);
                task.UpdateItem(item.ToTaskItem());
                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistController.SelectItem", ex);
            }
        }

        public void SelectPreviousItem()
        {
            try
            {
                task.Stop();
                playlist.SelectPreviousPlaylistItem();
                task.Previous();
                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistController.SelectPreviousItem", ex);
            }
        }

        public void SelectNextItem()
        {
            try
            {
                task.Stop();
                playlist.SelectNextPlaylistItem();
                task.Next();
                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistController.SelectNextItem", ex);
            }
        }
    }
}

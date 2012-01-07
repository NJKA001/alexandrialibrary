using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IPlaylistController playlistController;

        private void playlistItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var listBoxItem = element.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var item = listBoxItem.DataContext as IPlaylistItemViewModel;
                if (item == null)
                    return;

                playlistController.SelectItem(item);
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistView.playlistItem_MouseDoubleClick", ex);
            }
        }

        private void ClearExistingItemStatus()
        {
            foreach (var existing in playlistController.Items.Where(x => x.IsPaused || x.IsPlaying || x.IsStopped))
            {
                existing.ClearStatus();
            }
        }

        private void OnItemStarted(ITaskViewModel task)
        {
            try
            {
                ClearExistingItemStatus();

                var playing = playlistController.Items.Where(x => x.Id == task.CurrentItem.Id).FirstOrDefault();
                if (playing != null)
                {
                    playing.IsPlaying = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistView.OnItemStarted", ex);
            }
        }

        private void OnItemPaused(ITaskViewModel taskViewModel)
        {
            if (playlistController.CurrentItem == null)
                return;

            playlistController.CurrentItem.IsPaused = true;
        }

        public void OnItemResumed(ITaskViewModel taskViewModel)
        {
            if (playlistController.CurrentItem == null)
                return;

            playlistController.CurrentItem.IsPlaying = true;
        }

        private void OnItemStopped(ITaskViewModel taskViewModel)
        {
            if (playlistController.CurrentItem == null)
                return;

            playlistController.CurrentItem.IsStopped = true;
        }

        private void OnItemChanged(TaskItem item)
        {
            try
            {
                ClearExistingItemStatus();

                var playing = playlistController.Items.Where(x => x.Id == item.Id).FirstOrDefault();
                if (playing != null)
                {
                    playing.IsPlaying = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  PlaylistView.OnItemChanged", ex);
            }
        }

        //private void OnItemEnded(TaskItem item)
        //{
        //    try
        //    {
        //        var playing = playlistController.Items.Where(x => x.Id == item.Id).FirstOrDefault();
        //        if (playing != null)
        //        {
        //            playing.ClearStatus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("  PlaylistView.OnItemEnded", ex);
        //    }
        //}

        public void Initialize(ILogger logger, IPlaylistController playlistController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (playlistController == null)
                throw new ArgumentNullException("playlistController");

            this.logger = logger;
            this.playlistController = playlistController;

            try
            {
                playlistController.AddStartedCallback(OnItemStarted);
                playlistController.AddPausedCallback(OnItemPaused);
                playlistController.AddResumedCallback(OnItemResumed);
                playlistController.AddStoppedCallback(OnItemStopped);
                playlistController.AddItemChangedCallback(OnItemChanged);
                //playlistController.AddResultsCallback(OnItemEnded);

                this.DataContext = playlistController.Playlist;

                var first = playlistController.Items.FirstOrDefault();
                if (first != null)
                {
                    //first.IsPlaying = true;
                    first.IsSelected = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("PlaylistView.Initialize", ex);
            }
        }
    }
}

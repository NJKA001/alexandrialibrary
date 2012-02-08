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

using log4net;
using TagLib;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.Models;


namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MediaView.xaml
    /// </summary>
    public partial class MediaListView : UserControl
    {
        public MediaListView()
        {
            InitializeComponent();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(MediaListView));
        private ITrackController trackController;
        private ITagController tagController;
        private PlaybackView playbackView;
        private MediaPropertyView mediaPropertyView;

        private IPicture copiedPicture;
        private Point trackItemDragStartPoint = new Point(0, 0);
        private IOldTrack trackToDrag = null;

        private void TrackListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedTrack != null)
            {
                playbackView.SetNowPlaying(SelectedTrack);
                playbackView.PlayCurrentTrack();
            }
        }

        private void ItemImageCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var track = SelectedTrack;
                if (track != null)
                {
                    var file = TagLib.File.Create(track.Path);
                    if (file.Tag != null && file.Tag.Pictures != null && file.Tag.Pictures.Length > 0)
                        copiedPicture = file.Tag.Pictures[0];
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void ItemImagePaste_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var track = SelectedTrack;
                if (track != null && copiedPicture != null)
                {
                    tagController.AddPicture(track, copiedPicture);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void ItemImage_Drop(object sender, System.Windows.DragEventArgs e)
        {
            try
            {
                var uiElement = sender as UIElement;
                if (uiElement == null)
                    return;


                var item = uiElement.FindContainingListViewItem();
                if (item == null)
                    return;

                var track = item.DataContext as IOldTrack;

                if (track != null)
                {
                    var image = e.Data.GetData(DataFormats.Bitmap) as System.Windows.Controls.Image;
                    if (image != null)
                    {
                        return;
                    }
                    var html = e.Data.GetData(DataFormats.Html) as string;
                    if (!string.IsNullOrEmpty(html))
                    {
                        var regex = new System.Text.RegularExpressions.Regex("src=['\"](?<PATH>[^\"']+)");
                        var match = regex.Match(html);
                        if (match != null)
                        {
                            var path = match.Groups["PATH"].Value;
                            if (!string.IsNullOrEmpty(path))
                            {
                                var request = System.Net.HttpWebRequest.Create(path);
                                var response = request.GetResponse();
                                if (response != null)
                                {
                                    using (var stream = response.GetResponseStream())
                                    {
                                        var buffer = stream.ToBuffer();
                                        var data = new ByteVector(buffer);
                                        var picture = new TagLib.Picture(data);
                                        tagController.AddPicture(track, picture);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("ItemImage_Drop", ex);
            }
        }

        private void TrackItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            trackItemDragStartPoint = e.GetPosition(null);
            log.Debug("MainWindow.TrackItem_PreviewMouseLeftButtonDown: x=" + trackItemDragStartPoint.X + " y=" + trackItemDragStartPoint.Y);
        }

        private void TrackItem_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var position = e.GetPosition(null);
                var offset = trackItemDragStartPoint - position;

                if (e.LeftButton == MouseButtonState.Pressed
                    && Math.Abs(offset.X) > SystemParameters.MinimumHorizontalDragDistance
                    && Math.Abs(offset.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var track = SelectedTrack;
                    if (track != null && track != trackToDrag)
                    {
                        trackToDrag = track;
                        var data = new DataObject("Track", track);
                        DragDrop.DoDragDrop(mediaList, data, DragDropEffects.Copy);
                        log.Debug("TrackItem_MouseMove: DoDragDrop");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("TrackItem_MouseMove", ex);
            }
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = SelectedTrack;
            if (track != null)
            {
                mediaPropertyView.Visibility = System.Windows.Visibility.Visible;
                mediaPropertyView.Track = track;
            }
            else
            {
                mediaPropertyView.Visibility = System.Windows.Visibility.Collapsed;
                mediaPropertyView.Track = null;
            }
        }

        public void Initialize(ITrackController trackController, ITagController tagController, PlaybackView playbackView, MediaPropertyView mediaPropertyView)
        {
            this.trackController = trackController;
            this.tagController = tagController;
            this.playbackView = playbackView;
            this.mediaPropertyView = mediaPropertyView;
            
            mediaList.ItemsSource = trackController.Tracks;
        }

        public IOldTrack SelectedTrack
        {
            get { return mediaList.SelectedItem as IOldTrack; }
        }
    }
}

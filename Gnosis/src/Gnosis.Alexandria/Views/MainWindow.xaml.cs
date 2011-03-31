using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using ControlPrimatives=System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TagLib;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using Gnosis.Core;
using Gnosis.Fmod;
using log4net;
using Gnosis.Alexandria.Events;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();

            try
            {
                log.Info("MainWindow.ctor: started");

                tagController = new TagController();
                trackController = new TrackController(trackRepository, tagController);
                sourceController = new SourceController(sourceRepository, trackController);
                playbackController = new PlaybackController();
                sourceView.Initialize(sourceController, trackController, tagController);
                searchView.Initialize(trackController);
                playbackView.Initialize(trackController, playbackController);

                TrackView.ItemsSource = trackController.Tracks;

                sourceView.SourceLoaded += new EventHandler<SourceLoadedEventArgs>(SourceLoaded);
                playbackController.CurrentTrackEnded += CurrentTrackEnded;
            }
            catch (Exception ex)
            {
                log.Error("MainWindow.ctor", ex);
            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindow));
        private readonly IRepository<ITrack> trackRepository = new TrackRepository();
        private readonly IRepository<ISource> sourceRepository = new SourceRepository();
        private readonly ITagController tagController;
        private readonly ITrackController trackController;
        private readonly ISourceController sourceController;
        private readonly IPlaybackController playbackController;

        private IPicture copiedPicture;
        private Point trackItemDragStartPoint = new Point(0, 0);
        private ITrack trackToDrag = null;

        private ITrack GetSelectedTrack()
        {
            return TrackView.SelectedItem as ITrack;
        }

        private void AddPicture(ITrack track, string path)
        {
            try
            {
                var picture = new TagLib.Picture(path);
                AddPicture(track, picture);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void AddPicture(ITrack track, IPicture picture)
        {
            try
            {
                var file = TagLib.File.Create(track.Path);
                var existingPictures = file.Tag.Pictures;
                if (existingPictures == null || existingPictures.Length == 0)
                {
                    file.Tag.Pictures = new IPicture[1] { picture };
                    file.Save();
                    track.ImageData = picture.Data;
                }
                else
                {
                    var pictures = new IPicture[existingPictures.Length + 1];
                    pictures[0] = picture;
                    for (var i = 1; i < pictures.Length; i++)
                        pictures[i] = existingPictures[i];

                    file.Tag.Pictures = pictures;
                    file.Save();
                    track.ImageData = picture.Data;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = GetSelectedTrack();
            if (track != null)
            {
                TrackPropertiesBorder.Visibility = System.Windows.Visibility.Visible;
                TrackProperties.DataContext = track;
            }
            else
            {
                TrackPropertiesBorder.Visibility = System.Windows.Visibility.Collapsed;
                TrackProperties.DataContext = null;
            }
        }

        private void CurrentTrackEnded(object sender, EventArgs args)
        {
            playbackView.PlayNextTrack();
        }

        private void TrackListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            playbackView.SetNowPlaying(GetSelectedTrack());
            playbackView.PlayCurrentTrack();
        }

        private void ChangePictureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var track = GetSelectedTrack();
                if (track != null)
                {
                    var dialog = new System.Windows.Forms.OpenFileDialog();
                    dialog.Filter = "Image Files (*.jpg,*.jpeg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif|All Files (*.*)|*.*";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        AddPicture(track, dialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void ItemImageCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var track = GetSelectedTrack();
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
                var track = GetSelectedTrack();
                if (track != null && copiedPicture != null)
                {
                    AddPicture(track, copiedPicture);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void SaveTrackPropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var track = GetSelectedTrack();
                if (track != null)
                {
                    tagController.SaveTag(track);
                    trackController.Save(track);
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
                var item = VisualHelper.FindContainingListViewItem(sender as UIElement);
                if (item == null)
                    return;

                var track = item.DataContext as ITrack;

                if (track != null)
                {
                    var image = e.Data.GetData(DataFormats.Bitmap) as Image;
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
                                        var buffer = stream.AsBuffer();
                                        var data = new ByteVector(buffer);
                                        var picture = new TagLib.Picture(data);
                                        AddPicture(track, picture);
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
                    var track = GetSelectedTrack();
                    if (track != null && track != trackToDrag)
                    {
                        trackToDrag = track;
                        var data = new DataObject("Track", track);
                        DragDrop.DoDragDrop(TrackView, data, DragDropEffects.Copy);
                        log.Debug("TrackItem_MouseMove: DoDragDrop");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("TrackItem_MouseMove", ex);
            }
        }

        private void SourceLoaded(object sender, SourceLoadedEventArgs args)
        {
            try
            {
                trackController.ClearTracks();
                if (args.Source is FileSystemSource || args.Source is DirectorySource)
                {
                    sourceController.LoadDirectories(args.Source);
                }

                foreach (var item in args.Source.Children)
                {
                    try
                    {
                        var track = trackController.Search(new Dictionary<string, object> { { "Path", item.Path } }).FirstOrDefault();
                        if (track == null)
                        {
                            track = trackController.ReadFromTag(item.Path);
                            trackController.Save(track);
                        }

                        if (track != null)
                        {
                            tagController.LoadPicture(track);
                            trackController.AddTrack(track);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("MainWindow.SourceLoaded: Could not load track path=" + item.Path, ex);
                    }
                }
                if (trackController.TrackCount > 0)
                {
                    var track = trackController.GetTrackAt(0);
                    track.IsSelected = true;
                    playbackController.Reset();
                    playbackView.SetNowPlaying(track);
                    playbackView.PlayCurrentTrack();
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadPlaylist_Clicked", ex);
            }
        }
    }
}

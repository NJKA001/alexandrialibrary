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
                sourceView.Initialize(sourceController, trackController, tagController);

                playbackTimer.Elapsed += new System.Timers.ElapsedEventHandler(PlaybackTimer_Elapsed);
                playbackTimer.Start();

                TrackView.ItemsSource = boundTracks;
                PlayButtonImage.DataContext = playbackStatus;

                var tracks = trackRepository.All();
                foreach (var track in tracks)
                {
                    tagController.LoadPicture(track);
                    boundTracks.Add(track);
                }

                player.CurrentAudioStreamEnded += new EventHandler<EventArgs>(CurrentTrackEnded);
                sourceView.SourceLoaded += new EventHandler<SourceLoadedEventArgs>(SourceLoaded);
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

        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();
        private readonly IAudioPlayer player = new AudioPlayer(new Fmod.AudioStreamFactory()) { PlayToggles = true };
        private readonly Timer playbackTimer = new Timer(1000);
        private readonly IPlaybackStatus playbackStatus = new PlaybackStatus();
        private ITrack currentTrack;
        private IPicture copiedPicture;
        private bool isAboutToPlay = false;
        private bool hasSeek = false;

        private ITrack GetSelectedTrack()
        {
            return TrackView.SelectedItem as ITrack;
        }

        private Uri GetCurrentUri()
        {
            return (currentTrack != null) ? new Uri(currentTrack.Path, UriKind.Absolute) : null;
        }

        private void SetCurrentTrack(ITrack track)
        {
            if (currentTrack != null)
                currentTrack.PlaybackStatus = null;

            if (track != null)
                track.PlaybackStatus = "Now Playing";

            currentTrack = track;
            hasSeek = false;

            NowPlayingMarquee.Dispatcher.Invoke((Action)delegate()
            {
                NowPlayingMarquee.DataContext = currentTrack;
                NowPlayingMarquee.Visibility = currentTrack != null ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void PlayCurrentTrack()
        {
            if (currentTrack != null)
            {
                var currentUri = GetCurrentUri();
                if (player.CurrentAudioStream == null)
                {
                    player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                }
                else
                {
                    var streamUri = new Uri(player.CurrentAudioStream.Path, UriKind.Absolute);
                    if (currentUri != streamUri)
                    {
                        player.Stop();
                        player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                    }
                }

                currentTrack.DurationLabel = string.Format("{0}:{1:00}", player.CurrentAudioStream.Duration.Minutes, player.Duration.Seconds);
                
                NowPlayingElapsedSlider.Dispatcher.Invoke((Action)delegate { NowPlayingElapsedSlider.Maximum = player.CurrentAudioStream.Duration.TotalSeconds; });

                currentTrack.ElapsedLabel = string.Format("{0}:{1:00}", player.Elapsed.Minutes, player.Elapsed.Seconds);

                isAboutToPlay = true;

                bool startAtZero = currentTrack.HasClipAt(TimeSpan.Zero);
                if (!startAtZero)
                {
                    //NOTE: We're going to be seeking so we want to mute to avoid any popping
                    player.Mute();
                }

                player.Play();

                if (!startAtZero)
                {
                    var clip = currentTrack.Clips.FirstOrDefault();
                    if (clip != null)
                    {
                        player.BeginSeek();
                        player.Seek(Convert.ToInt32(clip.Item1.TotalMilliseconds));
                    }
                    player.Unmute();
                }

                isAboutToPlay = false;

                playbackStatus.IsPlaying = (player.CurrentAudioStream.PlaybackState == PlaybackState.Playing);
                //PlayButton.Dispatcher.Invoke((Action)delegate() { PlayButton.Content = GetPlayButtonContent(); });
            }
        }

        private void PlayPreviousTrack()
        {
            if (currentTrack != null)
            {
                player.Stop();
                var index = boundTracks.IndexOf(currentTrack) - 1;
                if (index == -1)
                    index = boundTracks.Count - 1;

                SetCurrentTrack(boundTracks[index]);
                PlayCurrentTrack();
            }
        }

        private void PlayNextTrack()
        {
            if (currentTrack != null)
            {
                player.Stop();
                var index = boundTracks.IndexOf(currentTrack) + 1;
                if (index == boundTracks.Count)
                    index = 0;

                SetCurrentTrack(boundTracks[index]);
                PlayCurrentTrack();
            }
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

        private IEnumerable<KeyValuePair<string, object>> GetSearchCriteria(string search)
        {
            var criteria = new Dictionary<string, object>();

            var searchLike = string.Format("%{0}%", search);
            var searchHash = search.AsNameHash();
            var searchMetaphone = search.AsDoubleMetaphone();

            criteria.Add("Title", searchLike);
            criteria.Add("TitleHash", searchHash);
            criteria.Add("TitleMetaphone", searchMetaphone);
            criteria.Add("Artist", searchLike);
            criteria.Add("ArtistHash", searchHash);
            criteria.Add("ArtistMetaphone", searchMetaphone);
            criteria.Add("Album", searchLike);
            criteria.Add("AlbumHash", searchHash);
            criteria.Add("AlbumMetaphone", searchMetaphone);

            return criteria;
        }

        private void Search(string search)
        {
            try
            {
                IEnumerable<ITrack> tracks = null;

                if (!string.IsNullOrEmpty(search))
                {
                    var criteria = GetSearchCriteria(search);

                    tracks = trackRepository.Search(criteria);
                    if (tracks.Count() == 0 && search.Contains(' '))
                    {
                        var set = new HashSet<ITrack>();
                        foreach (var word in search.Split(' '))
                        {
                            foreach (var track in trackRepository.Search(GetSearchCriteria(word)))
                                set.Add(track);
                        }
                        tracks = set;
                    }
                }
                else
                {
                    tracks = trackRepository.All();
                }

                if (tracks != null)
                {
                    boundTracks.Clear();
                    foreach (var track in tracks)
                    {
                        tagController.LoadPicture(track);
                        boundTracks.Add(track);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Search(string)", ex);
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
            PlayNextTrack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousTrack();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentTrack == null)
            {
                var selectedTrack = GetSelectedTrack();
                if (selectedTrack != null)
                {
                    SetCurrentTrack(selectedTrack);
                }
                else if (boundTracks.Count > 0)
                {
                    SetCurrentTrack(boundTracks[0]);
                }
            }

            PlayCurrentTrack();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PlayNextTrack();
        }

        private void TrackListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetCurrentTrack(GetSelectedTrack());
            PlayCurrentTrack();
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Search(SearchTextBox.Text);
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search(SearchTextBox.Text);
                e.Handled = true;
            }
        }

        private Point trackItemDragStartPoint = new Point(0, 0);
        private ITrack trackToDrag = null;

        private void TrackItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            trackItemDragStartPoint = e.GetPosition(null);
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
                        System.Diagnostics.Debug.WriteLine("TrackItem_MouseMove: DoDragDrop");
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
                boundTracks.Clear();
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
                            boundTracks.Add(track);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("MainWindow.SourceLoaded: Could not load track path=" + item.Path, ex);
                    }
                }
                if (boundTracks.Count > 0)
                {
                    boundTracks[0].IsSelected = true;
                    player.Stop();
                    currentTrack = null;
                    PlayButton_Click(this, null);
                }
            }
            catch (Exception ex)
            {
                log.Error("LoadPlaylist_Clicked", ex);
            }
        }

        private void UpdatePlaybackStatus()
        {
            if (player != null && player.CurrentAudioStream != null && currentTrack != null)
            {
                player.RefreshPlayerStates();
                var elapsed = player.CurrentAudioStream.Elapsed;
                currentTrack.ElapsedLabel = string.Format("{0}:{1:00}", elapsed.Minutes, elapsed.Seconds);

                if (!player.SeekIsPending)
                {
                    currentTrack.Elapsed = elapsed.TotalSeconds;

                    if (!isAboutToPlay && !hasSeek)
                    {
                        if (!currentTrack.HasClipAt(elapsed))
                        {
                            var nextClip = currentTrack.GetNextClipFrom(elapsed);
                            if (nextClip != null)
                            {
                                player.Mute();
                                player.BeginSeek();
                                player.Seek(Convert.ToInt32(nextClip.Item1.TotalMilliseconds));
                                player.Unmute();
                            }
                            else
                            {
                                PlayNextTrack();
                            }
                        }
                    }
                }
            }
        }

        private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlaybackStatus();
        }

        private void NowPlayingElapsedSlider_DragStarted(object sender, ControlPrimatives.DragStartedEventArgs e)
        {
            try
            {
                if (player != null && player.CurrentAudioStream != null && currentTrack != null)
                {
                    player.BeginSeek();
                }
            }
            catch (Exception ex)
            {
                log.Error("NowPlayingElapsedSlider_DragStarted", ex);
            }
        }

        private void NowPlayingElapsedSlider_DragCompleted(object sender, ControlPrimatives.DragCompletedEventArgs e)
        {
            try
            {
                if (player != null && player.CurrentAudioStream != null && currentTrack != null && NowPlayingElapsedSlider.Value >= 0)
                {
                    hasSeek = true;
                    player.Seek(Convert.ToInt32(NowPlayingElapsedSlider.Value * 1000));
                    UpdatePlaybackStatus();
                }
            }
            catch (Exception ex)
            {
                log.Error("NowPlayingElapsedSlider_DragCompleted", ex);
            }
        }
    }
}

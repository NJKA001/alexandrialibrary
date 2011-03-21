using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

using TagLib;

using Gnosis.Archon.Models;
using Gnosis.Archon.Repositories;
using Gnosis.Core;
using Gnosis.Fmod;
using Gnosis.Archon.Helpers;

namespace Gnosis.Archon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                SourceView.ItemsSource = boundSources;
                TrackView.ItemsSource = boundTracks;

                var sources = sourceRepository.Search(new Dictionary<string, object> { { "Parent", null } });
                if (sources != null && sources.Count() > 0)
                {
                    foreach (var source in sources)
                    {
                        boundSources.Add(source);
                        LoadSourceChildren(source);
                    }
                }

                var tracks = trackRepository.All();
                if (tracks.Count() == 0)
                {
                    LoadMusic();
                }
                else
                {
                    foreach (var track in tracks)
                    {
                        LoadPicture(track);
                        boundTracks.Add(track);
                    }
                }

                player.CurrentAudioStreamEnded += new EventHandler<EventArgs>(CurrentTrackEnded);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                System.Diagnostics.Debug.WriteLine("MainWindow.ctor failed:" + ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private readonly ObservableCollection<ITrack> boundTracks = new ObservableCollection<ITrack>();
        private readonly ObservableCollection<ISource> boundSources = new ObservableCollection<ISource>();
        private readonly IRepository<ITrack> trackRepository = new TrackRepository();
        private readonly IRepository<ISource> sourceRepository = new SourceRepository();
        private readonly IAudioPlayer player = new AudioPlayer(new Fmod.AudioStreamFactory()) { PlayToggles = true };
        private ITrack currentTrack;
        private IPicture copiedPicture;

        private void LoadSourceChildren(ISource source)
        {
            var children = sourceRepository.Search(new Dictionary<string, object> { { "Parent", source.Id.ToString() } });
            if (children != null && children.Count() > 0)
            {
                foreach (var child in children)
                {
                    child.Parent = source;
                    source.AddChild(child);
                    LoadSourceChildren(child);
                }
            }
        }

        private void LoadDirectory(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                if (file.FullName.EndsWith(".mp3"))
                {
                    var tagFile = GetTagFile(file.FullName);

                    var track = GetTrack(file.FullName, tagFile.Tag);

                    boundTracks.Add(track);
                    trackRepository.Save(track);
                }
            }

            foreach (var child in directory.GetDirectories())
            {
                LoadDirectory(child);
            }
        }

        private void LoadMusic()
        {
            var directory = new DirectoryInfo(@"\\vmware-host\Shared Folders\Documents\My Music\Alexandria Anthology #1");
            //var directory = new DirectoryInfo(@"C:\Users\Public\Music\Sample Music");
            LoadDirectory(directory);
        }

        private TagLib.File GetTagFile(string path)
        {
            return TagLib.File.Create(path);
        }

        private void LoadPicture(ITrack track)
        {
            var file = GetTagFile(track.Path);
            if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
            {
                track.ImageData = file.Tag.Pictures[0].Data;
            }
        }

        private void LoadPicture(PlaylistItemSource source)
        {
            var file = GetTagFile(source.Path);
            if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
            {
                source.ImageData = file.Tag.Pictures[0].Data;
            }
        }

        private ITrack GetTrack(string path, TagLib.Tag tag)
        {
            var track = new Track() { Path = path };

            if (!string.IsNullOrEmpty(tag.Title))
                track.Title = tag.Title;

            if (!string.IsNullOrEmpty(tag.Album))
                track.Album = tag.Album;

            track.TrackNumber = tag.Track;
            track.DiscNumber = tag.Disc;

            if (!string.IsNullOrEmpty(tag.JoinedGenres))
                track.Genre = tag.JoinedGenres;

            if (!string.IsNullOrEmpty(tag.JoinedPerformers))
                track.Artist = tag.JoinedPerformers;

            if (tag.Year > 0 && tag.Year < int.MaxValue)
                track.ReleaseDate = new DateTime((int)tag.Year, 1, 1);

            if (tag.Pictures.Length > 0)
                track.ImageData = tag.Pictures[0].Data;

            return track;
        }

        private void SaveTag(ITrack track)
        {
            var file = TagLib.File.Create(track.Path);
            if (file.Tag != null)
            {
                if (!string.IsNullOrEmpty(track.Title))
                    file.Tag.Title = track.Title;

                if (!string.IsNullOrEmpty(track.Album))
                    file.Tag.Album = track.Album;

                file.Tag.Track = track.TrackNumber;
                file.Tag.Disc = track.DiscNumber;

                if (!string.IsNullOrEmpty(track.Artist))
                    file.Tag.Performers = track.Artist.Split(',', ';');
                
                if (!string.IsNullOrEmpty(track.Genre))
                    file.Tag.Genres = track.Genre.Split(',', ';');

                file.Tag.Year = Convert.ToUInt32(track.ReleaseYear);

                file.Save();
            }
        }

        private ITrack GetSelectedTrack()
        {
            return TrackView.SelectedItem as ITrack;
        }

        private string GetPlayButtonContent()
        {
            return player.CurrentAudioStream.PlaybackState == PlaybackState.Playing ? "Pause" : "Play";
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
            NowPlayingMarquee.DataContext = currentTrack;
            NowPlayingMarquee.Visibility = currentTrack != null ? Visibility.Visible : Visibility.Collapsed;
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

                player.Play();
                PlayButton.Content = GetPlayButtonContent();
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
                        LoadPicture(track);
                        boundTracks.Add(track);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Failed");
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
                    SaveTag(track);
                    trackRepository.Save(track);
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
                var track = GetSelectedTrack();
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
                System.Windows.MessageBox.Show(ex.Message, "Item Image Drag/Drop Failed");
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

        private ISource GetSelectedSource()
        {
            return SourceView.SelectedItem as ISource;
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var folder = new FolderSource { Name = "New Folder" };

                var source = GetSelectedSource();
                if (source != null)
                {
                    source.IsExpanded = true;
                    folder.Parent = source;
                    source.AddChild(folder);
                }
                else
                {
                    boundSources.Add(folder);
                }

                sourceRepository.Save(folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Folder Failed");
            }
        }

        private void AddPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var playlist = new PlaylistSource { Name = "New Playlist" };

                var source = GetSelectedSource();
                if (source != null)
                {
                    source.IsExpanded = true;
                    playlist.Parent = source;
                    source.AddChild(playlist);
                }
                else
                {
                    boundSources.Add(playlist);
                }

                sourceRepository.Save(playlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Playlist Failed");
            }
        }

        private void SourceItem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F2)
                {
                    var source = GetSelectedSource();
                    if (source != null)
                    {
                        source.IsBeingRenamed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Source Failed");
            }
        }

        private void SourceNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;
                    var source = GetSelectedSource();
                    var textBox = sender as TextBox;
                    if (source != null && source.IsBeingRenamed && textBox != null)
                    {
                        source.Name = textBox.Text;
                        source.IsBeingRenamed = false;
                        sourceRepository.Save(source);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Playlist Failed");
            }
        }

        private void SourceItem_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                var treeViewItem = sender as TreeViewItem;
                if (treeViewItem != null)
                {
                    var source = treeViewItem.Header as ISource;
                    if (source != null)
                    {
                        foreach (var child in source.Children)
                        {
                            var playlistItem = child as PlaylistItemSource;
                            if (playlistItem != null)
                            {
                                if (!string.IsNullOrEmpty(playlistItem.Path) && playlistItem.ImageData == null)
                                {
                                    LoadPicture(playlistItem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Source Expanded Failed");
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
                MessageBox.Show(ex.Message, "Track Item Drag Failed");
            }
        }

        private ISource GetSourceDropTarget(DragEventArgs e)
        {
            var element = e.OriginalSource as UIElement;
            if (element != null)
            {
                var item = VisualHelper.FindContainingTreeViewItem(element);
                if (item != null)
                {
                    return item.Header as ISource;
                }
            }

            return null;
        }

        private void SourceView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Track"))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                var playlist = GetSourceDropTarget(e) as PlaylistSource;
                if (playlist == null)
                {
                    e.Effects = DragDropEffects.None;
                }
            }
        }

        private void AddPlaylistItem(PlaylistSource playlist, ITrack track)
        {
            var item = new PlaylistItemSource()
            { 
                Parent = playlist,
                Path = track.Path,
                ImagePath = track.ImagePath, 
                ImageData = track.ImageData,
                Name = string.Format("{0} by {1}", track.Title ?? "Untitled", track.Artist ?? "Unknown Artist"),
                Number = playlist.Children.Count() + 1
            };

            sourceRepository.Save(item);

            playlist.AddChild(item);
            playlist.IsExpanded = true;
            item.IsSelected = true;
        }

        private void SourceView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var track = e.Data.GetData("Track") as ITrack;
                if (track != null)
                {
                    var playlist = GetSourceDropTarget(e) as PlaylistSource;
                    if (playlist != null)
                    {
                        AddPlaylistItem(playlist, track);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Source Item Drop Failed");
            }
        }

        private void LoadPlaylist_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem != null)
                {
                    PlaylistSource playlist = menuItem.CommandParameter as PlaylistSource;
                    if (playlist != null)
                    {
                        boundTracks.Clear();
                        foreach (var item in playlist.Children)
                        {
                            var track = trackRepository.Search(new Dictionary<string, object> { { "Path", item.Path } }).FirstOrDefault();
                            if (track != null)
                            {
                                LoadPicture(track);
                                boundTracks.Add(track);
                            }
                        }
                        if (boundTracks.Count > 0)
                        {
                            boundTracks[0].IsSelected = true;
                            PlayButton_Click(this, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Playlist Failed");
            }
        }

        private void SourceItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SourceView.ContextMenu = null;
                var item = sender as TreeViewItem;
                if (item != null)
                {
                    var source = item.Header as ISource;
                    if (source != null)
                    {
                        if (source is PlaylistSource)
                        {
                            var menu = new ContextMenu();
                            var loadPlaylistItem = new MenuItem { Header = "Load Playlist" };
                            loadPlaylistItem.CommandParameter = source;
                            loadPlaylistItem.Click += LoadPlaylist_Clicked;
                            menu.Items.Add(loadPlaylistItem);
                            SourceView.ContextMenu = menu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Source Item Preview Right Click Failed");
            }
        }
    }
}

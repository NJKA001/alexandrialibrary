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

using log4net;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Events;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SourceView.xaml
    /// </summary>
    public partial class SourceView : UserControl
    {
        public SourceView()
        {
            InitializeComponent();
        }

        private static ILog log = LogManager.GetLogger(typeof(SourceView));
        private ISourceController sourceController;
        private ITrackController trackController;
        private ITagController tagController;
        private readonly ObservableCollection<ISource> boundSources = new ObservableCollection<ISource>();

        #region Private Methods

        private void LoadSourceChildren(ISource source)
        {
            var children = sourceController.Search(new Dictionary<string, object> { { "Parent", source.Id.ToString() } });
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

        private void AddPlaylistItem(ISource source, ITrack track)
        {
            var item = sourceController.GetPlaylistItem(source, track);

            sourceController.Save(item);

            source.AddChild(item);
            source.IsExpanded = true;
            item.IsSelected = true;
        }

        private void LoadPicture(ISource source)
        {
            var file = tagController.GetFile(source.Path);
            if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
            {
                source.ImageData = file.Tag.Pictures[0].Data;
            }
        }

        private ISource GetSelectedSource()
        {
            return treeView.SelectedItem as ISource;
        }

        private void DeselectAll(ISource source)
        {
            if (source != null)
            {
                source.IsSelected = false;
                foreach (var child in source.Children)
                    DeselectAll(child);
            }
        }

        private void OnSourceLoaded(ISource source)
        {
            if (SourceLoaded != null)
                SourceLoaded(this, new SourceLoadedEventArgs(source));
        }

        #endregion

        #region Button Events

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

                sourceController.Save(folder);
            }
            catch (Exception ex)
            {
                log.Error("AddFolderButton_Click", ex);
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

                sourceController.Save(playlist);
            }
            catch (Exception ex)
            {
                log.Error("AddPlaylistButton_Click", ex);
            }
        }

        private void AddFileSystemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = GetSelectedSource();

                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var directory = new DirectoryInfo(dialog.SelectedPath);
                    var source = new FileSystemSource() { Name = directory.Name, Path = dialog.SelectedPath, Parent = parent };
                    trackController.LoadDirectory(directory);

                    sourceController.Save(source);

                    if (parent != null)
                    {
                        parent.AddChild(source);
                    }
                    else
                    {
                        boundSources.Add(source);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.AddFileSystemButton_Click", ex);
            }
        }

        private void addPodcastButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = GetSelectedSource();

                var source = new PodcastSource { Name = "New Podcast", Path = "unknown", Parent = parent };

                if (parent != null)
                {
                    parent.AddChild(source);
                }
                else
                {
                    boundSources.Add(source);
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.addPodcastButton_Click", ex);
                MessageBox.Show("There was an error trying to add a podcast.\n\n" + ex.Message, "Could Not Add Podcast");
            }
        }

        #endregion

        #region ContextMenu Events

        private void LoadSource_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem != null)
                {
                    var source = menuItem.CommandParameter as ISource;
                    if (source != null)
                    {
                        OnSourceLoaded(source);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.LoadSource_Clicked", ex);
            }
        }

        private void EditSource_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem != null)
                {
                    var source = menuItem.CommandParameter as ISource;
                    if (source != null)
                    {
                        source.IsBeingEdited = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.EditSource_Clicked", ex);
            }
        }

        private void DeleteSource_Clicked(object sender, RoutedEventArgs args)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem != null)
                {
                    var source = menuItem.CommandParameter as ISource;
                    if (source != null)
                    {
                        var result = MessageBox.Show("Are you sure that you want to delete this source?\n\nName: " + source.Name + "\nPath: " + source.Path, "Delete Source", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            sourceController.Delete(source.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.DeleteSource_Clicked", ex);
            }
        }

        #endregion

        #region SourceView Events

        private void SourceView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var result = VisualTreeHelper.HitTest(treeView, e.GetPosition(treeView));
                if (result != null)
                {
                    var element = result.VisualHit as UIElement;
                    var item = VisualHelper.FindContainingTreeViewItem(element);
                    if (item == null)
                    {
                        foreach (var source in boundSources)
                            DeselectAll(source);
                    }
                    else
                    {
                        item.IsSelected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView_PreviewMouseLeftButtonDown", ex);
            }
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
                log.Error("SouceView_Drop", ex);
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
                        if (source is FileSystemSource || source is DirectorySource)
                        {
                            sourceController.LoadDirectories(source);
                        }

                        if (source is PodcastSource)
                        {
                            sourceController.LoadPodcast(source);
                        }

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
                log.Error("SourceItem_Expanded", ex);
            }
        }

        private void SourceItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                treeView.ContextMenu = null;
                var item = sender as TreeViewItem;
                if (item != null)
                {
                    var source = item.Header as ISource;
                    if (source != null)
                    {
                        var menu = new ContextMenu();
                        
                        var loadMediaItem = new MenuItem { Header = "Load" };
                        loadMediaItem.CommandParameter = source;
                        loadMediaItem.Click += LoadSource_Clicked;
                        
                        var editItem = new MenuItem { Header = "Edit" };
                        editItem.CommandParameter = source;
                        editItem.Click += EditSource_Clicked;

                        var deleteItem = new MenuItem { Header = "Delete" };
                        deleteItem.CommandParameter = source;
                        deleteItem.Click += DeleteSource_Clicked;

                        menu.Items.Add(loadMediaItem);
                        menu.Items.Add(editItem);
                        menu.Items.Add(deleteItem);
                        treeView.ContextMenu = menu;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.SourceItem_PreviewMouseRightButtonDown", ex);
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
                        source.IsBeingEdited = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.SourceItem_KeyUp", ex);
            }
        }

        private ISource GetSourceFromTextBoxKeyUp(TextBox textBox)
        {
            var item = VisualHelper.FindContainingTreeViewItem(textBox);
            if (item != null)
                return item.Header as ISource;
            else return null;
        }

        private void SourceNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;
                    var source = GetSourceFromTextBoxKeyUp(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.Name = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromTextBoxKeyUp(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceView.SourceNameTextBox_KeyUp", ex);
            }
        }

        private void sourcePathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;

                    var source = GetSourceFromTextBoxKeyUp(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.Path = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromTextBoxKeyUp(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("sourcePathTextBox_KeyUp", ex);
            }
        }

        #endregion

        public EventHandler<SourceLoadedEventArgs> SourceLoaded { get; set; }

        public void Initialize(ISourceController sourceController, ITrackController trackController, ITagController tagController)
        {
            this.sourceController = sourceController;
            this.trackController = trackController;
            this.tagController = tagController;

            treeView.ItemsSource = boundSources;

            var sources = sourceController.Search(new Dictionary<string, object> { { "Parent", null } });
            if (sources != null && sources.Count() > 0)
            {
                foreach (var source in sources)
                {
                    boundSources.Add(source);
                    LoadSourceChildren(source);
                }
            }
        }
    }
}

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
using Gnosis.Alexandria.Extensions;
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
            var uri = new Uri(source.Path);
            if (uri.IsFile && System.IO.File.Exists(source.Path))
            {
                var file = tagController.GetFile(source.Path);
                if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
                {
                    source.ImageData = file.Tag.Pictures[0].Data;
                }
            }
        }

        private ISource GetSelectedSource()
        {
            var source = treeView.SelectedItem as ISource;
            if (source != null && source.IsSelected)
                return source;

            return null;
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

        private void addSpiderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = GetSelectedSource();

                var source = new SpiderSource { Name = "New Spider", Path = "unknown", Parent = parent };

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
                log.Error("SourceView.addSpiderButton_Click", ex);
                MessageBox.Show("There was an error trying to add a spider.\n\n" + ex.Message, "Could Not Add Spider");
            }
        }

        private void addYouTubeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = GetSelectedSource();

                var source = new YouTubeUserSource { Name = "New YouTube Feed", Path = "unknown", Parent = parent };

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
                log.Error("SourceView.addYouTubeButton_Click", ex);
                MessageBox.Show("There was an error trying to add a YouTube feed.\n\n" + ex.Message, "Could Not Add YouTube Feed");
            }
        }

        #endregion

        #region ContextMenu Events

        private void PlaySource_Clicked(object sender, RoutedEventArgs args)
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
                log.Error("SourceView.PlaySource_Clicked", ex);
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

        private void Delete(ISource source)
        {
            sourceController.Delete(source.Id);

            if (source.Parent != null)
            {
                source.Parent.RemoveChild(source);
            }
            else
            {
                if (boundSources.Contains(source))
                {
                    boundSources.Remove(source);
                }
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
                            Delete(source);
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

        private void sourceItem_MouseDoubleClick(object sender, MouseButtonEventArgs args)
        {
            try
            {
                //var item = sender as TreeViewItem;
                //if (item != null)
                //{
                //    var source = item.DataContext as ISource;
                //    if (source != null)
                //    {
                //        OnSourceLoaded(source);
                //    }
                //}
            }
            catch (Exception ex)
            {
                log.Error("sourceItem_MouseDoubleClick", ex);
            }
        }

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
                        //The user clicked on the scrollbar or some other chrome element - we don't want to deselect anything
                        if (element != null && element.IsWindowChrome())
                            return;

                        foreach (var source in boundSources)
                            source.DeselectAll();
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
                            sourceController.LoadPodcast(source, this);
                        }

                        if (source is SpiderSource)
                        {
                            sourceController.LoadSpider(source, this);
                        }

                        if (source is DeviceCatalogSource)
                        {
                            if (source.Children.Count() == 1 && source.Children.FirstOrDefault() is ProxySource)
                            {
                                sourceController.LoadDevices(source, this);
                            }
                        }

                        if (source is YouTubeUserSource)
                        {
                            //if (source.Children.Count() == 1 && source.Children.FirstOrDefault() is ProxySource)
                            //{
                                sourceController.LoadYouTubeUser(source, this);
                            //}
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

        private Image GetMenuIcon(string path)
        {
            var icon = new Image();
            icon.Width = 16;
            icon.Height = 16;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();

            icon.Source = image;
            return icon;
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
                        
                        var playItem = new MenuItem { Header = "Play" };
                        playItem.CommandParameter = source;
                        playItem.Click += PlaySource_Clicked;
                        playItem.Icon = GetMenuIcon("pack://application:,,,/Images/play.png");

                        var editItem = new MenuItem { Header = "Edit" };
                        editItem.CommandParameter = source;
                        editItem.Click += EditSource_Clicked;
                        editItem.Icon = GetMenuIcon("pack://application:,,,/Images/edit.png");

                        var deleteItem = new MenuItem { Header = "Delete" };
                        deleteItem.CommandParameter = source;
                        deleteItem.Click += DeleteSource_Clicked;
                        deleteItem.Icon = GetMenuIcon("pack://application:,,,/Images/delete.png");

                        menu.Items.Add(playItem);
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

        private ISource GetSourceFromChildElement(UIElement element)
        {
            var item = VisualHelper.FindContainingTreeViewItem(element);
            if (item != null)
                return item.Header as ISource;
            else return null;
        }

        private void RevertChanges(ISource source)
        {
            var original = sourceController.Get(source.Id);
            if (original != null)
            {
                source.Name = original.Name;
                source.Path = original.Path;
                source.ImagePath = original.ImagePath;
            }
            source.IsBeingEdited = false;
        }

        private void sourceNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;
                    var source = textBox.DataContext as ISource; //GetSourceFromChildElement(textBox);
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
                    var source = textBox.DataContext as ISource; //GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        RevertChanges(source);
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

                    var source = GetSourceFromChildElement(textBox);
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
                    var source = GetSourceFromChildElement(textBox);
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

        private void sourceImagePath_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;

                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.ImagePath = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("sourceImagePath_KeyUp", ex);
            }
        }

        private void sourceImagePattern_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;

                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.ImagePattern = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("sourceImagePattern_KeyUp", ex);
            }
        }

        private void sourceChildPattern_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;

                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.ChildPattern = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("sourceChildPattern_KeyUp", ex);
            }
        }

        private void sourcePagePattern_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;

                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.PagePattern = textBox.Text;
                        source.IsBeingEdited = false;
                        sourceController.Save(source);
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    var source = GetSourceFromChildElement(textBox);
                    if (source != null && source.IsBeingEdited)
                    {
                        source.IsBeingEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("sourcePagePattern_KeyUp", ex);
            }
        }

        private IEnumerable<ISource> GetChildrenForDepth(ISource source, int depth)
        {
            var results = new List<ISource>();

            if (depth < 1)
                depth = 1;
            else if (depth > 3)
                depth = 3;

            foreach (var child in source.Children)
            {
                if (depth == 1)
                    results.Add(child);
                else
                {
                    foreach (var grandchild in child.Children)
                    {
                        if (depth == 2)
                            results.Add(grandchild);
                        else
                        {
                            foreach (var greatGrandchild in grandchild.Children)
                            {
                                results.Add(greatGrandchild);
                            }
                        }
                    }
                }
            }

            return results;
        }

        private void sourceSetChildPatternsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                Grid parent = null;
                TextBox setContentPatternTextBox = null;
                TextBox setImagePatternTextBox = null;
                TextBox setPagePatternTextBox = null;
                TextBox setDepthTextBox = null;

                if (button == null)
                    return;

                parent = button.Parent as Grid;
                if (parent == null)
                    return;

                foreach (UIElement child in parent.Children)
                {
                    var textBox = child as TextBox;
                    if (textBox != null)
                    {
                        if (textBox.Name == "sourceSetContentPattern")
                            setContentPatternTextBox = textBox;
                        else if (textBox.Name == "sourceSetImagePattern")
                            setImagePatternTextBox = textBox;
                        else if (textBox.Name == "sourceSetPagePattern")
                            setPagePatternTextBox = textBox;
                        else if (textBox.Name == "sourceSetDepth")
                            setDepthTextBox = textBox;
                    }
                }

                if (setContentPatternTextBox == null || setImagePatternTextBox == null || setPagePatternTextBox == null || setDepthTextBox == null)
                    return;

                var source = GetSourceFromChildElement(button);
                if (source != null)
                {
                    var depth = 1;
                    int.TryParse(setDepthTextBox.Text, out depth);
                    var children = GetChildrenForDepth(source, depth);

                    foreach (var child in children)
                    {
                        try
                        {
                            child.ChildPattern = setContentPatternTextBox.Text;
                            child.ImagePattern = setImagePatternTextBox.Text;
                            child.PagePattern = setPagePatternTextBox.Text;
                            sourceController.Save(child);
                        }
                        catch (Exception ex)
                        {
                            log.Error("sourceSetChildPatternsButton_Click: Failed for child. name=" + child.Name, ex);
                        }
                    }
                }

                MessageBox.Show("Patterns set for child spiders to the indicated depth.", "Child Patterns Set");
            }
            catch (Exception ex)
            {
                log.Error("sourceSetChildPatternsButton_Click", ex);
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

            boundSources.Add(new DeviceCatalogSource { Name = "Devices" });

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

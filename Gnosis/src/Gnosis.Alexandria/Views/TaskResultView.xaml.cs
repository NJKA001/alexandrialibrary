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
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for TaskResultView.xaml
    /// </summary>
    public partial class TaskResultView : UserControl
    {
        public TaskResultView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private ISecurityContext securityContext;
        private IContentTypeFactory contentTypeFactory;
        private ITaskController taskController;
        private ITagController tagController;
        private IMediaItemController mediaItemController;
        private IVideoPlayer videoPlayer;

        private readonly IDictionary<Guid, ITaskResultViewModel> tabMap = new Dictionary<Guid, ITaskResultViewModel>();

        public void Initialize(ILogger logger, ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemController mediaItemController, ITaskController taskController, ITagController tagController, IVideoPlayer videoPlayer)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (contentTypeFactory == null)
                throw new ArgumentNullException("contentTypeFactory");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (tagController == null)
                throw new ArgumentNullException("tagController");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");

            this.logger = logger;
            this.securityContext = securityContext;
            this.contentTypeFactory = contentTypeFactory;
            this.taskController = taskController;
            this.mediaItemController = mediaItemController;
            this.tagController = tagController;
            this.videoPlayer = videoPlayer;
        }

        private void CloseTab(ITaskResultViewModel taskResultViewModel)
        {
            var key = taskResultViewModel.Id;
            if (taskResultViewModel != null && tabMap.ContainsKey(key))
            {
                resultControl.Items.Remove(taskResultViewModel.Control);
                tabMap.Remove(key);
            }
        }

        private void TaskStarted(ITaskViewModel taskViewModel)
        {
            try
            {
                if (taskViewModel == null)
                    return;
                if (tabMap.ContainsKey(taskViewModel.Id))
                    return;

                var catalogViewModel = taskViewModel as CatalogMediaTaskViewModel;
                if (catalogViewModel != null)
                {
                    Catalog(catalogViewModel);
                    return;
                }

                var searchViewModel = taskViewModel as SearchTaskViewModel;
                if (searchViewModel != null)
                {
                    Search(searchViewModel);
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.TaskStarted", ex);
            }
        }

        private void AddViewModel(ITaskViewModel taskViewModel, TabItem tabItem)
        {
            try
            {
                if (tabMap.ContainsKey(taskViewModel.Id))
                    throw new InvalidOperationException("There is already a tab for this task. name=" + taskViewModel.Name + " description=" + taskViewModel.Description);

                taskViewModel.AddStartedCallback(x => TaskStarted(x));

                var taskResultViewModel = new TaskResultViewModel(taskViewModel, tabItem);

                tabItem.DataContext = taskResultViewModel;
                taskResultViewModel.AddClosedCallback(x => CloseTab(x));

                tabMap.Add(taskViewModel.Id, taskResultViewModel);

                var existing = taskController.Tasks.Where(x => x.Id == taskViewModel.Id).FirstOrDefault();
                if (existing == null)
                {
                    taskController.AddTaskViewModel(taskViewModel);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.AddViewModel", ex);
            }
        }

        public void Catalog(CatalogMediaTaskViewModel catalogViewModel)
        {
            if (catalogViewModel == null)
                throw new ArgumentNullException("catalogViewModel");

            try
            {
                if (!tabMap.ContainsKey(catalogViewModel.Id))
                {
                    var catalogResultView = new CatalogResultView();
                    catalogResultView.Initialize(logger);

                    catalogViewModel.AddProgressCallback(progress => catalogResultView.AddProgressDetail(progress));
                    catalogViewModel.AddErrorCallback(error => catalogResultView.AddErrorDetail(error));
                    
                    var tabItem = new TabItem();
                    
                    TextBlock header = new TextBlock();
                    header.Inlines.Add(catalogViewModel.Name);
                    header.ToolTip = catalogViewModel.Description;
                    tabItem.Header = header;
                    
                    tabItem.Content = catalogResultView;
                    resultControl.Items.Add(tabItem);
                    tabItem.IsSelected = true;

                    AddViewModel(catalogViewModel, tabItem);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.Catalog", ex);
            }
        }

        public void Playlist(PlaylistTaskViewModel taskViewModel, IPlaylistViewModel playlist)
        {
            if (taskViewModel == null)
                throw new ArgumentNullException("taskViewModel");
            if (playlist == null)
                throw new ArgumentNullException("playlist");

            try
            {
                if (!tabMap.ContainsKey(taskViewModel.Id))
                {
                    var playlistController = new PlaylistController(logger, playlist, taskViewModel, videoPlayer);

                    var playlistView = new PlaylistView();
                    playlistView.Initialize(logger, playlistController);

                    var tabItem = new TabItem();

                    TextBlock header = new TextBlock();
                    header.Inlines.Add(taskViewModel.Description);
                    header.ToolTip = string.Format("{0}: {1}", taskViewModel.Name, taskViewModel.Description);
                    tabItem.Header = header;

                    tabItem.Content = playlistView;
                    resultControl.Items.Add(tabItem);
                    tabItem.IsSelected = true;

                    AddViewModel(taskViewModel, tabItem);

                    if (taskViewModel.Status == TaskStatus.Ready)
                    {
                        taskViewModel.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.Playlist", ex);
            }
        }

        public void Search(SearchTaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
                throw new ArgumentNullException("searchViewModel");

            try
            {
                if (!tabMap.ContainsKey(taskViewModel.Id))
                {
                    var searchResultView = new SearchResultView();
                    searchResultView.Initialize(logger, securityContext, contentTypeFactory, mediaItemController, taskController, tagController, this);

                    taskViewModel.AddResultsCallback(result => searchResultView.HandleSearchResult(result));

                    var tabItem = new TabItem();

                    TextBlock header = new TextBlock();
                    header.Inlines.Add(taskViewModel.Description);
                    header.ToolTip = string.Format("{0}: {1}", taskViewModel.Name, taskViewModel.Description);
                    tabItem.Header = header;
                    
                    tabItem.Content = searchResultView;
                    resultControl.Items.Add(tabItem);
                    tabItem.IsSelected = true;

                    AddViewModel(taskViewModel, tabItem);

                    if (taskViewModel.Status == TaskStatus.Ready)
                    {
                        taskViewModel.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.Search", ex);
            }
        }
    }
}

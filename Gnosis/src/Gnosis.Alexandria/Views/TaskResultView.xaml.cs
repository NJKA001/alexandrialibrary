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
        private ITaskController taskController;
        private IMediaItemController mediaItemController;
        private readonly IDictionary<Guid, ITaskResultViewModel> tabMap = new Dictionary<Guid, ITaskResultViewModel>();

        public void Initialize(ILogger logger, ITaskController taskController, IMediaItemController mediaItemController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");

            this.logger = logger;
            this.taskController = taskController;
            this.mediaItemController = mediaItemController;
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

        public void Search(SearchTaskViewModel searchViewModel)
        {
            if (searchViewModel == null)
                throw new ArgumentNullException("searchViewModel");

            try
            {
                if (!tabMap.ContainsKey(searchViewModel.Id))
                {
                    var searchResultView = new SearchResultView();
                    searchResultView.Initialize(logger, mediaItemController, taskController, this);

                    searchViewModel.AddResultsCallback(result => searchResultView.HandleSearchResult(result));

                    var tabItem = new TabItem();

                    TextBlock header = new TextBlock();
                    header.Inlines.Add(searchViewModel.Description);
                    header.ToolTip = string.Format("{0}: {1}", searchViewModel.Name, searchViewModel.Description);
                    tabItem.Header = header;
                    
                    tabItem.Content = searchResultView;
                    resultControl.Items.Add(tabItem);
                    tabItem.IsSelected = true;

                    AddViewModel(searchViewModel, tabItem);

                    if (searchViewModel.Status == TaskStatus.Ready)
                    {
                        searchViewModel.Start();
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

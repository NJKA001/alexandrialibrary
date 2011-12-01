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

        private void AddViewModel(ITaskViewModel taskViewModel, TabItem tabItem)
        {
            if (tabMap.ContainsKey(taskViewModel.Id))
                throw new InvalidOperationException("There is already a tab for this task. name=" + taskViewModel.Name + " description=" + taskViewModel.Description);

            var taskResultViewModel = new TaskResultViewModel(taskViewModel, tabItem);

            tabItem.DataContext = taskResultViewModel;
            taskResultViewModel.AddClosedCallback(x => CloseTab(x));

            tabMap.Add(taskViewModel.Id, taskResultViewModel);
            taskController.AddTaskViewModel(taskViewModel);
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
                    searchResultView.Initialize(logger, mediaItemController);

                    searchViewModel.AddResultsCallback(result => searchResultView.HandleSearchResult(result));

                    var tabItem = new TabItem();
                    tabItem.Header = searchViewModel.Description;
                    tabItem.Content = searchResultView;
                    resultControl.Items.Add(tabItem);
                    tabItem.IsSelected = true;

                    AddViewModel(searchViewModel, tabItem);

                    searchViewModel.Start();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskResultView.Search", ex);
            }
        }
    }
}

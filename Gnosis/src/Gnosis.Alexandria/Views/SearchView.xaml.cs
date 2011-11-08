using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IMediaDetailRepository repository;
        private ITaskController taskController;
        private readonly ObservableCollection<IMediaDetailViewModel> viewModels = new ObservableCollection<IMediaDetailViewModel>();

        private void HandleSearchResults(IEnumerable<IMediaDetail> results)
        {
            try
            {
                if (results == null)
                {
                    System.Diagnostics.Debug.WriteLine("search results are null");
                    return;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("search results count: " + results.Count());

                    Action action = () =>
                        {
                            foreach (var detail in results)
                                viewModels.Add(new MediaDetailViewModel(detail));
                        };

                    this.Dispatcher.Invoke(action, DispatcherPriority.DataBind);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  HandleSearchResults", ex);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var search = searchTextBox.Text;
                if (string.IsNullOrEmpty(search))
                    return;

                var task = repository.Search(search);
                task.AddResultsCallback(results => HandleSearchResults(results));
                var taskViewModel = new SearchTaskViewModel(logger, task, search);
                taskController.AddTask(taskViewModel);
                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  searchButton_Click", ex);
            }
        }

        public void Initialize(ILogger logger, IMediaDetailRepository repository, ITaskController taskController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (taskController == null)
                throw new ArgumentNullException("taskController");

            this.logger = logger;
            this.repository = repository;
            this.taskController = taskController;

            try
            {
                searchList.ItemsSource = viewModels;
            }
            catch (Exception ex)
            {
                logger.Error("  SearchView.Initialize", ex);
            }
        }
    }
}

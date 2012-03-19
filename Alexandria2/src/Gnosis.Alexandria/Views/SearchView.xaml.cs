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

//using Gnosis.Tags;
//using Gnosis.Tags.Id3.Id3v2;
using Gnosis.Tasks;
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
        private ITaskController taskController;
        private TaskResultView taskResultView;

        private void DoSearch()
        {
            try
            {
                var search = searchTextBox.Text;
                if (string.IsNullOrEmpty(search))
                    return;

                var searchViewModel = taskController.GetSearchViewModel(search);
                taskResultView.Search(searchViewModel);
            }
            catch (Exception ex)
            {
                logger.Error("  DoSearch", ex);
            }
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return || e.Key == Key.Enter)
                {
                    e.Handled = true;
                    DoSearch();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  searchTextBox_KeyUp", ex);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DoSearch();
        }

        public void Initialize(ILogger logger, ITaskController taskController, TaskResultView taskResultView)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (taskResultView == null)
                throw new ArgumentNullException("taskResultView");
            
            this.logger = logger;
            this.taskController = taskController;
            this.taskResultView = taskResultView;
        }
    }
}

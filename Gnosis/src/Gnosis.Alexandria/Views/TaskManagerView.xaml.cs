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

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for TaskManagerView.xaml
    /// </summary>
    public partial class TaskManagerView : UserControl
    {
        public TaskManagerView()
        {
            InitializeComponent();

            this.taskItemsControl.ItemsSource = taskViewModels;
        }

        private ILogger logger;
        private SpiderFactory spiderFactory;

        private readonly ObservableCollection<ITaskViewModel> taskViewModels = new ObservableCollection<ITaskViewModel>();
        private readonly IList<Action<ITaskViewModel>> startedCallbacks = new List<Action<ITaskViewModel>>();
        private readonly IList<Action<ITaskViewModel>> cancelledCallbacks = new List<Action<ITaskViewModel>>();

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var item = element.FindContainingItem<ListBoxItem>();
                if (item == null)
                    return;

                var viewModel = item.DataContext as ITaskViewModel;
                if (viewModel == null)
                    return;

                viewModel.Previous();
            }
            catch (Exception ex)
            {
                logger.Error("  previousButton_Click", ex);
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var item = element.FindContainingItem<ListBoxItem>();
                if (item == null)
                    return;

                var viewModel = item.DataContext as ITaskViewModel;
                if (viewModel == null)
                    return;

                switch (viewModel.Status)
                {
                    case TaskStatus.Cancelled:
                    case TaskStatus.Completed:
                    case TaskStatus.Failed:
                        viewModel.Reset();
                        viewModel.Start();
                        break;
                    case TaskStatus.Paused:
                        viewModel.Resume();
                        break;
                    case TaskStatus.Ready:
                        viewModel.Start();
                        break;
                    case TaskStatus.Running:
                        break;
                    default:
                        throw new InvalidOperationException("Invalid TaskStatus: " + viewModel.Status);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  playButton_Click", ex);
            }

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var item = element.FindContainingItem<ListBoxItem>();
                if (item == null)
                    return;

                var viewModel = item.DataContext as ITaskViewModel;
                if (viewModel == null)
                    return;

                if (viewModel.Status == TaskStatus.Running)
                    viewModel.Pause();
            }
            catch (Exception ex)
            {
                logger.Error("  pauseButton_Click", ex);
            }

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var item = element.FindContainingItem<ListBoxItem>();
                if (item == null)
                    return;

                var viewModel = item.DataContext as ITaskViewModel;
                if (viewModel == null)
                    return;

                if (viewModel.Status == TaskStatus.Paused || viewModel.Status == TaskStatus.Running)
                    viewModel.Cancel();
            }
            catch (Exception ex)
            {
                logger.Error("  stopButton_Click", ex);
            }

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = sender as UIElement;
                if (element == null)
                    return;

                var item = element.FindContainingItem<ListBoxItem>();
                if (item == null)
                    return;

                var viewModel = item.DataContext as ITaskViewModel;
                if (viewModel == null)
                    return;

                viewModel.Next();
            }
            catch (Exception ex)
            {
                logger.Error("  nextButton_Click", ex);
            }

        }

        public void Initialize(ILogger logger, SpiderFactory spiderFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (spiderFactory == null)
                throw new ArgumentNullException("spiderFactory");

            try
            {
                this.logger = logger;
                this.spiderFactory = spiderFactory;

                var catalogSpider = spiderFactory.CreateCatalogSpider();
                var defaultCatalogUrl = new Uri(@"C:\Documents and Settings\Administrator\My Documents\My Music"); //"C:\Users\Dan\Music");

                var task = new Gnosis.Tasks.CatalogMediaTask(logger, catalogSpider, defaultCatalogUrl, TimeSpan.Zero, 0);
                var taskViewModel = new CatalogMediaTaskViewModel(logger, task);
                taskViewModels.Add(taskViewModel);
            }
            catch (Exception ex)
            {
                logger.Error("TaskManagerView.Initialize", ex);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void elapsedSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }

        private void elapsedSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

        }
    }
}

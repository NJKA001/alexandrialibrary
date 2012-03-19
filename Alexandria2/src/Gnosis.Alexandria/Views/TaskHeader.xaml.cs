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
using System.Windows.Controls.Primitives;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.ViewModels;
using Gnosis.Tasks;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for TaskHeader.xaml
    /// </summary>
    public partial class TaskHeader : UserControl
    {
        public TaskHeader()
        {
            InitializeComponent();
        }

        public TaskHeader(ILogger logger, ITaskViewModel taskViewModel)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (taskViewModel == null)
                throw new ArgumentNullException("taskViewModel");

            InitializeComponent();

            this.logger = logger;
            this.taskViewModel = taskViewModel;
            this.DataContext = taskViewModel;
        }

        private readonly ILogger logger;
        private readonly ITaskViewModel taskViewModel;

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                taskViewModel.Previous();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.previousButton_Click", ex);
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (taskViewModel.Status)
                {
                    case TaskStatus.Cancelled:
                    case TaskStatus.Completed:
                    case TaskStatus.Failed:
                        taskViewModel.Reset();
                        taskViewModel.Start();
                        break;
                    case TaskStatus.Paused:
                        taskViewModel.Resume();
                        break;
                    case TaskStatus.Ready:
                        taskViewModel.Start();
                        break;
                    case TaskStatus.Running:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.playButton_Click", ex);
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (taskViewModel.Status == TaskStatus.Running)
                    taskViewModel.Pause();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.pauseButton_Click", ex);
            }

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (taskViewModel.Status == TaskStatus.Paused || taskViewModel.Status == TaskStatus.Running)
                    taskViewModel.Stop();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.stopButton_Click", ex);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                taskViewModel.Next();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.nextButton_Click", ex);
            }

        }

        private void elapsedSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            try
            {
                taskViewModel.BeginProgressUpdate();
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.elapsedSlider_DragStarted", ex);
            }
        }

        private void elapsedSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            try
            {
                var value = (int)Math.Ceiling(elapsedSlider.Value);

                taskViewModel.UpdateProgress(value);
            }
            catch (Exception ex)
            {
                logger.Error("  TaskHeader.elapsedSlider_DragCompleted", ex);
            }
        }
    }
}

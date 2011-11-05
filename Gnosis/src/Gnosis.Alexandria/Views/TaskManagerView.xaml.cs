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

            taskViewModels.Add(new TaskViewModel("Building Catalog"));
            taskViewModels.Add(new TaskViewModel("Tagging Audio", "pack://application:,,,/Images/File Audio-01.png"));
            taskViewModels.Add(new TaskViewModel("Tagging Video", "pack://application:,,,/Images/File Video-01.png"));
            taskViewModels.Add(new TaskViewModel("Tagging Images", "pack://application:,,,/Images/Image JPEG-01.png"));
            this.taskItemsControl.ItemsSource = taskViewModels;
        }

        private readonly ObservableCollection<ITaskViewModel> taskViewModels = new ObservableCollection<ITaskViewModel>();
    }
}

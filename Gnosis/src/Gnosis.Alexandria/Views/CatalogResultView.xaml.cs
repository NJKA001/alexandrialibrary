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

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for CatalogResultView.xaml
    /// </summary>
    public partial class CatalogResultView : UserControl
    {
        public CatalogResultView()
        {
            InitializeComponent();

            catalogResultControl.ItemsSource = viewModels;
        }

        private ILogger logger;
        private readonly ObservableCollection<ITaskDetailViewModel> viewModels = new ObservableCollection<ITaskDetailViewModel>();

        public void Initialize(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
        }

        public void AddErrorDetail(TaskError error)
        {
            var viewModel = new TaskErrorDetailViewModel(error);
 
            Action action = () => viewModels.Add(viewModel);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
            
        }

        public void AddProgressDetail(TaskProgress progress)
        {
            var viewModel = new TaskProgressDetailViewModel(progress);
            
            Action action = () => viewModels.Add(viewModel);
            Dispatcher.Invoke(action, DispatcherPriority.DataBind);
        }
    }
}

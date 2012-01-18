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
    /// Interaction logic for CommandView.xaml
    /// </summary>
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private ICommandController commandController;
        private ITaskController taskController;
        private TaskResultView taskResultView;

        private void commandItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var item = sender as ListBoxItem;
                if (item == null)
                    return;

                var viewModel = item.DataContext as ICommandViewModel;
                if (viewModel == null)
                    return;

                viewModel.Execute(taskController, taskResultView);
            }
            catch (Exception ex)
            {
                logger.Error("  commandItem_MouseDoubleClick", ex);
            }
        }

        public void Initialize(ILogger logger, ICommandController commandController, ITaskController taskController, TaskResultView taskResultView)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (commandController == null)
                throw new ArgumentNullException("commandController");
            if (taskController == null)
                throw new ArgumentNullException("taskController");
            if (taskResultView == null)
                throw new ArgumentNullException("taskResultView");

            this.logger = logger;
            this.commandController = commandController;
            this.taskController = taskController;
            this.taskResultView = taskResultView;

            try
            {
                commandItemContainer.ItemsSource = commandController.Commands;
            }
            catch (Exception ex)
            {
                logger.Error("  CommandView.Initialize", ex);
            }
        }
    }
}

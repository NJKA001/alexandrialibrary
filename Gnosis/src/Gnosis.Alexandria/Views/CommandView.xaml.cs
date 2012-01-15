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

        public void Initialize(ILogger logger, ICommandController commandController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (commandController == null)
                throw new ArgumentNullException("commandController");

            this.logger = logger;
            this.commandController = commandController;

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

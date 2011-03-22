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

using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for HomeTabView.xaml
    /// </summary>
    public partial class HomeTabView : ControlView, IHomeTabView
    {
        public HomeTabView()
        {
            InitializeComponent();

            ((IView) this).Title = "New Tab";
        }

        private void SearchEntered(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && !string.IsNullOrEmpty(txtSearch.Text))
            {
                e.Handled = true;
                
                var message = ServiceLocator.GetObject<INewSearchTabRequestedMessage>();
                message.Search = txtSearch.Text;
                Dispatch<INewSearchTabRequestedMessage>(Id, message);
            }
        }
    }
}

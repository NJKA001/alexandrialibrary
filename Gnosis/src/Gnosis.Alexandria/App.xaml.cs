using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var main = new MainWindow();
                main.Show();
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            videoPlayerControl.ShutDown();
        }

        private void launchVideoPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var videoPlayer = new Gnosis.Video.Vlc.VideoPlayer();
                //videoPlayer.Show();
            }
            catch (Exception ex)
            {
                var m = ex.Message;
            }
        }
    }
}

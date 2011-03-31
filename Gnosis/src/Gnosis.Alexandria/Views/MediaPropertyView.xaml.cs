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
    /// Interaction logic for MediaPropertyView.xaml
    /// </summary>
    public partial class MediaPropertyView : UserControl
    {
        public MediaPropertyView()
        {
            InitializeComponent();
        }

        private ITrackController trackController;
        private ITagController tagController;
        private ITrack track;

        private void ChangePictureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (track != null)
                {
                    var dialog = new System.Windows.Forms.OpenFileDialog();
                    dialog.Filter = "Image Files (*.jpg,*.jpeg,*.png,*.gif)|*.jpg;*.jpeg;*.png;*.gif|All Files (*.*)|*.*";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        tagController.AddPicture(track, dialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void SaveTrackPropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (track != null)
                {
                    tagController.SaveTag(track);
                    trackController.Save(track);
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public ITrack Track
        {
            get { return track; }
            set
            {
                track = value;
                this.DataContext = track;
            }
        }

        public void Initialize(ITrackController trackController, ITagController tagController)
        {
            this.trackController = trackController;
            this.tagController = tagController;
        }
    }
}

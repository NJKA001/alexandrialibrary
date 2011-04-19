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

using log4net;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Models;

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

        private static readonly ILog log = LogManager.GetLogger(typeof(MediaPropertyView));
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
                        if (track.Path.EndsWith(".mp3"))
                        {
                            tagController.AddPicture(track, dialog.FileName);
                        }
                        else
                        {
                            track.ImagePath = dialog.FileName;
                            trackController.Save(track);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("MediaPropertyView.ChangePictureButton_Click", ex);
                MessageBox.Show("There was an error trying to add a picture to this track.\n\n" + ex.Message, "Could Not Add Picture To Track");
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
                log.Error("MediaPropertyView.SaveTrackPropertiesButton_Click", ex);
                MessageBox.Show(ex.Message, "Could Not Save Track");
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

        private string GetHtml(string url)
        {
            var html = string.Empty;
            var request = System.Net.HttpWebRequest.Create(url);
            var response = request.GetResponse();
            if (response != null)
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }

            return html;
        }

        private string GetLyrics(string html)
        {
            var lyrics = string.Empty;

            if (!string.IsNullOrEmpty(html))
            {
                var regex = new System.Text.RegularExpressions.Regex("<img src='http://images.wikia.com/lyricwiki/images/phone_right.gif' alt='phone' width='16' height='17'/></a></div>(?<LYRICS>[^!]+)");
                var match = regex.Match(html);
                if (match != null)
                {
                    var encodedLyrics = match.Groups["LYRICS"].Value;
                    if (!string.IsNullOrEmpty(encodedLyrics))
                    {
                        lyrics = System.Web.HttpUtility.HtmlDecode(encodedLyrics.TrimEnd('<')).Replace("<br />", "\r\n");
                    }
                }
            }

            return lyrics;
        }

        private void lyricsExpander_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (track != null && e.Data.GetDataPresent(typeof(string)))
                {
                    var url = e.Data.GetData(typeof(string)) as string;
                    if (url != null && url.StartsWith("http://lyrics.wikia.com"))
                    {
                        var html = GetHtml(url);
                        var lyrics = GetLyrics(html);
                        if (!string.IsNullOrEmpty(lyrics))
                        {
                            track.Lyrics = lyrics;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        private void lyricsExpander_DragEnter(object sender, DragEventArgs e)
        {
            //foreach (var format in e.Data.GetFormats())
            //{
            //    var x = format;
            //}
            e.Effects = DragDropEffects.All;
        }

        private void countryTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    e.Handled = true;
                    if (textBox != null && track != null)
                    {
                        log.Info("MediaPropertyView.countryTextBox_KeyUp: Saving country=" + textBox.Text);
                        track.IsCountryBeingEdited = false;
                        track.Country = textBox.Text;
                        trackController.Save(track);
                        releaseDateTextBox.Focus();
                    }
                }
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    if (textBox != null && track != null)
                    {
                        track.IsCountryBeingEdited = false;
                        releaseDateTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("MediaPropertyView.countryTextBox_KeyUp", ex);
            }
        }

        private void countryImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (track != null && !track.IsCountryBeingEdited)
                {
                    track.IsCountryBeingEdited = true;
                    countryTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                log.Error("MediaPropertyView.countryImage_MouseUp", ex);
            }
        }
    }
}

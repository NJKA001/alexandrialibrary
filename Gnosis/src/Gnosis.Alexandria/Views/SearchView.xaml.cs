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

using Gnosis.Alexandria.Controllers;
using Gnosis.Core;
using System.ComponentModel;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            autocompleteListBox.ItemsSource = autocompleteTracks;
            imageLoader.WorkerSupportsCancellation = true;
            imageLoader.DoWork += LoadAutocompleteImages;
        }

        private ITrackController trackController;
        private ITagController tagController;
        private readonly ObservableCollection<ITrack> autocompleteTracks = new ObservableCollection<ITrack>();
        private string lastSearch = string.Empty;
        private const int maxSuggestions = 10;
        private BackgroundWorker imageLoader = new BackgroundWorker();

        private string GetSearch()
        {
            return searchTextBox.Text ?? string.Empty;
        }

        private void Filter()
        {
            trackController.Filter(GetSearch());
        }

        private void ClearAutocomplete()
        {
            autocompleteTracks.Clear();
            RefreshAutocomplete();
        }

        private void RefreshAutocomplete()
        {
            autocompletePopup.IsOpen = autocompleteTracks.Count > 0;
            imageLoader.CancelAsync();
            imageLoader.RunWorkerAsync();
        }

        private void LoadAutocompleteImages(object sender, DoWorkEventArgs args)
        {
            var worker = sender as BackgroundWorker;
            foreach (var track in autocompleteTracks)
            {
                if (worker.CancellationPending)
                {
                    args.Cancel = true;
                    break;
                }
                tagController.LoadPicture(track);       
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ClearAutocomplete();
                Filter();
            }
            else
            {
                var search = GetSearch();
                if (search != lastSearch)
                {
                    lastSearch = search;
                    ClearAutocomplete();
                    if (!string.IsNullOrEmpty(lastSearch) && lastSearch.Length > 1)
                    {

                        var suggestions = trackController.Search(lastSearch).Take(maxSuggestions);
                        if (suggestions != null)
                        {
                            foreach (var suggestion in suggestions)
                                autocompleteTracks.Add(suggestion);
                        }
                    }

                    RefreshAutocomplete();
                }
            }
        }

        private void autocompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autocompleteListBox.SelectedItem != null)
            {
                var track = autocompleteListBox.SelectedItem as ITrack;
                if (track != null)
                {
                    searchTextBox.Text = track.Title;
                    ClearAutocomplete();
                    Filter();
                }
            }
        }

        public void Initialize(ITrackController trackController, ITagController tagController)
        {
            this.trackController = trackController;
            this.tagController = tagController;
        }
    }
}

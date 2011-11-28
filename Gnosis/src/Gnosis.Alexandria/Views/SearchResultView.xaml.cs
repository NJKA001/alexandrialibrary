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
using Gnosis.Alexandria.Extensions;
using Gnosis.Alexandria.ViewModels;
using Gnosis.Image;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchResultView.xaml
    /// </summary>
    public partial class SearchResultView : UserControl
    {
        public SearchResultView()
        {
            InitializeComponent();

            searchResultContainer.DataContext = this;
        }

        private ILogger logger;
        private IMediaItemController mediaItemController;
        private readonly ObservableCollection<ISearchResultViewModel> results = new ObservableCollection<ISearchResultViewModel>();

        private void itemNamePanel_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var uiElement = sender as UIElement;
                if (uiElement == null)
                    return;

                var listBoxItem = uiElement.FindContainingItem<ListBoxItem>();
                if (listBoxItem == null)
                    return;

                var viewModel = listBoxItem.DataContext as ISearchResultViewModel;
                if (viewModel == null)
                    return;
                
                var image = e.Data.GetData(DataFormats.Bitmap) as System.Windows.Controls.Image;
                if (image != null)
                {
                    return;
                }

                var html = e.Data.GetData(DataFormats.Html) as string;
                if (!string.IsNullOrEmpty(html))
                {
                    var regex = new System.Text.RegularExpressions.Regex("src=['\"](?<PATH>[^\"']+)");
                    var match = regex.Match(html);
                    if (match != null)
                    {
                        var path = match.Groups["PATH"].Value;
                        if (!string.IsNullOrEmpty(path))
                        {
                            var request = System.Net.HttpWebRequest.Create(path);
                            var response = request.GetResponse();
                            if (response != null)
                            {
                                using (var stream = response.GetResponseStream())
                                {
                                    var thumbnail = new Uri(path, UriKind.Absolute);
                                    var thumbnailData = stream.ToBuffer();
                                    if (thumbnailData != null)
                                    {
                                        if (viewModel is ArtistSearchResultViewModel)
                                        {
                                            mediaItemController.SetArtistThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else if (viewModel is AlbumSearchResultViewModel)
                                        {
                                            mediaItemController.SetAlbumThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else if (viewModel is TrackSearchResultViewModel)
                                        {
                                            mediaItemController.SetTrackThumbnail(viewModel.MediaItem, thumbnail, thumbnailData);
                                            viewModel.UpdateThumbnail(thumbnail, thumbnailData);
                                        }
                                        else
                                            logger.Warn("  Cannot set thumbnail - unknown search result type: " + viewModel.GetType().Name);
                                    }

                                    //var data = new ByteVector(buffer);
                                    //var picture = new TagLib.Picture(data);
                                    //tagController.AddPicture(track, picture);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("itemImageBorder_Drop", ex);
            }
        }

        private void CloseViewModel(ISearchResultViewModel viewModel)
        {
            try
            {
                if (results.Contains(viewModel))
                    results.Remove(viewModel);
            }
            catch (Exception ex)
            {
                logger.Error("  CloseViewModel", ex);
            }
        }

        public IEnumerable<ISearchResultViewModel> Results
        {
            get { return results; }
        }

        public void Initialize(ILogger logger, IMediaItemController mediaItemController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaItemController == null)
                throw new ArgumentNullException("mediaItemController");

            this.logger = logger;
            this.mediaItemController = mediaItemController;
        }

        public void AddViewModel(ISearchResultViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            viewModel.AddCloseCallback(x => CloseViewModel(x));
            results.Add(viewModel);
        }
    }
}

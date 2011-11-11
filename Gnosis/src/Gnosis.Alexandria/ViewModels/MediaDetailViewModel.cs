using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class MediaDetailViewModel
        : IMediaDetailViewModel
    {
        public MediaDetailViewModel(IMediaDetail detail)
        {
            if (detail == null)
                throw new ArgumentNullException("detail");

            this.detail = detail;
        }

        private readonly IMediaDetail detail;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public IMediaDetail Detail
        {
            get { return detail; }
        }

        public string Type
        {
            get { return detail.Tag.Type.Name; }
        }

        public string Value
        {
            get { return detail.Tag.Tuple.ToString(); }
        }

        public object ArtistThumbnail
        {
            get { return detail.ArtistThumbnail; }
        }

        public object CollectionThumbnail
        {
            get { return detail.CollectionThumbnail; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

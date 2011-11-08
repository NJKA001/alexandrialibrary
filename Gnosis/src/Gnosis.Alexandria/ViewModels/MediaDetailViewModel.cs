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

        public string Type
        {
            get { return detail.Tag.Type.Name; }
        }

        public string Value
        {
            get { return detail.Tag.Tuple.ToString(); }
        }

        public object Thumbnail
        {
            get { return detail.Thumbnail; }
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

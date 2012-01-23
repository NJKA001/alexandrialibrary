using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Alexandria.ViewModels
{
    public class MediaMarqueeViewModel
        : IMediaMarqueeViewModel
    {
        public MediaMarqueeViewModel(IMediaMarquee marquee)
        {
            if (marquee == null)
                throw new ArgumentNullException("marquee");

            this.marquee = marquee;
            this.location = marquee.Location;
            this.category = marquee.Category;
            this.name = marquee.Name;
            this.subtitle = marquee.Subtitle;
        }

        private IMediaMarquee marquee;
        private Uri location;
        private MediaCategory category;
        private string name;
        private string subtitle;
        
        private bool isSelected;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Location
        {
            get { return location; }
        }

        public MediaCategory Category
        {
            get { return category; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Subtitle
        {
            get { return subtitle; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

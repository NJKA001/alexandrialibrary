using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class LinkViewModel
        : ILinkViewModel
    {
        public LinkViewModel(ILink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            this.link = link;
        }

        private readonly ILink link;
        
        private bool isClosed;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public long Id
        {
            get { return link.Id; }
        }

        public string Name
        {
            get { return link.Name; }
        }

        public string Relationship
        {
            get { return link.Relationship; }
        }

        public Uri Source
        {
            get { return link.Source; }
        }

        public Uri Target
        {
            get { return link.Target; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                isClosed = value;
                OnPropertyChanged("IsClosed");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

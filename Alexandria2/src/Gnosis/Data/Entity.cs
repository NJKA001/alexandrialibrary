using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public abstract class Entity
        : IEntity, INotifyPropertyChanged
    {
        private bool isChanged;

        private readonly ObservableCollection<ILink> links = new ObservableCollection<ILink>();
        private readonly ObservableCollection<ITag> tags = new ObservableCollection<ITag>();

        protected void NotifyPropertyChanged(string propertyName)
        {
            isChanged = true;

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsChanged
        {
            get { return isChanged; }
        }

        public IEnumerable<ILink> Links { get { return links; } }

        public IEnumerable<ITag> Tags { get { return tags; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddLink(ILink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            if (links.Contains(link))
                return;

            links.Add(link);
        }

        public void RemoveLink(ILink link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            if (!links.Contains(link))
                return;

            links.Remove(link);
        }

        public void AddTag(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (tags.Contains(tag))
                return;

            tags.Add(tag);
        }

        public void RemoveTag(ITag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (!tags.Contains(tag))
                return;

            tags.Remove(tag);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public abstract class Entity
        : IEntity
    {
        private string name;
        private string image;
        private readonly ObservableCollection<Link> links = new ObservableCollection<Link>();
        private readonly ObservableCollection<Tag> tags = new ObservableCollection<Tag>();

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public IEnumerable<Link> Links
        {
            get { return links; }
        }

        public IEnumerable<Tag> Tags
        {
            get { return tags; }
        }

        public void AddLink(Link link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            if (links.Contains(link))
                return;

            links.Add(link);
        }

        public void RemoveLink(Link link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            if (!links.Contains(link))
                return;

            links.Remove(link);
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (tags.Contains(tag))
                return;

            tags.Add(tag);
        }

        public void RemoveTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (!tags.Contains(tag))
                return;

            tags.Remove(tag);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

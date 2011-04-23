using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public abstract class SourceBase : ISource, INotifyPropertyChanged
    {
        protected SourceBase() : this(Guid.NewGuid())
        {
        }

        protected SourceBase(Guid id)
        {
            this.id = id;
            Path = string.Empty;
            ImagePath = string.Empty;
            Name = "Unknown Source";
            Creator = "Unknown Creator";
            Summary = string.Empty;
            Date = new DateTime(2000, 1, 1);
        }

        private Guid id;
        private string path;
        private string imagePath;
        private ICollection<byte> imageData;
        private ISource parent;
        private string name;
        private string creator;
        private string summary;
        private DateTime date;
        private int number;
        private readonly ObservableCollection<ISourceProperty> properties = new ObservableCollection<ISourceProperty>();
        private readonly ObservableCollection<ISource> children = new ObservableCollection<ISource>();
        private bool isExpanded;
        private bool isSelected;
        private bool isBeingEdited;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected IEnumerable<ISourceProperty> GetPropertiesWithName(string name)
        {
            return properties.Where(x => x.Name == name);
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ICollection<byte> ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;
                    OnPropertyChanged("ImageData");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public object ImageSource
        {
            get
            {
                return !string.IsNullOrEmpty(imagePath) ? (object)imagePath : imageData;
            }
        }

        public ISource Parent
        {
            get { return parent; }
            set
            {
                if (parent != value)
                {
                    parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NameHash = value != null ? value.AsNameHash() : string.Empty;
                    NameMetaphone = value != null ? value.AsDoubleMetaphone() : string.Empty;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("NameHash");
                    OnPropertyChanged("NameMetaphone");
                    OnPropertyChanged("Marquee");
                }
            }
        }

        public string NameHash
        {
            get;
            private set;
        }

        public string NameMetaphone
        {
            get;
            private set;
        }

        public string Creator
        {
            get { return creator; }
            set
            {
                if (creator != value && value != null)
                {
                    creator = value;
                    OnPropertyChanged("Creator");
                    OnPropertyChanged("Marquee");
                }
            }
        }

        public string Summary
        {
            get { return summary; }
            set
            {
                if (summary != value && value != null)
                {
                    summary = value;
                    OnPropertyChanged("Summary");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        #region Pattern Properties

        private string imagePattern = string.Empty;
        private string childPattern = string.Empty;
        private string pagePattern = string.Empty;

        public string ImagePattern
        {
            get { return imagePattern; }
            set
            {
                if (imagePattern != value && value != null)
                {
                    imagePattern = value;
                    OnPropertyChanged("ImagePattern");
                }
            }
        }

        public string ChildPattern
        {
            get { return childPattern; }
            set
            {
                if (childPattern != value && value != null)
                {
                    childPattern = value;
                    OnPropertyChanged("ChildPattern");
                }
            }
        }

        public string PagePattern
        {
            get { return pagePattern; }
            set
            {
                if (pagePattern != value && value != null)
                {
                    pagePattern = value;
                    OnPropertyChanged("PagePattern");
                }
            }
        }

        #endregion

        public IEnumerable<ISourceProperty> Properties
        {
            get { return properties; }
        }

        public IEnumerable<ISource> Children
        {
            get { return children; }
        }

        public virtual string Marquee
        {
            get { return string.Format("{0} {1}", Name ?? "Unknown", Creator != null ? string.Format("({0})", Creator) : string.Empty ); }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }

                if (isExpanded && parent != null)
                    parent.IsExpanded = true;
            }
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

        public bool IsBeingEdited
        {
            get { return isBeingEdited; }
            set
            {
                if (isBeingEdited != value)
                {
                    isBeingEdited = value;
                    OnPropertyChanged("IsBeingEdited");
                    OnPropertyChanged("DisplayVisibility");
                    OnPropertyChanged("EditVisibility");
                    OnPropertyChanged("PatternVisibility");
                }
            }
        }

        public Visibility DisplayVisibility
        {
            get { return IsBeingEdited ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility EditVisibility
        {
            get { return IsBeingEdited ? Visibility.Visible : Visibility.Collapsed; }
        }

        public virtual Visibility PatternVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public virtual void AddProperty(ISourceProperty property)
        {
            properties.Add(property);
            OnPropertyChanged("Properties");
        }

        public virtual void RemoveProperty(ISourceProperty property)
        {
            properties.Remove(property);
            OnPropertyChanged("Properties");
        }

        public virtual void AddChild(ISource child)
        {
            if (child != null && !(child is ProxySource))
            {
                if (children.Count == 1 && children[0] is ProxySource)
                    children.RemoveAt(0);
            }

            children.Add(child);
            OnPropertyChanged("Children");
        }

        public virtual void RemoveChild(ISource child)
        {
            children.Remove(child);
            OnPropertyChanged("Children");
        }

        public void DeselectAll()
        {
            IsSelected = false;
            foreach (var child in children)
                child.DeselectAll();
        }

        public bool IsDescendantOf(ISource source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (Parent == null)
                return false;

            return (Parent.Id == source.Id) ? true : Parent.IsDescendantOf(source);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

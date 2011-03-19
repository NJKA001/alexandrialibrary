﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Gnosis.Archon
{
    public abstract class SourceBase : ISource, INotifyPropertyChanged
    {
        protected SourceBase() : this(Guid.NewGuid())
        {
        }

        protected SourceBase(Guid id)
        {
            this.id = id;
        }

        private Guid id;
        private string path;
        private string imagePath;
        private ICollection<byte> imageData;
        private ISource parent;
        private string name;
        private readonly ObservableCollection<ISourceProperty> properties = new ObservableCollection<ISourceProperty>();
        private readonly ObservableCollection<ISource> children = new ObservableCollection<ISource>();
        private bool isExpanded;
        private bool isSelected;

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
                    OnPropertyChanged("Name");

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

        public IEnumerable<ISourceProperty> Properties
        {
            get { return properties; }
        }

        public IEnumerable<ISource> Children
        {
            get { return children; }
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

        public virtual void AddProperty(ISourceProperty property)
        {
            properties.Add(property);
            OnPropertyChanged("Properties");
        }

        public virtual void AddChild(ISource child)
        {
            children.Add(child);
            OnPropertyChanged("Children");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

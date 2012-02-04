using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public abstract class MediaItemViewModel
        : IMediaItemViewModel
    {
        protected MediaItemViewModel(IMediaItemController controller, IMediaItem item, string type, object icon)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            if (item == null)
                throw new ArgumentNullException("item");
            if (type == null)
                throw new ArgumentNullException("type");
            if (icon == null)
                throw new ArgumentNullException("icon");

            this.controller = controller;
            this.item = item;
            this.type = type;
            this.icon = icon;
        }

        protected readonly IMediaItemController controller;
        protected readonly IMediaItem item;
        
        private readonly string type;
        private readonly object icon;

        private bool linksInitialized;
        private bool tagsInitialized;

        private readonly ObservableCollection<ILinkViewModel> links = new ObservableCollection<ILinkViewModel>();
        private readonly ObservableCollection<ITagViewModel> tags = new ObservableCollection<ITagViewModel>();

        private bool isSelected;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Type
        {
            get { return type; }
        }

        public Uri Id
        {
            get { return item.Location; }
        }

        public string Name
        {
            get { return item.Name; }
        }

        public string Summary
        {
            get { return item.Summary; }
        }

        public uint Number
        {
            get { return item.Number; }
        }

        public TimeSpan Duration
        {
            get { return item.Duration; }
        }

        public string DurationString
        {
            get { return item.Duration.ToFormattedString(); }
        }

        public uint Height
        {
            get { return item.Height; }
        }

        public uint Width
        {
            get { return item.Width; }
        }

        public string Dimensions
        {
            get { return Height > 0 && Width > 0 ? string.Format("{0} x {1}", Width, Height) : string.Empty; }
        }

        public virtual string Years
        {
            get { return item.FromDate.Year.ToString(); }
        }

        public Uri Creator
        {
            get { return item.Creator; }
        }

        public string CreatorName
        {
            get { return item.CreatorName; }
        }

        public Uri Catalog
        {
            get { return item.Catalog; }
        }

        public string CatalogName
        {
            get { return item.CatalogName; }
        }

        public Uri Target
        {
            get { return item.Target; }
        }

        public string TargetType
        {
            get { return item.TargetType; }
        }

        public string UserName
        {
            get { return item.UserName; }
        }

        public object Icon
        {
            get { return icon; }
        }

        public object Image
        {
            get { return item.ThumbnailData != null && item.ThumbnailData.Length > 0 ? item.ThumbnailData : (object)item.Thumbnail; }
        }

        public Visibility DurationVisibility
        {
            get { return item.Duration > TimeSpan.Zero ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility SizeVisibility
        {
            get { return Height > 0 && Width > 0 ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IEnumerable<ILinkViewModel> Links
        {
            get
            {
                if (!linksInitialized)
                {
                    linksInitialized = true;
                    foreach (var link in controller.GetLinksBySource(item.Location))
                        links.Add(new LinkViewModel(link));
                }

                return links;
            }
        }

        public IEnumerable<ITagViewModel> Tags
        {
            get
            {
                if (!tagsInitialized)
                {
                    tagsInitialized = true;
                    foreach (var tag in controller.GetTags(item.Location))
                        tags.Add(new TagViewModel(tag));
                }

                return tags;
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

        public void AddLink(ILinkViewModel link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            links.Add(link);
        }

        public void RemoveLink(ILinkViewModel link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            if (links.Contains(link))
                links.Remove(link);
        }

        public IEnumerable<ILink> GetSystemLinks()
        {
            return item.GetLinks();
        }

        public void AddTag(ITagViewModel tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            tags.Add(tag);
        }

        public void RemoveTag(ITagViewModel tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            if (tags.Contains(tag))
                tags.Remove(tag);
        }

        public IEnumerable<ITag> GetSystemTags()
        {
            return item.GetTags();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IMediaItemViewModel
        : INotifyPropertyChanged
    {
        string Type { get; }

        Uri Id { get; }
        string Name { get; }
        string Summary { get; }
        uint Number { get; }
        TimeSpan Duration { get; }
        string DurationString { get; }
        uint Height { get; }
        uint Width { get; }
        string Dimensions { get; }
        string Years { get; }
        Uri Creator { get; }
        string CreatorName { get; }
        Uri Catalog { get; }
        string CatalogName { get; }
        Uri Target { get; }
        IContentType TargetType { get; }
        string UserName { get; }
        object Icon { get; }
        object Image { get; }

        Visibility DurationVisibility { get; }
        Visibility SizeVisibility { get; }

        IEnumerable<ILinkViewModel> Links { get; }
        IEnumerable<ITagViewModel> Tags { get; }

        bool IsSelected { get; set; }

        void AddLink(ILinkViewModel link);
        void RemoveLink(ILinkViewModel link);
        void AddTag(ITagViewModel tag);
        void RemoveTag(ITagViewModel tag);

        IEnumerable<ILink> GetSystemLinks();
        IEnumerable<ITag> GetSystemTags();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IEntity
        : INotifyPropertyChanged
    {
        string Name { get; set; }
        string Image { get; set; }

        IEnumerable<Link> Links { get; }
        IEnumerable<Tag> Tags { get; }

        void AddLink(Link link);
        void RemoveLink(Link link);
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEntity
        : INotifyPropertyChanged
    {
        bool IsChanged { get; }

        IEnumerable<ILink> Links { get; }
        IEnumerable<ITag> Tags { get; }

        void AddLink(ILink link);
        void RemoveLink(ILink link);

        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    public interface IEntity
        : INotifyPropertyChanged
    {
        Guid Id { get; }
        ITimeStamp TimeStamp { get; }

        bool IsNew();
        bool IsChanged();
    }
}

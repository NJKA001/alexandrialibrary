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
        [PrimaryKeyColumn]
        Guid Id { get; }

        ITimeStamp TimeStamp { get; }

        [ColumnIgnore]
        bool IsNew { get; }
        
        [ColumnIgnore]
        bool IsChanged { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IEntity
        : INotifyPropertyChanged
    {
        Guid Id { get; }

        ITimeStamp TimeStamp { get; }

        [ColumnIgnore]
        bool IsNew { get; }
        
        [ColumnIgnore]
        bool IsChanged { get; }
    }
}

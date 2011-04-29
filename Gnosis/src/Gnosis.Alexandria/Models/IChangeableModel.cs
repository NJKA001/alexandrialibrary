using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface IChangeableModel
        : IModel, INotifyPropertyChanged
    {
        ITimeStamp TimeStamp { get; }
        bool IsChanged { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IEntity
        : INotifyPropertyChanged
    {
        Guid Id { get; }
        DateTime TimeStamp { get; }

        bool IsNew();
        bool IsChanged();
        IEnumerable<IChild> GetChildren(ChildInfo childInfo);
        IEnumerable<IValue> GetValues(ValueInfo valueInfo);

        void Save(DateTime timeStamp);
    }
}

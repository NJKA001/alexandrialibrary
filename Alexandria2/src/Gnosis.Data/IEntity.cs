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
        Guid Id { get; }
        DateTime TimeStamp { get; }

        bool IsNew();
        bool IsChanged();
        bool IsInitialized();
        IEnumerable<IChild> GetChildren(EntityInfo childInfo);
        IEnumerable<IValue> GetValues(ValueInfo valueInfo);

        void Initialize(IEntityInitialState state);
        void InitializeChildren(string name, IEnumerable<IChild> children);
        void InitializeValues(string name, IEnumerable<IValue> values);
        void Save(DateTime timeStamp);
    }
}

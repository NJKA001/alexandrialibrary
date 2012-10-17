using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.States
{
    public interface IState
        : INotifyPropertyChanged
    {
        Guid StateId { get; }

        T GetState<T>(Guid stateId) where T : class, IState;
        IEnumerable<T> GetStates<T>() where T : class, IState;
    }
}

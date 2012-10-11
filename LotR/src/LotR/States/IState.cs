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
        IEnumerable<T> GetStates<T>() where T : IState;
    }
}

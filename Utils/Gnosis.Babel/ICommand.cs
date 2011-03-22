using System;
using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ICommand
    {
        Guid Id { get; }
        IEnumerable<IParameter> Parameters { get; }
        void AddStatement(IStatement statement);
        void SetCallback(Action<IModel, object> callback, IModel model);
        void InvokeCallback(object value);
        string ToString();
    }
}

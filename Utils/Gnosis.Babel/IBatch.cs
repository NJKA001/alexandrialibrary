using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IBatch
    {
        IEnumerable<ICommand> Commands { get; }
        void AddCommand(ICommand command);
        void InvokeCallback(Guid id, object value);
        ICommand GetCommand(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public class Batch : IBatch
    {
        private readonly IDictionary<Guid, ICommand> _commands = new Dictionary<Guid, ICommand>();

        public IEnumerable<ICommand> Commands
        {
            get { return _commands.Values; }
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command.Id, command);
        }

        public void InvokeCallback(Guid id, object value)
        {
            if (_commands.ContainsKey(id))
                _commands[id].InvokeCallback(value);
        }

        public ICommand GetCommand(Guid id)
        {
            return (_commands.ContainsKey(id)) ? _commands[id] : null;
        }
    }
}

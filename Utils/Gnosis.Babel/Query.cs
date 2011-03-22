using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public class Query<T> : IQuery<T>
    {
        public Query()
        {
        }

        private readonly IDictionary<Guid, ICommand> _commands = new Dictionary<Guid, ICommand>();

        public IEnumerable<ICommand> Commands
        {
            get { return _commands.Values; }
        }

        public IModelMapper<T> ModelMapper { get; set; }

        public ICache<T> Cache { get; set; }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command.Id, command);
        }
    }
}

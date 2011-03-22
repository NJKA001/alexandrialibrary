using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IQuery<T>
    {
        IEnumerable<ICommand> Commands { get; }
        IModelMapper<T> ModelMapper { get; set; }
        ICache<T> Cache { get; set; }
        void AddCommand(ICommand command);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public interface ICommandController
    {
        IEnumerable<ICommandViewModel> Commands { get; }
    }
}

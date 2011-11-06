using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITaskController
    {
        IEnumerable<ITaskViewModel> Tasks { get; }
        
        void AddTask(ITaskViewModel task);
        void RemoveTask(ITaskViewModel task);
    }
}

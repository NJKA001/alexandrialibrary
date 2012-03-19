using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class SimpleTaskViewModel
        : TaskViewModel
    {
        public SimpleTaskViewModel(ILogger logger, Action workFunction, string name, string description, object icon)
            : base(logger, new SimpleTask(logger, workFunction), name, description, icon)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITaskResultViewModel
    {
        bool IsSelected { get; set; }
        bool IsClosed { get; set; }
    }
}

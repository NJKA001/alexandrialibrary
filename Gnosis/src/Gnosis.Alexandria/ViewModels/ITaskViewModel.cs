using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITaskViewModel
    {
        string Name { get; }
        object ImageSource { get; }
        object StatusIconSource { get; }
        string ProgressDescription { get; }

        bool IsSelected { get; set; }
    }
}

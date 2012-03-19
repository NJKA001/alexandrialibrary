using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ITag
        : INotifyPropertyChanged
    {
        string Name { get; set; }
        Category Category { get; set; }
        Source Source { get; set; }
    }
}

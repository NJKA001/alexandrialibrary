using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ILink
        : INotifyPropertyChanged
    {
        string Name { get; set; }
        Relationship Relationship { get; set; }
        Source Source { get; set; }
        string Target { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITagViewModel
        : INotifyPropertyChanged
    {
        long Id { get; }
        IAlgorithm Algorithm { get; }
        string AlgorithmName { get; }
        ITagType Type { get; }
        string TypeName { get; }
        string Value { get; }

        bool IsClosed { get; set; }
        bool IsSelected { get; set; }
    }
}

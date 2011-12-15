using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITagViewModel
    {
        IAlgorithm Algorithm { get; }
        ITagType Type { get; }
        string TypeName { get; }
        string Value { get; }

        bool IsClosed { get; set; }
        bool IsSelected { get; set; }
    }
}
